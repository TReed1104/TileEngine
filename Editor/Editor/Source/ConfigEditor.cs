﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Editor
{
    public static class ConfigEditor
    {
        public const string itemConfigDirectoryPath = "../../../../Engine/TileEngine/Content/Items/";
        public const string entityConfigDirectoryPath = "../../../../Engine/TileEngine/Content/Entities/";
        public const string itemSaveDataDirectoryPath = "../../../../Engine/TileEngine/bin/Windows/x86/Debug/Content/SaveData/";
        public const string textureDirectoryPath = @"\Engine\TileEngine\Content\Textures";

        private static string[] ListOfItemConfigs_Raw { get; set; }
        private static string[] ListOfEntityConfigs_Raw { get; set; }
        public static List<string> ListOfItemConfigs { get; set; }
        public static List<string> ListOfEntityConfigs { get; set; }
        public static List<string> ListOfEntityTypes { get; set; }
        public static List<string> ListOfColours { get; set; }

        // Forms
        public static Form_MainMenu frmMainMenu { get; set; }
        public static Form_ItemMenu frmItemMenu { get; set; }
        public static Form_ItemAdd frmItemAdd { get; set; }
        public static Form_ItemEdit frmItemEdit { get; set; }
        public static Form_ItemDelete frmItemDelete { get; set; }
        public static Form_EntityMenu frmEntityMenu { get; set; }
        public static Form_EntityAdd frmEntityAdd { get; set; }
        public static Form_EntityEdit frmEntityEdit { get; set; }
        public static Form_EntityDelete frmEntityDelete { get; set; }

        static ConfigEditor()
        {
            // Initialise the lists top prevent errors.
            ListOfItemConfigs = new List<string>();
            ListOfEntityTypes = new List<string>();
            ListOfEntityConfigs = new List<string>();
            ListOfColours = new List<string>();

            // Main Forms
            frmMainMenu = new Form_MainMenu();

            // Item Forms
            frmItemMenu = new Form_ItemMenu();
            frmItemAdd = new Form_ItemAdd();
            frmItemEdit = new Form_ItemEdit();
            frmItemDelete = new Form_ItemDelete();

            // Entity Forms
            frmEntityMenu = new Form_EntityMenu();
            frmEntityAdd = new Form_EntityAdd();
            frmEntityEdit = new Form_EntityEdit();
            frmEntityDelete = new Form_EntityDelete();

        }
        public static void Load()
        {
            ConfigEditor.LoadItemConfigs();
            ConfigEditor.LoadEntityTypes();
            ConfigEditor.LoadEntityConfigs();
            ConfigEditor.LoadColours();
        }
        public static void LoadItemConfigs()
        {
            ListOfItemConfigs_Raw = Directory.GetFiles(itemConfigDirectoryPath);
            ListOfItemConfigs = new List<string>();

            string[] trimmedList = new string[ListOfItemConfigs_Raw.Length];
            for (int i = 0; i < ListOfItemConfigs_Raw.Length; i++)
            {
                string[] splitList = ListOfItemConfigs_Raw[i].Split('/');
                ListOfItemConfigs.Add(splitList[8]);
            }
        }
        public static void LoadEntityConfigs()
        {
            ListOfEntityConfigs_Raw = Directory.GetFiles(entityConfigDirectoryPath);
            ListOfEntityConfigs = new List<string>();

            string[] trimmedList = new string[ListOfEntityConfigs_Raw.Length];
            for (int i = 0; i < ListOfEntityConfigs_Raw.Length; i++)
            {
                string[] splitList = ListOfEntityConfigs_Raw[i].Split('/');
                ListOfEntityConfigs.Add(splitList[8]);
            }
        }
        public static void LoadEntityTypes()
        {
            ListOfEntityTypes = new List<string>();

            // Read the config file.
            XmlReader xmlReader = XmlReader.Create("Content/EntityTypes.conf");
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    if (xmlReader.Name == "entity_type")
                    {
                        ListOfEntityTypes.Add(xmlReader.GetAttribute("value"));
                    }
                }
            }
            xmlReader.Close();
        }
        public static void LoadColours()
        {
            ListOfColours = new List<string>();

            // Read the config file.
            XmlReader xmlReader = XmlReader.Create("Content/Colours.conf");
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    if (xmlReader.Name == "colour")
                    {
                        ListOfColours.Add(xmlReader.GetAttribute("value"));
                    }
                }
            }
            xmlReader.Close();
        }
        
    }
}
