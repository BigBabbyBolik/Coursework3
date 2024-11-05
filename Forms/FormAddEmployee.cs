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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kursach.Forms
{
    public partial class FormAddEmployee : Form
    {
        Query controller;

        int id;

        public FormAddEmployee()
        {
            InitializeComponent();
        }
        private void FormAddEmployee_Load(object sender, EventArgs e)
        {
            controller = new Query(ConnectionString.ConnectionStr);
            dataGridView1.DataSource = controller.Upgrate();

            controller.ComboBoxPost(comboBox2);
        }

        private void ResetTB()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            comboBox2.Text = string.Empty;
        }
        private void buttonadd_Click(object sender, EventArgs e)
        {
            int res = 0;
            if(comboBox2.Text == "менеджер")
            {
                res = 2;
            }
            else if(comboBox2.Text == "админ")
            {
                res = 1;
            }
            else if(comboBox2.Text == "директор")
            {
                res = 3;
            }
            else if(comboBox2.Text == null)
            {
                MessageBox.Show("Поле должность пустое!");
            }


            try
            {
                controller.Add(textBox3.Text, textBox2.Text, textBox1.Text, Convert.ToInt32(textBox4.Text), textBox5.Text, textBox6.Text, textBox7.Text, res);
            }
            catch(Exception)
            {
                MessageBox.Show("Заполните все поля правильно!");
            }

            ResetTB();

            dataGridView1.DataSource = controller.Upgrate();
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            controller.Delete(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_user"].Value.ToString()));
            ResetTB();

            dataGridView1.DataSource = controller.Upgrate();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                //id = dataGridView1.CurrentCell.RowIndex;
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox1.Text = row.Cells[2].Value.ToString();
                textBox2.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
                textBox6.Text = row.Cells[6].Value.ToString();
                textBox7.Text = row.Cells[7].Value.ToString();
                comboBox2.Text = row.Cells[8].Value.ToString();
            }
        }

        private void buttonchange_Click(object sender, EventArgs e)
        {
            int res = 0;
            if (comboBox2.Text == "менеджер")
            {
                res = 2;
            }
            else if (comboBox2.Text == "админ")
            {
                res = 1;
            }
            else if (comboBox2.Text == "директор")
            {
                res = 3;
            }

            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_user"].Value);

            try
            {
                controller.Change(id, textBox3.Text, textBox2.Text, textBox1.Text, Convert.ToInt32(textBox4.Text), textBox5.Text, textBox6.Text, textBox7.Text, res);
            }
            catch(Exception)
            {
                MessageBox.Show("Заполните все поля правильно!");
            }

            dataGridView1.DataSource = controller.Upgrate();

            ResetTB();
        }
    }
}
