﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kursach.ConnectionDB;

namespace Kursach.Forms
{
    public partial class FormReports : Form
    {
        Query controller;

        public FormReports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller = new Query(ConnectionString.ConnectionStr);
            dataGridView1.DataSource = controller.Upgrate();
        }
    }
}
