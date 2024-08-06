namespace Shopping
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            button1 = new Button();
            IDTextBox = new TextBox();
            panel1 = new Panel();
            QuantityTextBox = new TextBox();
            PriceTextBox = new TextBox();
            NameTextBox = new TextBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(420, 383);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Create";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // IDTextBox
            // 
            IDTextBox.Location = new Point(3, 3);
            IDTextBox.Name = "IDTextBox";
            IDTextBox.PlaceholderText = "ID";
            IDTextBox.Size = new Size(100, 23);
            IDTextBox.TabIndex = 5;
            IDTextBox.Text = "1";
            // 
            // panel1
            // 
            panel1.Controls.Add(QuantityTextBox);
            panel1.Controls.Add(PriceTextBox);
            panel1.Controls.Add(NameTextBox);
            panel1.Controls.Add(IDTextBox);
            panel1.Location = new Point(84, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(108, 171);
            panel1.TabIndex = 6;
            // 
            // QuantityTextBox
            // 
            QuantityTextBox.Location = new Point(3, 135);
            QuantityTextBox.Name = "QuantityTextBox";
            QuantityTextBox.PlaceholderText = "Quantity";
            QuantityTextBox.Size = new Size(100, 23);
            QuantityTextBox.TabIndex = 8;
            QuantityTextBox.Text = "2";
            // 
            // PriceTextBox
            // 
            PriceTextBox.Location = new Point(3, 89);
            PriceTextBox.Name = "PriceTextBox";
            PriceTextBox.PlaceholderText = "Price";
            PriceTextBox.Size = new Size(100, 23);
            PriceTextBox.TabIndex = 7;
            PriceTextBox.Text = "10";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(3, 46);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.PlaceholderText = "Name";
            NameTextBox.Size = new Size(100, 23);
            NameTextBox.TabIndex = 6;
            NameTextBox.Text = "Name";
            // 
            // button2
            // 
            button2.Location = new Point(3, 32);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "Find";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(3, 61);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 8;
            button3.Text = "Update";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(3, 90);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 9;
            button4.Text = "Delete";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(3, 119);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 10;
            button5.Text = "Sort";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(3, 148);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 11;
            button6.Text = "Print";
            button6.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(button1);
            splitContainer1.Panel1.Controls.Add(button6);
            splitContainer1.Panel1.Controls.Add(panel1);
            splitContainer1.Panel1.Controls.Add(button5);
            splitContainer1.Panel1.Controls.Add(button2);
            splitContainer1.Panel1.Controls.Add(button4);
            splitContainer1.Panel1.Controls.Add(button3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView1);
            splitContainer1.Size = new Size(644, 389);
            splitContainer1.SplitterDistance = 214;
            splitContainer1.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 269);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 12;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(661, 450);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "商品介面";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private DataGridView dataGridView1;
        private Button button1;
        private TextBox IDTextBox;
        private Panel panel1;
        private TextBox QuantityTextBox;
        private TextBox PriceTextBox;
        private TextBox NameTextBox;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private SplitContainer splitContainer1;
        private Label label1;
    }
}
