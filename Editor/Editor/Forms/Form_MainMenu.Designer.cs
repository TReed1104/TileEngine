namespace Editor
{
    partial class Form_MainMenu
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
            this.btnItemEditor = new System.Windows.Forms.Button();
            this.btnEntityEditor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnItemEditor
            // 
            this.btnItemEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnItemEditor.Location = new System.Drawing.Point(0, 0);
            this.btnItemEditor.Name = "btnItemEditor";
            this.btnItemEditor.Size = new System.Drawing.Size(284, 50);
            this.btnItemEditor.TabIndex = 0;
            this.btnItemEditor.Text = "Item Editor";
            this.btnItemEditor.UseVisualStyleBackColor = true;
            this.btnItemEditor.Click += new System.EventHandler(this.btn_ItemEditor_Click);
            // 
            // btnEntityEditor
            // 
            this.btnEntityEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEntityEditor.Location = new System.Drawing.Point(0, 50);
            this.btnEntityEditor.Name = "btnEntityEditor";
            this.btnEntityEditor.Size = new System.Drawing.Size(284, 50);
            this.btnEntityEditor.TabIndex = 3;
            this.btnEntityEditor.Text = "Entity Editor";
            this.btnEntityEditor.UseVisualStyleBackColor = true;
            this.btnEntityEditor.Click += new System.EventHandler(this.btn_EntityEditor_Click);
            // 
            // frm_MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.btnEntityEditor);
            this.Controls.Add(this.btnItemEditor);
            this.MinimumSize = new System.Drawing.Size(300, 140);
            this.Name = "frm_MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Engine Config Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnItemEditor;
        private System.Windows.Forms.Button btnEntityEditor;
    }
}

