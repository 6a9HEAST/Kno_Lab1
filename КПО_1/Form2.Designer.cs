namespace КПО_1
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
            dataGridView1 = new DataGridView();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            delete_but = new Button();
            add_but = new Button();
            Change_combobox = new ComboBox();
            Change_textBox = new TextBox();
            Change_but = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(914, 445);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(13, 454);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 3;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(13, 488);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(151, 27);
            textBox1.TabIndex = 4;
            // 
            // delete_but
            // 
            delete_but.Location = new Point(189, 453);
            delete_but.Margin = new Padding(3, 4, 3, 4);
            delete_but.Name = "delete_but";
            delete_but.Size = new Size(149, 28);
            delete_but.TabIndex = 5;
            delete_but.Text = "Удалить";
            delete_but.UseVisualStyleBackColor = true;
            delete_but.Click += delete_but_Click;
            // 
            // add_but
            // 
            add_but.Location = new Point(189, 487);
            add_but.Margin = new Padding(3, 4, 3, 4);
            add_but.Name = "add_but";
            add_but.Size = new Size(149, 28);
            add_but.TabIndex = 6;
            add_but.Text = "Добавить";
            add_but.UseVisualStyleBackColor = true;
            add_but.Click += add_but_Click;
            // 
            // Change_combobox
            // 
            Change_combobox.FormattingEnabled = true;
            Change_combobox.Location = new Point(525, 486);
            Change_combobox.Name = "Change_combobox";
            Change_combobox.Size = new Size(69, 28);
            Change_combobox.TabIndex = 7;
            Change_combobox.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // Change_textBox
            // 
            Change_textBox.Location = new Point(600, 487);
            Change_textBox.Name = "Change_textBox";
            Change_textBox.Size = new Size(151, 27);
            Change_textBox.TabIndex = 8;
            // 
            // Change_but
            // 
            Change_but.Location = new Point(757, 487);
            Change_but.Margin = new Padding(3, 4, 3, 4);
            Change_but.Name = "Change_but";
            Change_but.Size = new Size(149, 28);
            Change_but.TabIndex = 9;
            Change_but.Text = "Изменить";
            Change_but.UseVisualStyleBackColor = true;
            Change_but.Click += Change_but_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 533);
            Controls.Add(Change_but);
            Controls.Add(Change_textBox);
            Controls.Add(Change_combobox);
            Controls.Add(add_but);
            Controls.Add(delete_but);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Controls.Add(dataGridView1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Button delete_but;
        private Button add_but;
        private ComboBox Change_combobox;
        private TextBox Change_textBox;
        private Button Change_but;
    }
}