using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Turnero
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;password=");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            MySqlCommand Cmd = new MySqlCommand();
            Cmd = new MySqlCommand("SELECT *FROM people WHERE Usuario='" + UserText.Text + "' AND Contrasena='" + PasswordText.Text + "' ", connection); //Sistema de usuarios
            MySqlDataReader leer = Cmd.ExecuteReader();
            if (leer.Read())
            {
               // MessageBox.Show("Funciona");
                
                this.Visible = false;
                Form2 f2 = new Form2();
                f2.ShowDialog();
                Form1 f1 = new Form1();
                f1.Hide();

            }
            else
                MessageBox.Show("Error");

            connection.Close();
            
        }
    }
}
