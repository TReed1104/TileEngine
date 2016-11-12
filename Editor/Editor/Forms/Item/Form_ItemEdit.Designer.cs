namespace Editor
{
    partial class Form_ItemEdit
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
            this.lst_ItemConfigs = new System.Windows.Forms.ListBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_Tag = new System.Windows.Forms.Label();
            this.lbl_TextureTag = new System.Windows.Forms.Label();
            this.lbl_MaxDurability = new System.Windows.Forms.Label();
            this.lbl_BuyValue = new System.Windows.Forms.Label();
            this.lbl_SellValue = new System.Windows.Forms.Label();
            this.txt_ItemTag = new System.Windows.Forms.TextBox();
            this.txt_TextureTag = new System.Windows.Forms.TextBox();
            this.txt_SellValue = new System.Windows.Forms.TextBox();
            this.txt_BuyValue = new System.Windows.Forms.TextBox();
            this.txt_BaseDurability = new System.Windows.Forms.TextBox();
            this.btn_SaveChanges = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lst_ItemConfigs
            // 
            this.lst_ItemConfigs.Dock = System.Windows.Forms.DockStyle.Left;
            this.lst_ItemConfigs.FormattingEnabled = true;
            this.lst_ItemConfigs.Location = new System.Drawing.Point(0, 0);
            this.lst_ItemConfigs.Name = "lst_ItemConfigs";
            this.lst_ItemConfigs.ScrollAlwaysVisible = true;
            this.lst_ItemConfigs.Size = new System.Drawing.Size(180, 261);
            this.lst_ItemConfigs.TabIndex = 0;
            this.lst_ItemConfigs.SelectedIndexChanged += new System.EventHandler(this.lst_ItemConfigs_SelectedIndexChanged_1);
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(200, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(82, 24);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Edit Item";
            // 
            // lbl_Tag
            // 
            this.lbl_Tag.AutoSize = true;
            this.lbl_Tag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Tag.Location = new System.Drawing.Point(200, 50);
            this.lbl_Tag.Name = "lbl_Tag";
            this.lbl_Tag.Size = new System.Drawing.Size(64, 16);
            this.lbl_Tag.TabIndex = 2;
            this.lbl_Tag.Text = "Item Tag:";
            // 
            // lbl_TextureTag
            // 
            this.lbl_TextureTag.AutoSize = true;
            this.lbl_TextureTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TextureTag.Location = new System.Drawing.Point(200, 75);
            this.lbl_TextureTag.Name = "lbl_TextureTag";
            this.lbl_TextureTag.Size = new System.Drawing.Size(88, 16);
            this.lbl_TextureTag.TabIndex = 3;
            this.lbl_TextureTag.Text = "Texture_Tag:";
            // 
            // lbl_MaxDurability
            // 
            this.lbl_MaxDurability.AutoSize = true;
            this.lbl_MaxDurability.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MaxDurability.Location = new System.Drawing.Point(200, 100);
            this.lbl_MaxDurability.Name = "lbl_MaxDurability";
            this.lbl_MaxDurability.Size = new System.Drawing.Size(95, 16);
            this.lbl_MaxDurability.TabIndex = 5;
            this.lbl_MaxDurability.Text = "Max Durability:";
            // 
            // lbl_BuyValue
            // 
            this.lbl_BuyValue.AutoSize = true;
            this.lbl_BuyValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BuyValue.Location = new System.Drawing.Point(200, 125);
            this.lbl_BuyValue.Name = "lbl_BuyValue";
            this.lbl_BuyValue.Size = new System.Drawing.Size(72, 16);
            this.lbl_BuyValue.TabIndex = 6;
            this.lbl_BuyValue.Text = "Buy Value:";
            // 
            // lbl_SellValue
            // 
            this.lbl_SellValue.AutoSize = true;
            this.lbl_SellValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SellValue.Location = new System.Drawing.Point(200, 150);
            this.lbl_SellValue.Name = "lbl_SellValue";
            this.lbl_SellValue.Size = new System.Drawing.Size(72, 16);
            this.lbl_SellValue.TabIndex = 7;
            this.lbl_SellValue.Text = "Sell Value:";
            // 
            // txt_ItemTag
            // 
            this.txt_ItemTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ItemTag.Location = new System.Drawing.Point(330, 47);
            this.txt_ItemTag.Name = "txt_ItemTag";
            this.txt_ItemTag.Size = new System.Drawing.Size(189, 22);
            this.txt_ItemTag.TabIndex = 8;
            // 
            // txt_TextureTag
            // 
            this.txt_TextureTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TextureTag.Location = new System.Drawing.Point(330, 72);
            this.txt_TextureTag.Name = "txt_TextureTag";
            this.txt_TextureTag.Size = new System.Drawing.Size(189, 22);
            this.txt_TextureTag.TabIndex = 9;
            // 
            // txt_SellValue
            // 
            this.txt_SellValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SellValue.Location = new System.Drawing.Point(330, 147);
            this.txt_SellValue.Name = "txt_SellValue";
            this.txt_SellValue.Size = new System.Drawing.Size(189, 22);
            this.txt_SellValue.TabIndex = 10;
            // 
            // txt_BuyValue
            // 
            this.txt_BuyValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BuyValue.Location = new System.Drawing.Point(330, 122);
            this.txt_BuyValue.Name = "txt_BuyValue";
            this.txt_BuyValue.Size = new System.Drawing.Size(189, 22);
            this.txt_BuyValue.TabIndex = 11;
            // 
            // txt_BaseDurability
            // 
            this.txt_BaseDurability.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BaseDurability.Location = new System.Drawing.Point(330, 97);
            this.txt_BaseDurability.Name = "txt_BaseDurability";
            this.txt_BaseDurability.Size = new System.Drawing.Size(189, 22);
            this.txt_BaseDurability.TabIndex = 12;
            // 
            // btn_SaveChanges
            // 
            this.btn_SaveChanges.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_SaveChanges.Location = new System.Drawing.Point(180, 221);
            this.btn_SaveChanges.Name = "btn_SaveChanges";
            this.btn_SaveChanges.Size = new System.Drawing.Size(354, 40);
            this.btn_SaveChanges.TabIndex = 14;
            this.btn_SaveChanges.Text = "Save Changes";
            this.btn_SaveChanges.UseVisualStyleBackColor = true;
            this.btn_SaveChanges.Click += new System.EventHandler(this.btn_SaveChanges_Click);
            // 
            // Form_ItemEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(534, 261);
            this.Controls.Add(this.btn_SaveChanges);
            this.Controls.Add(this.txt_BaseDurability);
            this.Controls.Add(this.txt_BuyValue);
            this.Controls.Add(this.txt_SellValue);
            this.Controls.Add(this.txt_TextureTag);
            this.Controls.Add(this.txt_ItemTag);
            this.Controls.Add(this.lbl_SellValue);
            this.Controls.Add(this.lbl_BuyValue);
            this.Controls.Add(this.lbl_MaxDurability);
            this.Controls.Add(this.lbl_TextureTag);
            this.Controls.Add(this.lbl_Tag);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.lst_ItemConfigs);
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "Form_ItemEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Item";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ItemEdit_FormClosing);
            this.Load += new System.EventHandler(this.frm_ItemEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_ItemConfigs;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_Tag;
        private System.Windows.Forms.Label lbl_TextureTag;
        private System.Windows.Forms.Label lbl_MaxDurability;
        private System.Windows.Forms.Label lbl_BuyValue;
        private System.Windows.Forms.Label lbl_SellValue;
        private System.Windows.Forms.TextBox txt_ItemTag;
        private System.Windows.Forms.TextBox txt_TextureTag;
        private System.Windows.Forms.TextBox txt_SellValue;
        private System.Windows.Forms.TextBox txt_BuyValue;
        private System.Windows.Forms.TextBox txt_BaseDurability;
        private System.Windows.Forms.Button btn_SaveChanges;
    }
}