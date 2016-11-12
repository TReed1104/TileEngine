namespace Editor
{
    partial class Form_ItemDelete
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
            this.cbo_DeleteItem = new System.Windows.Forms.ComboBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.btn_DeleteItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbo_DeleteItem
            // 
            this.cbo_DeleteItem.FormattingEnabled = true;
            this.cbo_DeleteItem.Location = new System.Drawing.Point(12, 36);
            this.cbo_DeleteItem.Name = "cbo_DeleteItem";
            this.cbo_DeleteItem.Size = new System.Drawing.Size(260, 21);
            this.cbo_DeleteItem.TabIndex = 0;
            this.cbo_DeleteItem.SelectedIndexChanged += new System.EventHandler(this.cbo_DeleteItem_SelectedIndexChanged);
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(12, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(104, 24);
            this.lbl_Title.TabIndex = 16;
            this.lbl_Title.Text = "Delete Item";
            // 
            // btn_DeleteItem
            // 
            this.btn_DeleteItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_DeleteItem.Location = new System.Drawing.Point(0, 77);
            this.btn_DeleteItem.Name = "btn_DeleteItem";
            this.btn_DeleteItem.Size = new System.Drawing.Size(284, 40);
            this.btn_DeleteItem.TabIndex = 17;
            this.btn_DeleteItem.Text = "Delete Item";
            this.btn_DeleteItem.UseVisualStyleBackColor = true;
            this.btn_DeleteItem.Click += new System.EventHandler(this.btn_DeleteItem_Click);
            // 
            // Form_ItemDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 117);
            this.Controls.Add(this.btn_DeleteItem);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.cbo_DeleteItem);
            this.MinimumSize = new System.Drawing.Size(300, 156);
            this.Name = "Form_ItemDelete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Item";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ItemDelete_FormClosing);
            this.Load += new System.EventHandler(this.Form_ItemDelete_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo_DeleteItem;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Button btn_DeleteItem;
    }
}