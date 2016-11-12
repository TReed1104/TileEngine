using System;
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

        private static string[] ListOfItemConfigs_Raw { get; set; }
        private static string[] ListOfEntityConfigs_Raw { get; set; }
        public static List<string> ListOfItemConfigs { get; set; }
        public static List<string> ListOfEntityConfigs { get; set; }

        // Forms
        public static Form_MainMenu frmMainMenu { get; set; }
        public static Form_ItemMenu frmItemMenu { get; set; }
        public static Form_ItemAdd frmItemAdd { get; set; }
        public static Form_ItemEdit frmItemEdit { get; set; }
        public static Form_ItemDelete frmItemDelete { get; set; }
        public static Form_EntityMenu frmEntityMenu { get; set; }

        static ConfigEditor()
        {
            // Main Forms
            frmMainMenu = new Form_MainMenu();

            // Item Forms
            frmItemMenu = new Form_ItemMenu();
            frmItemAdd = new Form_ItemAdd();
            frmItemEdit = new Form_ItemEdit();
            frmItemDelete = new Form_ItemDelete();

            // Entity Forms
            frmEntityMenu = new Form_EntityMenu();
        }
        public static void Load()
        {
            ConfigEditor.LoadItemConfigs();
            ConfigEditor.LoadEntityConfigs();
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
    }
}
