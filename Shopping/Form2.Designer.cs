﻿namespace Shopping
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            dgv1 = new DataGridView();
            button1 = new Button();
            numericUpDown1 = new NumericUpDown();
            dgv2 = new DataGridView();
            button2 = new Button();
            NameTextBox = new TextBox();
            button3 = new Button();
            button4 = new Button();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            textBox1 = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgv2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(234, 455);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "商品";
            // 
            // dgv1
            // 
            dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv1.Location = new Point(16, 69);
            dgv1.Name = "dgv1";
            dgv1.ReadOnly = true;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv1.Size = new Size(464, 383);
            dgv1.TabIndex = 2;
            dgv1.CellClick += dataGridView1_CellClick;
            dgv1.ColumnHeaderMouseClick += dgv1_ColumnHeaderMouseClick;
            // 
            // button1
            // 
            button1.Location = new Point(486, 272);
            button1.Name = "button1";
            button1.Size = new Size(33, 23);
            button1.TabIndex = 3;
            button1.Text = "=>";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(486, 243);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(33, 23);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dgv2
            // 
            dgv2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv2.Location = new Point(525, 69);
            dgv2.Name = "dgv2";
            dgv2.ReadOnly = true;
            dgv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv2.Size = new Size(463, 383);
            dgv2.TabIndex = 5;
            dgv2.ColumnHeaderMouseClick += dgv2_ColumnHeaderMouseClick;
            // 
            // button2
            // 
            button2.Location = new Point(486, 301);
            button2.Name = "button2";
            button2.Size = new Size(33, 23);
            button2.TabIndex = 6;
            button2.Text = "<=";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(419, 472);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.PlaceholderText = "Name";
            NameTextBox.Size = new Size(100, 23);
            NameTextBox.TabIndex = 7;
            NameTextBox.Text = "Orange";
            // 
            // button3
            // 
            button3.Location = new Point(525, 472);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 8;
            button3.Text = "Search";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.Location = new Point(525, 501);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 9;
            button4.Text = "結帳";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(12, 505);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(79, 15);
            linkLabel1.TabIndex = 10;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "前往商品介面";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(97, 505);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(91, 15);
            linkLabel2.TabIndex = 10;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "前往客戶資料表";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(420, 502);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 11;
            textBox1.Text = "16423286";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(359, 505);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 12;
            label2.Text = "統一編號";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 531);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(NameTextBox);
            Controls.Add(button2);
            Controls.Add(dgv2);
            Controls.Add(numericUpDown1);
            Controls.Add(button1);
            Controls.Add(dgv1);
            Controls.Add(label1);
            Name = "Form2";
            Text = "購物車介面";
            FormClosed += Form2_FormClosed;
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dgv1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgv2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private DataGridView dataGridView1;
        private Button button1;
        private NumericUpDown numericUpDown1;
        private DataGridView dataGridView2;
        private Button button2;
        private DataGridView dgv1;
        private DataGridView dgv2;
        private TextBox NameTextBox;
        private Button button3;
        private Button button4;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private TextBox textBox1;
        private Label label2;
    }
}