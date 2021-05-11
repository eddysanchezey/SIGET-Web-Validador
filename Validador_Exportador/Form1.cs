using OfficeOpenXml;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Validador_Exportador
{
    public partial class Form1 : Form
    {
        public static string nombrePC = Dns.GetHostName();
        public string Cod_ticket_final = "";
        public string Estado = "";
        string usuario = "";
        string path_Libros = "";
        //Variables donde se guarda que tipo de ticket se registro
        public string LEM_Prueba_compras, LEM_Prueba_ventas, LEM_Prueba_diario, LEA_Prueba_activofijo, LEA_Prueba_kardex, LEA_Prueba_inventariobalance, LEA_Prueba_costos, RDJ_Prueba_kardex, RDJ_Prueba_compras, RDJ_Prueba_diario5_1, RDJ_Prueba_diario5_3, LEA_Prueba_kardex_adic = "";
        
        public string XML_Vent_Comp;
        public string RDJ2020,SIAF;
        public int num_RDJ_compra, num_RDJ_venta, num_RDJ_diario_5_1, num_RDJ_diario_5_3, num_LibroMayor;
        //Produccion
        //public string cadenaconexion= "Data Source= tcp:10.20.103.68,49172;Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
        //LocalS
        //PE2371665W2\\SQLEXPRESS 
        public string cadenaconexion = "Data Source= tcp:10.20.103.68,49172 ;Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
        public Form1()
        {

            InitializeComponent();
            //usuario
            //textbox3
            //password
            textBox4.PasswordChar = '*';
            //mensaje de bienvenida
            textBox5.Text = "Sesion no iniciada";
            textBox5.Visible = true;
            textBox5.Enabled = false;
            //Deshabiliatr el boton de carga archivos hasta que el login sea correcto y la lectura previa de archivos termine por parte del usuario
            button2.Enabled = false;
            //path de usuario para exportar los libros
            textBox6.Visible = false;
            //Texbox deshabilitado hasta que el login sea correcto
            textBox1.Enabled = false;
            textBox1.ScrollBars = ScrollBars.Vertical;

            //
            this.MaximumSize = new System.Drawing.Size(437, 346);
            //
            #region carpetas libros electronicos
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\PLB_NO_CVD"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\PLB_NO_CVD");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\CVD_1"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\CVD_1");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\CVD_2"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\CVD_2");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\CVD_3"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\CVD_3");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\PLB_Kardex_Adic"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\PLB_Kardex_Adic");
            #endregion
            #region carpetas xml
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\XML_Vent_Comp"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\XML_Vent_Comp");

            
            #endregion

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            //Verificar si el usuario esta activo (estado=1) y el correo-password son validos
            SqlConnection conn = new SqlConnection
            {
                //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//User ID=DemoLogin;Password=DemoLogin;Initial Catalog=DatabaseDemo;
                //conn.ConnectionString = "Data Source= tcp:10.20.103.68,49172;Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                ConnectionString = cadenaconexion
            };
            SqlCommand com = new SqlCommand("select correo from usuarios where estado=1 and correo='" + textBox3.Text + "' and contraseña='" + textBox4.Text + "'", conn);
            conn.Open();

            string ID_devuelto = (string)com.ExecuteScalar();
            conn.Close();
            //return "" + ID_devuelto;

            conn.Open();
            com = new SqlCommand("update Ticket_DgTAX.dbo.Ticket set Estado = 'Anulado' where datediff(MINUTE,Fecha_registro,GETDATE()) >= 90 and Archivos_exportados = 'no'", conn);
            int fila= (int)com.ExecuteNonQuery();
            conn.Close();
            
            // En caso el usuario no fuese valido no registrar ni consultar nada
            if (ID_devuelto == textBox3.Text)
            {
                //usuario y contraseña correctos
                textBox5.Visible = true;
                textBox5.Enabled = false;
                usuario = ID_devuelto;
                textBox5.Text = "Hola: " + ID_devuelto;
                //
                try
                {
                    #region Cuando se exporta los archivos se actualiza la tabla Tickets para que no se vuelva a cargar mas archivos usando el codigo del ticket

                    //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                    //conn.ConnectionString = "Data Source= tcp:10.20.103.68,49172;Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                    conn.ConnectionString = cadenaconexion;
                    com = new SqlCommand("select Archivos_exportados from ticket where Cod_ticket like '" + textBox2.Text + "'", conn);
                    conn.Open();

                    string Archivos_exportados = (string)com.ExecuteScalar();
                    conn.Close();
                    #endregion
                    //

                    //Verificar los datos de usuario o ticket
                    if (textBox2.Text == "" || Archivos_exportados == "si")
                    {
                        textBox1.Text = "";
                        textBox6.Text = path_Libros;
                        if (textBox2.Text == "")
                            MessageBox.Show("Ingresa el código de tu ticket, Ej: DG-999", "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (Archivos_exportados == "si")
                            MessageBox.Show("Se cargó archivos con el código: " + textBox2.Text + "\r\n" + " Ya no se puede cargar más archivos", "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Verificar el tipo de ticket selecionado
                        textBox1.Enabled = true;

                        SqlConnection conn_v = new SqlConnection
                        {
                            //conn_v.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                            //conn_v.ConnectionString = "Data Source= tcp:10.20.103.68,49172;Initial Catalog=Ticket_DgTAX; Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                            ConnectionString = cadenaconexion
                        };
                        SqlCommand com_v = new SqlCommand("select Senior from Ticket where Senior ='" + textBox3.Text + "' and  Cod_ticket like '" + textBox2.Text + "'", conn_v);
                        conn_v.Open();

                        string Senior_ = (string)com_v.ExecuteScalar();

                        conn_v.Close();
                        //
                        if (textBox3.Text == usuario && Senior_ == usuario)
                        {
                            try
                            {
                                conn.Open();
                                com = new SqlCommand("Select Estado from Ticket where Cod_ticket like '" + textBox2.Text + "'",conn);
                                Estado = (string)com.ExecuteScalar();
                                if (Estado=="Anulado")
                                {
                                    MessageBox.Show("Tu ticket esta anulado, dado que pasaron más 90 minutos desde que lo creaste y no cargaste la información.","Ticket anulado",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                                    button2.Enabled = false;
                                    return;
                                }
                                #region Verificar opciones marcadas del ticket

                                com = new SqlCommand("select LEM_Prueba_compras from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);
                                

                                LEM_Prueba_compras = (string)com.ExecuteScalar();


                                com = new SqlCommand("select LEM_Prueba_ventas from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);


                                LEM_Prueba_ventas = (string)com.ExecuteScalar();

                                com = new SqlCommand("select LEM_Prueba_diario from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);


                                LEM_Prueba_diario = (string)com.ExecuteScalar();

                                com = new SqlCommand("select LEA_Prueba_activofijo from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);


                                LEA_Prueba_activofijo = (string)com.ExecuteScalar();

                                com = new SqlCommand("select LEA_Prueba_kardex from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);


                                LEA_Prueba_kardex = (string)com.ExecuteScalar();

                                com = new SqlCommand("select LEA_Prueba_inventariobalance from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);


                                LEA_Prueba_inventariobalance = (string)com.ExecuteScalar();

                                com = new SqlCommand("select LEA_Prueba_costos from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);


                                LEA_Prueba_costos = (string)com.ExecuteScalar();

                                com = new SqlCommand("select RDJ_Prueba_kardex from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);


                                RDJ_Prueba_kardex = (string)com.ExecuteScalar();

                                com = new SqlCommand("select RDJ_Prueba_diario5_1 from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);

                                RDJ_Prueba_diario5_1 = (string)com.ExecuteScalar();

                                com = new SqlCommand("select RDJ_Prueba_diario5_3 from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);

                                RDJ_Prueba_diario5_3 = (string)com.ExecuteScalar();

                                com = new SqlCommand("select LEA_Prueba_kardex_adic from  ticket where Cod_ticket='" + textBox2.Text + "' and Senior='" + textBox3.Text + "'", conn);

                                string LEA_Prueba_kardex_adic = (string)com.ExecuteScalar();

                                com=new SqlCommand("select XML_Vent_Comp from ticket where Cod_ticket='" + textBox2.Text + "' ", conn);

                                XML_Vent_Comp = (string)com.ExecuteScalar();

                                com = new SqlCommand("select RDJ_extendido from ticket where Cod_ticket='" + textBox2.Text + "' ", conn);

                                RDJ2020 = (string)com.ExecuteScalar();

                                com = new SqlCommand("select SIAF from ticket where Cod_ticket='" + textBox2.Text + "' ", conn);
                                SIAF = (string)com.ExecuteScalar();
                                
                                conn.Close();
                                #endregion

                                //Ruta donde el usuario debe colocar los archivos
                                path_Libros = @"J:\COMMON\" + textBox3.Text.Replace(".", "_");
                                path_Libros = path_Libros.Replace("@", "_");
                                if (!Directory.Exists(path_Libros))
                                    Directory.CreateDirectory(path_Libros);

                                //path de usuario para exportar los libros 
                                textBox6.Visible = true;
                                textBox6.Text = path_Libros;
                                #region Asignar mensaje de aviso al usuario de los archivos que requiere cargar segun el ticekt 
                                textBox1.Text = "";
                                
                                if (LEM_Prueba_compras == "si")
                                { textBox1.Text += "Cargar libros de compras: " + "\r\n"; /*MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/ }
                                if (LEM_Prueba_ventas == "si")
                                { textBox1.Text += "Cargar libros de ventas: " + "\r\n"; /*MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/ }
                                if (LEM_Prueba_diario == "si")
                                { textBox1.Text += "Cargar libros de diario: " + "\r\n"; /*MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/ }
                                if (LEA_Prueba_activofijo == "si")
                                { textBox1.Text += "Cargar libros de activo fijo: " + "\r\n"; MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                if (LEM_Prueba_diario == "si" || LEM_Prueba_compras == "si" || LEM_Prueba_compras == "si")
                                {
                                    MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                if (LEA_Prueba_inventariobalance == "si")
                                { textBox1.Text += "Cargar libros de inventario y balance: " + "\r\n"; MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                if (LEA_Prueba_kardex == "si")
                                { textBox1.Text += "Cargar libros de Kardex y Establecimientos anexos en excel: " + "\r\n"; MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                //textBox1.Text += "Cargar libros de kardex: " + LEA_Prueba_kardex + "\r\n";

                                /*
                                if (RDJ_Prueba_kardex == "si" || RDJ_Prueba_diario5_1 == "si" || RDJ_Prueba_diario5_3 == "si")
                                { textBox1.Text += "Cargar libros obligados de Ventas, Compras , Diario 5.1 y Diario 5.3 " + "\r\n"; MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                */
                                if (RDJ2020 == "si")
                                { 
                                    textBox1.Text += "Cargar libros obligados de No Domiciliados, Compras , Ventas y Libro Mayor " + "\r\n"; MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                                }
                                if (SIAF == "si")
                                {
                                    textBox1.Text += "Cargar los formatos obliatorios de Data_Tributaria y Data_Financiera" + "\r\n";

                                }


                                //if (RDJ_Prueba_diario5_1 == "si")
                                //{ textBox1.Text += "Ventas, Kardex, Compras , Diario 5.1 Diario 5.3: " + "\r\n"; MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                //if (RDJ_Prueba_diario5_3 == "si")
                                //{ textBox1.Text += "Ventas, Kardex, Compras , Diario 5.1 Diario 5.3: " + "\r\n"; MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                if (LEA_Prueba_kardex_adic == "si")
                                { textBox1.Text += "Cargar libros obligados de Ventas, Kardex, Compras y Diario : " + "\r\n"; MessageBox.Show(textBox1.Text, "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                //Escritura en Textbox de los archivos permitidos para cargar segun el numero de Ticket
                                textBox1.Text += "Mis archivos estan en: " + path_Libros + "\r\n";
                                textBox1.Text += "File_name_PLE_SUNAT" + "|" + "Tipo_Libro" + "|" + "Size en bytes" + "\r\n";
                                #endregion
                                
                                //Leer los archivos colocados en la ruta por el usuario
                                string ruc, año, mes, dia , lastnumber;
                                num_RDJ_compra = 0; num_RDJ_venta = 0; num_RDJ_diario_5_1 = 0; num_RDJ_diario_5_3 = 0;
                                num_LibroMayor = 0;
                                DirectoryInfo di = new DirectoryInfo(path_Libros);
                                int flg_DatTribut=0, flgDatFinan=0;
                                var rucLE = "";
                                var añoLE = "";
                                foreach (var file in di.GetFiles()){
                                    if (file.Name.Length == 37 &&  file.Name.Substring(30, 1) == "2")
                                    {
                                        MessageBox.Show("Las P.E no analizan los L.E en dólares ( LE________#2###.ttx )" + "\r\n" + "Por favor retirar el txt: " +file.Name);
                                        button2.Enabled = false;
                                        return;
                                    }
                                    if (file.Name.Length == 37 && (file.Name.Substring(29, 1) == "0" || file.Name.Substring(29, 1) == "1") && file.Length==0)
                                    {
                                        MessageBox.Show("Las P.E requieren que los libros tengan como minimo los campos con palotes vacios ( | ) " + "\r\n" + "Por favor retirar o editar el txt: " + file.Name);
                                        button2.Enabled = false;
                                        return;
                                    }
                                    if (file.Name.Length == 23 && file.Length == 0 )
                                    {
                                        //Libro Mayor LM
                                        if (int.TryParse(file.Name.Substring(3, 11), out _)){
                                            MessageBox.Show("Las P.E requieren que el Libro Mayor contenga el RUC del cliente segun el formato LM_RRRRRRRRRRRR_YYYY.txt" + "\r\n" + "Por favor editar el txt: " + file.Name);
                                            button2.Enabled = false;
                                            return;
                                        }
                                        if (int.TryParse(file.Name.Substring(14, 4), out _))
                                        {
                                            MessageBox.Show("Las P.E requieren que el Libro Mayor contenga el Año del cliente segun el formato LM_RRRRRRRRRRRR_YYYY.txt" + "\r\n" + "Por favor editar el txt: " + file.Name);
                                            button2.Enabled = false;
                                            return;
                                        }
                                        MessageBox.Show("Las P.E requieren que el Libro Mayor contengaN REGISTROS en  el formato LM_RRRRRRRRRRRR_YYYY.txt" + "\r\n" + "Por favor editar el txt: " + file.Name);
                                        button2.Enabled = false;
                                        return;
                                    }
                                    if (file.Name.Length == 37 && file.Length > 0 && file.Extension.Contains("txt") && file.Length > 0)
                                    {
                                        rucLE = file.Name.Substring(2, 11);
                                        añoLE = file.Name.Substring(13, 4);
                                        break;
                                    }
                                }
                                foreach (var fi in di.GetFiles())
                                {

                                    var rucLM = "";
                                    var añoLM = "";
                                    //Libro Mayo o LM
                                    if (fi.Name.Length == 23 && fi.Length > 0 && fi.Extension.Contains("txt") && fi.Length > 0)
                                    {
                                        rucLM = fi.Name.Substring(3, 11);
                                        añoLM = fi.Name.Substring(14, 4);

                                        if (rucLM != rucLE || añoLM != añoLE)
                                        {
                                            MessageBox.Show( "Se identifico más de un Libro Mayor con RUC distinto"+ "\r\n"+"Por favor colocar el Libro Mayor en el formato LM_RRRRRRRRRR_YYYY.txt con el año:" + añoLE + " y ruc " + rucLE , "LM cargado: " + fi.Name);
                                            button2.Enabled = false;
                                            return;
                                        }

                                            

                                    }
                                    
                                    
                                    #region lectura libros electronicos txt
                                    if ( (fi.Name.Length == 37 ) && fi.Length > 0 && fi.Extension.Contains("txt") && fi.Length>0  )
                                    {
                                        
                                        ruc = fi.Name.Substring(2, 11);
                                        año = fi.Name.Substring(13, 4);
                                        mes = fi.Name.Substring(17, 2);
                                        dia = fi.Name.Substring(19, 2);

                                        lastnumber = fi.Name.Substring(29, 4);

                                        if(ruc!=rucLE || año != añoLE)
                                        {
                                            MessageBox.Show("Por favor colocar el Libro cargado  debe tener el año:" + añoLE + " y ruc " + rucLE, "Libro cargado: " + fi.Name);
                                            button2.Enabled = false;
                                            return;
                                        }


                                        if (lastnumber.Substring(1, 1) == "2")
                                        {
                                            MessageBox.Show("Por favor retirar el Libro " + fi.Name+ " dado esta expresado  en dolares (2) LERRRRRRRRRRRRYYYTMMDD____#2###.txt", "Ver " + fi.Name);
                                            button2.Enabled = false;
                                            return;
                                        }
                                        var tipLibo = fi.Name.Substring(21, 4);
                                        


                                        #region Libros mensuales
                                        if (LEM_Prueba_compras == "si")
                                        { //LEM_Prueba_compras = "si"; 
                                            if (fi.Name.Substring(21, 2) == "08")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEM_Prueba_compras" + "|" + fi.Length + "\r\n";
                                                if (!File.Exists(fi.DirectoryName + @"\" +"LE" + ruc + "" + año + "" + mes+""+dia + "0501"+fi.Name.Substring(25,12)))
                                                {
                                                    MessageBox.Show("Por favor colocar El libro Diario (5.1) TXT de la compalia o el Diario Vacio : " + "LE" + ruc + "" + año + "" + mes + "" + dia + "0501" + fi.Name.Substring(25, 12));
                                                    button2.Enabled = false;
                                                    return;
                                                }
                                            }
                                        }
                                        if (LEM_Prueba_ventas == "si")
                                        { //LEM_Prueba_ventas = "si";
                                            if (fi.Name.Substring(21, 2) == "14")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEM_Prueba_ventas" + " | " + fi.Length + "\r\n";
                                            }
                                            if (!File.Exists(fi.DirectoryName + @"\" + "LE" + ruc + "" + año + "" + mes + "" + dia + "0501" + fi.Name.Substring(25, 12)))
                                            {
                                                MessageBox.Show("Por favor colocar El libro Diario (5.1) TXT de la compalia o el Diario Vacio : " + "LE" + ruc + "" + año + "" + mes + "" + dia + "0501" + fi.Name.Substring(25, 12));
                                                button2.Enabled = false;
                                                return;
                                            }

                                        }
                                        if (LEM_Prueba_diario == "si")
                                        { //LEM_Prueba_diario = "si"; 
                                            if (fi.Name.Substring(21, 2) == "05")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEM_Prueba_diario" + " | " + fi.Length + "\r\n";
                                            }

                                        }

                                        #endregion

                                        #region Libro_anual
                                        if (LEA_Prueba_activofijo == "si")
                                        {
                                            if (fi.Name.Substring(21, 2) == "07")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEA_Prueba_activofijo" + "|" + fi.Length + "\r\n";
                                            }


                                        }
                                        if (LEA_Prueba_kardex == "si")
                                        {
                                            if (fi.Name.Substring(21, 2) == "13")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEA_Prueba_kardex" + "|" + fi.Length + "\r\n";
                                                if (File.Exists(path_Libros + @"\Establecimiento_" + ruc + "_" + año+ ".xlsx"))
                                                {
                                                    textBox1.Text += "Nombre de archivo correcto: " + "Establecimiento_" + ruc + "_" + año + ".xlsx" + "\r\n";
                                                }
                                                else
                                                {
                                                    textBox1.Text += "Por favor colocar el anexo de establecimientos segun el formato: " + "Establecimiento_" + ruc + "_" + año + ".xlsx" + " Sino se usara establecimientos vaciós" + "\r\n";
                                                    MessageBox.Show("Por favor colocar el anexo de establecimientos segun el formato: " + "Establecimiento_" + ruc + "_" + año + ".xlsx", path_Libros + @"\LM_RRRRRRRRRR_AAAA.txt");
                                                    button2.Enabled = false;
                                                    return;
                                                }

                                            }


                                        }
                                        if (LEA_Prueba_inventariobalance == "si")
                                        {
                                            if (fi.Name.Substring(21, 2) == "03")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEA_Prueba_inventariobalance" + "|" + fi.Length + "\r\n";
                                            }


                                        }
                                        if (LEA_Prueba_costos == "si")
                                        {
                                            if (fi.Name.Substring(21, 2) == "10")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEA_Prueba_costos" + "|" + fi.Length + "\r\n";
                                            }

                                        }
                                        if (LEA_Prueba_kardex_adic == "si")
                                        {
                                            if (fi.Name.Substring(21, 2) == "13")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEA_Prueba_kardex_adic" + "|" + fi.Length + "\r\n";
                                            }
                                            if (fi.Name.Substring(21, 2) == "08")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEA_Prueba_kardex_adic" + "|" + fi.Length + "\r\n";
                                                if (!File.Exists(fi.DirectoryName + @"\" + "LE" + ruc + "" + año + "" + mes + "" + dia + "0501" + fi.Name.Substring(25, 12)))
                                                {
                                                    MessageBox.Show("Por favor colocar El libro Diario (5.1) TXT de la compalia o el Diario Vacio : " + "LE" + ruc + "" + año + "" + mes + "" + dia + "0501" + fi.Name.Substring(25, 12));
                                                    button2.Enabled = false;
                                                    return;
                                                }
                                            }
                                            if (fi.Name.Substring(21, 2) == "14")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEA_Prueba_kardex_adic" + "|" + fi.Length + "\r\n";
                                                if (!File.Exists(fi.DirectoryName + @"\" + "LE" + ruc + "" + año + "" + mes + "" + dia + "0501" + fi.Name.Substring(25, 12)))
                                                {
                                                    MessageBox.Show("Por favor colocar El libro Diario (5.1) TXT de la compalia o el Diario Vacio : " + "LE" + ruc + "" + año + "" + mes + "" + dia + "0501" + fi.Name.Substring(25, 12));
                                                    button2.Enabled = false;
                                                    return;
                                                }
                                            }
                                            if (fi.Name.Substring(21, 4) == "0501")
                                            {
                                                textBox1.Text += fi.Name + "|" + "LEA_Prueba_kardex_adic" + "|" + fi.Length + "\r\n";
                                            }

                                        }
                                        #endregion

                                        #region libros_rdj version 2019
                                        /*
                                        if (RDJ_Prueba_kardex == "si" || RDJ_Prueba_diario5_1 == "si" || RDJ_Prueba_diario5_3 == "si")
                                        {
                                            if (fi.Name.Substring(21, 2) == "13")
                                            {
                                                //Se  comenta esta parte dado que se elimino el nodo de kardex que importa modeler deacuerdo a la reunion con Glenda, Jose Barja ,Nolan A y Luis Romero el 19/9/2019 de 3 a 7 pm
                                                // textBox1.Text += fi.Name + "|" + "RDJ_Prueba_kardex" + "|" + fi.Length + "\r\n";
                                            }
                                            if (fi.Name.Substring(21, 2) == "08")
                                            {
                                                textBox1.Text += fi.Name + "|" + "RDJ_Prueba_Compra" + "|" + fi.Length + "\r\n";

                                                if (File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21)+"1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                                {
                                                    //Existe el libro electronico ventas par (mismo cliente y periodo) de compras
                                                    num_RDJ_compra += 1;
                                                }
                                                else
                                                {
                                                    textBox1.Text += "Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!";
                                                    button2.Enabled = false;
                                                    MessageBox.Show("Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!", "No puedes continuar hasta completar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                                                    return;
                                                }
                                            }
                                            if (fi.Name.Substring(21, 2) == "14")
                                            {
                                                textBox1.Text += fi.Name + "|" + "RDJ_Prueba_Venta" + "|" + fi.Length + "\r\n";
                                                if (File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                                {
                                                    //Existe el libro electronico ventas par (mismo cliente y periodo) de compras
                                                    num_RDJ_venta += 1;
                                                    
                                                }
                                                else
                                                {
                                                    textBox1.Text += "Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!";
                                                    button2.Enabled = false;
                                                    MessageBox.Show("Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" +"en la ruta!!", "No puedes continuar hasta completar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                    return;
                                                }
                                            }
                                            if (fi.Name.Substring(21, 4) == "0501")
                                            {
                                                textBox1.Text += fi.Name + "|" + "RDJ_Prueba_diario5_1" + "|" + fi.Length + "\r\n";
                                                if (File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)) && File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                                {
                                                    //Existe el libro electronico ventas par (mismo cliente y periodo) de compras
                                                    num_RDJ_diario_5_1 += 1;
                                                    
                                                }
                                                else
                                                {
                                                    if (!File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                                    {
                                                        textBox1.Text += "Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!";
                                                        button2.Enabled = false;
                                                        MessageBox.Show("Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!", "No puedes continuar hasta completar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                        return;
                                                    }
                                                    if (!File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                                    {
                                                        textBox1.Text += "Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!";
                                                        button2.Enabled = false;
                                                        MessageBox.Show("Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!", "No puedes continuar hasta completar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                        return;
                                                    }

                                                }
                                            }
                                            if (fi.Name.Substring(21, 4) == "0503")
                                            {
                                                textBox1.Text += fi.Name + "|" + "RDJ_Prueba_diario5_3" + "|" + fi.Length + "\r\n";
                                                if (File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)) && File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                                {
                                                    //Existe el libro electronico ventas par (mismo cliente y periodo) de compras
                                                    num_RDJ_diario_5_3 += 1;
                                                }
                                                else
                                                {
                                                    if (!File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                                    {
                                                        textBox1.Text += "Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!";
                                                        button2.Enabled = false;
                                                        MessageBox.Show("Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!", "No puedes continuar hasta completar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                        return;
                                                    }
                                                    if (!File.Exists(path_Libros + "\\" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                                    {
                                                        textBox1.Text += "Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!";
                                                        button2.Enabled = false;
                                                        MessageBox.Show("Se requiere el archivo: " + "\r\n" + fi.Name.Substring(0, 21) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12) + "\r\n" + "en la ruta!!", "No puedes continuar hasta completar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                        return;
                                                    }

                                                }
                                            }

                                            
                                        }
                                        */
                                        #endregion

                                        #region Libros RDJ2020
                                        if (RDJ2020 == "si")
                                        {
                                            if (fi.Name.Substring(21, 2) == "08" || fi.Name.Substring(21, 2) == "14" )
                                            {
                                                //Se  comenta esta parte dado que se elimino el nodo de kardex que importa modeler deacuerdo a la reunion con Glenda, Jose Barja ,Nolan A y Luis Romero el 19/9/2019 de 3 a 7 pm
                                                if(fi.Name.Substring(21, 4) == "0801")
                                                {
                                                    // textBox1.Text += fi.Name + "|" + "RDJ_Prueba_kardex" + "|" + fi.Length + "\r\n";
                                                    textBox1.Text += fi.Name + "|" + "RDJ2020_Prueba_Compras" + "|" + fi.Length + "\r\n";

                                                }
                                                if (fi.Name.Substring(21, 4) == "0802")
                                                {
                                                    // textBox1.Text += fi.Name + "|" + "RDJ_Prueba_kardex" + "|" + fi.Length + "\r\n";
                                                    textBox1.Text += fi.Name + "|" + "RDJ2020_Prueba_NoDom" + "|" + fi.Length + "\r\n";

                                                }
                                                if (fi.Name.Substring(21, 4) == "1401")
                                                {
                                                    // textBox1.Text += fi.Name + "|" + "RDJ_Prueba_kardex" + "|" + fi.Length + "\r\n";
                                                    textBox1.Text += fi.Name + "|" + "RDJ2020_Prueba_Ventas" + "|" + fi.Length + "\r\n";

                                                }
                                                //Verificar ruc y año en LM
                                                
                                                if(rucLM!="" || añoLM != "")
                                                {
                                                    if (rucLE != rucLM || añoLE != añoLM)
                                                    {
                                                        MessageBox.Show("Se identifico más de un Libro Mayor con RUC distinto" + "\r\n" + "Por favor colocar el Libro Mayor en el formato LM_RRRRRRRRRR_YYYY.txt con el año:" + añoLE + " y ruc " + rucLE, "LM cargado: " + fi.Name);
                                                        button2.Enabled = false;
                                                        return;
                                                    }

                                                }       
                                                    
                                                
                                                //Cargar el LM correcto 
                                                if (File.Exists(path_Libros + "\\" + "LM_" + ruc+"_"+año +".TXT") && num_LibroMayor==0 )
                                                {
                                                    //Existe el libro electronico mayor
                                                    textBox1.Text += fi.Name + "|" + "RDJ2020_Prueba_LibroMayor" + "|" + fi.Length + "\r\n";
                                                    num_LibroMayor = +1;
                                                }
                                                else
                                                {
                                                    if (num_LibroMayor == 0)
                                                    {
                                                        MessageBox.Show("Por favor colocar el Libro Mayor en el formato LM_RRRRRRRRRR_YYYY.txt", path_Libros + @"\LM_RRRRRRRRRR_AAAA.txt");
                                                        button2.Enabled = false;
                                                        return;
                                                    }
                                                    

                                                }
                                                

                                                
                                                
                                            }

                                        }
                                        #endregion

                                        
                                    }
                                    #endregion
                                    #region SIAF
                                    if (SIAF == "si")
                                    {
                                        
                                        if (fi.Name.Length == 40 && fi.Length > 0 && fi.Extension.ToLower() == ".xlsx")
                                        {
                                            ruc = fi.Name.Substring(0, 11);
                                            año = fi.Name.Substring(15, 4);
                                            mes = fi.Name.Substring(12, 2);
                                            #region Mover Archivos USados en el Script
                                            if (!File.Exists(fi.DirectoryName + @"\" + ruc + "_" + mes + "_" + año + "_Data_Tributaria.xlsx"))
                                            {
                                                MessageBox.Show("Por favor colocar la Data_Tributaria en el formato xlsx", path_Libros + "RRRRRRRRRR_MM_AAAA_Data_Tributaria.txt");
                                                button2.Enabled = false;
                                                flg_DatTribut = 0;
                                                return;
                                            }
                                            else
                                            {
                                                if(flg_DatTribut*1==0)
                                                    textBox1.Text += fi.Name + "|" + "Data_Tributaria" + "|" + fi.Length + "\r\n";
                                                
                                                
                                                //Verificar el nombre de la Hoja
                                                
                                                if (!validarNombreHoja(fi.DirectoryName + @"\" + ruc + "_" + mes + "_" + año + "_Data_Tributaria.xlsx", "Data_Tributaria")){
                                                    button2.Enabled = false;
                                                    flg_DatTribut = 0;
                                                    return;
                                                }
                                                else
                                                {
                                                    //Se encontro el archivo y el nombre de lsa hoja es correcta
                                                    flg_DatTribut += 1;
                                                }
                                            }
                                                

                                            if (!File.Exists(fi.DirectoryName + @"\" + ruc + "_" + mes + "_" + año + "_Data_Financiera.xlsx"))
                                            {
                                                MessageBox.Show("Por favor colocar Data_Financiera en el formato xlsx", path_Libros + "RRRRRRRRRR_MM_AAAA_Data_Financiera.xlsx");
                                                button2.Enabled = false;
                                                flgDatFinan = 0;
                                                return;
                                            }
                                            else
                                            {
                                                if (flgDatFinan==0)
                                                {
                                                    textBox1.Text += fi.Name + "|" + "Data_Financiera" + "|" + fi.Length + "\r\n";
                                                }                                                
                                                //Verificar el nombre de la Hoja
                                                if (!validarNombreHoja(fi.DirectoryName + @"\" + ruc + "_" + mes + "_" + año + "_Data_Financiera.xlsx", "Data_Financiera"))
                                                {
                                                    button2.Enabled = false;
                                                    flgDatFinan = 0;
                                                    return;
                                                }
                                                else
                                                {
                                                    flgDatFinan += 1;
                                                }
                                            }
                                                
                                            #endregion


                                        }

                                    }
                                    #endregion
                                    #region lectura de xml
                                    if (XML_Vent_Comp == "si")
                                    {   
                                        
                                        if (fi.Extension.Contains("xml") || fi.Extension.Contains("zip"))
                                        {
                                            //xml de comprobantes de pago electronicos (facturacion electrónica)

                                            textBox1.Text += fi.Name + "|" + "XML_Vent_Comp" + "|" + fi.Length + "\r\n";

                                        }
                                        else
                                        {
                                            if(!File.Exists(path_Libros+ "\\Lista_RUC_Contribuyente.txt"))
                                            {
                                                MessageBox.Show("Por favor colocar el RUC del Contribuyente por analizar en el formato txt", path_Libros + "Lista_RUC_Contribuyente.txt");
                                                button2.Enabled = false;
                                                return;
                                            }
                                            
                                            if (fi.Name == "Lista_RUC_Contribuyente.txt")
                                            {
                                                textBox1.Text += fi.Name + "|" + "Lista_RUC_Contribuyente" + "|" + fi.Length + "\r\n";
                                            }
                                            
                                            if (fi.Name.Length == 37)
                                            {
                                                //textBox1.Text = fi.Name.Substring(21, 2) + "\r\n";
                                                if (fi.Name.Substring(21, 2) == "08" && fi.Extension.Contains("txt") && fi.Name.Length == 37)
                                                {
                                                    textBox1.Text += fi.Name + "|" + "LEM_Prueba_compras" + "|" + fi.Length + "\r\n";
                                                }

                                                if (fi.Name.Substring(21, 2) == "14" && fi.Extension.Contains("txt") && fi.Name.Length == 37)
                                                {
                                                    textBox1.Text += fi.Name + "|" + "LEM_Prueba_ventas" + "|" + fi.Length + "\r\n";
                                                }
                                                if (fi.Name.Substring(21, 2) == "05" && fi.Extension.Contains("txt") && fi.Name.Length == 37)
                                                {
                                                    textBox1.Text += fi.Name + "|" + "LEM_Prueba_diario" + "|" + fi.Length + "\r\n";
                                                }

                                            }
                                            

                                        }
                                    }
                                    #endregion
                                }
                                /*
                                if (RDJ_Prueba_kardex == "si" || RDJ_Prueba_diario5_1 == "si" || RDJ_Prueba_diario5_3 == "si")
                                {
                                    if(num_RDJ_diario_5_3==0 || num_RDJ_diario_5_1==0)
                                    {
                                        if (num_RDJ_diario_5_3 == 0)
                                            {
                                            MessageBox.Show("No se encuentra ningun libro Diario 5.1 que corresponda a los periodos de los libros Compras (08) o Ventas (14)" + "\r\n" + "en la ruta!!", "No puedes continuar hasta completar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            button2.Enabled = false;
                                            return;
                                        }
                                        if (num_RDJ_diario_5_1 == 0)
                                        {
                                            MessageBox.Show("No se encuentra ningun libro Diario 5.1 que corresponda a los periodos de los libros Compras (08) o Ventas (14)" + "\r\n" + "en la ruta!!", "No puedes continuar hasta completar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            button2.Enabled = false;
                                            return;
                                        }

                                    }
                                }
                                */

                                button2.Enabled = true;
                            }
                            catch (Exception ex)
                            {
                                button2.Enabled = false;
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El codigo de ticket no me pertenece. No puedo cargar archivos", "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Text = "";
                            textBox6.Text = path_Libros;
                            button2.Enabled = false;
                        }

                    }

                }
                catch
                {
                    MessageBox.Show("Consulta el ticket ingresado en el ticketero", "Mensaje de ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            try
            {
                #region Cuando se exporta los archivos se actualiza la tabla Tickets para que no se vuelva a cargar mas archivos usando el codigo del ticket
                SqlConnection conn_ = new SqlConnection
                {
                    //conn_.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                    //conn_.ConnectionString = "Data Source= tcp:10.20.103.68,49172;User ID=DemoLogin;Password=DemoLogin;Initial Catalog=DatabaseDemo;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                    //conn_.ConnectionString = "Data Source=tcp:10.20.103.68,49172;Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                    ConnectionString = cadenaconexion
                };

                SqlCommand com_ = new SqlCommand("select Archivos_exportados from ticket where Cod_ticket like '" + textBox2.Text + "'", conn_);
                conn_.Open();

                string Archivos_exportados = (string)com_.ExecuteScalar();
                conn_.Close();

                #endregion

                if (Archivos_exportados == "si")
                {
                    textBox1.Text = "";
                    MessageBox.Show("Se cargó archivos con el codigo: " + textBox2.Text + "\r\n" + " Ya no se puede cargar más archivos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    button2.Enabled = false;
                }
                else
                {

                    DialogResult dialogResult = MessageBox.Show("Al presionar 'Yes' se carga y mueve todos los archivos mostrados en el recuadro central para ser procesados." + "\r\n" + "Luego de la carga no se puede cargar más archivos usando el código de ticket : " + textBox2.Text + "\r\n" + "En caso se genere o use otro ticket para cargar más archivos a un ticket anterior, no surtirá efecto.", "Verifica si todos los archivos estan completos o son los correctos.", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (dialogResult == DialogResult.Yes)
                    {

                        #region Cuando se exporta los archivos se actualiza la tabla Tickets para que no se vuelva a cargar mas archivos usando el codigo del ticket
                        SqlConnection conn = new SqlConnection
                        {
                            //conn.ConnectionString = "Data Source=10.20.244.25; Initial Catalog=Ticket_DgTAX; Integrated Security=True;";
                            //conn.ConnectionString = "Data Source= tcp:10.20.103.68,4917;User ID=DemoLogin;Password=DemoLogin;Initial Catalog=DatabaseDemo;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler

                            //conn.ConnectionString = "Data Source= tcp:10.20.103.68,49172;Initial Catalog=Ticket_DgTAX;Integrated Security=True;";//24/09/2019 cambio de Servidor, dado que el anterior esta reservado para ejecutar flujos de modeler
                            ConnectionString = cadenaconexion
                        };
                        SqlCommand com = new SqlCommand("update ticket set Archivos_exportados='si'  where Cod_ticket like '" + textBox2.Text + "'", conn);
                        conn.Open();
                        string ID_devuelto = (string)com.ExecuteScalar();
                        conn.Close();

                        #endregion
                        //
                        Cod_ticket_final = "";
                        conn.Open();
                        com = new SqlCommand("SELECT Cod_ticket FROM Ticket where Cod_ticket like '" + textBox2.Text + "'", conn);
                        Cod_ticket_final = (string)com.ExecuteScalar();
                        conn.Close();

                        DirectoryInfo di = new DirectoryInfo(path_Libros);
                        #region Mover Kardex Adicionales
                        if (LEA_Prueba_kardex_adic == "si")
                        {
                            foreach (var fi in di.GetFiles())       
                            {

                                if (File.Exists(path_Libros + "\\" + fi.Name) && fi.Name.Length == 37 && fi.Length>0 && fi.Name.Substring(29, 4) != "2")
                                {
                                    if (fi.Name.Substring(21, 2) == "13" && fi.Name.Length == 37)
                                    {

                                        if (fi.Name.Length == 37)
                                        {
                                            string ruc = fi.Name.Substring(2, 13);
                                            string año = fi.Name.Substring(13, 4);
                                            string mes = fi.Name.Substring(17, 2);
                                            string dia = fi.Name.Substring(19, 2);

                                            if (File.Exists(path_Libros + "\\" + "Establecimiento_" + ruc + "_" + año + ".xlsx"))
                                            {
                                                Mover_archivo_NOCVD(path_Libros + "\\" + "Establecimiento_" + ruc + "_" + año +  ".xlsx", "Establecimiento_" + ruc + "_" + año  + ".xlsx");
                                            }

                                        }
                                        Mover_archivo_PLB_Kardex_Adic(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                    if (fi.Name.Substring(21, 2) == "08")
                                    {
                                        Mover_archivo_PLB_Kardex_Adic(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                    if (fi.Name.Substring(21, 2) == "14")
                                    {
                                        Mover_archivo_PLB_Kardex_Adic(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                    if (fi.Name.Substring(21, 4) == "0501")
                                    {
                                        Mover_archivo_PLB_Kardex_Adic(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                    //mover_archivo(path_Libros + "\\" + fi.Name, fi.Name);

                                }

                            }

                        }
                        #endregion
                        #region Activo fijo
                        if (LEA_Prueba_activofijo == "si")
                        {
                            foreach (var fi in di.GetFiles())
                            {
                                if (File.Exists(path_Libros + "\\" + fi.Name) && fi.Name.Length == 37 && fi.Length>0)
                                {
                                    if (fi.Name.Substring(21, 2) == "07" && fi.Name.Length == 37)
                                    {
                                        Mover_archivo_NOCVD(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                }
                            }


                        }
                        #endregion
                        
                        #region RDJ
                        if ((RDJ_Prueba_kardex == "si" || RDJ_Prueba_diario5_1 == "si" || RDJ_Prueba_diario5_3 == "si") && num_RDJ_diario_5_1>0 && num_RDJ_diario_5_3>0)
                        {
                            
                            foreach (var fi in di.GetFiles())
                            {
                                if (File.Exists(path_Libros + "\\" + fi.Name) && (fi.Name.Length == 37 )&& fi.Length>0)
                                {
                                    if ((fi.Name.Substring(21, 2) == "13" || fi.Name.Substring(21, 2) == "14" || fi.Name.Substring(21, 2) == "08" || fi.Name.Substring(21, 4) == "0501" || fi.Name.Substring(21, 4) == "0503") && fi.Name.Length == 37)
                                    {
                                        Mover_archivo_RDJ(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                }
                            }


                        }

                        #endregion

                        #region RDJ2020
                        if (RDJ2020 == "si")
                        {
                            foreach (var fi in di.GetFiles())
                            {
                                
                                if (File.Exists(path_Libros + "\\" + fi.Name) && (fi.Name.Length == 37 || fi.Name.Length == 23) &&  fi.Length>0 )
                                {
                                    
                                    if (fi.Name.Length==23 && fi.Name.Substring(0, 2) == "LM" )
                                    {
                                        Mover_archivo_RDJ(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                    else
                                    {
                                        if (fi.Name.Length == 37)
                                        {
                                            Mover_archivo_RDJ(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                        
                                    }



                                }
                            }

                        }

                        #endregion

                        #region SIAF
                        if (SIAF == "si")
                        {
                            foreach (var fi in di.GetFiles())
                            {
                                if (File.Exists(path_Libros + "\\" + fi.Name) && (fi.Name.Length == 40) && fi.Extension.ToLower()==".xlsx")
                                {

                                    Mover_archivo_RDJ(path_Libros + "\\" + fi.Name, fi.Name);

                                }
                            }

                        }
                        #endregion

                        #region kardex
                        if (LEA_Prueba_kardex == "si")
                        {
                            foreach (var fi in di.GetFiles())
                            {

                                if (File.Exists(path_Libros + "\\" + fi.Name) && fi.Name.Length == 37 && fi.Length>0)
                                {
                                    if (fi.Name.Substring(21, 2) == "13" && fi.Name.Length == 37)
                                    {
                                        string ruc = fi.Name.Substring(2, 11);
                                        string año = fi.Name.Substring(13, 4);
                                        string mes = fi.Name.Substring(17, 2);
                                        string dia = fi.Name.Substring(19, 2);
                                        Mover_archivo_NOCVD(path_Libros + "\\" + fi.Name, fi.Name);
                                        if (fi.Name.Length == 37)
                                        {
                                            

                                            if (File.Exists(path_Libros + "\\" + "Establecimiento_" + ruc + "_" + año + ".xlsx"))
                                            {
                                                Mover_archivo_NOCVD(path_Libros + "\\" + "Establecimiento_" + ruc + "_" + año +  ".xlsx", "Establecimiento_" + ruc + "_" + año + ".xlsx");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                        #endregion
                        #region Inventario y balance
                        if (LEA_Prueba_inventariobalance == "si")
                        {
                            foreach (var fi in di.GetFiles())
                            {
                                if (File.Exists(path_Libros + "\\" + fi.Name) && fi.Name.Length == 37 && fi.Length>0 && fi.Name.Substring(29, 4) != "2")
                                {
                                    if (fi.Name.Substring(21, 2) == "03")
                                    {
                                        Mover_archivo_NOCVD(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Costos
                        if (LEA_Prueba_costos == "si")
                        {
                            foreach (var fi in di.GetFiles())
                            {
                                if (File.Exists(path_Libros + "\\" + fi.Name) && fi.Name.Length == 37 && fi.Length>0 && fi.Name.Substring(29, 4) != "2")
                                {
                                    if (fi.Name.Substring(21, 2) == "10" && fi.Name.Length == 37)
                                    {
                                        Mover_archivo_NOCVD(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                }
                            }

                        }
                        #endregion

                        #region libro mensual Compras-ventas-diario
                        if (LEM_Prueba_compras == "si" || LEM_Prueba_ventas == "si" || LEM_Prueba_diario == "si")
                        {
                            DirectoryInfo cvd_uno = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\CVD_1");
                            DirectoryInfo cvd_dos = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\CVD_1");
                            DirectoryInfo cvd_tres = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\CVD_1");
                            long size_cvd_uno = 0;
                            long size_cvd_dos = 0;
                            long size_cvd_tres = 0;
                            foreach (var arch_cvd_uno in cvd_uno.GetFiles())
                            {
                                size_cvd_uno += arch_cvd_uno.Length;
                            }
                            foreach (var arch_cvd_dos in cvd_uno.GetFiles())
                            {
                                size_cvd_dos += arch_cvd_dos.Length;
                            }
                            foreach (var arch_cvd_tres in cvd_uno.GetFiles())
                            {
                                size_cvd_tres += arch_cvd_tres.Length;
                            }


                            if (size_cvd_uno <= size_cvd_dos && size_cvd_uno <= size_cvd_tres)
                            {
                                foreach (var fi in di.GetFiles())
                                {
                                    if (File.Exists(path_Libros + "\\" + fi.Name) && fi.Name.Length == 37 && fi.Length>0 && fi.Name.Substring(29, 4) != "2")
                                    {
                                        if (fi.Name.Substring(21, 2) == "08" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_1(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                        if (fi.Name.Substring(21, 2) == "14" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_1(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                        if (fi.Name.Substring(21, 2) == "05" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_1(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                    }
                                }

                            }



                            if (size_cvd_dos <= size_cvd_uno && size_cvd_dos <= size_cvd_tres)
                            {
                                foreach (var fi in di.GetFiles())
                                {
                                    if (File.Exists(path_Libros + "\\" + fi.Name) && fi.Name.Length == 37 && fi.Length>0 && fi.Name.Substring(29, 4) != "2")
                                    {
                                        if (fi.Name.Substring(21, 2) == "08" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_2(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                        if (fi.Name.Substring(21, 2) == "14" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_2(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                        if (fi.Name.Substring(21, 2) == "05" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_2(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                    }
                                }

                            }

                            if (size_cvd_tres <= size_cvd_uno && size_cvd_tres <= size_cvd_dos)
                            {
                                foreach (var fi in di.GetFiles())
                                {
                                    if (File.Exists(path_Libros + "\\" + fi.Name) && fi.Name.Length == 37 && fi.Length>0 && fi.Name.Substring(29, 4) != "2")
                                    {
                                        if (fi.Name.Substring(21, 2) == "08" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_3(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                        if (fi.Name.Substring(21, 2) == "14" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_3(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                        if (fi.Name.Substring(21, 2) == "05" && fi.Name.Length == 37)
                                        {
                                            Mover_archivo_CVD_3(path_Libros + "\\" + fi.Name, fi.Name);
                                        }
                                    }
                                }

                            }

                        }
                        #endregion


                        #region Mover XML Planillas
                        #endregion

                        #region Mover XML CDP electronicos
                        if (XML_Vent_Comp == "si")
                        {
                            foreach (var fi in di.GetFiles())
                            {
                                if (File.Exists(path_Libros + "\\" + fi.Name) && (fi.Extension.ToLower().Contains("xml") || fi.Extension.ToLower().Contains("zip")) && fi.Length>0)
                                {
                                    Mover_archivo_XMLcdp(path_Libros + "\\" + fi.Name, fi.Name);
                                }
                                else
                                {
                                    if (fi.Name == "Lista_RUC_Contribuyente.txt" && fi.Length>0)
                                    {
                                        Mover_archivo_XMLcdp(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                   
                                    if (fi.Name.Substring(21, 2) == "08" && fi.Extension.ToLower().Contains("txt") && fi.Name.Length == 37 && fi.Length>0)
                                    {
                                        Mover_archivo_XMLcdp(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                    if (fi.Name.Substring(21, 2) == "14" && fi.Extension.ToLower().Contains("txt") & fi.Name.Length == 37 && fi.Length>0)
                                    {
                                        Mover_archivo_XMLcdp(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                    if (fi.Name.Substring(21, 2) == "05" && fi.Extension.ToLower().Contains("txt") & fi.Name.Length == 37 &fi.Length>0)
                                    {                                        
                                        Mover_archivo_XMLcdp(path_Libros + "\\" + fi.Name, fi.Name);
                                    }
                                }
                            }

                        }
                        #endregion
                        MessageBox.Show("Archivos exportados", "Carga recibida", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //
                        conn.Open();
                        com = new SqlCommand("update Ticket set Fecha_arch_exportados=getdate() where Cod_ticket like '" + textBox2.Text + "'", conn);
                        ID_devuelto = (string)com.ExecuteScalar();
                        conn.Close();
                        //UPDATE NOMBRE PC donde se cargo la data
                        conn.Open();
                        com = new SqlCommand("update Ticket set PC_ingresar = '" + nombrePC + "' where Cod_ticket like '" + textBox2.Text + "'", conn);
                        ID_devuelto = (string)com.ExecuteScalar();
                        conn.Close();
                        //UPDATE Estado del ticket
                        conn.Open();
                        com = new SqlCommand("update Ticket set Estado = 'Cargado' where Cod_ticket like '" + textBox2.Text + "'", conn);
                        ID_devuelto = (string)com.ExecuteScalar();
                        conn.Close();
                        //Registro los nombre de archicos de la data cargada
                        conn.Open();
                        com = new SqlCommand("insert into Arch_Export (Cod_ticket,Archivos) values ('" + textBox2.Text + "','" + textBox1.Text + "')", conn);

                        ID_devuelto = (string)com.ExecuteScalar();
                        conn.Close();

                        button2.Enabled = false;
                    }
                    else
                        if (dialogResult == DialogResult.No)
                        {

                        }


                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private bool validarNombreHoja(string rutaLibro, string tipoTicket)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo excel = new FileInfo(rutaLibro);
            ExcelPackage pck = new ExcelPackage(excel);

            try
            {
                ExcelWorkbook wb = pck.Workbook;

                ExcelWorksheet hoja = wb.Worksheets.First();

                string hojaName = hoja.Name;

                if (tipoTicket == "Data_Tributaria")
                {
                    if (!hojaName.Equals("DATA_TRIBUTARIA"))
                    {
                        
                        MessageBox.Show("DT - La hoja se llama: " + hojaName + " ,debe llamarse: DATA_TRIBUTARIA");
                        pck.Dispose();
                        return false;
                    }
                    pck.Dispose();
                    return true;
                }
                if (tipoTicket == "Data_Financiera")
                {
                    if (!hojaName.Equals("DATA_FINANCIERA"))
                    {
                        MessageBox.Show("DF - La hoja se llama: " + hojaName+ " ,debe llamarse: DATA_FINANCIERA");
                        pck.Dispose();
                        return false;
                    }
                    pck.Dispose();
                    return true;
                }
                else
                {
                    MessageBox.Show("Error: Nombre Hoja '" + hojaName + "' no permitido!");
                    pck.Dispose();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: Algo paso y tienes que revizarlo " + ex.ToString());
                pck.Dispose();
                throw;
            }
        }

        private void Mover_archivo_NOCVD(string ruta_origen, string archivo)
        {
            try
            {
                //MessageBox.Show(Cod_ticket_final);
                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\PLB_NO_CVD\" + archivo;
                //J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB
                string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final ;
                #region En caso se tenga un archivo con el mismo nombre se elimina para poder mover el archivo traido por parametro
                if (!Directory.Exists(destinationFile))
                    Directory.CreateDirectory(destinationFile);

                destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }
                    
                
                #endregion
                
                //MessageBox.Show(destinationFile);
                System.IO.File.Move(ruta_origen, destinationFile);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
        private void Mover_archivo_CVD_1(string ruta_origen, string archivo)
        {
            try
            {

                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\CVD_1\" + archivo;
                //J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB
                string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final;
                #region En caso se tenga un archivo con el mismo nombre se elimina para poder mover el archivo traido por parametro
                if (!Directory.Exists(destinationFile))
                    Directory.CreateDirectory(destinationFile);

                destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }


                #endregion



                System.IO.File.Move(ruta_origen, destinationFile);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
        private void Mover_archivo_CVD_2(string ruta_origen, string archivo)
        {
            try
            {

                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\CVD_2\" + archivo;
                //J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB
                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final;
                #region En caso se tenga un archivo con el mismo nombre se elimina para poder mover el archivo traido por parametro
                if (!Directory.Exists(destinationFile))
                    Directory.CreateDirectory(destinationFile);

                destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }


                #endregion



                System.IO.File.Move(ruta_origen, destinationFile);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
        private void Mover_archivo_CVD_3(string ruta_origen, string archivo)
        {
            try
            {

                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\CVD_3\" + archivo;
                //J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB
                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final;
                string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final;
                #region En caso se tenga un archivo con el mismo nombre se elimina para poder mover el archivo traido por parametro
                if (!Directory.Exists(destinationFile))
                    Directory.CreateDirectory(destinationFile);

                destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }


                #endregion



                System.IO.File.Move(ruta_origen, destinationFile);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
        private void Mover_archivo_PLB_Kardex_Adic(string ruta_origen, string archivo)
        {
            try
            {

                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\PLB_Kardex_Adic\" + archivo;
                //J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB
                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final;
                string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final;
                #region En caso se tenga un archivo con el mismo nombre se elimina para poder mover el archivo traido por parametro
                if (!Directory.Exists(destinationFile))
                    Directory.CreateDirectory(destinationFile);

                destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }


                #endregion

                System.IO.File.Copy(ruta_origen, destinationFile);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
        private void Mover_archivo_RDJ(string ruta_origen, string archivo)
        {
            try
            {
                
                    
                //string destinationFile = @"J:\COMMON\Eddy Fernando TAX\PLB_NO_CVD\" + archivo;
                //J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB
                string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final;
                #region En caso se tenga un archivo con el mismo nombre se elimina para poder mover el archivo traido por parametro
                if (!Directory.Exists(destinationFile))
                    Directory.CreateDirectory(destinationFile);

                destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }


                #endregion

                //MessageBox.Show(destinationFile);
                System.IO.File.Move(ruta_origen, destinationFile);
                
                
               
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
        private void Mover_archivo_XMLcdp (string ruta_origen,string archivo)
        {
            try
            {
                string destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final;
                #region En caso se tenga un archivo con el mismo nombre se elimina para poder mover el archivo traido por parametro
                if (!Directory.Exists(destinationFile))
                    Directory.CreateDirectory(destinationFile);

                destinationFile = @"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB\" + Cod_ticket_final + "\\" + archivo;
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }


                #endregion

                System.IO.File.Move(ruta_origen, destinationFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
