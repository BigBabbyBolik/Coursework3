using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kursach.ConnectionDB;

namespace Kursach.Forms
{
    public partial class FormAddProductInOrder : Form
    {
        Query controller;

        public FormAddProductInOrder()
        {
            InitializeComponent();
        }

        private void FormAddProductInOrder_Load(object sender, EventArgs e)
        {
            controller = new Query(ConnectionString.ConnectionStr);
            dataGridView1.DataSource = controller.UpgrateManager();

            controller.ComboProduct(comboBox1);
            controller.ComboOrder(comboBox2);
        }

        private void ResetTB()
        {
            textBox1.Text = string.Empty;
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                controller.AddManager(comboBox1.Text, Convert.ToInt32(comboBox2.Text), Convert.ToInt32(textBox1.Text));
            }
            catch(Exception)
            {
                MessageBox.Show("Заполните все поля правильно!");
            }

            ResetTB();

            dataGridView1.DataSource = controller.UpgrateManager();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            controller.DeleteManager(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_orderproduct"].Value.ToString()));

            ResetTB();

            dataGridView1.DataSource = controller.UpgrateManager();
        }
    }
}
