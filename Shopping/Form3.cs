using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shopping.Form1;

namespace Shopping
{
    public partial class Form3 : Form
    {
        public string connectionString = "Server=.\\SQL2022;Database=SCdb;User Id=sa;Password=1qaz@wsx;";

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO CustomerInfo (UnifiedID, CompanyName, Region, PaymentMethod) VALUES (@UnifiedID, @CompanyName, @Region, @PaymentMethod)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UnifiedID", textBox2.Text);
                command.Parameters.AddWithValue("@CompanyName", textBox1.Text);
                command.Parameters.AddWithValue("@Region", comboBox1.SelectedItem.ToString());

                // 將付款方式轉成逗號分隔的字串
                var selectedPaymentMethods = checkedListBox1.CheckedItems.Cast<string>();
                command.Parameters.AddWithValue("@PaymentMethod", string.Join(", ", selectedPaymentMethods));

                command.ExecuteNonQuery();
            }

            LoadCustomerData();  // 重新加載資料
        }

        private void LoadCustomerData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM CustomerInfo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable customerTable = new DataTable();
                adapter.Fill(customerTable);

                // 繫結資料到 DataGridView
                dataGridView1.DataSource = customerTable;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadCustomerData();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE CustomerInfo SET CompanyName = @CompanyName, Region = @Region, PaymentMethod = @PaymentMethod WHERE UnifiedID = @UnifiedID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UnifiedID", textBox2.Text);
                command.Parameters.AddWithValue("@CompanyName", textBox1.Text);
                command.Parameters.AddWithValue("@Region", comboBox1.SelectedItem.ToString());

                var selectedPaymentMethods = checkedListBox1.CheckedItems.Cast<string>();
                command.Parameters.AddWithValue("@PaymentMethod", string.Join(", ", selectedPaymentMethods));

                command.ExecuteNonQuery();
            }

            LoadCustomerData();  // 重新加載資料
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM CustomerInfo WHERE UnifiedID = @UnifiedID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UnifiedID", textBox2.Text);
                command.ExecuteNonQuery();
            }

            LoadCustomerData();  // 重新加載資料
        }

    }
}
