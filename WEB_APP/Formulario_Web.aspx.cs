using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WEB_APP
{
    public partial class Formulario_Web : System.Web.UI.Page
    {
        //public static string dominio = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        public static string nombrePC = Dns.GetHostName();
        public string path_Libros = "";
        public string usuario = "";
        String UserName_ey;
        int flgadmin;
        ServiceReference1.WSSoapClient wS = new ServiceReference1.WSSoapClient();
        


        protected void Page_Load(object sender, EventArgs e)
        {

            #region Recupero el user-name-login
            //ocultar primer item
            RadioButtonList2.Items[0].Attributes.Add("style", "display:none"); ;

            UserName_ey = Request.QueryString["UserName_ey"];
            flgadmin = wS.RetornarRol(UserName_ey);
            Button5.Visible = false;
            if (flgadmin == 1)
            {
                Button5.Visible = true;
            }
            Label2.Text = "Hola: " + UserName_ey;
            TextBox9.Text = UserName_ey;
            usuario = UserName_ey;
            #endregion

            #region Crear Carpeta en cliente
            //string newFolder = "Digital Tax _ LE";
            path_Libros = @"J:\COMMON\" + UserName_ey.Replace(".", "_");
            path_Libros = path_Libros.Replace("@", "_");
            //path_Libros = System.IO.Path.Combine(
            //   Environment.GetFolderPath(Environment.SpecialFolder.Desktop),newFolder);

            if (!System.IO.Directory.Exists(path_Libros))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path_Libros);
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }

            }
            #endregion
            //
            
            Titulo_registrar_valor.Visible = true;
            CrearTicketid.Visible = true;
            BuscarTicketid.Visible = false;
            Titulo_buscar_valor.Visible = false;

            Titulo_editarestado_valor.Visible = false;
            EditarEstadoid.Visible = false;



            //Subservice line
            if (!IsPostBack)
            {
                /*
                SqlConnection conn = new SqlConnection();
                //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                conn.ConnectionString = @"Data Source=10.20.103.68,49172;Initial Catalog=Ticket_DgTAX;Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler

                //string ConnectString = "server=10.20.103.68,49172\\SQLEXPRESS;database=Ticket_DgTAX;integrated security=True";
                //string ConnectString = "Data Source=10.20.103.68,49172 ;Initial Catalog=Ticket_DgTAX; Integrated Security=True";
                //Select distinct Jobs.Subserviceline from usuarios,Jobs,usuarios_job where Jobs.Estado = 1 and usuarios.correo = usuarios_job.correo and usuarios_job.Number_engagement = Jobs.Number_engagement and usuarios.correo = '" + UserName_ey+"'"Select distinct Jobs.Subserviceline from usuarios,Jobs,usuarios_job where Jobs.Estado=1 and usuarios.correo=usuarios_job.correo and usuarios_job.Number_engagement=Jobs.Number_engagement and usuarios.correo='" + UserName_ey + "'";
                string QueryString = "Select top 1  Jobs.Subserviceline from Jobs where Jobs.Estado=1";
                //SqlConnection conn = new SqlConnection(ConnectString);
                conn.Open();
                SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, conn);
                DataSet ds = new DataSet();
                //myCommand.Fill(ds);

                //Select1.DataSource = ds;
                //Select1.DataTextField = "Subserviceline";
                
                //Select1.DataValueField = "Subserviceline";
                //Select1.DataBind();
                //---
                SqlCommand command = new SqlCommand(QueryString, conn);
                string Subserviceline = Convert.ToString(command.ExecuteScalar());
                
                TextBox6.Text = Subserviceline;

                //Jobs dejado sin efecto
                /*
                QueryString = "Select distinct Jobs.Number_engagement, CONCAT(Jobs.Number_engagement,' - ',Jobs.Client_engagement) as Job_  from usuarios,Jobs,usuarios_job where Jobs.Estado=1 and usuarios.correo=usuarios_job.correo and usuarios_job.Number_engagement=Jobs.Number_engagement and usuarios.correo='" + UserName_ey + "'";
                myCommand = new SqlDataAdapter(QueryString, conn);
                ds = new DataSet();
                myCommand.Fill(ds);

                Select2.DataSource = ds;
                Select2.DataTextField = "Job_";
                Select2.DataValueField = "Number_engagement";
                Select2.DataBind();
                */
                //---


                //Manager dejado sin efecto
                /*
                QueryString = "Select distinct Jobs.Manager_name from usuarios,Jobs,usuarios_job where Jobs.Estado=1 and usuarios.correo=usuarios_job.correo and usuarios_job.Number_engagement=Jobs.Number_engagement and usuarios.correo='" + UserName_ey + "'";
                myCommand = new SqlDataAdapter(QueryString, conn);
                ds = new DataSet();
                myCommand.Fill(ds);

                //Select3.DataSource = ds;
                //Select3.DataTextField = "Manager_name";
                //Select3.DataValueField = "Manager_name";
                //Select3.DataBind();

                //---
                command = new SqlCommand(QueryString, conn);
                string Manager_name = Convert.ToString(command.ExecuteScalar());
                                
                TextBox8.Text = Manager_name;
                */
                TextBox6.Text = "BTC";
                ServiceReference1.WSSoapClient wS = new ServiceReference1.WSSoapClient();
                DataSet ds = wS.LLenarListaGerente();
                //Seleccion de Gerente
                //QueryString = "Select distinct Manager_name  from Jobs where Jobs.Estado=1 ";
                //myCommand = new SqlDataAdapter(QueryString, conn);
                //ds = new DataSet();
                //myCommand.Fill(ds);

                Select1.DataSource = ds;
                Select1.DataTextField = "Manager_name";
                Select1.DataValueField = "Manager_name";
                Select1.DataBind();
                //Senior
                //conn.Close();
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //var lcDiv = XhtmlMobileDocType.getElementById('divEstado');

            VistaGrabarTicket();



        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            BuscarTicket();

        }


        protected void Button2_Click1(object sender, EventArgs e)
        {
            VistaBuscarTicket();

        }

        private DateTime Devolver_fecha_prevista(DateTime Fecha_reg_tckt_actual, DateTime Fecha_termino_ult_ticket)
        {   // el parametro Fecha_termino_ult_ticket=fecha y hora en que los libros electonicos del ticket anterior terminaron de procesarse
            // el parametro fecha_devuelta= promesa de entrega de informacion por codigo de ticket actual registrado
            //el inicio del proceso del nuevo ticket inicia luego de finalizar el ticket anterior

            // en caso el ultimo ticket termine antes que el registro del ticket nuevo, la fecha de inicio del proceso parte desde el momento en que se registra el nuevo ticket y no del termino del ticket anterior.
            if (Fecha_termino_ult_ticket <= Fecha_reg_tckt_actual)
            {
                Fecha_termino_ult_ticket = Fecha_reg_tckt_actual;
            }


            int dia_semana;

            //            Friday  5
            //Indicates Friday.

            //Monday  1
            //Indicates Monday.

            //Saturday    6
            //Indicates Saturday.

            //Sunday  0
            //Indicates Sunday.

            //Thursday    4
            //Indicates Thursday.

            //Tuesday 2
            //Indicates Tuesday.

            //Wednesday   3
            //Indicates Wednesday.
            DateTime Fecha_reg_tckt_actual_final = Fecha_termino_ult_ticket;
            //dia_semana = (int)Fecha_termino_ult_ticket.DayOfWeek;
            //incio de jornada 08:30
            DateTime ini_jornada;// 8:30 del dia que se registra el ticket
            ini_jornada = Fecha_termino_ult_ticket.Date.AddDays(0);
            ini_jornada = ini_jornada.AddHours(8);
            ini_jornada = ini_jornada.AddMinutes(30);
            ini_jornada = ini_jornada.AddSeconds(0);
            //fin de jornada 19:00
            DateTime fin_jornada;// 19:00 del dia que se registra el ticket
            fin_jornada = Fecha_termino_ult_ticket.Date.AddDays(0);
            fin_jornada = fin_jornada.AddHours(19);
            fin_jornada = fin_jornada.AddMinutes(0);
            fin_jornada = fin_jornada.AddSeconds(0);

            //A partir de las 2pm (14:00) el tiempo de entrega se calcula las horas restantes hasta el fin de jornada y el resto de horas faltantes para terminar de procesar se cargan a partir del inicio de jornada del dia siguiente

            //hora de corte (2pm)
            DateTime hora_corte;
            hora_corte = Fecha_termino_ult_ticket.Date.AddDays(0);
            hora_corte = hora_corte.AddHours(14);
            hora_corte = hora_corte.AddMinutes(0);
            hora_corte = hora_corte.AddSeconds(0);

            double horas_trabajadas = 0;
            double horas_faltantes = 0;


            //CASO1:Cuando registren un ticket entre las 2pm(hora de corte) y 7 pm(fin de jornada)
            if (Fecha_termino_ult_ticket >= hora_corte && Fecha_termino_ult_ticket <= fin_jornada)
            {

                horas_trabajadas = (fin_jornada - Fecha_termino_ult_ticket).TotalHours;
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                if (horas_trabajadas <= 5)
                {
                    horas_faltantes = 5 - horas_trabajadas;

                }
                Fecha_reg_tckt_actual_final = ini_jornada.AddDays(1);


                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(horas_faltantes);

                //return Fecha_reg_tckt_actual;
                //Si el ticket anterior finaliza un viernes, la fecha prevista del nuevo ticket es lunes
                dia_semana = (int)Fecha_reg_tckt_actual_final.DayOfWeek;

                if (dia_semana > 5)//sabado
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays((6 - dia_semana) + 2);
                }
                if (dia_semana == 0)//domingo
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays(1);
                }

                //Fin

                goto fin;
            }
            //CASO2:Cuando registren un ticket despues de 7 pm(fin de jornada)
            if (Fecha_termino_ult_ticket > fin_jornada)
            {
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                //fecha prevista= 5 horas despues del inicio de jornada del dia siguiente del registro del ticket
                Fecha_reg_tckt_actual_final = ini_jornada.AddDays(1);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(5);
                //return Fecha_reg_tckt_actual;
                //Si el ticket anterior finaliza un viernes, la fecha prevista del nuevo ticket es lunes
                dia_semana = (int)Fecha_reg_tckt_actual_final.DayOfWeek;
                if (dia_semana > 5)//sabado
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays((6 - dia_semana) + 2);
                }
                if (dia_semana == 0)//domingo
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays(1);
                }
                //Fin
                goto fin;
            }
            //CASO3:Cuando registren un ticket antes del inicio de jornada desde las 0:00 horas
            if (Fecha_termino_ult_ticket < ini_jornada)
            {
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                //fecha prevista= 5 horas despues del inicio de jornada en el dia del registro del ticket
                Fecha_reg_tckt_actual_final = ini_jornada.AddHours(5);
                //return Fecha_reg_tckt_actual;
                //Si el ticket anterior finaliza un viernes, la fecha prevista del nuevo ticket es lunes
                dia_semana = (int)Fecha_reg_tckt_actual_final.DayOfWeek;
                if (dia_semana > 5)//sabado
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays((6 - dia_semana) + 2);
                }
                if (dia_semana == 0)//domingo
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays(1);
                }
                //Fin
                goto fin;


            }
            //CASO4:Registro del nuevo ticket antes del fin de corte
            //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
            Fecha_reg_tckt_actual_final = Fecha_termino_ult_ticket.AddHours(5);
            dia_semana = (int)Fecha_reg_tckt_actual_final.DayOfWeek;
            if (dia_semana > 5)//sabado
            {
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays((6 - dia_semana) + 2);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.Date;
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(8);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddMinutes(30);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddSeconds(0);
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(5);
            }
            if (dia_semana == 0)//Domingo
            {
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays(1);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.Date;
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(8);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddMinutes(30);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddSeconds(0);
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(5);
            }
        fin:
            return Fecha_reg_tckt_actual_final;
        }
        
        
        protected void Button7_Click(object sender, EventArgs e)
        {

            //Luego de seleccionar un job refrescar los label de "Sub service line " y "Gerente" antes del 19/09/2019
            //Luego de seleccionar un job refrescar los label de "Sub service line " luego del 19/09/2019
            //if (!IsPostBack)
            //string job = Select2.Value.ToString().Substring(0, 8);
            {//Actualizacion de Sub services line & Gerente

                //Subserviceline


                TextBox6.Text = "BTC";

                //Manager dejado sin efecto
                //QueryString = "Select distinct Jobs.Manager_name from usuarios,Jobs,usuarios_job where Jobs.Estado=1 and usuarios.correo=usuarios_job.correo and usuarios_job.Number_engagement=Jobs.Number_engagement and usuarios.correo='" + UserName_ey + "' and Jobs.Number_engagement=" + job;
                //myCommand = new SqlDataAdapter(QueryString, myConnection);
                //ds = new DataSet();
                //myCommand.Fill(ds);

                //command = new SqlCommand(QueryString, myConnection);
                //string Manager_name = Convert.ToString(command.ExecuteScalar());

                //TextBox8.Text = Manager_name;
                /*
                QueryString = "Select distinct Jobs.Number_engagement, CONCAT(Jobs.Number_engagement,' - ',Jobs.Client_engagement) as Job_  from Jobs  where Jobs.Estado=1 and Jobs.Manager_name  like '" + Select1.Value + "'";
                myCommand = new SqlDataAdapter(QueryString, myConnection);
                ds = new DataSet();
                myCommand.Fill(ds);

                Select2.DataSource = ds;
                Select2.DataTextField = "Job_";
                Select2.DataValueField = "Number_engagement";
                Select2.DataBind();
                */
                ServiceReference1.WSSoapClient wS = new ServiceReference1.WSSoapClient();
                DataSet ds = new DataSet();
                ds = wS.LLenarListaJob(Select1.Value);
                Select2.DataSource = ds;
                Select2.DataTextField = "Job_";
                Select2.DataValueField = "Number_engagement";
                Select2.DataBind();


            }


        }



        protected void Button8_Click(object sender, EventArgs e)
        {

            #region Boton registrar ticket
            if (Select2.Value != "")
            {
                ServiceReference1.WSSoapClient wS_ = new ServiceReference1.WSSoapClient();
                TextBox6.Text = wS_.Subserviceline(Int32.Parse(Select2.Value.Substring(0, 8)));
                //try
                //{
                #region variables
                //TextBox7.Text = Select2.Value.ToString().Substring(0,8);
                string job = Select2.Value.ToString().Substring(0, 8);
                //
                DateTime ahora, fecha_prevista;
                ahora = DateTime.Now;//Fecha_reg_tckt_actual
                                     //El tiempo de atencion del ticket registrado empieza al finalizar la petición de trabajo anterior. Simula un proceso donde se forma fila para ser atendido por una sola estacion de trabajo (Cajero*)
                                     ////SqlConnection conn = new SqlConnection();
                                     ////conn.ConnectionString ="Server=PE2349330W3\\SQLEXPRESS;Database=Ticket_DgTAX;Trusted_Connection=True;";
                                     ////SqlCommand com = new SqlCommand("select top 1 Fecha_prevista from Ticket order by fecha_prevista desc", conn);
                                     ////conn.Open();

                ////DateTime Ultima_fecha_prevista_ = (DateTime)com.ExecuteScalar();//Fecha_termino_ult_ticket
                ////conn.Close();
                //
                DateTime Ultima_fecha_prevista_ = wS_.Fecha_fin_ult_ticket();
                //
                //fecha_prevista = Ultima_fecha_prevista_;
                //1=24hrs  o.125=3horas
                string Libros_seleccionados = "";

                string LEM_Prueba_compras = "no";
                string LEM_Tablero_compras = "no";
                string LEM_Prueba_ventas = "no";
                string LEM_Tablero_ventas = "no";
                string LEM_Prueba_diario = "no";
                string LEM_Tablero_diario = "no";

                string LEA_Prueba_activofijo = "no";
                string LEA_Tablero_activofijo = "no";
                string LEA_Prueba_kardex = "no";
                string LEA_Tablero_kardex = "no";
                string LEA_Prueba_inventariobalance = "no";
                string LEA_Tablero_inventariobalance = "no";
                string LEA_Prueba_costos = "no";
                string LEA_Tablero_costos = "no";

                string RDJ_Prueba_kardex = "no";
                string RDJ_Tablero_kardex = "no";
                string RDJ_Prueba_compras = "no";
                string RDJ_Tablero_compras = "no";
                string RDJ_Prueba_diario5_1 = "no";
                string RDJ_Tablero_diario5_1 = "no";
                string RDJ_Prueba_diario5_3 = "no";
                string RDJ_Tablero_diario5_3 = "no";

                string LEA_Prueba_kardex_adic = "no";
                string LEA_Tablero_kardex_adic = "no";
                string RDJ_eficiencia = "no";
                string RDJ_extendido = "no";

                string XML = "no";
                string SIAF = "no";
                #endregion
                //
                #region XML TEMPORALES
                string XML_compras = "no";
                string XML_ventas = "no";

                #endregion
                //
                #region Cruce_FV_LE
                string Cruce_FV_LE = "no";
                #endregion

                //RDJ 0 y 1 , 2 Detracciones , 3:Pruebas DJ y 4: XML
                if (RadioButtonList2.Items[0].Selected || RadioButtonList2.Items[1].Selected || RadioButtonList2.Items[2].Selected || RadioButtonList2.Items[3].Selected || RadioButtonList2.Items[4].Selected || RadioButtonList2.Items[5].Selected || RadioButtonList2.Items[6].Selected || RadioButtonList2.Items[7].Selected)
                {
                    //fecha_prevista = Devolver_fecha_prevista(ahora, Ultima_fecha_prevista_); Ultima_fecha_prevista_ = fecha_prevista;

                    fecha_prevista = ahora.AddDays(3);
                    Ultima_fecha_prevista_ = fecha_prevista;

                    if (RadioButtonList2.Items[0].Selected)
                    {
                        //RDJ 5 pruebas
                        RadioButtonList2.Items[0].Selected = false; Libros_seleccionados = "3";
                        RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "si"; RDJ_Prueba_diario5_1 = "si"; RDJ_Prueba_diario5_3 = "si"; RDJ_eficiencia = "si";  //el combo RDJ_ eficiencia reemplaza a RDJ_kardex adicional cambio 23/09/2019
                    }
                    if (RadioButtonList2.Items[1].Selected)
                    {

                        //RDJ 20 pruebas
                        RadioButtonList2.Items[1].Selected = false; RadioButtonList2.Items[2].Selected = false; RadioButtonList2.Items[3].Selected = false; RadioButtonList2.Items[4].Selected = false; Libros_seleccionados = "3";
                        RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "si"; RDJ_Prueba_diario5_1 = "si"; RDJ_Prueba_diario5_3 = "si"; RDJ_extendido = "si"; //el combo RDJ_ extendido reemplaza a RDJ_kardex adicional cambio 23/09/2019
                    }
                    if (RadioButtonList2.Items[2].Selected)
                    {

                        //SIAF
                        RadioButtonList2.Items[1].Selected = false; RadioButtonList2.Items[2].Selected = false; RadioButtonList2.Items[3].Selected = false; RadioButtonList2.Items[4].Selected = false; Libros_seleccionados = "0";
                        RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "no"; RDJ_Prueba_diario5_1 = "no"; RDJ_Prueba_diario5_3 = "no"; RDJ_extendido = "no"; //el combo RDJ_ extendido reemplaza a RDJ_kardex adicional cambio 23/09/2019
                        XML = "no"; SIAF = "si";
                    }

                    if (RadioButtonList2.Items[4].Selected)
                    {

                        //XML COMPRAS - Ventas
                        RadioButtonList2.Items[1].Selected = false; RadioButtonList2.Items[2].Selected = false; RadioButtonList2.Items[3].Selected = false; RadioButtonList2.Items[4].Selected = false; Libros_seleccionados = "0";
                        RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "no"; RDJ_Prueba_diario5_1 = "no"; RDJ_Prueba_diario5_3 = "no"; RDJ_extendido = "no"; //el combo RDJ_ extendido reemplaza a RDJ_kardex adicional cambio 23/09/2019
                        XML = "si";
                    }
                    if (RadioButtonList2.Items[5].Selected)
                    {

                        //XML  Ventas
                        RadioButtonList2.Items[1].Selected = false; RadioButtonList2.Items[2].Selected = false; RadioButtonList2.Items[3].Selected = false; RadioButtonList2.Items[4].Selected = false;
                        RadioButtonList2.Items[5].Selected = false;
                        RadioButtonList2.Items[6].Selected = false;
                        Libros_seleccionados = "0";
                        RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "no"; RDJ_Prueba_diario5_1 = "no"; RDJ_Prueba_diario5_3 = "no"; RDJ_extendido = "no"; //el combo RDJ_ extendido reemplaza a RDJ_kardex adicional cambio 23/09/2019
                        XML = "no";
                        XML_compras = "no";
                        XML_ventas = "si";
                    }
                    if (RadioButtonList2.Items[6].Selected)
                    {

                        //XML COMPRAS
                        RadioButtonList2.Items[1].Selected = false; RadioButtonList2.Items[2].Selected = false; RadioButtonList2.Items[3].Selected = false; RadioButtonList2.Items[4].Selected = false;
                        RadioButtonList2.Items[5].Selected = false;
                        RadioButtonList2.Items[6].Selected = false;
                        Libros_seleccionados = "0";
                        RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "no"; RDJ_Prueba_diario5_1 = "no"; RDJ_Prueba_diario5_3 = "no"; RDJ_extendido = "no"; //el combo RDJ_ extendido reemplaza a RDJ_kardex adicional cambio 23/09/2019
                        XML = "no";
                        XML_compras = "si";
                        XML_ventas = "no";
                    }
                    if (RadioButtonList2.Items[7].Selected)
                    {

                        //Cruce FV y LE
                        //NO IMPLEMENTADO
                        
                        RadioButtonList2.Items[1].Selected = false; RadioButtonList2.Items[2].Selected = false; RadioButtonList2.Items[3].Selected = false; RadioButtonList2.Items[4].Selected = false; Libros_seleccionados = "0";
                        RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "no"; RDJ_Prueba_diario5_1 = "no"; RDJ_Prueba_diario5_3 = "no"; RDJ_extendido = "no"; //el combo RDJ_ extendido reemplaza a RDJ_kardex adicional cambio 23/09/2019
                        XML = "no";
                        Cruce_FV_LE = "si";
                    }
                    //for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                    //{
                    //    if (CheckBoxList2.Items[i].Selected)
                    //    {
                    //        //fecha_prevista = fecha_prevista.AddHours(5);

                    //        Libros_seleccionados = i.ToString();
                    //        if (i == 0)
                    //        { RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "si"; RDJ_Prueba_diario5_1 = "si"; RDJ_Prueba_diario5_3 = "si"; RDJ_eficiencia = "si"; } //el combo RDJ_ eficiencia reemplaza a RDJ_kardex adicional cambio 23/09/2019
                    //        if (i == 1 || i == 2 || i == 3 || i == 4)
                    //        { RDJ_Prueba_kardex = "no"; RDJ_Prueba_compras = "si"; RDJ_Prueba_diario5_1 = "si"; RDJ_Prueba_diario5_3 = "si"; RDJ_extendido = "si"; }//el combo RDJ_ extendido reemplaza a RDJ_kardex adicional cambio 23/09/2019
                    //desmarcar
                    //        CheckBoxList2.Items[i].Selected = false;
                    //    }
                    //}
                }

                //Libros mensuales
                if (CheckBoxList3.Items[0].Selected || CheckBoxList3.Items[1].Selected || CheckBoxList3.Items[2].Selected)
                {
                    //fecha_prevista = Devolver_fecha_prevista(ahora, Ultima_fecha_prevista_); Ultima_fecha_prevista_ = fecha_prevista;
                    fecha_prevista = ahora.AddDays(3);
                    Ultima_fecha_prevista_ = fecha_prevista;
                    for (int i = 0; i < CheckBoxList3.Items.Count; i++)
                        if (CheckBoxList3.Items[i].Selected)
                        {
                            //fecha_prevista = fecha_prevista.AddHours(5);
                            Libros_seleccionados = i.ToString();

                            if (i == 0)
                            { LEM_Prueba_compras = "si"; }
                            if (i == 1)
                            { LEM_Prueba_ventas = "si"; }
                            if (i == 2)
                            { LEM_Prueba_diario = "si"; }
                            //Desmarcar
                            CheckBoxList3.Items[i].Selected = false;

                        }
                }

                //Libros anuales
                for (int i = 0; i < RadioButtonList1.Items.Count; i++)
                    if (RadioButtonList1.Items[i].Selected)
                    {
                        //fecha_prevista = Devolver_fecha_prevista(ahora, Ultima_fecha_prevista_); Ultima_fecha_prevista_ = fecha_prevista;
                        fecha_prevista = ahora.AddDays(3);
                        Ultima_fecha_prevista_ = fecha_prevista;
                        Libros_seleccionados = i.ToString();

                        if (i == 0)
                        { LEA_Prueba_activofijo = "si"; }
                        if (i == 1)
                        { LEA_Prueba_kardex = "si"; }
                        if (i == 2)
                        { LEA_Prueba_inventariobalance = "si"; }
                        if (i == 3)
                        { LEA_Prueba_costos = "si"; }
                        if (i == 4)
                        { LEA_Prueba_kardex_adic = "si"; }
                        //desmarcar
                        RadioButtonList1.Items[i].Selected = false;

                    }

                //if (TextBox6.Text != "" && int.Parse(TextBox7.Text) > 0 && TextBox8.Text != "" && TextBox9.Text != "")
                //if (TextBox6.Text != "" && int.Parse(job) > 0 && TextBox8.Text != "" && TextBox9.Text != "")
                if (TextBox6.Text != "" && int.Parse(job) > 0 && Select1.Value != "" && TextBox9.Text != "")
                    if (Libros_seleccionados != "")
                    {
                        Libros_seleccionados = "";
                        ServiceReference1.WSSoapClient wS = new ServiceReference1.WSSoapClient();
                        //string resultado = wS.RegistroTicket_v2(TextBox6.Text, int.Parse(TextBox7.Text), TextBox8.Text, TextBox9.Text, ahora, fecha_prevista,
                        //string resultado = wS.RegistroTicket_v2(TextBox6.Text, int.Parse(job), TextBox8.Text, TextBox9.Text, ahora, Ultima_fecha_prevista_,
                        string resultado = wS.RegistroTicket_v2(TextBox6.Text, int.Parse(job), Select1.Value, TextBox9.Text, ahora, Ultima_fecha_prevista_,
                             LEM_Prueba_compras,
                                 LEM_Tablero_compras,
                                 LEM_Prueba_ventas,
                                 LEM_Tablero_ventas,
                                 LEM_Prueba_diario,
                                 LEM_Tablero_diario,

                                 LEA_Prueba_activofijo,
                                 LEA_Tablero_activofijo,
                                 LEA_Prueba_kardex,
                                 LEA_Tablero_kardex,
                                 LEA_Prueba_inventariobalance,
                                 LEA_Tablero_inventariobalance,
                                 LEA_Prueba_costos,
                                 LEA_Tablero_costos,

                                 RDJ_Prueba_kardex,
                                 RDJ_Tablero_kardex,
                                 RDJ_Prueba_compras,
                                 RDJ_Tablero_compras,
                                 RDJ_Prueba_diario5_1,
                                 RDJ_Tablero_diario5_1,
                                 RDJ_Prueba_diario5_3,
                                 RDJ_Tablero_diario5_3,
                                 nombrePC,
                                 LEA_Prueba_kardex_adic,
                                 LEA_Tablero_kardex_adic,
                                 RDJ_eficiencia, RDJ_extendido,
                                 XML,
                                 XML_compras,
                                 XML_ventas,
                                 Cruce_FV_LE,
                                 SIAF);

                        TextBox10.Text = resultado;

                    }


                //}
                //catch
                //{
                //    //No se selecciono job
                //}
            }

            #endregion


            //}
            //catch
            //{
            //    //No se selecciono job o el servidor esta apagado/reiniciando, Borrar dlls del servidor y reiniciarlo (error ocurrido al lanzar el proyecto desde consola (pruebas) sin antes no suspender el servicio en servidor)
            //}

        }


        protected void textbox_KeyUp(object sender, KeyEventArgs e)
        {
            // Do whatever you need.
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down                
                VistaBuscarTicket();

                BuscarTicket();

            }
        }

        public void VistaBuscarTicket() {
            CrearTicketid.Visible = false;
            BuscarTicketid.Visible = true;

            Titulo_registrar_valor.Visible = false;
            Titulo_buscar_valor.Visible = true;

            Button2.BackColor = Color.FromArgb(190, 171, 11);
            Color _color = System.Drawing.ColorTranslator.FromHtml("#ebda00");
            Button1.BackColor = _color;
            Button5.BackColor = _color;
        }
        public void VistaGrabarTicket()
        {
            CrearTicketid.Visible = true;
            BuscarTicketid.Visible = false;

            Titulo_registrar_valor.Visible = true;
            Titulo_buscar_valor.Visible = false;


            Titulo_editarestado_valor.Visible = false;
            EditarEstadoid.Visible = false;

            Button1.BackColor = Color.FromArgb(190, 171, 11);
            Color _color = System.Drawing.ColorTranslator.FromHtml("#ebda00");
            Button2.BackColor = _color;
            Button5.BackColor = _color;
        }

        public void BuscarTicket()
        {
            try
            {
                ServiceReference1.WSSoapClient wS = new ServiceReference1.WSSoapClient();
                DataSet ds = wS.ConsultarEstado(TextBox11.Text);
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                CrearTicketid.Visible = false;
                BuscarTicketid.Visible = true;

                Titulo_registrar_valor.Visible = false;
                Titulo_buscar_valor.Visible = true;


                Titulo_editarestado_valor.Visible = false;
                EditarEstadoid.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void TicketProcesando_Init(object sender, EventArgs e)
        {
            //Traer a pantalla el ultimo ticket por procesar
            try
            {
                ServiceReference1.WSSoapClient wS = new ServiceReference1.WSSoapClient();
                TicketProcesando.Text = wS.TicketProcesando();

                NumeroProcesando.Text = wS.NumeroTicketsProcesando();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            EditarEstado();
            Button5.BackColor = Color.FromArgb(190, 171, 11);
            Color _color = System.Drawing.ColorTranslator.FromHtml("#ebda00");
            //color boton no seleccionado
            Button1.BackColor = _color;
            Button2.BackColor = _color;
        }
        public void EditarEstado()
        {
            Titulo_editarestado_valor.Visible = true;
            EditarEstadoid.Visible = true;

            Titulo_registrar_valor.Visible = false;
            Titulo_buscar_valor.Visible = false;

            BuscarTicketid.Visible = false;
            CrearTicketid.Visible = false;


        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // codigo
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            string estado = "";
            DateTime fecha = DateTime.Now;
            if (TextBox2.Text != "")
            {
                Label3.Text = "Código Requerido";
                if (listaOpcionesEstados.Items[0].Selected == true)
                {
                    //Anulado
                    estado = listaOpcionesEstados.Items[0].Value;
                    //editar en base de datos
                }
                if (listaOpcionesEstados.Items[1].Selected == true)
                {
                    //Procesando
                    estado = listaOpcionesEstados.Items[1].Value;
                    //Fecha Requerida
                    if (TextBox12.Text != "")
                    {
                        //editar ticket
                        fecha = (DateTime.Parse(TextBox12.Text)) ;
                    }
                    else
                    {
                        Label3.Text = "Fecha requerida";
                    }
                    
                }
                if (listaOpcionesEstados.Items[2].Selected == true)
                {
                    //Terminado
                    estado = listaOpcionesEstados.Items[2].Value;
                    //editar en base de datos
                }

                int idDevuelto=wS.VerificarCodigo(TextBox2.Text);
                if (idDevuelto > 0)
                {
                    wS.EditarEstado(TextBox2.Text, estado, fecha);
                    Label3.Text = "Ticket editado";
                }
                else
                {
                    Label3.Text = "Ticket no editado, revisa el codigo";
                }
                
            }

            EditarEstado();

            

        }
    }
   
}