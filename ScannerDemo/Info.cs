﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScannerDemo
{
    public partial class InfoDialog : Form
    {
        public InfoDialog()
        {
            InitializeComponent();
            btnOk.Visible = false;
        }

        public void InfoText(string text)
        {
            info_text.Text = text;
        }

        public void showInfoDialog(Boolean showButton)
        {
            btnOk.Visible = showButton;
            this.ShowDialog();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(btnOk.Visible)
            { this.Hide(); }

        }
    }
}
