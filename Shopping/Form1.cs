using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Shopping
{
    public partial class Form1 : Form
    {
        private ShoppingCart _shoppingCart;

        public Form1()
        {
            InitializeComponent();
            _shoppingCart = new ShoppingCart();
            _shoppingCart.AddDefaultProducts(); // 添加預設資料
            LoadProducts();
        }

        private void LoadProducts()
        {
            dataGridView1.DataSource = _shoppingCart.GetProducts();
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

            public Product(int id, string name, decimal price, int quantity)
            {
                Id = id;
                Name = name;
                Price = price;
                Quantity = quantity;
            }
        }

        public class ShoppingCart
        {
            public List<Product> _products;

            public ShoppingCart()
            {
                _products = new List<Product>();
            }

            public void AddProduct(Product product)
            {
                _products.Add(product);
            }

            public List<Product> GetProducts()
            {
                return _products;
            }

            public void DeleteProduct(int? id = null, string name = null)
            {
                var product = _products.FirstOrDefault(p => (id == null || p.Id == id) && (name == null || p.Name == name));
                if (product != null)
                {
                    _products.Remove(product);
                }
            }

            public void UpdateProduct(int id, string name, decimal price, int quantity)
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    product.Name = name;
                    product.Price = price;
                    product.Quantity = quantity;
                }
            }

            public void SortProducts(string columnName, SortOrder sortOrder)
            {
                switch (columnName)
                {
                    case "Id":
                        _products = (sortOrder == SortOrder.Ascending) ?
                            _products.OrderBy(p => p.Id).ToList() :
                            _products.OrderByDescending(p => p.Id).ToList();
                        break;
                    case "Name":
                        _products = (sortOrder == SortOrder.Ascending) ?
                            _products.OrderBy(p => p.Name).ToList() :
                            _products.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "Price":
                        _products = (sortOrder == SortOrder.Ascending) ?
                            _products.OrderBy(p => p.Price).ToList() :
                            _products.OrderByDescending(p => p.Price).ToList();
                        break;
                    case "Quantity":
                        _products = (sortOrder == SortOrder.Ascending) ?
                            _products.OrderBy(p => p.Quantity).ToList() :
                            _products.OrderByDescending(p => p.Quantity).ToList();
                        break;
                }
            }

            public void AddDefaultProducts()
            {
                _products.Add(new Product(1, "Apple", 1.20m, 10));
                _products.Add(new Product(2, "Banana", 0.80m, 20));
                _products.Add(new Product(3, "Orange", 1.50m, 15));
                _products.Add(new Product(4, "Mango", 2.00m, 5));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(IDTextBox.Text, out int id))
            {

                MessageBox.Show("請輸入有效的商品ID。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var existingId = _shoppingCart.GetProducts().FirstOrDefault(p => p.Id == id);
            if (existingId != null)
            {
                MessageBox.Show("該ID的商品已經存在。請使用其他ID。", "ID已存在", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = NameTextBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("請輸入有效的商品名稱。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("請輸入有效的商品價格。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("請輸入有效的商品數量。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product = new Product(id, name, price, quantity);
            _shoppingCart.AddProduct(product);

            RefreshDataGrid();
            IDTextBox.Text = (dataGridView1.RowCount + 1).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int? id = null;
            if (int.TryParse(IDTextBox.Text, out int idValue))
            {
                id = idValue;
            }

            string name = string.IsNullOrWhiteSpace(NameTextBox.Text) ? null : NameTextBox.Text;

            decimal? price = null;
            if (decimal.TryParse(PriceTextBox.Text, out decimal priceValue))
            {
                price = priceValue;
            }

            int? quantity = null;
            if (int.TryParse(QuantityTextBox.Text, out int quantityValue))
            {
                quantity = quantityValue;
            }

            if (id == null && name == null && price == null && quantity == null)
            {
                MessageBox.Show("請輸入至少一個查詢條件。");
                return;
            }

            var products = _shoppingCart.GetProducts();
            var query = products.Where(p =>
                (id == null || p.Id == id) &&
                (name == null || p.Name == name) &&
                (price == null || p.Price == price) &&
                (quantity == null || p.Quantity == quantity)).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;

            dataGridView1.ClearSelection();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var product = row.DataBoundItem as Product;
                if (product != null && query.Contains(product))
                {
                    row.Selected = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(IDTextBox.Text, out int id))
            {
                MessageBox.Show("請輸入有效的商品ID。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = NameTextBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("請輸入有效的商品名稱。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("請輸入有效的商品價格。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("請輸入有效的商品數量。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _shoppingCart.UpdateProduct(id, name, price, quantity);

            RefreshDataGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int? id = null;
            if (int.TryParse(IDTextBox.Text, out int idValue))
            {
                id = idValue;
            }

            string name = string.IsNullOrWhiteSpace(NameTextBox.Text) ? null : NameTextBox.Text;

            _shoppingCart.DeleteProduct(id, name);

            RefreshDataGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count && e.ColumnIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];

                if (selectedRow != null)
                {
                    IDTextBox.Text = selectedRow.Cells["Id"].Value?.ToString();
                    NameTextBox.Text = selectedRow.Cells["Name"].Value?.ToString() ?? "Unnamed";
                    PriceTextBox.Text = selectedRow.Cells["Price"].Value?.ToString();
                    QuantityTextBox.Text = selectedRow.Cells["Quantity"].Value?.ToString();
                }
            }
        }
        bool sorted;
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            sorted = !sorted;//First time become true
            if (e.ColumnIndex >= 0)
            {

                string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
                SortOrder sortOrder = sorted ? SortOrder.Ascending : SortOrder.Descending;
                _shoppingCart.SortProducts(columnName, sortOrder);

                RefreshDataGrid();
            }
        }

        private SortOrder GetSortOrder(int columnIndex)
        {
            if (dataGridView1.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                dataGridView1.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                dataGridView1.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                dataGridView1.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }

        private void RefreshDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _shoppingCart.GetProducts();
            dataGridView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 初始化代碼
            LoadProducts();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
