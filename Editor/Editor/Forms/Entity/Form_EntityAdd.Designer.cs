namespace Editor
{
    partial class Form_EntityAdd
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
            this.btn_AddNewEntity = new System.Windows.Forms.Button();
            this.txt_TextureTag = new System.Windows.Forms.TextBox();
            this.txt_SrcFrameX = new System.Windows.Forms.TextBox();
            this.txt_EntityTag = new System.Windows.Forms.TextBox();
            this.lbl_Colour = new System.Windows.Forms.Label();
            this.lbl_SrcFrameSize = new System.Windows.Forms.Label();
            this.lbl_TextureTag = new System.Windows.Forms.Label();
            this.lbl_EntityType = new System.Windows.Forms.Label();
            this.lbl_EntityTag = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_SrcFrameX = new System.Windows.Forms.Label();
            this.lbl_SrcFrameY = new System.Windows.Forms.Label();
            this.txt_SrcFrameY = new System.Windows.Forms.TextBox();
            this.cbo_EntityType = new System.Windows.Forms.ComboBox();
            this.cbo_Colours = new System.Windows.Forms.ComboBox();
            this.btn_TexturePicker = new System.Windows.Forms.Button();
            this.txt_health = new System.Windows.Forms.TextBox();
            this.lbl_health = new System.Windows.Forms.Label();
            this.txt_damage = new System.Windows.Forms.TextBox();
            this.lbl_damage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_AddNewEntity
            // 
            this.btn_AddNewEntity.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_AddNewEntity.Location = new System.Drawing.Point(0, 409);
            this.btn_AddNewEntity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_AddNewEntity.Name = "btn_AddNewEntity";
            this.btn_AddNewEntity.Size = new System.Drawing.Size(516, 62);
            this.btn_AddNewEntity.TabIndex = 6;
            this.btn_AddNewEntity.Text = "Save Changes";
            this.btn_AddNewEntity.UseVisualStyleBackColor = true;
            this.btn_AddNewEntity.Click += new System.EventHandler(this.btn_AddNewEntity_Click);
            // 
            // txt_TextureTag
            // 
            this.txt_TextureTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TextureTag.Location = new System.Drawing.Point(213, 155);
            this.txt_TextureTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_TextureTag.Name = "txt_TextureTag";
            this.txt_TextureTag.ReadOnly = true;
            this.txt_TextureTag.Size = new System.Drawing.Size(250, 30);
            this.txt_TextureTag.TabIndex = 2;
            // 
            // txt_SrcFrameX
            // 
            this.txt_SrcFrameX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SrcFrameX.Location = new System.Drawing.Point(242, 195);
            this.txt_SrcFrameX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_SrcFrameX.Name = "txt_SrcFrameX";
            this.txt_SrcFrameX.Size = new System.Drawing.Size(104, 30);
            this.txt_SrcFrameX.TabIndex = 3;
            this.txt_SrcFrameX.Text = "64";
            // 
            // txt_EntityTag
            // 
            this.txt_EntityTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EntityTag.Location = new System.Drawing.Point(213, 72);
            this.txt_EntityTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_EntityTag.Name = "txt_EntityTag";
            this.txt_EntityTag.Size = new System.Drawing.Size(282, 30);
            this.txt_EntityTag.TabIndex = 0;
            // 
            // lbl_Colour
            // 
            this.lbl_Colour.AutoSize = true;
            this.lbl_Colour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Colour.Location = new System.Drawing.Point(20, 238);
            this.lbl_Colour.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Colour.Name = "lbl_Colour";
            this.lbl_Colour.Size = new System.Drawing.Size(70, 25);
            this.lbl_Colour.TabIndex = 32;
            this.lbl_Colour.Text = "Colour";
            // 
            // lbl_SrcFrameSize
            // 
            this.lbl_SrcFrameSize.AutoSize = true;
            this.lbl_SrcFrameSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SrcFrameSize.Location = new System.Drawing.Point(19, 195);
            this.lbl_SrcFrameSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SrcFrameSize.Name = "lbl_SrcFrameSize";
            this.lbl_SrcFrameSize.Size = new System.Drawing.Size(180, 25);
            this.lbl_SrcFrameSize.TabIndex = 31;
            this.lbl_SrcFrameSize.Text = "Source Frame Size";
            // 
            // lbl_TextureTag
            // 
            this.lbl_TextureTag.AutoSize = true;
            this.lbl_TextureTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TextureTag.Location = new System.Drawing.Point(18, 158);
            this.lbl_TextureTag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TextureTag.Name = "lbl_TextureTag";
            this.lbl_TextureTag.Size = new System.Drawing.Size(125, 25);
            this.lbl_TextureTag.TabIndex = 30;
            this.lbl_TextureTag.Text = "Texture Tag:";
            // 
            // lbl_EntityType
            // 
            this.lbl_EntityType.AutoSize = true;
            this.lbl_EntityType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EntityType.Location = new System.Drawing.Point(18, 115);
            this.lbl_EntityType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_EntityType.Name = "lbl_EntityType";
            this.lbl_EntityType.Size = new System.Drawing.Size(116, 25);
            this.lbl_EntityType.TabIndex = 29;
            this.lbl_EntityType.Text = "Entity Type:";
            // 
            // lbl_EntityTag
            // 
            this.lbl_EntityTag.AutoSize = true;
            this.lbl_EntityTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EntityTag.Location = new System.Drawing.Point(20, 75);
            this.lbl_EntityTag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_EntityTag.Name = "lbl_EntityTag";
            this.lbl_EntityTag.Size = new System.Drawing.Size(106, 25);
            this.lbl_EntityTag.TabIndex = 28;
            this.lbl_EntityTag.Text = "Entity Tag:";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(18, 14);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(147, 33);
            this.lbl_Title.TabIndex = 27;
            this.lbl_Title.Text = "Add Entity";
            // 
            // lbl_SrcFrameX
            // 
            this.lbl_SrcFrameX.AutoSize = true;
            this.lbl_SrcFrameX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SrcFrameX.Location = new System.Drawing.Point(208, 195);
            this.lbl_SrcFrameX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SrcFrameX.Name = "lbl_SrcFrameX";
            this.lbl_SrcFrameX.Size = new System.Drawing.Size(26, 25);
            this.lbl_SrcFrameX.TabIndex = 33;
            this.lbl_SrcFrameX.Text = "X";
            // 
            // lbl_SrcFrameY
            // 
            this.lbl_SrcFrameY.AutoSize = true;
            this.lbl_SrcFrameY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SrcFrameY.Location = new System.Drawing.Point(357, 195);
            this.lbl_SrcFrameY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SrcFrameY.Name = "lbl_SrcFrameY";
            this.lbl_SrcFrameY.Size = new System.Drawing.Size(25, 25);
            this.lbl_SrcFrameY.TabIndex = 34;
            this.lbl_SrcFrameY.Text = "Y";
            // 
            // txt_SrcFrameY
            // 
            this.txt_SrcFrameY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SrcFrameY.Location = new System.Drawing.Point(392, 195);
            this.txt_SrcFrameY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_SrcFrameY.Name = "txt_SrcFrameY";
            this.txt_SrcFrameY.Size = new System.Drawing.Size(103, 30);
            this.txt_SrcFrameY.TabIndex = 4;
            this.txt_SrcFrameY.Text = "64";
            // 
            // cbo_EntityType
            // 
            this.cbo_EntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_EntityType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_EntityType.FormattingEnabled = true;
            this.cbo_EntityType.Location = new System.Drawing.Point(213, 112);
            this.cbo_EntityType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbo_EntityType.MaxDropDownItems = 10;
            this.cbo_EntityType.Name = "cbo_EntityType";
            this.cbo_EntityType.Size = new System.Drawing.Size(282, 33);
            this.cbo_EntityType.TabIndex = 35;
            // 
            // cbo_Colours
            // 
            this.cbo_Colours.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Colours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_Colours.FormattingEnabled = true;
            this.cbo_Colours.Location = new System.Drawing.Point(213, 235);
            this.cbo_Colours.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbo_Colours.Name = "cbo_Colours";
            this.cbo_Colours.Size = new System.Drawing.Size(282, 33);
            this.cbo_Colours.TabIndex = 36;
            // 
            // btn_TexturePicker
            // 
            this.btn_TexturePicker.Location = new System.Drawing.Point(456, 155);
            this.btn_TexturePicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_TexturePicker.Name = "btn_TexturePicker";
            this.btn_TexturePicker.Size = new System.Drawing.Size(39, 41);
            this.btn_TexturePicker.TabIndex = 37;
            this.btn_TexturePicker.Text = "...";
            this.btn_TexturePicker.UseVisualStyleBackColor = true;
            this.btn_TexturePicker.Click += new System.EventHandler(this.btn_TexturePicker_Click);
            // 
            // txt_health
            // 
            this.txt_health.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_health.Location = new System.Drawing.Point(213, 278);
            this.txt_health.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_health.Name = "txt_health";
            this.txt_health.Size = new System.Drawing.Size(282, 30);
            this.txt_health.TabIndex = 38;
            // 
            // lbl_health
            // 
            this.lbl_health.AutoSize = true;
            this.lbl_health.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_health.Location = new System.Drawing.Point(22, 281);
            this.lbl_health.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_health.Name = "lbl_health";
            this.lbl_health.Size = new System.Drawing.Size(68, 25);
            this.lbl_health.TabIndex = 39;
            this.lbl_health.Text = "Health";
            // 
            // txt_damage
            // 
            this.txt_damage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_damage.Location = new System.Drawing.Point(213, 318);
            this.txt_damage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_damage.Name = "txt_damage";
            this.txt_damage.Size = new System.Drawing.Size(282, 30);
            this.txt_damage.TabIndex = 40;
            // 
            // lbl_damage
            // 
            this.lbl_damage.AutoSize = true;
            this.lbl_damage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_damage.Location = new System.Drawing.Point(22, 321);
            this.lbl_damage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_damage.Name = "lbl_damage";
            this.lbl_damage.Size = new System.Drawing.Size(86, 25);
            this.lbl_damage.TabIndex = 41;
            this.lbl_damage.Text = "Damage";
            // 
            // Form_EntityAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 471);
            this.Controls.Add(this.txt_damage);
            this.Controls.Add(this.lbl_damage);
            this.Controls.Add(this.txt_health);
            this.Controls.Add(this.lbl_health);
            this.Controls.Add(this.btn_TexturePicker);
            this.Controls.Add(this.cbo_Colours);
            this.Controls.Add(this.cbo_EntityType);
            this.Controls.Add(this.txt_SrcFrameY);
            this.Controls.Add(this.lbl_SrcFrameY);
            this.Controls.Add(this.lbl_SrcFrameX);
            this.Controls.Add(this.btn_AddNewEntity);
            this.Controls.Add(this.txt_TextureTag);
            this.Controls.Add(this.txt_SrcFrameX);
            this.Controls.Add(this.txt_EntityTag);
            this.Controls.Add(this.lbl_Colour);
            this.Controls.Add(this.lbl_SrcFrameSize);
            this.Controls.Add(this.lbl_TextureTag);
            this.Controls.Add(this.lbl_EntityType);
            this.Controls.Add(this.lbl_EntityTag);
            this.Controls.Add(this.lbl_Title);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form_EntityAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_EntityAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_EntityAdd_FormClosing);
            this.Load += new System.EventHandler(this.Form_EntityAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_AddNewEntity;
        private System.Windows.Forms.TextBox txt_TextureTag;
        private System.Windows.Forms.TextBox txt_SrcFrameX;
        private System.Windows.Forms.TextBox txt_EntityTag;
        private System.Windows.Forms.Label lbl_Colour;
        private System.Windows.Forms.Label lbl_SrcFrameSize;
        private System.Windows.Forms.Label lbl_TextureTag;
        private System.Windows.Forms.Label lbl_EntityType;
        private System.Windows.Forms.Label lbl_EntityTag;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_SrcFrameX;
        private System.Windows.Forms.Label lbl_SrcFrameY;
        private System.Windows.Forms.TextBox txt_SrcFrameY;
        private System.Windows.Forms.ComboBox cbo_EntityType;
        private System.Windows.Forms.ComboBox cbo_Colours;
        private System.Windows.Forms.Button btn_TexturePicker;
        private System.Windows.Forms.TextBox txt_health;
        private System.Windows.Forms.Label lbl_health;
        private System.Windows.Forms.TextBox txt_damage;
        private System.Windows.Forms.Label lbl_damage;
    }
}