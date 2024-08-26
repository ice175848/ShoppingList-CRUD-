namespace Shopping
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
            ((System.ComponentModel.ISupportInitialize)dgv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgv2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(230, 398);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "商品";
            // 
            // dgv1
            // 
            dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv1.Location = new Point(12, 12);
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
            button1.Location = new Point(482, 215);
            button1.Name = "button1";
            button1.Size = new Size(33, 23);
            button1.TabIndex = 3;
            button1.Text = "=>";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(482, 186);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(33, 23);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dgv2
            // 
            dgv2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv2.Location = new Point(521, 12);
            dgv2.Name = "dgv2";
            dgv2.ReadOnly = true;
            dgv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv2.Size = new Size(463, 383);
            dgv2.TabIndex = 5;
            dgv2.ColumnHeaderMouseClick += dgv2_ColumnHeaderMouseClick;
            // 
            // button2
            // 
            button2.Location = new Point(482, 244);
            button2.Name = "button2";
            button2.Size = new Size(33, 23);
            button2.TabIndex = 6;
            button2.Text = "<=";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 450);
            Controls.Add(button2);
            Controls.Add(dgv2);
            Controls.Add(numericUpDown1);
            Controls.Add(button1);
            Controls.Add(dgv1);
            Controls.Add(label1);
            Name = "Form2";
            Text = "購物車介面";
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
    }
}