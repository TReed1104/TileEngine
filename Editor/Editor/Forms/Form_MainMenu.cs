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
    public partial class Form_MainMenu : Form
    {
        public Form_MainMenu()
        {
            InitializeComponent();
            ConfigEditor.Load();
        }

        private void btn_ItemEditor_Click(object sender, EventArgs e)
        {
            ConfigEditor.frmItemMenu.Show();
            ConfigEditor.frmMainMenu.Hide();
        }

        private void btn_EntityEditor_Click(object sender, EventArgs e)
        {
            ConfigEditor.frmEntityMenu.Show();
            ConfigEditor.frmMainMenu.Hide();
        }
    }
}
