﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form_ItemMenu : Form
    {
        // Vars

        // Constructors
        public Form_ItemMenu()
        {
            InitializeComponent();

        }

        // Events
        private void btn_AddItem_Click(object sender, EventArgs e)
        {
            ConfigEditor.frmItemAdd = new Form_ItemAdd();
            ConfigEditor.frmItemAdd.Show();
            ConfigEditor.frmItemMenu.Hide();
        }
        private void btn_EditItem_Click(object sender, EventArgs e)
        {
            ConfigEditor.frmItemEdit = new Form_ItemEdit();
            ConfigEditor.frmItemEdit.Show();
            ConfigEditor.frmItemMenu.Hide();
        }
        private void btn_DeleteItem_Click(object sender, EventArgs e)
        {
            ConfigEditor.frmItemDelete = new Form_ItemDelete();
            ConfigEditor.frmItemDelete.Show();
            ConfigEditor.frmItemMenu.Hide();
        }

        private void Form_ItemMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigEditor.frmMainMenu.Show();
        }
    }
}