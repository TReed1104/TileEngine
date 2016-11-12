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
    public partial class Form_ItemEdit : Form
    {        
        // Vars
        private const int minimumDurability = 0;

        public string loaded_ItemTag { get; set; }
        private string loaded_TextureTag { get; set; }
        private int loaded_BaseDurability { get; set; }
        private int loaded_BuyValue { get; set; }
        private int loaded_SellValue { get; set; }

        // Constructors
        public Form_ItemEdit()
        {
            InitializeComponent();

            this.loaded_ItemTag = "NULL";
            this.loaded_TextureTag = "NULL";
            this.loaded_BaseDurability = -1;
            this.loaded_BuyValue = -1;
            this.loaded_SellValue = -1;
        }

        // Methods
        public void LoadItem(string configFileName)
        {
            // Read the config file.
            XmlReader xmlReader = XmlReader.Create(ConfigEditor.itemConfigDirectoryPath + configFileName);
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    if (xmlReader.Name == "tag")
                    {
                        loaded_ItemTag = xmlReader.GetAttribute("value");
                    }
                    if (xmlReader.Name == "texture_tag")
                    {
                        loaded_TextureTag = xmlReader.GetAttribute("value");
                    }
                    if (xmlReader.Name == "durability_base")
                    {
                        loaded_BaseDurability = int.Parse(xmlReader.GetAttribute("value"));
                    }
                    if (xmlReader.Name == "value_buy")
                    {
                        loaded_BuyValue = int.Parse(xmlReader.GetAttribute("value"));
                    }
                    if (xmlReader.Name == "value_sell")
                    {
                        loaded_SellValue = int.Parse(xmlReader.GetAttribute("value"));
                    }
                }
            }
            xmlReader.Close();
        }
        public void SaveItem(string configFileName)
        {
            bool isAttributeChanged_ItemTag = (loaded_ItemTag != txt_ItemTag.Text);
            bool isAttributeChanged_TextureTag = (loaded_TextureTag != txt_TextureTag.Text);
            bool isAttributeChanged_BaseDurability = (loaded_BaseDurability != int.Parse(txt_BaseDurability.Text));
            bool isAttributeChanged_BuyValue = (loaded_BuyValue != int.Parse(txt_BuyValue.Text));
            bool isAttributeChanged_SellValue = (loaded_SellValue != int.Parse(txt_SellValue.Text));

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;          // prevents strange formatting, but then needs new whitespaces.

            // Loads the XML file.
            xmlDocument.Load(ConfigEditor.itemConfigDirectoryPath + configFileName);
            

            XmlNode xmlNode_Root = xmlDocument.SelectSingleNode("item_config");
            if (isAttributeChanged_ItemTag)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("tag");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_ItemTag.Text.ToString();
            }
            if (isAttributeChanged_TextureTag)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("texture_tag");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_TextureTag.Text.ToString();
            }
            if (isAttributeChanged_BaseDurability)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("durability_base");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_BaseDurability.Text.ToString();
            }
            if (isAttributeChanged_BuyValue)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("value_buy");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_BuyValue.Text.ToString();
            }
            if (isAttributeChanged_SellValue)
            {
                XmlNode xmlNode_ToEdit = xmlNode_Root.SelectSingleNode("value_sell");
                XmlAttribute xmlAttribute_ToEdit = xmlNode_ToEdit.Attributes["value"];
                xmlAttribute_ToEdit.Value = txt_SellValue.Text.ToString();
            }

            // Saves the XML file.
            if (!isAttributeChanged_ItemTag)
            {
                xmlDocument.Save(ConfigEditor.itemConfigDirectoryPath + configFileName);
            }
            else
            {
                // Checks if the renamed Item already exists.
                string newFileName = txt_ItemTag.Text.Replace(" ", "");
                if (!File.Exists(ConfigEditor.itemConfigDirectoryPath + newFileName + ".conf"))
                {
                    xmlDocument.Save(ConfigEditor.itemConfigDirectoryPath + newFileName + ".conf");
                    File.Delete(ConfigEditor.itemConfigDirectoryPath + configFileName);
                }
                else
                {
                    Console.WriteLine("A Item Config with that name already exists, try again.");
                }
            }

        }
        // Events
        private void Form_ItemEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmItemMenu.Show();
        }
        private void frm_ItemEditor_Load(object sender, EventArgs e)
        {
            ConfigEditor.LoadItemConfigs();
            lst_ItemConfigs.DataSource = ConfigEditor.ListOfItemConfigs;
        }
        private void lst_ItemConfigs_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadItem(lst_ItemConfigs.Items[lst_ItemConfigs.SelectedIndex] as string);

            // Display the Items attributes.
            txt_ItemTag.Text = loaded_ItemTag;
            txt_TextureTag.Text = loaded_TextureTag;
            txt_BaseDurability.Text = loaded_BaseDurability.ToString();
            txt_BuyValue.Text = loaded_BuyValue.ToString();
            txt_SellValue.Text = loaded_SellValue.ToString();
        }
        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            SaveItem(lst_ItemConfigs.Items[lst_ItemConfigs.SelectedIndex] as string);
            ConfigEditor.LoadItemConfigs();
            lst_ItemConfigs.DataSource = ConfigEditor.ListOfItemConfigs;
            LoadItem(lst_ItemConfigs.Items[lst_ItemConfigs.SelectedIndex] as string);
        }
    }
}
