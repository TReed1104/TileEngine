namespace Editor
{
    partial class Form_EntityMenu
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
            this.btn_EditEntity = new System.Windows.Forms.Button();
            this.btn_AddEntity = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_DeleteEntity
            // 
            this.btn_DeleteEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_DeleteEntity.Location = new System.Drawing.Point(0, 100);
            this.btn_DeleteEntity.Name = "btn_DeleteEntity";
            this.btn_DeleteEntity.Size = new System.Drawing.Size(284, 50);
            this.btn_DeleteEntity.TabIndex = 9;
            this.btn_DeleteEntity.Text = "Delete Entity";
            this.btn_DeleteEntity.UseVisualStyleBackColor = true;
            this.btn_DeleteEntity.Click += new System.EventHandler(this.btn_DeleteEntity_Click);
            // 
            // btn_EditEntity
            // 
            this.btn_EditEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_EditEntity.Location = new System.Drawing.Point(0, 50);
            this.btn_EditEntity.Name = "btn_EditEntity";
            this.btn_EditEntity.Size = new System.Drawing.Size(284, 50);
            this.btn_EditEntity.TabIndex = 8;
            this.btn_EditEntity.Text = "Edit Entity";
            this.btn_EditEntity.UseVisualStyleBackColor = true;
            this.btn_EditEntity.Click += new System.EventHandler(this.btn_EditEntity_Click);
            // 
            // btn_AddEntity
            // 
            this.btn_AddEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_AddEntity.Location = new System.Drawing.Point(0, 0);
            this.btn_AddEntity.Name = "btn_AddEntity";
            this.btn_AddEntity.Size = new System.Drawing.Size(284, 50);
            this.btn_AddEntity.TabIndex = 7;
            this.btn_AddEntity.Text = "Add Entity";
            this.btn_AddEntity.UseVisualStyleBackColor = true;
            this.btn_AddEntity.Click += new System.EventHandler(this.btn_AddEntity_Click);
            // 
            // Form_EntityMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 151);
            this.Controls.Add(this.btn_DeleteEntity);
            this.Controls.Add(this.btn_EditEntity);
            this.Controls.Add(this.btn_AddEntity);
            this.MinimumSize = new System.Drawing.Size(300, 190);
            this.Name = "Form_EntityMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entity Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_EntityMenu_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DeleteEntity;
        private System.Windows.Forms.Button btn_EditEntity;
        private System.Windows.Forms.Button btn_AddEntity;
    }
}