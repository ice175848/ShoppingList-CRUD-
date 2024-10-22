using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using static Shopping.Form1;
using static Shopping.Form2;

namespace Shopping
{
    public partial class Form2 : Form
    {
        private ShoppingCart _shoppingCart;
        private BuyingList _buyingList;
        public string connectionString = "Server=.\\SQL2022;Database=SCdb;User Id=sa;Password=1qaz@wsx;";
        private string lastSortedColumn = null;
        private System.Windows.Forms.SortOrder lastSortOrder = System.Windows.Forms.SortOrder.None;


        public Form2()
        {
            InitializeComponent();
            if (_shoppingCart == null)
            {
                _shoppingCart = new ShoppingCart();  // 在這裡初始化
            }
            if (_buyingList == null)
            {
                _buyingList = new BuyingList();  // 初始化購物清單
            }

            numericUpDown1.Minimum = 1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadShoppingCartData();

            LoadProducts();
        }
        private void LoadShoppingCartData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ShoppingCart";  // 查詢購物車資料
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable shoppingCartTable = new DataTable();
                adapter.Fill(shoppingCartTable);

                dgv2.DataSource = shoppingCartTable;  // 繫結資料到 dgv2
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Products";  // 確保使用正確的 SQL 查詢語句
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable productsTable = new DataTable();
                adapter.Fill(productsTable);

                // 綁定資料到 DataGridView
                dgv1.DataSource = productsTable;
            }
            /*
            int selectedRowIndex1 = dgv1.CurrentCell != null ? dgv1.CurrentCell.RowIndex : -1;
            int selectedRowIndex2 = dgv2.CurrentCell != null ? dgv2.CurrentCell.RowIndex : -1;

            dgv1.DataSource = new BindingList<Product>(_shoppingCart.GetProducts());
            dgv2.DataSource = new BindingList<Product>(_buyingList.GetProducts());

            if (selectedRowIndex1 >= 0 && selectedRowIndex1 < dgv1.Rows.Count)
            {
                dgv1.CurrentCell = dgv1.Rows[selectedRowIndex1].Cells[0];
            }

            if (selectedRowIndex2 >= 0 && selectedRowIndex2 < dgv2.Rows.Count)
            {
                dgv2.CurrentCell = dgv2.Rows[selectedRowIndex2].Cells[0];
            }

            UpdateTotals();*/
        }


        public class BuyingList : ProductList
        {
            public BuyingList() : base()
            {
            }

            public void AddProduct(Product product)
            {
                _products.Add(product);
            }

            public void RemoveProduct(Product product)
            {
                _products.Remove(product);
            }

        }

        private void UpdateTotals()
        {
            // 計算 DataGridView1 中的庫存總數
            int totalStock = _shoppingCart.GetProducts().Sum(p => p.Quantity);

            // 計算 DataGridView2 中的購物車總數
            int totalCart = _buyingList.GetProducts().Sum(p => p.Quantity);

            // 將結果顯示在 DataGridView1 和 DataGridView2 中的總數行或其他地方
            // 如果你有特定的位置來顯示這些總數，請更新代碼
            Console.WriteLine($"庫存總數: {totalStock}");
            Console.WriteLine($"購物車總數: {totalCart}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv1.SelectedRows[0];
                var productId = selectedRow.Cells["ProductID"].Value.ToString();
                var name = selectedRow.Cells["Name"].Value.ToString();
                var price = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
                var quantityToMove = (int)numericUpDown1.Value;

                // 確認庫存不會小於0
                if (Convert.ToInt32(selectedRow.Cells["Quantity"].Value) < quantityToMove)
                {
                    MessageBox.Show("庫存不足，無法加入購物車。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 更新 Products 表中的庫存數量
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE Products SET Quantity = Quantity - @quantity WHERE ProductID = @productId";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@quantity", quantityToMove);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.ExecuteNonQuery();
                }

                // 檢查購物車中是否已經存在該商品
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM ShoppingCart WHERE ProductID = @productId";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@productId", productId);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        // 如果購物車中已經存在該商品，更新數量
                        string updateCartQuery = "UPDATE ShoppingCart SET Quantity = Quantity + @quantity WHERE ProductID = @productId";
                        SqlCommand updateCartCommand = new SqlCommand(updateCartQuery, connection);
                        updateCartCommand.Parameters.AddWithValue("@quantity", quantityToMove);
                        updateCartCommand.Parameters.AddWithValue("@productId", productId);
                        updateCartCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // 如果購物車中沒有該商品，插入新行
                        string insertQuery = "INSERT INTO ShoppingCart (ProductID, Name, Price, Quantity) VALUES (@productId, @name, @price, @quantity)";
                        SqlCommand command = new SqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@quantity", quantityToMove);
                        command.ExecuteNonQuery();
                    }
                }
                RememberCurrentSort();
                // 同步更新 dgv1 和 dgv2
                LoadProducts();           // 更新 dgv1
                LoadShoppingCartData();   // 更新 dgv2
                 // 恢復排序狀態
                RestoreSort();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv2.SelectedRows[0];
                var productId = selectedRow.Cells["ProductID"].Value.ToString();
                var quantityToMove = (int)numericUpDown1.Value;

                if (Convert.ToInt32(selectedRow.Cells["Quantity"].Value) < quantityToMove)
                {
                    MessageBox.Show("購物車內商品數量不足，無法返還庫存。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 更新購物車內商品數量
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE ShoppingCart SET Quantity = Quantity - @quantity WHERE ProductID = @productId";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@quantity", quantityToMove);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.ExecuteNonQuery();

                    // 如果購物車內的商品數量變為 0，則從購物車移除
                    string deleteQuery = "DELETE FROM ShoppingCart WHERE ProductID = @productId AND Quantity = 0";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@productId", productId);
                    deleteCommand.ExecuteNonQuery();
                }

                // 更新 Products 表內商品數量
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateStockQuery = "UPDATE Products SET Quantity = Quantity + @quantity WHERE ProductID = @productId";
                    SqlCommand command = new SqlCommand(updateStockQuery, connection);
                    command.Parameters.AddWithValue("@quantity", quantityToMove);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.ExecuteNonQuery();
                }

                // 更新 dgv1 和 dgv2
                LoadProducts();           // 更新庫存（dgv1）
                LoadShoppingCartData();   // 更新購物車（dgv2）
            }
            else
            {
                MessageBox.Show("請選取一個商品返還庫存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        /*private void button2_Click(object sender, EventArgs e)
        {
            if (dgv2.SelectedRows.Count > 0)
            {
                var selectedRow = dgv2.SelectedRows[0];
                var selectedProduct = selectedRow.DataBoundItem as Product;

                if (selectedProduct != null)
                {
                    int quantityToMove = (int)numericUpDown1.Value;

                    if (selectedProduct.Quantity < quantityToMove)
                    {
                        MessageBox.Show("購物車內商品數量不足，無法返還庫存。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 從購物車中減少商品數量
                    selectedProduct.Quantity -= quantityToMove;

                    // 如果購物車內的數量變為0，則從購物車中移除該商品
                    if (selectedProduct.Quantity == 0)
                    {
                        _buyingList.RemoveProduct(selectedProduct);
                    }

                    // 檢查庫存中是否已存在該商品
                    var existingProductInStock = _shoppingCart.GetProducts().FirstOrDefault(p => p.Id == selectedProduct.Id);

                    if (existingProductInStock != null)
                    {
                        // 如果存在，則增加庫存中的數量
                        existingProductInStock.Quantity += quantityToMove;
                    }

                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("請選取一個商品返還庫存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }*/
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgv1.Rows.Count)
            {
                // 在這裡，你可以執行需要的操作，比如將選中的產品詳細信息顯示在表單上
                DataGridViewRow selectedRow = dgv1.Rows[e.RowIndex];
                Product selectedProduct = selectedRow.DataBoundItem as Product;

                if (selectedProduct != null)
                {
                    // 例如，這裡可以顯示選中的產品信息到UI的其他部分
                    Console.WriteLine($"選中產品: {selectedProduct.Name}, 庫存: {selectedProduct.Quantity}");
                }
            }
        }

        private bool dgv1_sorted = false;

        private void dgv1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dgv1.Columns[e.ColumnIndex].Name;
            System.Windows.Forms.SortOrder sortOrder = dgv1_sorted ? System.Windows.Forms.SortOrder.Descending : System.Windows.Forms.SortOrder.Ascending;

            if (lastSortedColumn != columnName)
            {
                // 如果點擊了不同的列，重置為升序排序
                sortOrder = System.Windows.Forms.SortOrder.Ascending;
                dgv1_sorted = false;
            }

            dgv1_sorted = !dgv1_sorted; // 切換排序方向

            // 執行排序
            dgv1.Sort(dgv1.Columns[columnName], sortOrder == System.Windows.Forms.SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

            lastSortedColumn = columnName; // 記錄最後排序的列
            lastSortOrder = sortOrder;     // 記錄最後的排序方向
        }

        bool dgv2_sorted;

        private void dgv2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv2_sorted = !dgv2_sorted;//First time become true
            if (e.ColumnIndex >= 0)
            {
                string columnName = dgv2.Columns[e.ColumnIndex].Name;
                System.Windows.Forms.SortOrder sortOrder = dgv2_sorted ? System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
                _buyingList.SortProducts(columnName, sortOrder);
                LoadProducts();
            }
        }
        private void RememberCurrentSort()
        {
            if (dgv1.SortedColumn != null)
            {
                lastSortedColumn = dgv1.SortedColumn.Name;
                lastSortOrder = dgv1.SortOrder;
            }
            else
            {
                lastSortedColumn = null;
                lastSortOrder = System.Windows.Forms.SortOrder.None;
            }
        }

        private void RestoreSort()
        {
            if (!string.IsNullOrEmpty(lastSortedColumn))
            {
                ListSortDirection direction = (lastSortOrder == System.Windows.Forms.SortOrder.Ascending) ?
                                                ListSortDirection.Ascending : ListSortDirection.Descending;
                dgv1.Sort(dgv1.Columns[lastSortedColumn], direction);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Product> query = null;
            if (!string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                string name = NameTextBox.Text;
                query = _shoppingCart.GetProducts().Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

                dgv1.ClearSelection();
                foreach (DataGridViewRow row in dgv1.Rows)
                {
                    var product = row.DataBoundItem as Product;
                    if (product != null && query.Contains(product))
                    {
                        row.Selected = true;
                    }
                }
                query = _buyingList.GetProducts().Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

                dgv2.ClearSelection();
                foreach (DataGridViewRow row in dgv2.Rows)
                {
                    var product = row.DataBoundItem as Product;
                    if (product != null && query.Contains(product))
                    {
                        row.Selected = true;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 構建顯示購物車內容的訊息
            string message = "購物車內容:\n\n";
            int totalQuantity = 0;
            decimal totalAmount = 0m;

            foreach (var product in _buyingList.GetProducts())
            {
                message += $"品項: {product.Name}\n數量: {product.Quantity}\n金額: {product.Price * product.Quantity:C}\n\n";
                totalQuantity += product.Quantity;
                totalAmount += product.Price * product.Quantity;
            }

            message += $"總數量: {totalQuantity}\n";
            message += $"總金額: {totalAmount:C}\n";

            // 顯示訊息框，帶有確認按鈕
            MessageBox.Show(message, "購物車總覽", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();  // 完全關閉應用程式
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }
    }
}