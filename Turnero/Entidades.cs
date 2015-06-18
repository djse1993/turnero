using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Turnero
{
    class Persona
    {
        #region "Propiedades"
        public int id
        {
            get;
            set;
        }
        public string Nombre
        { get; set; }
        #endregion
        
    }
    class Visitas
    {
        #region"Propiedades"
        public int NumPacientes
        {
            get;
            set;
        }
        public int ContPacientes
        {
            get;
            set;
        }
        public bool UsuarioExis
        {
            get;
            set;
        }
        public int ContEstudios
        {
            get;
            set;
        
        }
        public int Organizar
        {
            get;
            set;

        }
        public int MoverEstudios
        {
            get;
            set;
        }

        #endregion
        #region"Constructor"
        public Visitas()
        {
            ContPacientes = 0;
            ContEstudios = 0;
            UsuarioExis = false;
        } 
        
        #endregion
        #region "Metodos/funciones"
        public virtual void Organizar_Pacientes()
        {
        
        }
        
        #endregion

    }
    class Paciente : Persona
    {
        #region "Propiedades"
        public string ID
        { get; set; }
        public String Nombrepaciente
        {
            get;
            set;
        }
        public String IDestudios
        {
            get;
            set;
        }
        public String NombreESTUDIO
        {
            get;
            set;
        }
        public DateTime Timepo_llegada
        {
            get;
            set;
        }
        public int[] ID_Estudios_array
        {
            get;
            set;
        }
        public int AUX
        {
            get;
            set;
        }

        public int AUX_2
        {
            get;
            set;
        }
        public int N
        {
            get;
            set;
        }
        public int POSMIN
        {
            get;
            set;
        }

        public int I_AUX
        {
            get;
            set;
        }
        public String Estudios_paciente
        {
            get;
            set;
        }
        public DateTime HORA
        {
            get;
            set;
        }
        public int CONt_N_estudios
        {
            get;
            set;
        }
        public int[] estudios_part2
        {
            get;
            set;

        }
        public String Nombre_Paciente
        {
            get;
            set;
        }
        public int ID_Paciente
        {
            get;
            set;
        }
        public string HORA_ESTUDIO
        {
            get;
            set;
        }
        public TimeSpan TIEMPOMINUTE
        {
            get;
            set;
        }
        public string ID_STUDY
        {
            get;
            set;
        }
        public int CONT
        {
            get;
            set;
        }
        #endregion
        #region"Constructor"
        public Paciente()
        {
            AUX = 0;
            CONt_N_estudios = 0;
        } 
        
        #endregion
        #region "Metodos/Funciones"
        public void Organizar_Matriz()
        {
            for (int i = 0; i < this.ID_STUDY.Length; i++) // separar en una matriz los ID's de los estudios para algoritmo de ordenamiento.
            {
                if (this.ID_STUDY[i] >= 48 && this.ID_STUDY[i] <= 57)
                {
                    if (i > 0 && this.ID_STUDY[i] == 49)
                        this.AUX = 10;
                    if (i > 0 && this.ID_STUDY[i] == 50)
                        this.AUX = 20;
                    this.AUX += Convert.ToInt32(this.ID_STUDY[i]) - 48;
                }

            }
                  
        }
        public void Mayor_Menor()
        {
            //selecccion// DE MAYOR A Menor (OJO) 
            this.N = this.ID_Estudios_array.Length;
            for (int i = 0; i < this.N - 1; i++)
            {
                //buscamos el minimo
                //suponemos que es el primero.
                this.POSMIN = i;
                //nos movemos por el resto
                for (int j = i + 1; j < this.N; j++)
                {
                    // si este es menor todavia
                    if (this.ID_Estudios_array[j] > this.ID_Estudios_array[this.POSMIN])
                    {
                        //tomamos su posicion
                        this.POSMIN = j;
                    }
                }
                //intercambiar la posicion i y el minimo encontrado
                this.I_AUX = this.ID_Estudios_array[i];
                this.ID_Estudios_array[i] = this.ID_Estudios_array[this.POSMIN];
                this.ID_Estudios_array[this.POSMIN] = this.I_AUX;
            }
           
            ////
        }
        public void Menor_Mayor()
        {
            //selecccion// DE MENOR A MAYOR (OJO) 
            this.N = this.ID_Estudios_array.Length;
            for (int i = 0; i < this.N - 1; i++)
            {
                //buscamos el minimo
                //suponemos que es el primero.
                this.POSMIN = i;
                //nos movemos por el resto
                for (int j = i + 1; j < this.N; j++)
                {
                    // si este es menor todavia
                    if (this.ID_Estudios_array[j] < this.ID_Estudios_array[this.POSMIN])
                    {
                        //tomamos su posicion
                        this.POSMIN = j;
                    }
                }
                //intercambiar la posicion i y el minimo encontrado
                this.I_AUX = this.ID_Estudios_array[i];
                this.ID_Estudios_array[i] = this.ID_Estudios_array[this.POSMIN];
                this.ID_Estudios_array[this.POSMIN] = this.I_AUX;
            }
           
            ////
 
        }
        #endregion
    }

    class usuario : Persona
    {
        #region "Propiedades"
        public string Contrasena
        { get; set; }
        public Boolean Oranizado { get; set; }
        #endregion
        #region "Contructores"
        
        #endregion

    }

    class Panel_Basico : Visitas
    {
        #region "Propiedades"
        public DateTime Hora_realizar
        { get; set; }
        public Boolean Oranizado { get; set; }
        #endregion
        #region "Constructores"

        #endregion
        #region "Metodos/Funcones
        public virtual void Organizar_Pacientes()
        {
            for (int i = 0; i < this.ContPacientes; i++)
            {
                if (this.NumPacientes < this.Organizar)
                {
                    this.Oranizado = true;
                }

            }
        }
	    #endregion
    }
    class serologia : Visitas
    {
        #region "Propiedades"
        public DateTime Hora_realizar
        { get; set; }
        public Boolean Organizado { get; set; }
        #endregion
        #region "Constructores"


        #endregion
        #region "Metodos/Funcones
        public virtual void Organizar_Pacientes()
        {
            for (int i = 0; i < this.ContPacientes; i++)
            {
                if (this.NumPacientes < this.Organizar)
                {
                    this.Organizado = true;
                }

            }
        }
        #endregion
    }
    class Quimica_clinica : Visitas
    {
        #region "Propiedades"
        public DateTime Hora_realizar
        { get; set; }
        #endregion
        #region "Constructores"


        #endregion
        #region "Metodos/Funcones
        public virtual void Organizar_Pacientes()
        {
            for (int i = 0; i < this.ContPacientes; i++)
            {
                if (this.NumPacientes < this.Organizar)
                {
                    this.Organizar += i;
                }
               

            }
        }
        #endregion
    }
    class Bacteriología : Visitas
    {
        #region "Propiedades"
        public DateTime Hora_realizar
        { get; set; }
        public Boolean Oranizado { get; set; }
        #endregion
        #region "Constructores"
        
        #endregion
        #region "Metodos/Funcones
        public virtual void Organizar_Pacientes()
        {
            for (int i = 0; i < this.ContPacientes; i++)
            {
                if (this.NumPacientes < this.Organizar)
                {
                    this.Oranizado = true;
                }
            
            }
        }
        #endregion
    }


}
