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
    public partial class Form_EntityDelete : Form
    {
        // Vars
        private string entityToDelete { get; set; }

        // Cosntructors
        public Form_EntityDelete()
        {
            InitializeComponent();
            entityToDelete = "";
        }

        // Methods

        // Events
        private void Form_EntityDelete_Load(object sender, EventArgs e)
        {
            ConfigEditor.LoadEntityConfigs();
            cbo_DeleteEntity.DataSource = ConfigEditor.ListOfEntityConfigs;
        }
        private void Form_EntityDelete_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmEntityMenu.Show();
        }
        private void cbo_DeleteEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            entityToDelete = (cbo_DeleteEntity.Items[cbo_DeleteEntity.SelectedIndex] as string);
        }
        private void btn_DeleteEntity_Click(object sender, EventArgs e)
        {
            File.Delete(ConfigEditor.entityConfigDirectoryPath + entityToDelete);
            ConfigEditor.LoadEntityConfigs();
            cbo_DeleteEntity.DataSource = ConfigEditor.ListOfEntityConfigs;
            this.Close();
        }
    }
}
