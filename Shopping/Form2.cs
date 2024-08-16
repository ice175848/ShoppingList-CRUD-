using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shopping.Form1;

namespace Shopping
{
    public partial class Form2 : Form
    {
        public ShoppingCart _shoppingCart;
        public Form2(ShoppingCart shoppingCart)
        {
            InitializeComponent();
            _shoppingCart = shoppingCart;

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
        }
    }
}
