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
    public partial class FormAddOrder : Form
    {
        Query controller;

        public FormAddOrder()
        {
            InitializeComponent();
        } 

        private void ResetTB()
        {
            textBox1.Text = string.Empty;
        }
        
        private void FormAddOrder_Load(object sender, EventArgs e)
        {
            controller = new Query(ConnectionString.ConnectionStr);
            dataGridView1.DataSource = controller.UpgateManager2();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                controller.AddManager2(Convert.ToInt32(textBox1.Text));
            }
            catch(Exception)
            {
                MessageBox.Show("Заполните все поля правильно!");
            }

            ResetTB();

            dataGridView1.DataSource = controller.UpgateManager2();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            controller.DeleteManager2(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_order"].Value.ToString()));

            ResetTB();

            dataGridView1.DataSource = controller.UpgateManager2();
        }
    }
}
