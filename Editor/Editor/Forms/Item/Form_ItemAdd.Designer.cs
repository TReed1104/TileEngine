namespace Editor
{
    partial class Form_ItemAdd
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
            this.btn_AddNewItem = new System.Windows.Forms.Button();
            this.txt_BaseDurability = new System.Windows.Forms.TextBox();
            this.txt_BuyValue = new System.Windows.Forms.TextBox();
            this.txt_SellValue = new System.Windows.Forms.TextBox();
            this.txt_TextureTag = new System.Windows.Forms.TextBox();
            this.txt_ItemTag = new System.Windows.Forms.TextBox();
            this.lbl_SellValue = new System.Windows.Forms.Label();
            this.lbl_BuyValue = new System.Windows.Forms.Label();
            this.lbl_BaseDurability = new System.Windows.Forms.Label();
            this.lbl_TextureTag = new System.Windows.Forms.Label();
            this.lbl_Tag = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_AddNewItem
            // 
            this.btn_AddNewItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_AddNewItem.Location = new System.Drawing.Point(0, 221);
            this.btn_AddNewItem.Name = "btn_AddNewItem";
            this.btn_AddNewItem.Size = new System.Drawing.Size(350, 40);
            this.btn_AddNewItem.TabIndex = 5;
            this.btn_AddNewItem.Text = "Save Changes";
            this.btn_AddNewItem.UseVisualStyleBackColor = true;
            this.btn_AddNewItem.Click += new System.EventHandler(this.btn_SaveChanges_Click);
            // 
            // txt_BaseDurability
            // 
            this.txt_BaseDurability.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BaseDurability.Location = new System.Drawing.Point(142, 96);
            this.txt_BaseDurability.Name = "txt_BaseDurability";
            this.txt_BaseDurability.Size = new System.Drawing.Size(189, 22);
            this.txt_BaseDurability.TabIndex = 2;
            // 
            // txt_BuyValue
            // 
            this.txt_BuyValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BuyValue.Location = new System.Drawing.Point(142, 121);
            this.txt_BuyValue.Name = "txt_BuyValue";
            this.txt_BuyValue.Size = new System.Drawing.Size(189, 22);
            this.txt_BuyValue.TabIndex = 3;
            // 
            // txt_SellValue
            // 
            this.txt_SellValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SellValue.Location = new System.Drawing.Point(142, 146);
            this.txt_SellValue.Name = "txt_SellValue";
            this.txt_SellValue.Size = new System.Drawing.Size(189, 22);
            this.txt_SellValue.TabIndex = 4;
            // 
            // txt_TextureTag
            // 
            this.txt_TextureTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TextureTag.Location = new System.Drawing.Point(142, 71);
            this.txt_TextureTag.Name = "txt_TextureTag";
            this.txt_TextureTag.Size = new System.Drawing.Size(189, 22);
            this.txt_TextureTag.TabIndex = 1;
            // 
            // txt_ItemTag
            // 
            this.txt_ItemTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ItemTag.Location = new System.Drawing.Point(142, 46);
            this.txt_ItemTag.Name = "txt_ItemTag";
            this.txt_ItemTag.Size = new System.Drawing.Size(189, 22);
            this.txt_ItemTag.TabIndex = 0;
            // 
            // lbl_SellValue
            // 
            this.lbl_SellValue.AutoSize = true;
            this.lbl_SellValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SellValue.Location = new System.Drawing.Point(12, 149);
            this.lbl_SellValue.Name = "lbl_SellValue";
            this.lbl_SellValue.Size = new System.Drawing.Size(72, 16);
            this.lbl_SellValue.TabIndex = 20;
            this.lbl_SellValue.Text = "Sell Value:";
            // 
            // lbl_BuyValue
            // 
            this.lbl_BuyValue.AutoSize = true;
            this.lbl_BuyValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BuyValue.Location = new System.Drawing.Point(12, 124);
            this.lbl_BuyValue.Name = "lbl_BuyValue";
            this.lbl_BuyValue.Size = new System.Drawing.Size(72, 16);
            this.lbl_BuyValue.TabIndex = 19;
            this.lbl_BuyValue.Text = "Buy Value:";
            // 
            // lbl_BaseDurability
            // 
            this.lbl_BaseDurability.AutoSize = true;
            this.lbl_BaseDurability.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BaseDurability.Location = new System.Drawing.Point(12, 99);
            this.lbl_BaseDurability.Name = "lbl_BaseDurability";
            this.lbl_BaseDurability.Size = new System.Drawing.Size(95, 16);
            this.lbl_BaseDurability.TabIndex = 18;
            this.lbl_BaseDurability.Text = "Max Durability:";
            // 
            // lbl_TextureTag
            // 
            this.lbl_TextureTag.AutoSize = true;
            this.lbl_TextureTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TextureTag.Location = new System.Drawing.Point(12, 74);
            this.lbl_TextureTag.Name = "lbl_TextureTag";
            this.lbl_TextureTag.Size = new System.Drawing.Size(88, 16);
            this.lbl_TextureTag.TabIndex = 17;
            this.lbl_TextureTag.Text = "Texture_Tag:";
            // 
            // lbl_Tag
            // 
            this.lbl_Tag.AutoSize = true;
            this.lbl_Tag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Tag.Location = new System.Drawing.Point(12, 49);
            this.lbl_Tag.Name = "lbl_Tag";
            this.lbl_Tag.Size = new System.Drawing.Size(64, 16);
            this.lbl_Tag.TabIndex = 16;
            this.lbl_Tag.Text = "Item Tag:";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(12, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(85, 24);
            this.lbl_Title.TabIndex = 15;
            this.lbl_Title.Text = "Add Item";
            // 
            // Form_ItemAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 261);
            this.Controls.Add(this.btn_AddNewItem);
            this.Controls.Add(this.txt_BaseDurability);
            this.Controls.Add(this.txt_BuyValue);
            this.Controls.Add(this.txt_SellValue);
            this.Controls.Add(this.txt_TextureTag);
            this.Controls.Add(this.txt_ItemTag);
            this.Controls.Add(this.lbl_SellValue);
            this.Controls.Add(this.lbl_BuyValue);
            this.Controls.Add(this.lbl_BaseDurability);
            this.Controls.Add(this.lbl_TextureTag);
            this.Controls.Add(this.lbl_Tag);
            this.Controls.Add(this.lbl_Title);
            this.Name = "Form_ItemAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Item";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ItemAdd_FormClosing);
            this.Load += new System.EventHandler(this.FormItemAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_AddNewItem;
        private System.Windows.Forms.TextBox txt_BaseDurability;
        private System.Windows.Forms.TextBox txt_BuyValue;
        private System.Windows.Forms.TextBox txt_SellValue;
        private System.Windows.Forms.TextBox txt_TextureTag;
        private System.Windows.Forms.TextBox txt_ItemTag;
        private System.Windows.Forms.Label lbl_SellValue;
        private System.Windows.Forms.Label lbl_BuyValue;
        private System.Windows.Forms.Label lbl_BaseDurability;
        private System.Windows.Forms.Label lbl_TextureTag;
        private System.Windows.Forms.Label lbl_Tag;
        private System.Windows.Forms.Label lbl_Title;
    }
}