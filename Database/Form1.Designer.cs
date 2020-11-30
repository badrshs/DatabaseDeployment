namespace Database
{
    sealed partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RunBtn = new System.Windows.Forms.Button();
            this.filePathListBox = new System.Windows.Forms.ListBox();
            this.checkThConnectionBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectionStringTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RunBtn
            // 
            this.RunBtn.Enabled = false;
            this.RunBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RunBtn.Location = new System.Drawing.Point(897, 556);
            this.RunBtn.Margin = new System.Windows.Forms.Padding(5);
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(144, 67);
            this.RunBtn.TabIndex = 2;
            this.RunBtn.Text = "Run";
            this.RunBtn.UseVisualStyleBackColor = true;
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // filePathListBox
            // 
            this.filePathListBox.DisplayMember = "ScriptName";
            this.filePathListBox.FormattingEnabled = true;
            this.filePathListBox.ItemHeight = 20;
            this.filePathListBox.Location = new System.Drawing.Point(24, 121);
            this.filePathListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filePathListBox.Name = "filePathListBox";
            this.filePathListBox.Size = new System.Drawing.Size(1017, 404);
            this.filePathListBox.Sorted = true;
            this.filePathListBox.TabIndex = 3;
            // 
            // checkThConnectionBtn
            // 
            this.checkThConnectionBtn.Location = new System.Drawing.Point(853, 44);
            this.checkThConnectionBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkThConnectionBtn.Name = "checkThConnectionBtn";
            this.checkThConnectionBtn.Size = new System.Drawing.Size(189, 31);
            this.checkThConnectionBtn.TabIndex = 4;
            this.checkThConnectionBtn.Text = "Check The Connection";
            this.checkThConnectionBtn.UseVisualStyleBackColor = true;
            this.checkThConnectionBtn.Click += new System.EventHandler(this.checkThConnectionBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(353, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connection String";
            // 
            // ConnectionStringTextBox
            // 
            this.ConnectionStringTextBox.Location = new System.Drawing.Point(24, 44);
            this.ConnectionStringTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.ConnectionStringTextBox.Name = "ConnectionStringTextBox";
            this.ConnectionStringTextBox.Size = new System.Drawing.Size(820, 27);
            this.ConnectionStringTextBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 637);
            this.Controls.Add(this.checkThConnectionBtn);
            this.Controls.Add(this.filePathListBox);
            this.Controls.Add(this.RunBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConnectionStringTextBox);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Database Deployement";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RunBtn;
        private System.Windows.Forms.ListBox filePathListBox;
        private System.Windows.Forms.Button checkThConnectionBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ConnectionStringTextBox;
    }
}

