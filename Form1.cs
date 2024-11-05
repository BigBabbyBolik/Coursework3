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


namespace Kursach
{
    public partial class Form1 : Form
    {
        Query controller;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            controller = new Query(ConnectionString.ConnectionStr);

            string post = controller.Autorization(textBoxLogin.Text, textBoxPassword.Text);

            if(post == "менеджер")
            {
                Form2 form2 = new Form2();
                form2.Show();
                this.Close();
            }
            else if(post =="админ")
            {
                Form3 form3 = new Form3();
                form3.Show();
                this.Close();
            }
            else if(post == "директор")
            {
                Form4 form4 = new Form4(); 
                form4.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }
        }

       
    }
}
