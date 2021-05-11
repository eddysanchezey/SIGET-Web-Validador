using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data;
using System.Data.SqlClient;

namespace ServicioWeb
{
    /// <summary>
    /// Summary description for WS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    //Local
    //"Server= PE2371665W2\\SQLEXPRESS;Database=Ticket_DgTAX;Trusted_Connection=True;";

    //Produccion 
    //"Server= Data Source= tcp:10.20.103.68,49172\\SQLEXPRESS;Database=Ticket_DgTAX;Trusted_Connection=True;";
    public class WS : System.Web.Services.WebService
    {
        public string cadenaconexion = "Data Source= tcp:10.20.103.68,49172\\SQLEXPRESS; Database=Ticket_DgTAX;Trusted_Connection=True;";
        [WebMethod]
        public string RetornarAcceso(string correo, string contraseña)
        {
            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                ConnectionString = cadenaconexion
            };
            conn.Open();
            SqlCommand com = new SqlCommand("select correo from usuarios where estado = 1 and correo = '" + correo.ToString() + "' and contraseña = '" + contraseña.ToString() + "'", conn);
            /*
            SqlCommand cmd = new SqlCommand("TESTLOGIN", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parm = new SqlParameter("@return", SqlDbType.Int);
            parm.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(parm);
            cmd.Parameters.Add(new SqlParameter("@UserName", txtUserName.Text.ToString().Trim()));
            cmd.Parameters.Add(new SqlParameter("@password", txtPassword.Text.ToString().Trim()));

            SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();
            int id = Convert.ToInt32(parm.Value);
            */

            string correo_devuelto = (string)com.ExecuteScalar();
            conn.Close();
            return ""+ correo_devuelto;
        }


        [WebMethod]
        public int RetornarRol(string correo)
        {
            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                ConnectionString = cadenaconexion
            };
            conn.Open();
            SqlCommand com = new SqlCommand("select flg_admin from usuarios where estado = 1 and correo = '" + correo.ToString() + "'", conn);
            
            int fladmin = (int)com.ExecuteScalar ();
            conn.Close();
            return  fladmin;
        }
          
        
        [WebMethod]
        public void ActualizarCombox(string abc, string consulta )
        {

        }
        //
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public DataSet ConsultarEstado(string codigo)
        {
                string LEM_Prueba_compras, LEM_Prueba_ventas, LEM_Prueba_diario, LEA_Prueba_activofijo, LEA_Prueba_kardex, LEA_Prueba_inventariobalance, LEA_Prueba_costos, RDJ_Prueba_kardex, RDJ_Prueba_compras, RDJ_Prueba_diario5_1, RDJ_Prueba_diario5_3, LEA_Prueba_kardex_adic,XML,SIAF = "";
            string XML_compras, XML_ventas;
            string Cruce_FV_LE;


            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172; Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString=cadenaconexion
            };
            conn.Open();
                #region Verificar opciones marcadas del ticket
                

                SqlCommand com = new SqlCommand("select LEM_Prueba_compras from  ticket where Cod_ticket like '" + codigo + "'", conn);
               

                LEM_Prueba_compras = (string)com.ExecuteScalar();


                com = new SqlCommand("select LEM_Prueba_ventas  from  ticket where Cod_ticket like '" + codigo + "'", conn);

            LEM_Prueba_ventas = (string)com.ExecuteScalar();

                com = new SqlCommand("select LEM_Prueba_diario from  ticket where Cod_ticket like '" + codigo + "'", conn);

            LEM_Prueba_diario = (string)com.ExecuteScalar();

                com = new SqlCommand("select LEA_Prueba_activofijo from  ticket where Cod_ticket like '" + codigo + "'", conn);

            LEA_Prueba_activofijo = (string)com.ExecuteScalar();

                com = new SqlCommand("select LEA_Prueba_kardex from  ticket where Cod_ticket like '" + codigo + "'", conn);

            LEA_Prueba_kardex = (string)com.ExecuteScalar();

                com = new SqlCommand("select LEA_Prueba_inventariobalance from  ticket where Cod_ticket like '" + codigo + "'", conn);

            LEA_Prueba_inventariobalance = (string)com.ExecuteScalar();

                com = new SqlCommand("select LEA_Prueba_costos from  ticket where Cod_ticket like '" + codigo + "'", conn);

            LEA_Prueba_costos = (string)com.ExecuteScalar();

                com = new SqlCommand("select RDJ_Prueba_kardex from  ticket where Cod_ticket like '" + codigo + "'", conn);

            RDJ_Prueba_kardex = (string)com.ExecuteScalar();

                com = new SqlCommand("select RDJ_Prueba_diario5_1 from  ticket where Cod_ticket like '" + codigo + "'", conn);
            RDJ_Prueba_diario5_1 = (string)com.ExecuteScalar();

                com = new SqlCommand("select RDJ_Prueba_diario5_3 from  ticket where Cod_ticket like '" + codigo + "'", conn);
            RDJ_Prueba_diario5_3 = (string)com.ExecuteScalar();

            com = new SqlCommand("select RDJ_eficiencia from  ticket where Cod_ticket like '" + codigo + "'", conn);
            string RDJ_eficiencia = (string)com.ExecuteScalar();

            com = new SqlCommand("select RDJ_extendido from  ticket where Cod_ticket like '" + codigo + "'", conn);
            string RDJ_extendido = (string)com.ExecuteScalar();

            com = new SqlCommand("select LEA_Prueba_kardex_adic from  ticket where Cod_ticket like '" + codigo + "'", conn);
            LEA_Prueba_kardex_adic = (string)com.ExecuteScalar();

            com = new SqlCommand("select XML_Vent_Comp from  ticket where Cod_ticket like '" + codigo + "'", conn);
            XML = (string)com.ExecuteScalar();

            com = new SqlCommand("select SIAF from  ticket where Cod_ticket like '" + codigo + "'", conn);
            SIAF = (string)com.ExecuteScalar();

            com = new SqlCommand("select XML_Comp from  ticket where Cod_ticket like '" + codigo + "'", conn);
            XML_compras = (string)com.ExecuteScalar();

            com = new SqlCommand("select XML_Vent from  ticket where Cod_ticket like '" + codigo + "'", conn);
            XML_ventas = (string)com.ExecuteScalar();

            com = new SqlCommand("select Cruce_FV_LE from  ticket where Cod_ticket like '" + codigo + "'", conn);
            Cruce_FV_LE = (string)com.ExecuteScalar();
            
            conn.Close();
            #endregion
                #region construir query con las opciones marcadas
                string Opciones_elegidas = "";
                if (LEA_Prueba_costos == "si")
                    Opciones_elegidas += ",'','LEA_Prueba_costos:', LEA_Prueba_costos, CHAR(13) + CHAR(10)  ";
                if (LEM_Prueba_compras == "si")
                    Opciones_elegidas += ",'','LEM_Prueba_compras:', LEM_Prueba_compras, CHAR(13) + CHAR(10)  ";
            
                if (LEM_Prueba_ventas == "si")
                    Opciones_elegidas += ",'','LEM_Prueba_ventas:', LEM_Prueba_ventas, CHAR(13) + CHAR(10)  ";
                if (LEM_Prueba_diario == "si")
                     Opciones_elegidas += ",'','LEM_Prueba_diario:', LEM_Prueba_diario, CHAR(13) + CHAR(10)  ";
                if (LEA_Prueba_activofijo == "si")
                    Opciones_elegidas += ",'','LEA_Prueba_activofijo:', LEA_Prueba_activofijo, CHAR(13) + CHAR(10)  ";
                if (LEA_Prueba_inventariobalance == "si")
                    Opciones_elegidas += ",'','LEA_Prueba_inventariobalance:', LEA_Prueba_inventariobalance, CHAR(13) + CHAR(10)  ";
                if (LEA_Prueba_kardex == "si" )
                    Opciones_elegidas += ",'','LEA_Prueba_kardex:', LEA_Prueba_kardex, CHAR(13) + CHAR(10)  ";
            //if ( RDJ_Prueba_kardex == "si")
            //    Opciones_elegidas += ",'','RDJ_Prueba_kardex:', RDJ_Prueba_kardex, CHAR(13) + CHAR(10)  ";

            //if (RDJ_Prueba_diario5_1 == "si")
            //    Opciones_elegidas += ",'','RDJ_Prueba_diario5_1:', RDJ_Prueba_diario5_1, CHAR(13) + CHAR(10)  ";
            //if (RDJ_Prueba_diario5_3 == "si")
            //    Opciones_elegidas += ",'','RDJ_Prueba_diario5_3:', RDJ_Prueba_diario5_3, CHAR(13) + CHAR(10)  ";
            if ((RDJ_Prueba_diario5_3 == "si" || RDJ_Prueba_diario5_1 == "si") && RDJ_eficiencia=="si")
                Opciones_elegidas += ",'','RDJ_Prueba_eficiencia  :', RDJ_Prueba_diario5_3, CHAR(13) + CHAR(10)  ";

            if ((RDJ_Prueba_diario5_3 == "si" || RDJ_Prueba_diario5_1 == "si") && RDJ_extendido == "si")
                Opciones_elegidas += ",'','RDJ_Prueba_extendida  :', RDJ_Prueba_diario5_3, CHAR(13) + CHAR(10)  ";

            if (LEA_Prueba_kardex_adic == "si")
                    Opciones_elegidas += ",'','LEA_Prueba_kardex_adic:', LEA_Prueba_kardex_adic, CHAR(13) + CHAR(10)  ";

            if (XML == "si")
                Opciones_elegidas += ",'','XML:', 'XML Compras-Ventas', CHAR(13) + CHAR(10)  ";

            if(SIAF=="si")
                Opciones_elegidas += ",'','SIAF:', 'SIAF ', CHAR(13) + CHAR(10)  ";

            if(XML_compras == "si")
                Opciones_elegidas += ",'','XML:', 'XML_compras ', CHAR(13) + CHAR(10)  ";
            if (XML_ventas == "si")
                Opciones_elegidas += ",'','XML:', 'XML_ventas ', CHAR(13) + CHAR(10)  ";
            if (Cruce_FV_LE == "si")
                Opciones_elegidas += ",'','Cruce_FV_LE:', 'Cruce_FV_LE ', CHAR(13) + CHAR(10)  ";

            
            if (Opciones_elegidas == "")
            {
                Opciones_elegidas = " 'LEM_Prueba_compras:',LEM_Prueba_compras,CHAR(13) + CHAR(10)," +
                                    "'LEM_Tablero_compras:',LEM_Tablero_compras,CHAR(13) + CHAR(10)," +
                                    "'LEM_Prueba_ventas:', LEM_Prueba_ventas, CHAR(13) + CHAR(10)," +
                                    "'LEM_Tablero_ventas:', LEM_Tablero_ventas,CHAR(13) + CHAR(10)," +
                                    "'LEM_Prueba_diario:',   LEM_Prueba_diario,CHAR(13) + CHAR(10)," +
                                    "'LEM_Tablero_diario:',   LEM_Tablero_diario,CHAR(13) + CHAR(10)," +

                                    "'LEA_Prueba_activofijo:',   LEA_Prueba_activofijo,CHAR(13) + CHAR(10)," +
                                    "'LEA_Tablero_activofijo:',   LEA_Tablero_activofijo,CHAR(13) + CHAR(10)," +
                                    "'LEA_Prueba_kardex:',  LEA_Prueba_kardex,CHAR(13) + CHAR(10)," +
                                    "'LEA_Tablero_kardex:',  LEA_Tablero_kardex,CHAR(13) + CHAR(10)," +
                                    "'LEA_Prueba_inventariobalance:',  LEA_Prueba_inventariobalance,CHAR(13) + CHAR(10)," +
                                    "'LEA_Tablero_inventariobalance:', LEA_Tablero_inventariobalance,CHAR(13) + CHAR(10)," +
                                    "'LEA_Prueba_costos:',  LEA_Prueba_costos,CHAR(13) + CHAR(10)," +
                                    "'LEA_Tablero_costos:',   LEA_Tablero_costos,CHAR(13) + CHAR(10)," +

                                    "'RDJ_Prueba_kardex:',  RDJ_Prueba_kardex,CHAR(13) + CHAR(10)," +
                                    "'RDJ_Tablero_kardex:',  RDJ_Tablero_kardex,CHAR(13) + CHAR(10)," +
                                    "'RDJ_Prueba_compras:',  RDJ_Prueba_compras,CHAR(13) + CHAR(10)," +
                                    "'RDJ_Tablero_compras:',  RDJ_Tablero_compras,CHAR(13) + CHAR(10)," +
                                    "'RDJ_Prueba_diario5_1:',  RDJ_Prueba_diario5_1,CHAR(13) + CHAR(10)," +
                                    "'RDJ_Tablero_diario5_1:',  RDJ_Tablero_diario5_1,CHAR(13) + CHAR(10)," +
                                    "'RDJ_Prueba_diario5_3:', RDJ_Prueba_diario5_3,CHAR(13) + CHAR(10)," +
                                    "'RDJ_Tablero_diario5_3:', RDJ_Tablero_diario5_3,CHAR(13) + CHAR(10)," +
                                    "'LEA_Prueba_kardex_adic: ',LEA_Prueba_kardex_adic,CHAR(13) + CHAR(10)," +
                                    "'LEA_Tablero_kardex_adic: ',LEA_Tablero_kardex_adic";
            }
            #endregion
            //cadena para cargar una tabla
            const int V = 1;
            SqlDataAdapter da = new SqlDataAdapter("Select Cod_ticket,Area,Gerente,Senior,Fecha_registro,Fecha_prevista,Estado, CONCAT("+ Opciones_elegidas.Substring(1, Opciones_elegidas.Length - V) +  " ) as Solicitado from Ticket where Cod_ticket like '" + codigo + "'", conn);

                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            
            
        }

        [WebMethod]
        public DataSet LLenarListaGerente()
        {
            SqlConnection conn = new SqlConnection
            {
                //"Server=PE2349330W3\\SQLEXPRESS;Database=Ticket_DgTAX;Trusted_Connection=True;"

                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString=cadenaconexion
            };
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select distinct Manager_name  from Jobs where Jobs.Estado=1", conn);

            conn.Close();
            DataSet ds = new DataSet();

            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet LLenarListaJob(string Nomb_Gerente)
        {
            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString=cadenaconexion
            };
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select distinct Jobs.Number_engagement, CONCAT(Jobs.Number_engagement,' - ',Jobs.Client_engagement) as Job_  from Jobs  where Jobs.Estado=1 and Jobs.Manager_name  like '" + Nomb_Gerente + "'", conn);

            conn.Close();
            DataSet ds = new DataSet();

            da.Fill(ds);
            return ds;
        }
        [WebMethod]

        public string Subserviceline(int Job)
        {
            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString=cadenaconexion
            };
            conn.Open();
            
            string QueryString = "Select  Jobs.Subserviceline from Jobs where Jobs.Estado=1 and  Jobs.Number_engagement  = " + Job;

            SqlCommand command = new SqlCommand(QueryString, conn);
            string Subserviceline = Convert.ToString(command.ExecuteScalar());
            conn.Close();
            return Subserviceline;
        }
        [WebMethod]
        public string RegistroTicket(string Area, int Job, string Gerente,string Senior,DateTime Fecha_registro, DateTime Fecha_prevista)
        {
            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString=cadenaconexion
            };
            SqlCommand com = new SqlCommand("spInsert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@Area", Area);
            com.Parameters.AddWithValue("@Job", Job);
            com.Parameters.AddWithValue("@Gerente", Gerente);
            com.Parameters.AddWithValue("@Senior", Senior);
            com.Parameters.AddWithValue("@Fecha_registro", Fecha_registro);
            com.Parameters.AddWithValue("@Fecha_prevista", Fecha_prevista);

            conn.Open();
            int registro = com.ExecuteNonQuery();
            
            conn.Close();

            string codigo = RetornarCodigo( Area,  Job,  Gerente,  Senior,  Fecha_registro,  Fecha_prevista);

            if (registro > 0)
                return "Registro ingresado exitosamente: El código del ticket es " + codigo;
            else
                return "Registro no ingresado";
            
        }

        [WebMethod]
        public string RegistroTicket_v2(string Area, int Job, string Gerente, string Senior, DateTime Fecha_registro, DateTime Fecha_prevista,
            string LEM_Prueba_compras,            
            string LEM_Tablero_compras,
            string LEM_Prueba_ventas,
            string LEM_Tablero_ventas,
            string LEM_Prueba_diario,
            string LEM_Tablero_diario,

            string LEA_Prueba_activofijo,
            string LEA_Tablero_activofijo,
            string LEA_Prueba_kardex,
            string LEA_Tablero_kardex,
            string LEA_Prueba_inventariobalance,
            string LEA_Tablero_inventariobalance,
            string LEA_Prueba_costos,
            string LEA_Tablero_costos,

            string RDJ_Prueba_kardex,
            string RDJ_Tablero_kardex,
            string RDJ_Prueba_compras,
            string RDJ_Tablero_compras,
            string RDJ_Prueba_diario5_1,
            string RDJ_Tablero_diario5_1,
            string RDJ_Prueba_diario5_3,
            string RDJ_Tablero_diario5_3,

            string PC_ingresar,
            string LEA_Prueba_kardex_adic,
            string LEA_Tablero_kardex_adic,

            string RDJ_eficiencia,
            string RDJ_extendido,
            string XML,
            //
            string XML_compras,
            string XML_ventas,
            string Cruce_FV_LE,
            //
            string SIAF)
        {
            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString=cadenaconexion
            };
            SqlCommand com = new SqlCommand("spInsert_v2", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@Area", Area);
            com.Parameters.AddWithValue("@Job", Job);
            com.Parameters.AddWithValue("@Gerente", Gerente);
            com.Parameters.AddWithValue("@Senior", Senior);
            com.Parameters.AddWithValue("@Fecha_registro", Fecha_registro);
            com.Parameters.AddWithValue("@Fecha_prevista", Fecha_prevista);

            com.Parameters.AddWithValue("@LEM_Prueba_compras", LEM_Prueba_compras);
            com.Parameters.AddWithValue("@LEM_Tablero_compras", LEM_Tablero_compras);
            com.Parameters.AddWithValue("@LEM_Prueba_ventas", LEM_Prueba_ventas);
            com.Parameters.AddWithValue("@LEM_Tablero_ventas", LEM_Tablero_ventas);
            com.Parameters.AddWithValue("@LEM_Prueba_diario", LEM_Prueba_diario);
            com.Parameters.AddWithValue("@LEM_Tablero_diario", LEM_Tablero_diario);
            com.Parameters.AddWithValue("@LEA_Prueba_activofijo", LEA_Prueba_activofijo);
            com.Parameters.AddWithValue("@LEA_Tablero_activofijo", LEA_Tablero_activofijo);
            com.Parameters.AddWithValue("@LEA_Prueba_kardex", LEA_Prueba_kardex);
            com.Parameters.AddWithValue("@LEA_Tablero_kardex", LEA_Tablero_kardex);
            com.Parameters.AddWithValue("@LEA_Prueba_inventariobalance", LEA_Prueba_inventariobalance);
            com.Parameters.AddWithValue("@LEA_Tablero_inventariobalance", LEA_Tablero_inventariobalance);
            com.Parameters.AddWithValue("@LEA_Prueba_costos", LEA_Prueba_costos);
            com.Parameters.AddWithValue("@LEA_Tablero_costos", LEA_Tablero_costos);
            com.Parameters.AddWithValue("@RDJ_Prueba_kardex", RDJ_Prueba_kardex);

            com.Parameters.AddWithValue("@RDJ_Tablero_kardex", RDJ_Tablero_kardex);
            com.Parameters.AddWithValue("@RDJ_Prueba_compras", RDJ_Prueba_compras);
            com.Parameters.AddWithValue("@RDJ_Tablero_compras", RDJ_Tablero_compras);
            com.Parameters.AddWithValue("@RDJ_Prueba_diario5_1", RDJ_Prueba_diario5_1);
            com.Parameters.AddWithValue("@RDJ_Tablero_diario5_1", RDJ_Tablero_diario5_1);
            com.Parameters.AddWithValue("@RDJ_Prueba_diario5_3", RDJ_Prueba_diario5_3);
            com.Parameters.AddWithValue("@RDJ_Tablero_diario5_3", RDJ_Tablero_diario5_3);

            


            com.Parameters.AddWithValue("@PC_ingresar", PC_ingresar);
            com.Parameters.AddWithValue("@LEA_Prueba_kardex_adic", LEA_Prueba_kardex_adic);
            com.Parameters.AddWithValue("@LEA_Tablero_kardex_adic", LEA_Tablero_kardex_adic);
            //cambio 24/09/2019
            //se agrega los campos RDJ_eficiencia y RDJ_extendido (el primero ejecuta un flujo reducido de 5 pruebas (son las pruebas clasicas que BTC realiza manualmente), mientras que el segundo ejecuta 20 pruebas (se cobra como una venta de producto diferenciado, BTC no las ejecuta manualmente por los cruces con diario ventas y compras exhaustivo)
            com.Parameters.AddWithValue("@RDJ_eficiencia", RDJ_eficiencia);
            com.Parameters.AddWithValue("@RDJ_extendido", RDJ_extendido);
            com.Parameters.AddWithValue("@XML_Vent_Comp", XML);
            //
            com.Parameters.AddWithValue("@XML_Vent", XML_ventas);
            com.Parameters.AddWithValue("@XML_Comp", XML_compras);
            com.Parameters.AddWithValue("@Cruce_FV_LE", Cruce_FV_LE);
            
            //
            com.Parameters.AddWithValue("@SIAF", SIAF);
            conn.Open();
            int registro = com.ExecuteNonQuery();

            conn.Close();

            string codigo = RetornarCodigo(Area, Job, Gerente, Senior, Fecha_registro, Fecha_prevista);

            if (registro > 0)
                return "Registro ingresado exitosamente: El código del ticket es " + codigo;
            else
                return "Registro no ingresado";

        }

        public string RetornarCodigo(string Area, int Job, string Gerente, string Senior, DateTime Fecha_registro, DateTime Fecha_prevista)
        {
            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                //conn.ConnectionString = @"Data Source= 10.20.103.68,49172; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString=cadenaconexion
            };
            SqlCommand com = new SqlCommand("spDevolverCodigo", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@Area", Area);
            com.Parameters.AddWithValue("@Job", Job);
            com.Parameters.AddWithValue("@Gerente", Gerente);
            com.Parameters.AddWithValue("@Senior", Senior);
            com.Parameters.AddWithValue("@Fecha_registro", Fecha_registro);
            com.Parameters.AddWithValue("@Fecha_prevista", Fecha_prevista);

            conn.Open();

            //int ID_devuelto = (int)com.ExecuteScalar();//cambio 01/10/2019 Augusto indica que el codigo de ticket debe contener 4 digitos Ej: DG-0001, DG-0456 ...DG-0010
            string ID_devuelto = (string)com.ExecuteScalar();
            conn.Close();

            //return "DG-"+ID_devuelto;
            return ID_devuelto;
        }
        [WebMethod]
        public DateTime Fecha_fin_ult_ticket()
        {
            SqlConnection conn = new SqlConnection
            {
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString=cadenaconexion
            };
            SqlCommand com = new SqlCommand("select top 1 Fecha_prevista from Ticket order by fecha_prevista desc", conn);
            conn.Open();

            DateTime Ultima_fecha_prevista_ = (DateTime)com.ExecuteScalar();//Fecha_termino_ult_ticket
            conn.Close();
            return Ultima_fecha_prevista_;
        }

        [WebMethod]
        public string TicketProcesando()
        {
            SqlConnection conn = new SqlConnection
            {
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString = cadenaconexion
            };

            SqlCommand com = new SqlCommand("select top(1) Cod_ticket from ticket where estado in ('Procesando') order by id desc",conn);
            
            
            conn.Open();
            string codigoTicket = (string)com.ExecuteScalar();
            /*
            SqlCommand cmd = new SqlCommand("TicketProcesando", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.NVarChar);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            //object value = (cmd.ExecuteScalar().ToString());
            //cmd.ExecuteNonQuery();
            var algo= cmd.ExecuteScalar();

            string codigoTicket = (string)returnParameter.ToString();

            string retunvalue = (string)cmd.Parameters["RetVal"].Value.ToString();

            */
            conn.Close();
            return codigoTicket;
        }
        [WebMethod]
        public string NumeroTicketsProcesando()
        {
            SqlConnection conn = new SqlConnection
            {
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString = cadenaconexion
            };
            conn.Open();
            //SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM Ticket WHERE Estado='Procesando'");
            //string numeroTicktesProcesando = (string)com.ExecuteScalar();

            SqlCommand cmd = new SqlCommand("NumeroTicktesProcesando", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            string execution = (string)cmd.ExecuteScalar();
            string numeroTicktesProcesando = (string)returnParameter.Value.ToString();


            conn.Close();
            
            return numeroTicktesProcesando;
        }

        [WebMethod]
        public void EditarEstado(string CodigoTicket, string Estado, DateTime FechaPrevista)
        {
            SqlConnection conn = new SqlConnection
            {
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString = cadenaconexion
            };

            SqlCommand com = new SqlCommand("ActualizarEstado", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@codigoTicket", CodigoTicket);
            com.Parameters.AddWithValue("@descripcionEstado", Estado);
            com.Parameters.AddWithValue("@fechaPrevista", FechaPrevista);

            conn.Open();
            int registro = com.ExecuteNonQuery();

            conn.Close();
        }
        [WebMethod]
        public int VerificarCodigo( string codigo)
        {
            SqlConnection conn = new SqlConnection
            {
                //ConnectionString = "Data Source= tcp:10.20.103.68,49172;Database=Ticket_DgTAX;Trusted_Connection=True;"
                ConnectionString = cadenaconexion
            };
            SqlCommand com = new SqlCommand("select id from Ticket where cod_ticket='" + codigo+"'", conn);

            conn.Open();
            int registro = (int) com.ExecuteScalar();

            conn.Close();

            return registro;
        }
    }
}
