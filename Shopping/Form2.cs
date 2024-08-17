using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static Shopping.Form1;

namespace Shopping
{
    public partial class Form2 : Form
    {
        private Form1 _form1;
        private ShoppingCart _shoppingCart;
        private BuyingList _buyingList;
        private Product _selectedProduct; // 用來暫存選中的Product

        public Form2(ShoppingCart shoppingCart, Form1 form1)
        {
            InitializeComponent();
            _shoppingCart = shoppingCart;
            _form1 = form1;
            _buyingList = new BuyingList();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _shoppingCart.GetProducts();
            dataGridView1.Refresh();
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = _buyingList.GetProducts();
            dataGridView2.Refresh();
        }

        public class BuyingList
        {
            public List<Product> _list;

            public BuyingList()
            {
                _list = new List<Product>();
            }

            public void AddProduct(Product product)
            {
                _list.Add(product);
            }

            public void RemoveProduct(Product product)
            {
                _list.Remove(product);
            }

            public List<Product> GetProducts()
            {
                return _list;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count && e.ColumnIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                var selectedProduct = selectedRow.DataBoundItem as Product;

                if (selectedProduct != null)
                {
                    var productsInDataGridView2 = dataGridView2.DataSource as BindingList<Product>;
                    if (productsInDataGridView2 != null)
                    {
                        productsInDataGridView2.Add(selectedProduct);
                    }
                    else
                    {
                        // 如果DataGridView2沒有綁定到BindingList<Product>，先創建並綁定
                        productsInDataGridView2 = new BindingList<Product> { selectedProduct };
                        dataGridView2.DataSource = productsInDataGridView2;
                    }
                    LoadProducts();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 檢查是否選取了DataGridView1中的行
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 取得選取的行
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // 取得選取的Product物件
                Product selectedProduct = selectedRow.DataBoundItem as Product;

                if (selectedProduct != null)
                {
                    // 檢查_buyingList中是否已存在該產品
                    var existingProduct = _buyingList._list.FirstOrDefault(p => p.Name == selectedProduct.Name);

                    if (existingProduct != null)
                    {
                        // 如果存在，則增加該產品的數量
                        existingProduct.Quantity += (int)numericUpDown1.Value;
                    }
                    else
                    {
                        // 如果不存在，將產品新增到購買列表中，並設定數量為numericUpDown的值
                        _buyingList.AddProduct(selectedProduct);
                        selectedProduct.Quantity = (int)numericUpDown1.Value;
                    }

                    // 更新DataGridView2的資料
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = _buyingList.GetProducts();
                }
            }
            else
            {
                // 如果沒有選取任何行，顯示提示訊息
                MessageBox.Show("請選取一筆資料再進行操作。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count>0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                Product selectedProduct = selectedRow.DataBoundItem as Product;
                if(selectedProduct!=null)
                {
                    selectedProduct.Quantity -= (int)numericUpDown1.Value;
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("不存在");
                    return;
                }
            }
            else
            {
                // 如果沒有選取任何行，顯示提示訊息
                MessageBox.Show("請選取一筆資料再進行操作。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
