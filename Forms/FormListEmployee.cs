using System;
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
    public partial class FormListEmployee : Form
    {
        Query controller;

        public FormListEmployee()
        {
            InitializeComponent();
        }

        private void FormListEmployee_Load(object sender, EventArgs e)
        {
            controller = new Query(ConnectionString.ConnectionStr);
            dataGridView1.DataSource = controller.Upgrate();
        }
    }
}
