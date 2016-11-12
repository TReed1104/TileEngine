namespace Editor
{
    partial class Form_ItemMenu
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
            this.btn_AddItem = new System.Windows.Forms.Button();
            this.btn_EditItem = new System.Windows.Forms.Button();
            this.btn_DeleteItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_AddItem
            // 
            this.btn_AddItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_AddItem.Location = new System.Drawing.Point(0, 0);
            this.btn_AddItem.Name = "btn_AddItem";
            this.btn_AddItem.Size = new System.Drawing.Size(284, 50);
            this.btn_AddItem.TabIndex = 4;
            this.btn_AddItem.Text = "Add New Item";
            this.btn_AddItem.UseVisualStyleBackColor = true;
            this.btn_AddItem.Click += new System.EventHandler(this.btn_AddItem_Click);
            // 
            // btn_EditItem
            // 
            this.btn_EditItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_EditItem.Location = new System.Drawing.Point(0, 50);
            this.btn_EditItem.Name = "btn_EditItem";
            this.btn_EditItem.Size = new System.Drawing.Size(284, 50);
            this.btn_EditItem.TabIndex = 5;
            this.btn_EditItem.Text = "Edit Item";
            this.btn_EditItem.UseVisualStyleBackColor = true;
            this.btn_EditItem.Click += new System.EventHandler(this.btn_EditItem_Click);
            // 
            // btn_DeleteItem
            // 
            this.btn_DeleteItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_DeleteItem.Location = new System.Drawing.Point(0, 100);
            this.btn_DeleteItem.Name = "btn_DeleteItem";
            this.btn_DeleteItem.Size = new System.Drawing.Size(284, 50);
            this.btn_DeleteItem.TabIndex = 6;
            this.btn_DeleteItem.Text = "Delete Item";
            this.btn_DeleteItem.UseVisualStyleBackColor = true;
            this.btn_DeleteItem.Click += new System.EventHandler(this.btn_DeleteItem_Click);
            // 
            // Form_ItemMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 151);
            this.Controls.Add(this.btn_DeleteItem);
            this.Controls.Add(this.btn_EditItem);
            this.Controls.Add(this.btn_AddItem);
            this.MinimumSize = new System.Drawing.Size(300, 190);
            this.Name = "Form_ItemMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ItemMenu_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_AddItem;
        private System.Windows.Forms.Button btn_EditItem;
        private System.Windows.Forms.Button btn_DeleteItem;
    }
}