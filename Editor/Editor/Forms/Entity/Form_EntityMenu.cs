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
        public Form_EntityMenu()
        {
            InitializeComponent();
        }

        private void frm_EntityEditor_Load(object sender, EventArgs e)
        {

        }

        private void Form_EntityMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmMainMenu.Show();
        }
    }
}
