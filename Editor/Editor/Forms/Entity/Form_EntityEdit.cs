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
    public partial class Form_EntityEdit : Form
    {
        // Vars
        private string loaded_EntityTag { get; set; }
        private string loaded_EntityType { get; set; }
        private string loaded_TextureTag { get; set; }
        private int loaded_SrcFrameSizeX { get; set; }
        private int loaded_SrcFrameSizeY { get; set; }
        private string loaded_Colour { get; set; }
        private string loaded_SaveData { get; set; }

        // Constructors
        public Form_EntityEdit()
        {
            InitializeComponent();
        }

        // Methods
        public void LoadEntity(string configFileName)
        {
            // Read the config file.
            XmlReader xmlReader = XmlReader.Create(ConfigEditor.entityConfigDirectoryPath + configFileName);
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    if (xmlReader.Name == "tag")
                    {
                        loaded_EntityTag = xmlReader.GetAttribute("value");
                    }
                    if (xmlReader.Name == "entity_type")
                    {
                        loaded_EntityType = xmlReader.GetAttribute("value");
                    }
                    if (xmlReader.Name == "texture_tag")
                    {
                        loaded_TextureTag = xmlReader.GetAttribute("value");
                    }
                    if (xmlReader.Name == "src_frame_size_x")
                    {
                        loaded_SrcFrameSizeX = int.Parse(xmlReader.GetAttribute("value"));
                    }
                    if (xmlReader.Name == "src_frame_size_y")
                    {
                        loaded_SrcFrameSizeY = int.Parse(xmlReader.GetAttribute("value"));
                    }
                    if (xmlReader.Name == "colour")
                    {
                        loaded_Colour = xmlReader.GetAttribute("value");
                    }
                    if (xmlReader.Name == "save_data")
                    {
                        loaded_SaveData = xmlReader.GetAttribute("value");
                    }
                }
            }
            xmlReader.Close();
        }
        public void SaveEntity(string configFileName)
        {
            bool isAttributeChanged_EntityTag = (loaded_EntityTag != txt_EntityTag.Text);
            bool isAttributeChanged_EntityType = (loaded_EntityType != cbo_EntityType.Items[cbo_EntityType.SelectedIndex] as string);
            bool isAttributeChanged_TextureTag = (loaded_TextureTag != txt_TextureTag.Text);
            bool isAttributeChanged_SrcFrameX = (loaded_SrcFrameSizeX != int.Parse(txt_SrcFrameX.Text));
            bool isAttributeChanged_SrcFrameY = (loaded_SrcFrameSizeY != int.Parse(txt_SrcFrameY.Text));
            bool isAttributeChanged_Colour = (loaded_Colour != cbo_Colours.Items[cbo_Colours.SelectedIndex] as string);
            bool isAttributeChanged_SaveData = (loaded_SaveData != txt_SaveData.Text);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;          // prevents strange formatting, but then needs new whitespaces.

            // Loads the XML file.
            xmlDocument.Load(ConfigEditor.entityConfigDirectoryPath + configFileName);


            XmlNode xmlNode_Root = xmlDocument.SelectSingleNode("entity_config");
            if (isAttributeChanged_EntityTag)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("tag");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_EntityTag.Text.ToString();
            }
            if (isAttributeChanged_EntityType)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("entity_type");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = (cbo_EntityType.Items[cbo_EntityType.SelectedIndex] as string);
            }
            if (isAttributeChanged_TextureTag)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("texture_tag");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_TextureTag.Text.ToString();
            }
            if (isAttributeChanged_SrcFrameX)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("src_frame_size_x");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_SrcFrameX.Text.ToString();
            }
            if (isAttributeChanged_SrcFrameY)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("src_frame_size_y");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_SrcFrameY.Text.ToString();
            }
            if (isAttributeChanged_Colour)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("colour");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = cbo_Colours.Items[cbo_Colours.SelectedIndex].ToString();
            }
            if (isAttributeChanged_SaveData)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("save_data");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_SaveData.Text.ToString();
            }

            // Saves the XML file.
            if (!isAttributeChanged_EntityTag)
            {
                xmlDocument.Save(ConfigEditor.entityConfigDirectoryPath + configFileName);
            }
            else
            {
                // Checks if the renamed Item already exists.
                string newFileName = txt_EntityTag.Text.Replace(" ", "");
                if (!File.Exists(ConfigEditor.entityConfigDirectoryPath + newFileName + ".conf"))
                {
                    xmlDocument.Save(ConfigEditor.entityConfigDirectoryPath + newFileName + ".conf");
                    File.Delete(ConfigEditor.entityConfigDirectoryPath + configFileName);
                }
                else
                {
                    Console.WriteLine("A Entity Config with that name already exists, try again.");
                }
            }
        }

        // Events
        private void Form_EntityEdit_Load(object sender, EventArgs e)
        {
            ConfigEditor.LoadEntityConfigs();
            lst_EntityConfigs.DataSource = ConfigEditor.ListOfEntityConfigs;
            cbo_EntityType.DataSource = ConfigEditor.ListOfEntityTypes;
            cbo_Colours.DataSource = ConfigEditor.ListOfColours;

            // Load the Entities
            LoadEntity(lst_EntityConfigs.Items[lst_EntityConfigs.SelectedIndex] as string);

            // Display the Items attributes.
            txt_EntityTag.Text = loaded_EntityTag;
            cbo_EntityType.SelectedIndex = cbo_EntityType.FindStringExact(loaded_EntityType);
            txt_TextureTag.Text = loaded_TextureTag;
            txt_SrcFrameX.Text = loaded_SrcFrameSizeX.ToString();
            txt_SrcFrameY.Text = loaded_SrcFrameSizeY.ToString();
            cbo_Colours.SelectedIndex = cbo_Colours.FindStringExact(loaded_Colour);
            txt_SaveData.Text = loaded_SaveData;
        }
        private void Form_EntityEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmEntityMenu.Show();
        }
        private void lst_EntityConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEntity(lst_EntityConfigs.Items[lst_EntityConfigs.SelectedIndex] as string);

            // Display the Items attributes.
            txt_EntityTag.Text = loaded_EntityTag;
            cbo_EntityType.SelectedIndex = cbo_EntityType.FindStringExact(loaded_EntityType);
            txt_TextureTag.Text = loaded_TextureTag;
            txt_SrcFrameX.Text = loaded_SrcFrameSizeX.ToString();
            txt_SrcFrameY.Text = loaded_SrcFrameSizeY.ToString();
            cbo_Colours.SelectedIndex = cbo_Colours.FindStringExact(loaded_Colour);
            txt_SaveData.Text = loaded_SaveData;

        }
        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            SaveEntity(lst_EntityConfigs.Items[lst_EntityConfigs.SelectedIndex] as string);
            ConfigEditor.LoadEntityConfigs();
            lst_EntityConfigs.DataSource = ConfigEditor.ListOfEntityConfigs;
            LoadEntity(lst_EntityConfigs.Items[lst_EntityConfigs.SelectedIndex] as string);
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
