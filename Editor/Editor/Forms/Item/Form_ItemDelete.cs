using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form_ItemDelete : Form
    {
        // Vars
        private string itemToDelete { get; set; }

        // Constructors
        public Form_ItemDelete()
        {
            InitializeComponent();
            itemToDelete = "";
        }

        // Methods

        // Events
        private void Form_ItemDelete_Load(object sender, EventArgs e)
        {
            ConfigEditor.LoadItemConfigs();
            cbo_DeleteItem.DataSource = ConfigEditor.ListOfItemConfigs;
        }
        private void Form_ItemDelete_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmItemMenu.Show();
        }
        private void cbo_DeleteItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemToDelete = (cbo_DeleteItem.Items[cbo_DeleteItem.SelectedIndex] as string);
        }
        private void btn_DeleteItem_Click(object sender, EventArgs e)
        {
            File.Delete(ConfigEditor.itemConfigDirectoryPath + itemToDelete);
            ConfigEditor.LoadItemConfigs();
            cbo_DeleteItem.DataSource = ConfigEditor.ListOfItemConfigs;
            this.Close();
        }
    }
}
