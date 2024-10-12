using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;
using static Shopping.Form2;

namespace Shopping
{
    public partial class Form1 : Form
    {
        public ShoppingCart _shoppingCart;
        public Form2.BuyingList BuyingList { get; private set; }

        public Form1()
        {
            InitializeComponent();
            BuyingList = new Form2.BuyingList();

            Image createImage = ResizeImage(Properties.Resources.Create, new Size(20, 20));
            Image deleteImage = ResizeImage(Properties.Resources.Delete, new Size(20, 20));
            Image findImage = ResizeImage(Properties.Resources.Find, new Size(20, 20));
            Image updateImage = ResizeImage(Properties.Resources.Update, new Size(20, 20));

            button1.Image = createImage;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.TextAlign = ContentAlignment.MiddleRight;

            button2.Image = findImage;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.TextAlign = ContentAlignment.MiddleRight;

            button3.Image = updateImage;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.TextAlign = ContentAlignment.MiddleRight;

            button4.Image = deleteImage;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.TextAlign = ContentAlignment.MiddleRight;
            _shoppingCart = new ShoppingCart();
            _shoppingCart.AddDefaultProducts();
            LoadProducts();


        }
        private Image ResizeImage(Image image, Size size)
        {
            Bitmap resized = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.DrawImage(image, new Rectangle(Point.Empty, size));
            }
            return resized;
        }
        internal void LoadProducts()
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
        public class ProductList
        {
            public List<Product> _products;

            public ProductList()
            {
                _products = new List<Product>();
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

            public List<Product> GetProducts()
            {
                return _products;
            }
        }


        public class ShoppingCart:ProductList
        {
            //public List<Product> _products;

            public ShoppingCart():base()
            {
                //_products = new List<Product>();
            }

            public void AddProduct(Product product)
            {
                _products.Add(product);
            }

            public List<Product> GetProducts()
            {
                return _products;
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

            public void DeleteProduct(int? id = null, string name = null)
            {
                // 首先嘗試根據 id 刪除
                if (id != null)
                {
                    var productById = _products.FirstOrDefault(p => p.Id == id);
                    if (productById != null)
                    {
                        _products.Remove(productById);
                        return;
                    }
                }

                // 如果 id 為 null 或沒有找到符合的產品，則嘗試根據 name 刪除
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var productByName = _products.FirstOrDefault(p => p.Name == name);
                    if (productByName != null)
                    {
                        _products.Remove(productByName);
                    }
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

            public void AddDefaultProducts()
            {
                _products.Add(new Product(1, "Apple", 1.20m, 10));
                _products.Add(new Product(2, "Banana", 0.80m, 20));
                _products.Add(new Product(3, "Orange", 1.50m, 15));
                _products.Add(new Product(4, "Mango", 2.00m, 5));
            }
        }

        private void button1_Click(object sender, EventArgs e)//Create
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
            IDTextBox.Text = (int.Parse(IDTextBox.Text) + 1).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 優先順序: id > Name > price > quantity
            List<Product> query = null;

            // 優先順序1: id
            if (int.TryParse(IDTextBox.Text, out int idValue))
            {
                query = _shoppingCart.GetProducts().Where(p => p.Id == idValue).ToList();
            }
            // 優先順序2: Name
            else if (!string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                string name = NameTextBox.Text;
                query = _shoppingCart.GetProducts().Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            // 優先順序3: price
            else if (decimal.TryParse(PriceTextBox.Text, out decimal priceValue))
            {
                query = _shoppingCart.GetProducts().Where(p => p.Price == priceValue).ToList();
            }
            // 優先順序4: quantity
            else if (int.TryParse(QuantityTextBox.Text, out int quantityValue))
            {
                query = _shoppingCart.GetProducts().Where(p => p.Quantity == quantityValue).ToList();
            }

            if (query == null || query.Count == 0)
            {
                MessageBox.Show("未找到符合條件的商品。", "查詢結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _shoppingCart.GetProducts();

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


        private void button3_Click(object sender, EventArgs e)//Update
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

        private void button4_Click(object sender, EventArgs e)//Delete
        {
            int? id = null;
            string? name = null;

            if (int.TryParse(IDTextBox.Text, out int idValue))
            {
                id = idValue;
            }

            name = string.IsNullOrWhiteSpace(NameTextBox.Text) ? null : NameTextBox.Text;
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

        public void RefreshDataGrid()
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

        private void button6_Click(object sender, EventArgs e)//Print
        {
            this.Hide();
            Form2 form2 = new Form2(_shoppingCart,this);
            form2.ShowDialog(); 

            this.Show();
        }
    }
}
