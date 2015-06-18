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
    public partial class Form2 : Form
    {
        Visitas Visita = new Visitas();
        Paciente paciente = new Paciente();
        MySqlCommand delcmd = new MySqlCommand();
        MySqlConnection con = new MySqlConnection();
        MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;password=");
        
        

        public Form2()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox11.Text = Convert.ToString(DateTime.Now.Hour + ":" + DateTime.Now.Minute +":"+DateTime.Now.Second );
            if (CantPacientes_Text.Text != "")
            {
                label12.Visible = true;
                Nombre_Text.Visible = true;
                label15.Visible = true;
                label13.Visible = true;
                IDEstudio_text.Visible = true;
                label14.Visible = true;
                NombreEstudio_Text.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                label11.Visible = false;
                CantPacientes_Text.Visible = false;
                button1.Visible = false;
                label16.Visible = true;
                button4.Visible = true;
                departamento_text.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                Horallega_text.Visible = true;


                Visita.NumPacientes = Convert.ToInt32(CantPacientes_Text.Text);
                //textBox8.Text = Convert.ToString(Visita.ContPacientes);
                //textBox11.Text = "Probando";/*Convert.ToString(Visita.ContPacientes + 1);*/
                label16.Text = Convert.ToString(Visita.ContPacientes + 1);
            }
            else 
            {
                MessageBox.Show("Campos vacios");
            }
           
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Visita.ContEstudios < 9) ///Condicion que no permite que un paciente realice mas de 10 estudios.
            {
                
                Visita.ContEstudios += 1;

                if (IDEstudio_text.Text != "")
                    paciente.IDestudios += IDEstudio_text.Text + "-";
                IDEstudio_text.Text = "";
                NombreEstudio_Text.Text = "";
                departamento_text.Text = "";

                Organizadorlabel.Visible = true;
                MenorcheckBox.Visible = true;
                MayorcheckBox.Visible = true;
                ///Visualizar opcion de des grade peq.

            }
            else
            {
                MessageBox.Show("Ha introducido la mayor cantidad de estudios posibles por paciente");
                
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 0 || e.KeyChar > 1))
            {
                
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {   
            if (Visita.ContEstudios == 0)
                Visita.Organizar = 0;
            else if (MenorcheckBox.Checked == true)
                Visita.Organizar = 1;
            else if (MayorcheckBox.Checked == true)
                Visita.Organizar = 2;

            ////Chequear qu el nombre del paciente no se repite.
            connection.Open();
            MySqlCommand Cmd = new MySqlCommand();
            Cmd = new MySqlCommand("SELECT *FROM paciente WHERE NombrePaciente='" + Nombre_Text.Text + "' ", connection);
            MySqlDataReader leer = Cmd.ExecuteReader();
            if (leer.Read())
            {
                // MessageBox.Show("Funciona");
                
                
                Visita.UsuarioExis = true;
                MessageBox.Show("Este paciente ya existe");

            }
            connection.Close();


            


            if (MenorcheckBox.Checked == false && MayorcheckBox.Checked == false  && Visita.ContEstudios > 0) 
            {

                Visita.UsuarioExis = true;
                MessageBox.Show("Elija su metodo de ordenamiento");
            }

            //insertando el usuario en la tabla de departamento correspondiente//
            
            if (Visita.ContEstudios == 0)
            {
                paciente.ID_STUDY = IDEstudio_text.Text;
                if (IDEstudio_text.Text != "" && Nombre_Text.Text != "")
                {
                    paciente.Organizar_Matriz();
                }
            }

           if (Horallega_text.Text != "")
           {
                paciente.HORA = Convert.ToDateTime(Horallega_text.Text);
              
            }
            
            

            
            
            //guardando en la base de dato cada estudio realizado por el paciente//
            if (IDEstudio_text.Text != "" && Visita.UsuarioExis==false)
            {
                if (IDEstudio_text.Text != "")
                    paciente.IDestudios += IDEstudio_text.Text + "-";


                /// Para mas de un estudio

                Visita.MoverEstudios = 0;
                
               
                if (Visita.ContEstudios > 0)  // para salvar los id's 
                {
                    paciente.ID_STUDY = paciente.IDestudios;
                    paciente.ID_Estudios_array = new int[Visita.ContEstudios + 1];
                    if (IDEstudio_text.Text != "" && Nombre_Text.Text != "")
                    {
                        for (int i = 0; i < paciente.ID_STUDY.Length; i++)
                        {
                           
                            if (paciente.ID_STUDY[i] >= 48 && paciente.ID_STUDY[i] <= 57)
                            {
                                if (i > 0 && paciente.ID_STUDY[i - 1] == 49)
                                    paciente.AUX = 10;
                                if (i > 0 && paciente.ID_STUDY[i - 1] == 50)
                                    paciente.AUX = 20;
                                paciente.AUX += Convert.ToInt32(paciente.ID_STUDY[i]) - 48;
                            }


                            if (paciente.ID_STUDY[i] == 45)
                            {
                                paciente.ID_Estudios_array[Visita.MoverEstudios] = paciente.AUX;
                                Visita.MoverEstudios += 1;
                                paciente.AUX = 0;
                            }
                        }
                        
                    }

                }
                if (MenorcheckBox.Checked == true)
                {
                    paciente.Menor_Mayor();
                }
                if (MayorcheckBox.Checked == true)
                {
                    paciente.Mayor_Menor();
                }

                //guardando la informacion personal del paciente.
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("INSERT INTO paciente (NombrePaciente,IDEstudios,HoraLlegadaPaciente,Mayormenor) VALUE('" + Nombre_Text.Text + "','" + paciente.IDestudios + "', '" + Horallega_text.Text + "', '" + Visita.Organizar + "')", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Se ha guardado la información del paciente correctamente");
                connection.Close();
                connection.Open();
                Cmd = new MySqlCommand();
                Cmd = new MySqlCommand("SELECT *FROM paciente WHERE NombrePaciente='" + Nombre_Text.Text + "' ", connection);
                 leer = Cmd.ExecuteReader();
                if (leer.Read())
                {
                    

                    paciente.ID = leer.GetString(0);
                    Visita.UsuarioExis = true;
                    

                }
                connection.Close();
                
                /// INSERTANDO ORDENANADAMENTE LOS ESTUDIOS ///
                if (Visita.ContEstudios > 0)
                {
                    for (int i = 0; i < paciente.ID_Estudios_array.Length; i++)
                    {
                        if(i > 0 && (paciente.ID_Estudios_array[i-1]==1 || paciente.ID_Estudios_array[i-1]==6 || paciente.ID_Estudios_array[i-1]==11 || paciente.ID_Estudios_array[i-1]==16 ))
                        {
                            paciente.TIEMPOMINUTE = new TimeSpan(paciente.HORA.Ticks);
                            paciente.TIEMPOMINUTE = paciente.TIEMPOMINUTE.Add(new TimeSpan(0, 2, 0));
                            paciente.HORA = new DateTime(paciente.TIEMPOMINUTE.Ticks);
                           
                        }

                        if(i > 0 && (paciente.ID_Estudios_array[i-1]==2 || paciente.ID_Estudios_array[i-1]==7 || paciente.ID_Estudios_array[i-1]==12 || paciente.ID_Estudios_array[i-1]==17 ))
                        {
                            paciente.TIEMPOMINUTE = new TimeSpan(paciente.HORA.Ticks);
                            paciente.TIEMPOMINUTE = paciente.TIEMPOMINUTE.Add(new TimeSpan(0, 4, 0));
                            paciente.HORA = new DateTime(paciente.TIEMPOMINUTE.Ticks);
                            
                        }
                        if(i > 0 && (paciente.ID_Estudios_array[i-1]==3 || paciente.ID_Estudios_array[i-1]==8 || paciente.ID_Estudios_array[i-1]==13 || paciente.ID_Estudios_array[i-1]==18 ))
                        {
                            paciente.TIEMPOMINUTE= new TimeSpan(paciente.HORA.Ticks);
                            paciente.TIEMPOMINUTE = paciente.TIEMPOMINUTE.Add(new TimeSpan(0, 6, 0));
                            paciente.HORA = new DateTime(paciente.TIEMPOMINUTE.Ticks);
                          
                        }
                        if (i > 0 && (paciente.ID_Estudios_array[i-1] == 4 || paciente.ID_Estudios_array[i-1] == 9 || paciente.ID_Estudios_array[i-1] == 14 || paciente.ID_Estudios_array[i-1] == 19))
                        {
                            paciente.TIEMPOMINUTE = new TimeSpan(paciente.HORA.Ticks);
                            paciente.TIEMPOMINUTE = paciente.TIEMPOMINUTE.Add(new TimeSpan(0, 8, 0));
                            paciente.HORA = new DateTime(paciente.TIEMPOMINUTE.Ticks);
                            
                        }
                        if (i > 0 && (paciente.ID_Estudios_array[i-1] == 5 || paciente.ID_Estudios_array[i-1] == 10 || paciente.ID_Estudios_array[i-1] == 15 || paciente.ID_Estudios_array[i-1] == 20))
                        {
                            paciente.TIEMPOMINUTE = new TimeSpan(paciente.HORA.Ticks);
                            paciente.TIEMPOMINUTE = paciente.TIEMPOMINUTE.Add(new TimeSpan(0, 10, 0));
                            paciente.HORA = new DateTime(paciente.TIEMPOMINUTE.Ticks);
                            
                        }
                        if (paciente.ID_Estudios_array[i] > 0 && paciente.ID_Estudios_array[i] <= 5)
                        {
                            connection.Open();
                            MySqlCommand kmd = new MySqlCommand();
                            kmd = new MySqlCommand("INSERT INTO panel_basico(IDEstudio,NombrePaciente,Hora_Estudio,IDpaciente) VALUE ('" + paciente.ID_Estudios_array[i] + "','" + Nombre_Text.Text + "' , '" + Convert.ToString(paciente.HORA.TimeOfDay) + "','" + paciente.ID + "' ) ", connection);
                            kmd.ExecuteNonQuery();
                            connection.Close();
                        }
                        if (paciente.ID_Estudios_array[i] >= 6 && paciente.ID_Estudios_array[i] <= 10)
                        {
                            connection.Open();
                            MySqlCommand kmd_2 = new MySqlCommand();
                            kmd_2 = new MySqlCommand("INSERT INTO quimica_clinica(IDEstudio,NombrePaciente,Hora_Estudio,IDpaciente) VALUE ('" + paciente.ID_Estudios_array[i] + "','" + Nombre_Text.Text + "' , '" + Convert.ToString(paciente.HORA.TimeOfDay) + "','" + paciente.ID + "') ", connection);
                            kmd_2.ExecuteNonQuery();
                            connection.Close();
                        }
                        if (paciente.ID_Estudios_array[i] >= 11 && paciente.ID_Estudios_array[i] <= 15)
                        {
                            connection.Open();
                            MySqlCommand kmd_3 = new MySqlCommand();
                            kmd_3 = new MySqlCommand("INSERT INTO serologia(IDEstudio,NombrePaciente,Hora_Estudio,IDpaciente) VALUE ('" + paciente.ID_Estudios_array[i] + "','" + Nombre_Text.Text + "' , '" + Convert.ToString(paciente.HORA.TimeOfDay) + "','" + paciente.ID + "') ", connection);
                            kmd_3.ExecuteNonQuery();
                            connection.Close();
                        }
                        if (paciente.ID_Estudios_array[i] >= 16 && paciente.ID_Estudios_array[i] <= 20)
                        {
                            connection.Open();
                            MySqlCommand kmd_4 = new MySqlCommand();
                            kmd_4 = new MySqlCommand("INSERT INTO bacteriologia(IDEstudio,NombrePaciente,Hora_Estudio,IDpaciente) VALUE ('" + paciente.ID_Estudios_array[i] + "','" + Nombre_Text.Text + "' , '" + Convert.ToString(paciente.HORA.TimeOfDay) + "','" + paciente.ID + "') ", connection);
                            kmd_4.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    
                    

                
                }
                
                //// tablas de estudios  // Aqui se van insertando los estudios correspondientes a cada departamento.
                if (Visita.ContEstudios == 0)
                {

                    if (paciente.AUX > 0 && paciente.AUX <= 5)
                    {
                        connection.Open();
                        MySqlCommand kmd = new MySqlCommand();
                        kmd = new MySqlCommand("INSERT INTO panel_basico(IDEstudio,NombrePaciente,Hora_Estudio,IDpaciente) VALUE ('" + paciente.AUX + "','" + Nombre_Text.Text + "' , '" + Horallega_text.Text + "', '" + paciente.ID + "') ", connection);
                        kmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    if (paciente.AUX >= 6 && paciente.AUX <= 10)
                    {
                        connection.Open();
                        MySqlCommand kmd_2 = new MySqlCommand();
                        kmd_2 = new MySqlCommand("INSERT INTO quimica_clinica(IDEstudio,NombrePaciente,Hora_Estudio,IDpaciente) VALUE ('" + paciente.AUX + "','" + Nombre_Text.Text + "' , '" + Horallega_text.Text + "', '" + paciente.ID + "') ", connection);
                        kmd_2.ExecuteNonQuery();
                        connection.Close();
                    }
                    if (paciente.AUX >= 11 && paciente.AUX <= 15)
                    {
                        connection.Open();
                        MySqlCommand kmd_3 = new MySqlCommand();
                        kmd_3 = new MySqlCommand("INSERT INTO serologia(IDEstudio,NombrePaciente,Hora_Estudio,IDpaciente) VALUE ('" + paciente.AUX + "','" + Nombre_Text.Text + "' , '" + Horallega_text.Text + "', '" + paciente.ID + "') ", connection);
                        kmd_3.ExecuteNonQuery();
                        connection.Close();
                    }
                    if (paciente.AUX >= 16 && paciente.AUX <= 20)
                    {
                        connection.Open();
                        MySqlCommand kmd_4 = new MySqlCommand();
                        kmd_4 = new MySqlCommand("INSERT INTO bacteriologia(IDEstudio,NombrePaciente,Hora_Estudio,IDpaciente) VALUE ('" + paciente.AUX + "','" + Nombre_Text.Text + "' , '" + Horallega_text.Text + "', '" + paciente.ID + "') ", connection);
                        kmd_4.ExecuteNonQuery();
                        connection.Close();
                    }

                }
                
                IDEstudio_text.Text = "";
                NombreEstudio_Text.Text = "";
                departamento_text.Text = "";
                Nombre_Text.Text = "";
                paciente.IDestudios = "";
                MenorcheckBox.Checked = false;
                MayorcheckBox.Checked = false;
                if (Visita.ContPacientes < Visita.NumPacientes )
                {
                    Visita.ContPacientes += 1;
                    label16.Text = Convert.ToString(Visita.ContPacientes + 1);

                }
                
                Organizadorlabel.Visible = false;
                MenorcheckBox.Visible = false;
                MayorcheckBox.Visible = false;
                Visita.ContEstudios = 0;
            }
            else
                MessageBox.Show("Campos vacios");
            Visita.UsuarioExis = false;


            
            if (Visita.ContPacientes == Visita.NumPacientes)
            {
                
                /// POner todo invisible.
                Visita.ContPacientes = 0;
                Visita.NumPacientes = 0;
                label12.Visible = false;
                Nombre_Text.Visible = false;
                label15.Visible = false;
                label13.Visible = false;
                IDEstudio_text.Visible = false;
                label14.Visible = false;
                NombreEstudio_Text.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                label11.Visible = true;
                CantPacientes_Text.Visible = true;
                button1.Visible = true;
                label16.Visible = false;
                button4.Visible = false;
                departamento_text.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                Horallega_text.Visible = false;
                MenorcheckBox.Visible = false;
                MayorcheckBox.Visible = false;
                Organizadorlabel.Visible = false;
                CantPacientes_Text.Text = "";
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            paciente.Timepo_llegada = DateTime.Now;
            connection.Open();
            MySqlCommand Cmd = new MySqlCommand();
            if (NombreEstudio_Text.Text == "" && IDEstudio_text.Text != "")
            {
                Cmd = new MySqlCommand("SELECT *FROM estudios_departamentos WHERE ID='" + IDEstudio_text.Text + "'", connection);
                MySqlDataReader Leer = Cmd.ExecuteReader();
                if (Leer.Read())
                {
                    NombreEstudio_Text.Text = Leer.GetString(1);
                    departamento_text.Text = Leer.GetString(2);
                }
            }
            if (IDEstudio_text.Text == "" && NombreEstudio_Text.Text!= "")
            {
                Cmd = new MySqlCommand("SELECT *FROM estudios_departamentos WHERE Nombre='" + NombreEstudio_Text.Text + "'", connection);
                MySqlDataReader Leer = Cmd.ExecuteReader();
                if (Leer.Read())
                {
                    IDEstudio_text.Text = Leer.GetString(0);
                    departamento_text.Text = Leer.GetString(2);
                }
            }
            Horallega_text.Text = Convert.ToString(paciente.Timepo_llegada.Hour + ":" + paciente.Timepo_llegada.Minute + ":" + paciente.Timepo_llegada.Second);
            connection.Close();
        }

        private void Nombre_Text_TextChanged(object sender, EventArgs e)
        {

        }

        private void IDEstudio_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void NombreEstudio_Text_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NombreEstudio_Text.Text = "";
            IDEstudio_text.Text = "";
            departamento_text.Text = "";

        }

        private void departamento_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void departamento_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 0 || e.KeyChar > 1))
            {

                e.Handled = true;
            }
        }

        private void IDEstudio_text_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NombreEstudio_Text.Text = "";
            IDEstudio_text.Text = "";
            departamento_text.Text = "";
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void Horallega_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 0 || e.KeyChar > 1))
            {

                e.Handled = true;
            }
        }

        private void Horallega_text_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void MenorcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MayorcheckBox.Checked = false;
           
        }

        private void MayorcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MenorcheckBox.Checked = false;         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void IDEstudio_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            MySqlDataReader reader;
            try
            {
                //creamos nuestra cadena de conexion//
                cn = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;password=");
                //abrimos nuestra conexion
                cn.Open();
                //creamos nuestra sentencia
                cmd = new MySqlCommand("SELECT Hora_Estudio,NombrePaciente,IDEstudio,IDpaciente FROM panel_basico", cn);
                //ejecutamos nuestra sentencia y llenamos eldatareader
                reader = cmd.ExecuteReader(); 
                
                //aqui llenamos un ciclo while para llenar las filas de datagrid viewer y agregarlas
                while (reader.Read())
                {
                    this.dataGridView1.Rows.Add(reader.GetValue(1), reader.GetValue(3), reader.GetValue(2), reader.GetValue(0));
                }
                cn.Close();
                
                //eso es todo, ahora probemos
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " +ex.Message);
            }
            



            
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            MySqlDataReader reader;
            try
            {
                //creamos nuestra cadena de conexion//
                cn = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;password=");
                //abrimos nuestra conexion
                cn.Open();
                //creamos nuestra sentencia
                cmd = new MySqlCommand("SELECT Hora_Estudio,NombrePaciente,IDEstudio,IDpaciente FROM serologia", cn);
                //ejecutamos nuestra sentencia y llenamos eldatareader
                reader = cmd.ExecuteReader();

                //aqui llenamos un ciclo while para llenar las filas de datagrid viewer y agregarlas
                while (reader.Read())
                {
                    this.dataGridView2.Rows.Add(reader.GetValue(1), reader.GetValue(3), reader.GetValue(2), reader.GetValue(0));
                }
                cn.Close();

                //eso es todo, ahora probemos
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            MySqlDataReader reader;
            try
            {
                //creamos nuestra cadena de conexion//
                cn = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;password=");
                //abrimos nuestra conexion
                cn.Open();
                //creamos nuestra sentencia
                cmd = new MySqlCommand("SELECT Hora_Estudio,NombrePaciente,IDEstudio,IDpaciente FROM bacteriologia", cn);
                //ejecutamos nuestra sentencia y llenamos eldatareader
                reader = cmd.ExecuteReader();

                //aqui llenamos un ciclo while para llenar las filas de datagrid viewer y agregarlas
                while (reader.Read())
                {
                    this.dataGridView3.Rows.Add(reader.GetValue(1), reader.GetValue(3), reader.GetValue(2), reader.GetValue(0));
                }
                cn.Close();

                //eso es todo, ahora probemos
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            MySqlDataReader reader;
            try
            {
                //creamos nuestra cadena de conexion//
                cn = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;password=");
                //abrimos nuestra conexion
                cn.Open();
                //creamos nuestra sentencia
                cmd = new MySqlCommand("SELECT Hora_Estudio,NombrePaciente,IDEstudio,IDpaciente FROM quimica_clinica", cn);
                //ejecutamos nuestra sentencia y llenamos eldatareader
                reader = cmd.ExecuteReader();

                //aqui llenamos un ciclo while para llenar las filas de datagrid viewer y agregarlas
                while (reader.Read())
                {
                    this.dataGridView4.Rows.Add(reader.GetValue(1), reader.GetValue(3), reader.GetValue(2), reader.GetValue(0));
                }
                cn.Close();

                //eso es todo, ahora probemos
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Borrando del datagrid
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }


           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Borrando del datagrid
            foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
            {
                dataGridView2.Rows.RemoveAt(item.Index);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Borrando del datagrid
            foreach (DataGridViewRow item in this.dataGridView3.SelectedRows)
            {
                dataGridView3.Rows.RemoveAt(item.Index);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Borrando del datagrid
            foreach (DataGridViewRow item in this.dataGridView4.SelectedRows)
            {
                dataGridView4.Rows.RemoveAt(item.Index);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                paciente.CONt_N_estudios = 0;
                //
                connection.Open();
                MySqlCommand Cmd = new MySqlCommand();
                Cmd = new MySqlCommand("SELECT *FROM paciente WHERE NombrePaciente='" + textBox1.Text + "' AND IDPaciente='" + textBox2.Text + "' ", connection);
                MySqlDataReader leer = Cmd.ExecuteReader();
                if (leer.Read())
                {
                    paciente.Estudios_paciente = leer.GetString(2);
                    paciente.ID_Paciente = Convert.ToInt32(leer.GetString(0));
                    paciente.Nombre_Paciente = leer.GetString(1);
                }
                else
                    MessageBox.Show("Error de datos");
                
                connection.Close();
                for (int i = 0; i < paciente.Estudios_paciente.Length; i++) 
                {
                    if (paciente.Estudios_paciente[i] == 45)
                    {
                        paciente.CONt_N_estudios++; // para saber lA CANTIDAD DE ESTUDIOS que el va a realizar.
                    }
                }
                paciente.CONT = 0;

                paciente.estudios_part2 = new int[paciente.CONt_N_estudios];
                for (int i = 0; i < paciente.Estudios_paciente.Length; i++)
                {
                    if (paciente.Estudios_paciente[i] != 45) // para sacar los ids del estudio de la matriz, obviamos el guion
                    {
                        if (i > 0 && paciente.Estudios_paciente[i - 1] == 49)
                            paciente.AUX_2 = 10;
                        if (i > 0 && paciente.Estudios_paciente[i - 1] == 50)
                            paciente.AUX_2 = 20;

                        paciente.AUX_2 += Convert.ToInt32(paciente.Estudios_paciente[i]) - 48;
                    }
                    if (paciente.Estudios_paciente[i] == 45)
                    {
                        paciente.estudios_part2[paciente.CONT] = paciente.AUX_2;
                        paciente.CONT++;
                        paciente.AUX_2 = 0;

                    }


                }

                for (int i = 0; i < paciente.CONt_N_estudios; i++)
                {
                    if (paciente.estudios_part2[i] > 0 && paciente.estudios_part2[i] < 6 ) //extrayendo la hora de cada estudio seleccionado por el paciente.
                    {
                        connection.Open();
                        Cmd = new MySqlCommand("SELECT *FROM panel_basico WHERE NombrePaciente='" + paciente.Nombre_Paciente + "' AND IDEstudio='" + paciente.estudios_part2[i] + "'", connection);
                        MySqlDataReader leer1 = Cmd.ExecuteReader();
                        if (leer1.Read())
                        {
                            paciente.HORA_ESTUDIO = leer1.GetString(0);

                        }
                        connection.Close();
                    }

                    if (paciente.estudios_part2[i] > 5 && paciente.estudios_part2[i] < 11)
                    {
                        connection.Open();
                        Cmd = new MySqlCommand("SELECT *FROM quimica_clinica WHERE NombrePaciente='" + paciente.Nombre_Paciente + "' AND IDEstudio='" + paciente.estudios_part2[i] + "'", connection);
                        MySqlDataReader leer2 = Cmd.ExecuteReader();
                        if (leer2.Read())
                        {
                            paciente.HORA_ESTUDIO = leer2.GetString(0); 
                        }
                        connection.Close();
                    }
                    if (paciente.estudios_part2[i] > 10 && paciente.estudios_part2[i] < 16)
                    {
                        connection.Open();
                        Cmd = new MySqlCommand("SELECT *FROM  serologia WHERE NombrePaciente='" + paciente.Nombre_Paciente + "' AND IDEstudio='" + paciente.estudios_part2[i] + "'", connection);
                        MySqlDataReader leer3 = Cmd.ExecuteReader();
                        if (leer3.Read())
                        {
                            paciente.HORA_ESTUDIO = leer3.GetString(2);
                           // MessageBox.Show(Convert.ToString(paciente.HORA_ESTUDIO));
                        }
                        connection.Close();
                    }
                    if (paciente.estudios_part2[i] > 15 && paciente.estudios_part2[i] < 21)
                    {
                        connection.Open();
                        Cmd = new MySqlCommand("SELECT *FROM bacteriologia WHERE NombrePaciente='" + paciente.Nombre_Paciente + "' AND IDEstudio='" + paciente.estudios_part2[i] + "'", connection);
                        MySqlDataReader leer4 = Cmd.ExecuteReader();
                        if (leer4.Read())
                        {
                            paciente.HORA_ESTUDIO = leer4.GetString(2);
                            //MessageBox.Show(Convert.ToString(paciente.HORA_ESTUDIO));
                        }
                        connection.Close();
                    }

                    connection.Open();
                    MySqlCommand comd = new MySqlCommand(); // insertando todo lo que el paciente se hara en una sola tabla como un calendario
                    comd = new MySqlCommand("INSERT INTO paciente_temporal(NombrePaciente,IDPaciente, hora_estudio, idestudio) VALUE('" + paciente.Nombre_Paciente + "','" + paciente.ID_Paciente + "', '" + paciente.HORA_ESTUDIO + "', '" + paciente.estudios_part2[i] + "')", connection);
                    comd.ExecuteNonQuery();

                    connection.Close();
                }
                MySqlConnection cn;
                MySqlCommand cmd;
                MySqlDataReader reader;
                try
                {
                    //creamos nuestra cadena de conexion//
                    cn = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;password=");
                    //abrimos nuestra conexion
                    cn.Open();
                    //creamos nuestra sentencia
                    cmd = new MySqlCommand("SELECT Hora_Estudio,NombrePaciente,IDEstudio,IDpaciente FROM paciente_temporal", cn);
                    //ejecutamos nuestra sentencia y llenamos eldatareader
                    reader = cmd.ExecuteReader();

                    //aqui llenamos un ciclo while para llenar las filas de datagrid viewer y agregarlas
                    while (reader.Read())
                    {
                        this.dataGridView5.Rows.Add(reader.GetValue(1), reader.GetValue(3), reader.GetValue(2), reader.GetValue(0));
                    }
                    cn.Close();

                    //eso es todo, ahora probemos
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Campos Vacios");
            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView5.SelectedRows)
            {
                //Borrando del datagrid
                dataGridView5.Rows.RemoveAt(item.Index);
            }
            connection.Open();
            MySqlCommand comd = new MySqlCommand();
            comd = new MySqlCommand("DELETE FROM paciente_temporal where NombrePaciente='" + textBox1.Text + "' ", connection);
            comd.ExecuteNonQuery();
            connection.Close();

        }

        private void NombreEstudio_Text_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != ""  )
            {
                if (textBox4.Text == textBox5.Text)
                {

                    connection.Open();
                    delcmd = new MySqlCommand("INSERT INTO people (Usuario,Contrasena) VALUE ('" + textBox3.Text + "','" + textBox4.Text + "') ", connection);
                    delcmd.ExecuteNonQuery();
                    MessageBox.Show("Agregado");
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                }
                else
                    MessageBox.Show("Las contraseña no son iguales");
            }
            else
                MessageBox.Show("Campos vacios");


        }
    }
        
}

