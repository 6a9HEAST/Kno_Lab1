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
            save = new Button();
            label1 = new Label();
            label2 = new Label();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // save
            // 
            save.Location = new Point(105, 111);
            save.Margin = new Padding(3, 4, 3, 4);
            save.Name = "save";
            save.Size = new Size(261, 31);
            save.TabIndex = 1;
            save.Text = "Выполнить выборку";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 11);
            label1.Name = "label1";
            label1.Size = new Size(235, 20);
            label1.TabIndex = 3;
            label1.Text = "Выполнить выборку по столбцу:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 55);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 4;
            label2.Text = "Значение:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(261, 11);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(203, 28);
            comboBox1.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(105, 52);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(166, 27);
            textBox1.TabIndex = 6;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 161);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(save);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button save;
        private Label label1;
        private Label label2;
        private ComboBox comboBox1;
        private TextBox textBox1;
    }
}