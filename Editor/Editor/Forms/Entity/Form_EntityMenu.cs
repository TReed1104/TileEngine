using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form_EntityMenu : Form
    {
        // Vars

        // Constructors
        public Form_EntityMenu()
        {
            InitializeComponent();
        }

        // Methods
        private void Form_EntityMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmMainMenu.Show();
        }
        private void btn_AddEntity_Click(object sender, EventArgs e)
        {
            ConfigEditor.frmEntityAdd = new Form_EntityAdd();
            ConfigEditor.frmEntityAdd.Show();
            ConfigEditor.frmEntityMenu.Hide();
        }
        private void btn_EditEntity_Click(object sender, EventArgs e)
        {
            ConfigEditor.frmEntityEdit = new Form_EntityEdit();
            ConfigEditor.frmEntityEdit.Show();
            ConfigEditor.frmEntityMenu.Hide();
        }
        private void btn_DeleteEntity_Click(object sender, EventArgs e)
        {
            ConfigEditor.frmEntityDelete = new Form_EntityDelete();
            ConfigEditor.frmEntityDelete.Show();
            ConfigEditor.frmEntityMenu.Hide();
        }
    }
}
