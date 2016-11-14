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
                string entityType = (cbo_EntityType.Items[cbo_EntityType.SelectedIndex] as string);
                xmlWriter.WriteAttributeString("value", entityType);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("texture_tag");
                xmlWriter.WriteAttributeString("value", txt_TextureTag.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("src_frame_size");
                xmlWriter.WriteAttributeString("x", txt_SrcFrameX.Text);
                xmlWriter.WriteAttributeString("y", txt_SrcFrameY.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("colour");
                string colour = (cbo_Colours.Items[cbo_Colours.SelectedIndex] as string);
                xmlWriter.WriteAttributeString("value", colour);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("save_data");
                string saveName = "";
                if (entityType == "Player")
                {
                    saveName = txt_EntityTag.Text + ".sav";
                }
                xmlWriter.WriteAttributeString("value", saveName);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");


                xmlWriter.WriteStartElement("base_health");
                xmlWriter.WriteAttributeString("value", txt_health.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("base_damage");
                xmlWriter.WriteAttributeString("value", txt_damage.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteElementString("animations", "");

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
            cbo_EntityType.DataSource = ConfigEditor.ListOfEntityTypes;
            cbo_Colours.DataSource = ConfigEditor.ListOfColours;
        }

        private void btn_TexturePicker_Click(object sender, EventArgs e)
        {
            OpenFileDialog filePickerDialog = new OpenFileDialog();
            string basePath = Application.StartupPath;
            string trimmedPath = basePath.Replace(@"\Editor\Editor\bin\Debug", "");


            string textureDirectoryPath = trimmedPath + ConfigEditor.textureDirectoryPath;

            filePickerDialog.InitialDirectory = textureDirectoryPath;
            if (filePickerDialog.ShowDialog() == DialogResult.OK)
            {
                string rawFilePath = filePickerDialog.FileName;
                string[] splitFilePath = rawFilePath.Split('\\');
                string fullFileName = splitFilePath[splitFilePath.Length - 1];
                string[] fileNameSplit = fullFileName.Split('.');
                string finalTextureTag = fileNameSplit[0];
                txt_TextureTag.Text = finalTextureTag;
            }
        }
    }
}
