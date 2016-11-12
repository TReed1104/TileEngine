using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Editor
{
    public partial class Form_EntityAdd : Form
    {
        // Vars

        // Constructors
        public Form_EntityAdd()
        {
            InitializeComponent();
        }

        // Methods

        // Events
        private void btn_AddNewEntity_Click(object sender, EventArgs e)
        {
            string entityName = txt_EntityTag.Text;
            using (XmlWriter xmlWriter = XmlWriter.Create(ConfigEditor.entityConfigDirectoryPath + entityName + ".conf"))
            {
                #region // Write a default save file
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteStartElement("entity_config");
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("tag");
                xmlWriter.WriteAttributeString("value", txt_EntityTag.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("entity_type");
                xmlWriter.WriteAttributeString("value", txt_EntityType.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("texture_tag");
                xmlWriter.WriteAttributeString("value", txt_TextureTag.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("src_frame_size_x");
                xmlWriter.WriteAttributeString("value", txt_SrcFrameX.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("src_frame_size_y");
                xmlWriter.WriteAttributeString("value", txt_SrcFrameY.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("colour");
                xmlWriter.WriteAttributeString("value", txt_Colour.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("save_data");
                string saveName = "";
                if (txt_EntityType.Text == "Player")
                {
                    saveName = txt_EntityTag.Text + ".sav";
                }
                xmlWriter.WriteAttributeString("value", saveName);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
                #endregion
            }
            this.Close();
        }
        private void Form_EntityAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmEntityMenu.Show();
        }
        private void Form_EntityAdd_Load(object sender, EventArgs e)
        {
            ConfigEditor.LoadEntityConfigs();
        }
    }
}
