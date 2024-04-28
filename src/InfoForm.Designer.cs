namespace src
{
    partial class InfoForm
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
            label1 = new Label();
            linkLabel1 = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 9);
            label1.Name = "label1";
            label1.Size = new Size(220, 44);
            label1.TabIndex = 0;
            label1.Text = "Use CTRL+WIN+[0~9] to\r\nswitch Virtual Desktops";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            linkLabel1.Location = new Point(16, 78);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(288, 17);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://github.com/tabarra/txDeskSwitcher";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // InfoForm
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 104);
            Controls.Add(linkLabel1);
            Controls.Add(label1);
            Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "InfoForm";
            Text = "txDeskSwitcher";
            FormClosing += InfoForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private LinkLabel linkLabel1;
    }
}