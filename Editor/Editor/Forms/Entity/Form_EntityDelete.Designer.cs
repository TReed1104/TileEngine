namespace Editor
{
    partial class Form_EntityDelete
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
            this.btn_DeleteEntity = new System.Windows.Forms.Button();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.cbo_DeleteEntity = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_DeleteEntity
            // 
            this.btn_DeleteEntity.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_DeleteEntity.Location = new System.Drawing.Point(0, 77);
            this.btn_DeleteEntity.Name = "btn_DeleteEntity";
            this.btn_DeleteEntity.Size = new System.Drawing.Size(284, 40);
            this.btn_DeleteEntity.TabIndex = 20;
            this.btn_DeleteEntity.Text = "Delete Entity";
            this.btn_DeleteEntity.UseVisualStyleBackColor = true;
            this.btn_DeleteEntity.Click += new System.EventHandler(this.btn_DeleteEntity_Click);
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(12, 4);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(114, 24);
            this.lbl_Title.TabIndex = 19;
            this.lbl_Title.Text = "Delete Entity";
            // 
            // cbo_DeleteEntity
            // 
            this.cbo_DeleteEntity.FormattingEnabled = true;
            this.cbo_DeleteEntity.Location = new System.Drawing.Point(12, 31);
            this.cbo_DeleteEntity.Name = "cbo_DeleteEntity";
            this.cbo_DeleteEntity.Size = new System.Drawing.Size(260, 21);
            this.cbo_DeleteEntity.TabIndex = 18;
            this.cbo_DeleteEntity.SelectedIndexChanged += new System.EventHandler(this.cbo_DeleteEntity_SelectedIndexChanged);
            // 
            // Form_EntityDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 117);
            this.Controls.Add(this.btn_DeleteEntity);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.cbo_DeleteEntity);
            this.MinimumSize = new System.Drawing.Size(300, 156);
            this.Name = "Form_EntityDelete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Entity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_EntityDelete_FormClosing);
            this.Load += new System.EventHandler(this.Form_EntityDelete_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_DeleteEntity;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.ComboBox cbo_DeleteEntity;
    }
}