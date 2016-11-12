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
    public partial class Form_ItemAdd : Form
    {
        // Vars


        // Constructors
        public Form_ItemAdd()
        {
            InitializeComponent();
        }

        // Methods

        // Events
        private void FormItemAdd_Load(object sender, EventArgs e)
        {
            ConfigEditor.LoadItemConfigs();
        }
        private void Form_ItemAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmItemMenu.Show();
        }
        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            string itemName = txt_ItemTag.Text;
            using (XmlWriter xmlWriter = XmlWriter.Create(ConfigEditor.itemConfigDirectoryPath + itemName + ".conf"))
            {
                #region // Write a default save file
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteStartElement("item_config");
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("tag");
                xmlWriter.WriteAttributeString("value", txt_ItemTag.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("texture_tag");
                xmlWriter.WriteAttributeString("value", txt_TextureTag.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("durability_base");
                xmlWriter.WriteAttributeString("value", txt_BaseDurability.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("value_buy");
                xmlWriter.WriteAttributeString("value", txt_BuyValue.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n\t");

                xmlWriter.WriteStartElement("value_sell");
                xmlWriter.WriteAttributeString("value", txt_SellValue.Text);
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
    }
}
