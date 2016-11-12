namespace Editor
{
    partial class Form_EntityEdit
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
            this.btn_SaveChanges = new System.Windows.Forms.Button();
            this.lst_EntityConfigs = new System.Windows.Forms.ListBox();
            this.txt_SrcFrameY = new System.Windows.Forms.TextBox();
            this.lbl_SourceFrameY = new System.Windows.Forms.Label();
            this.lbl_SourceFrameX = new System.Windows.Forms.Label();
            this.txt_TextureTag = new System.Windows.Forms.TextBox();
            this.txt_SrcFrameX = new System.Windows.Forms.TextBox();
            this.txt_Colour = new System.Windows.Forms.TextBox();
            this.txt_EntityType = new System.Windows.Forms.TextBox();
            this.txt_EntityTag = new System.Windows.Forms.TextBox();
            this.lbl_SellValue = new System.Windows.Forms.Label();
            this.lbl_SourceframeSize = new System.Windows.Forms.Label();
            this.lbl_TextureTag = new System.Windows.Forms.Label();
            this.lbl_EntityType = new System.Windows.Forms.Label();
            this.lbl_EntityTag = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_SaveData = new System.Windows.Forms.Label();
            this.txt_SaveData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_SaveChanges
            // 
            this.btn_SaveChanges.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_SaveChanges.Location = new System.Drawing.Point(180, 221);
            this.btn_SaveChanges.Name = "btn_SaveChanges";
            this.btn_SaveChanges.Size = new System.Drawing.Size(354, 40);
            this.btn_SaveChanges.TabIndex = 16;
            this.btn_SaveChanges.Text = "Save Changes";
            this.btn_SaveChanges.UseVisualStyleBackColor = true;
            this.btn_SaveChanges.Click += new System.EventHandler(this.btn_SaveChanges_Click);
            // 
            // lst_EntityConfigs
            // 
            this.lst_EntityConfigs.Dock = System.Windows.Forms.DockStyle.Left;
            this.lst_EntityConfigs.FormattingEnabled = true;
            this.lst_EntityConfigs.Location = new System.Drawing.Point(0, 0);
            this.lst_EntityConfigs.Name = "lst_EntityConfigs";
            this.lst_EntityConfigs.ScrollAlwaysVisible = true;
            this.lst_EntityConfigs.Size = new System.Drawing.Size(180, 261);
            this.lst_EntityConfigs.TabIndex = 15;
            this.lst_EntityConfigs.SelectedIndexChanged += new System.EventHandler(this.lst_EntityConfigs_SelectedIndexChanged);
            // 
            // txt_SrcFrameY
            // 
            this.txt_SrcFrameY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SrcFrameY.Location = new System.Drawing.Point(449, 125);
            this.txt_SrcFrameY.Name = "txt_SrcFrameY";
            this.txt_SrcFrameY.Size = new System.Drawing.Size(70, 22);
            this.txt_SrcFrameY.TabIndex = 39;
            this.txt_SrcFrameY.Text = "64";
            // 
            // lbl_SourceFrameY
            // 
            this.lbl_SourceFrameY.AutoSize = true;
            this.lbl_SourceFrameY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SourceFrameY.Location = new System.Drawing.Point(426, 125);
            this.lbl_SourceFrameY.Name = "lbl_SourceFrameY";
            this.lbl_SourceFrameY.Size = new System.Drawing.Size(17, 16);
            this.lbl_SourceFrameY.TabIndex = 48;
            this.lbl_SourceFrameY.Text = "Y";
            // 
            // lbl_SourceFrameX
            // 
            this.lbl_SourceFrameX.AutoSize = true;
            this.lbl_SourceFrameX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SourceFrameX.Location = new System.Drawing.Point(327, 125);
            this.lbl_SourceFrameX.Name = "lbl_SourceFrameX";
            this.lbl_SourceFrameX.Size = new System.Drawing.Size(16, 16);
            this.lbl_SourceFrameX.TabIndex = 47;
            this.lbl_SourceFrameX.Text = "X";
            // 
            // txt_TextureTag
            // 
            this.txt_TextureTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TextureTag.Location = new System.Drawing.Point(330, 100);
            this.txt_TextureTag.Name = "txt_TextureTag";
            this.txt_TextureTag.Size = new System.Drawing.Size(189, 22);
            this.txt_TextureTag.TabIndex = 37;
            // 
            // txt_SrcFrameX
            // 
            this.txt_SrcFrameX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SrcFrameX.Location = new System.Drawing.Point(349, 125);
            this.txt_SrcFrameX.Name = "txt_SrcFrameX";
            this.txt_SrcFrameX.Size = new System.Drawing.Size(71, 22);
            this.txt_SrcFrameX.TabIndex = 38;
            this.txt_SrcFrameX.Text = "64";
            // 
            // txt_Colour
            // 
            this.txt_Colour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Colour.Location = new System.Drawing.Point(330, 150);
            this.txt_Colour.Name = "txt_Colour";
            this.txt_Colour.Size = new System.Drawing.Size(189, 22);
            this.txt_Colour.TabIndex = 40;
            // 
            // txt_EntityType
            // 
            this.txt_EntityType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EntityType.Location = new System.Drawing.Point(330, 75);
            this.txt_EntityType.Name = "txt_EntityType";
            this.txt_EntityType.Size = new System.Drawing.Size(189, 22);
            this.txt_EntityType.TabIndex = 36;
            // 
            // txt_EntityTag
            // 
            this.txt_EntityTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EntityTag.Location = new System.Drawing.Point(330, 50);
            this.txt_EntityTag.Name = "txt_EntityTag";
            this.txt_EntityTag.Size = new System.Drawing.Size(189, 22);
            this.txt_EntityTag.TabIndex = 35;
            // 
            // lbl_SellValue
            // 
            this.lbl_SellValue.AutoSize = true;
            this.lbl_SellValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SellValue.Location = new System.Drawing.Point(200, 150);
            this.lbl_SellValue.Name = "lbl_SellValue";
            this.lbl_SellValue.Size = new System.Drawing.Size(47, 16);
            this.lbl_SellValue.TabIndex = 46;
            this.lbl_SellValue.Text = "Colour";
            // 
            // lbl_SourceframeSize
            // 
            this.lbl_SourceframeSize.AutoSize = true;
            this.lbl_SourceframeSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SourceframeSize.Location = new System.Drawing.Point(200, 125);
            this.lbl_SourceframeSize.Name = "lbl_SourceframeSize";
            this.lbl_SourceframeSize.Size = new System.Drawing.Size(122, 16);
            this.lbl_SourceframeSize.TabIndex = 45;
            this.lbl_SourceframeSize.Text = "Source Frame Size";
            // 
            // lbl_TextureTag
            // 
            this.lbl_TextureTag.AutoSize = true;
            this.lbl_TextureTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TextureTag.Location = new System.Drawing.Point(200, 100);
            this.lbl_TextureTag.Name = "lbl_TextureTag";
            this.lbl_TextureTag.Size = new System.Drawing.Size(84, 16);
            this.lbl_TextureTag.TabIndex = 44;
            this.lbl_TextureTag.Text = "Texture Tag:";
            // 
            // lbl_EntityType
            // 
            this.lbl_EntityType.AutoSize = true;
            this.lbl_EntityType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EntityType.Location = new System.Drawing.Point(200, 75);
            this.lbl_EntityType.Name = "lbl_EntityType";
            this.lbl_EntityType.Size = new System.Drawing.Size(78, 16);
            this.lbl_EntityType.TabIndex = 43;
            this.lbl_EntityType.Text = "Entity Type:";
            // 
            // lbl_EntityTag
            // 
            this.lbl_EntityTag.AutoSize = true;
            this.lbl_EntityTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EntityTag.Location = new System.Drawing.Point(200, 50);
            this.lbl_EntityTag.Name = "lbl_EntityTag";
            this.lbl_EntityTag.Size = new System.Drawing.Size(71, 16);
            this.lbl_EntityTag.TabIndex = 42;
            this.lbl_EntityTag.Text = "Entity Tag:";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(200, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(92, 24);
            this.lbl_Title.TabIndex = 41;
            this.lbl_Title.Text = "Edit Entity";
            // 
            // lbl_SaveData
            // 
            this.lbl_SaveData.AutoSize = true;
            this.lbl_SaveData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SaveData.Location = new System.Drawing.Point(201, 175);
            this.lbl_SaveData.Name = "lbl_SaveData";
            this.lbl_SaveData.Size = new System.Drawing.Size(72, 16);
            this.lbl_SaveData.TabIndex = 49;
            this.lbl_SaveData.Text = "Save Data";
            // 
            // txt_SaveData
            // 
            this.txt_SaveData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SaveData.Location = new System.Drawing.Point(330, 175);
            this.txt_SaveData.Name = "txt_SaveData";
            this.txt_SaveData.Size = new System.Drawing.Size(189, 22);
            this.txt_SaveData.TabIndex = 50;
            // 
            // Form_EntityEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(534, 261);
            this.Controls.Add(this.txt_SaveData);
            this.Controls.Add(this.lbl_SaveData);
            this.Controls.Add(this.txt_SrcFrameY);
            this.Controls.Add(this.lbl_SourceFrameY);
            this.Controls.Add(this.lbl_SourceFrameX);
            this.Controls.Add(this.txt_TextureTag);
            this.Controls.Add(this.txt_SrcFrameX);
            this.Controls.Add(this.txt_Colour);
            this.Controls.Add(this.txt_EntityType);
            this.Controls.Add(this.txt_EntityTag);
            this.Controls.Add(this.lbl_SellValue);
            this.Controls.Add(this.lbl_SourceframeSize);
            this.Controls.Add(this.lbl_TextureTag);
            this.Controls.Add(this.lbl_EntityType);
            this.Controls.Add(this.lbl_EntityTag);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.btn_SaveChanges);
            this.Controls.Add(this.lst_EntityConfigs);
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "Form_EntityEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Entity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_EntityEdit_FormClosing);
            this.Load += new System.EventHandler(this.Form_EntityEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_SaveChanges;
        private System.Windows.Forms.ListBox lst_EntityConfigs;
        private System.Windows.Forms.TextBox txt_SrcFrameY;
        private System.Windows.Forms.Label lbl_SourceFrameY;
        private System.Windows.Forms.Label lbl_SourceFrameX;
        private System.Windows.Forms.TextBox txt_TextureTag;
        private System.Windows.Forms.TextBox txt_SrcFrameX;
        private System.Windows.Forms.TextBox txt_Colour;
        private System.Windows.Forms.TextBox txt_EntityType;
        private System.Windows.Forms.TextBox txt_EntityTag;
        private System.Windows.Forms.Label lbl_SellValue;
        private System.Windows.Forms.Label lbl_SourceframeSize;
        private System.Windows.Forms.Label lbl_TextureTag;
        private System.Windows.Forms.Label lbl_EntityType;
        private System.Windows.Forms.Label lbl_EntityTag;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_SaveData;
        private System.Windows.Forms.TextBox txt_SaveData;
    }
}