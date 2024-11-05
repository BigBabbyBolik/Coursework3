using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Reflection;


namespace Kursach.ConnectionDB
{
    internal class Query
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataAdapter adapter;
        DataTable bufferTable;

        public Query(string conn)
        {
            connection = new OleDbConnection(conn);
            bufferTable = new DataTable();
        }

        public string Autorization(string user_login, string user_password)
        {
            connection.Open();

            command = new OleDbCommand("SELECT loginandpassword.login, loginandpassword.password, post.post\r" +
                    "FROM post INNER JOIN(users INNER JOIN loginandpassword ON users.id_user = loginandpassword.id_user) ON post.id_post = users.post\r" +
                    "WHERE (((loginandpassword.login)=[@user_login]) AND ((loginandpassword.password)=[@user_password]));", connection);

            command.Parameters.AddWithValue("@user_login", user_login);
            command.Parameters.AddWithValue("@user_password", user_password);

            OleDbDataReader reader = command.ExecuteReader();

            List<string> post = new List<string>();

            while (reader.Read())
            {
                post.Add(reader.GetString(2));
            }

            //connection.Close();

            if (post.Count() == 0)
            {
                return null;
            }
            else
            {
                return post[0];
            }
        }

        public void Change(int id, string first_name, string last_name, string sur_name, int age, string address, string email, string phone, int post)
        {
            connection.Open();

            command = new OleDbCommand($"UPDATE users set fname = '{first_name}', " +
                $"sname = '{sur_name}',lname = '{last_name}', age = '{age}', " +
                $"address = '{address}', email = '{email}', phone = '{phone}', " +
                $" post = '{post}'" +
                $"WHERE id_user = id", connection);

            command.Parameters.AddWithValue("@id_user", id);
            command.Parameters.AddWithValue("@fname", first_name);
            command.Parameters.AddWithValue("@lname", last_name);
            command.Parameters.AddWithValue("@sname", sur_name);
            command.Parameters.AddWithValue("@age", age);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@post", post);

            command.ExecuteNonQuery();

            connection.Close();
        }


        public DataTable Upgrate()
        {
            //connection.Open();
            adapter = new OleDbDataAdapter("SELECT users.id_user, users.fname, users.sname, users.lname, users.age, users.address, users.email, users.phone, post.post\r\n" +
                "FROM post INNER JOIN users ON post.id_post = users.post;", connection);
            bufferTable.Clear();
            adapter.Fill(bufferTable);
            //connection.Close();
            return bufferTable;
        }

        public DataTable UpgateAdmin()
        {
            adapter = new OleDbDataAdapter("SELECT loginandpassword.id_loginandpassword, users.sname, users.fname, loginandpassword.login, loginandpassword.password, post.post\r\n" +
                "FROM post INNER JOIN (users INNER JOIN loginandpassword ON users.id_user = loginandpassword.id_user) " +
                "ON post.id_post = users.post;", connection);
            bufferTable.Clear();
            adapter.Fill(bufferTable);
            return bufferTable;
        }

        public DataTable UpgrateManager()
        {
            adapter = new OleDbDataAdapter("SELECT orderproduct.id_orderproduct, menu.product_n, orderproduct.id_order, orderproduct.count\r\n" +
                "FROM orders INNER JOIN (menu INNER JOIN orderproduct ON menu.id_menu = orderproduct.product) ON orders.id_order = orderproduct.id_order;", connection);
            bufferTable.Clear();
            adapter.Fill(bufferTable);
            return bufferTable;
        }

        public DataTable UpgateManager2()
        {
            adapter = new OleDbDataAdapter("SELECT * FROM orders;", connection);
            bufferTable.Clear();
            adapter.Fill(bufferTable);
            return bufferTable;
        }


        public void Add(string first_name, string last_name, string sur_name, int age, string address, string email, string phone, int post)
        {
            connection.Open();

            command = new OleDbCommand($"INSERT INTO users(fname, sname, lname, age, address, email, phone, post) VALUES(@first_name, @sur_name, @last_name, @age, @address, @email, @phone, @post)", connection);

            command.Parameters.AddWithValue("@fname", first_name);
            command.Parameters.AddWithValue("@sname", sur_name);
            command.Parameters.AddWithValue("@lname", last_name);
            command.Parameters.AddWithValue("@age", age);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@post", post);

            command.ExecuteNonQuery();

            connection.Close();
        }


        //Надо подумать над запрсом и таблицей в Admin  
        public void AddAdmin(int id, string user_login, string user_password)
        {
            connection.Open();

            //command = new OleDbCommand("SELECT id_user, sname FROM users WHERE users.sname = @sname", connection);

            //command.Parameters.AddWithValue("@sname", sur_name);

            //OleDbDataReader reader = command.ExecuteReader();

            //List<int> list = new List<int>();

            //while (reader.Read())
            //{
            //    list.Add(reader.GetInt32(0));
            //}

            //int id = list[0];

            command = new OleDbCommand("INSERT INTO [loginandpassword]([login], [password], [id_user]) VALUES(@user_login, @user_password, @id)", connection);

            command.Parameters.AddWithValue("@login", user_login);
            command.Parameters.AddWithValue("@password", user_password);
            command.Parameters.AddWithValue("@id_user", id);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public void AddManager(string product, int order, int count)
        {
            connection.Open();

            command = new OleDbCommand("SELECT id_menu, product_n FROM menu WHERE menu.product_n = @product", connection);

            command.Parameters.AddWithValue("@product", product);

            OleDbDataReader reader = command.ExecuteReader();

            List<int> list = new List<int>();

            while (reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }

            int id_product = list[0];

            command = new OleDbCommand("INSERT INTO [orderproduct]([product], [id_order], [count]) VALUES(@id_product, @order, @count)", connection);

            command.Parameters.AddWithValue("@id_product", id_product);
            command.Parameters.AddWithValue("@order", order);
            command.Parameters.AddWithValue("@count", count);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void AddManager2(int table)
        {
            connection.Open();

            command = new OleDbCommand("INSERT INTO [orders]([table]) VALUES(@table)", connection);

            command.Parameters.AddWithValue("@table", table);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteManager2(int order)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM orders WHERE id_order = {order}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteManager(int id_product)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM orderproduct WHERE id_orderproduct = {id_product}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteAdmin(int id_passwordusers)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM loginandpassword WHERE id_loginandpassword = {id_passwordusers}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int id_user)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM users WHERE id_user = {id_user}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ComboBoxPost(ComboBox box)
        {
            OleDbCommand command = new OleDbCommand("SELECT post FROM post", connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                box.Items.Add(reader.GetString(0));
            }
            connection.Close();
        }

        public void ComboBoxFio(ComboBox box)
        {
            OleDbCommand command = new OleDbCommand("SELECT id_user FROM users", connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                box.Items.Add(reader.GetInt32(0));
            }
            connection.Close();
        }

        public void ComboProduct(ComboBox product)
        {
            OleDbCommand command = new OleDbCommand("SELECT product_n FROM menu;", connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                product.Items.Add(reader.GetString(0));
            }
            connection.Close();
        }

        public void ComboOrder(ComboBox order)
        {
            OleDbCommand command = new OleDbCommand("SELECT id_order FROM orders;", connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.Items.Add(reader.GetInt32(0));
            }
            connection.Close();
        }
    }
}
