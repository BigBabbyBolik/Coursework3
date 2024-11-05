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
    public partial class FormLoginAndPassword : Form
    {
        Query controller;

        public FormLoginAndPassword()
        {
            InitializeComponent();
        }

        private void FormLoginAndPassword_Load(object sender, EventArgs e)
        {
            controller = new Query(ConnectionString.ConnectionStr);
            dataGridView1.DataSource = controller.UpgateAdmin();

            controller.ComboBoxFio(comboBox1);
        }

        private void ResetTB()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            comboBox1.Text = string.Empty;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                controller.AddAdmin(Convert.ToInt32(comboBox1.Text), textBox1.Text, textBox2.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("Заполните все поля правильно!");
            }

            ResetTB();

            dataGridView1.DataSource = controller.UpgateAdmin();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            controller.DeleteAdmin(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_loginandpassword"].Value.ToString()));

            ResetTB();

            dataGridView1.DataSource = controller.UpgateAdmin();
        }
    }
}
