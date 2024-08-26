using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Form2(ShoppingCart shoppingCart, Form1 form1)
        {
            InitializeComponent();
            _shoppingCart = shoppingCart;
            _buyingList = form1.BuyingList;
            numericUpDown1.Minimum = 1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
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

            UpdateTotals();
        }


        public class BuyingList:ProductList
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
            // 檢查是否選取了DataGridView1中的行
            if (dgv1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv1.SelectedRows[0];
                Product selectedProduct = selectedRow.DataBoundItem as Product;

                if (selectedProduct != null)
                {
                    int quantityToMove = (int)numericUpDown1.Value;

                    if (selectedProduct.Quantity < quantityToMove)
                    {
                        MessageBox.Show("庫存不足，無法加入購物車。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    selectedProduct.Quantity -= quantityToMove;

                    var existingProductInCart = _buyingList._products.FirstOrDefault(p => p.Id == selectedProduct.Id);

                    if (existingProductInCart != null)
                    {
                        existingProductInCart.Quantity += quantityToMove;
                    }
                    else
                    {
                        var productToAdd = new Product(selectedProduct.Id, selectedProduct.Name, selectedProduct.Price, quantityToMove);
                        _buyingList.AddProduct(productToAdd);
                    }

                    LoadProducts(); // 重新加載產品並更新總數
                }
            }
            else
            {
                MessageBox.Show("請選取一個商品加入購物車。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
        }
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

        bool dgv1_sorted;
        private void dgv1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv1_sorted = !dgv1_sorted;//First time become true
            if (e.ColumnIndex >= 0)
            {
                string columnName = dgv1.Columns[e.ColumnIndex].Name;
                SortOrder sortOrder = dgv1_sorted ? SortOrder.Ascending : SortOrder.Descending;
                _shoppingCart.SortProducts(columnName, sortOrder);
                LoadProducts();
            }

        }
        bool dgv2_sorted;

        private void dgv2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv2_sorted = !dgv2_sorted;//First time become true
            if (e.ColumnIndex >= 0)
            {
                string columnName = dgv2.Columns[e.ColumnIndex].Name;
                SortOrder sortOrder = dgv2_sorted ? SortOrder.Ascending : SortOrder.Descending;
                _buyingList.SortProducts(columnName, sortOrder);
                LoadProducts();
            }
        }
    }
}