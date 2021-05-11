using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjecutarFlujo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string HINICIO = "";
        public void iterar()
        {
            DirectoryInfo mainfolder = new DirectoryInfo(@"C:\Users\VS943JJ\Documents\temw");

            foreach (var folders in mainfolder.GetDirectories())
            {
                DirectoryInfo folder = new DirectoryInfo(folders.FullName);
                foreach (var di in folder.GetFiles())
                {
                    MessageBox.Show(di.Name, folder.Name);
                }

                try
                {
                    Directory.Delete(folders.FullName, recursive: true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.Message);
                }
            }
        }

        private void mainFlujos()
        {
            //Task.Delay(20);
            if (!Directory.Exists(@"C:\Digital Tax"))
            {
                Directory.CreateDirectory(@"C:\Digital Tax");
            }
            //Leer archivo Txt
            string path = @"C:\Digital Tax\texto.txt";
            if (File.Exists(path))
            {
                //Verificar contenido archivo Txt
                string respond= ReadFile(path);

                if (respond == "SI")
                {
                    //Se esta ejecutando un el mismo robot o instancia, no se ejecuta                    
                }
                else
                {
                    WriteFile(path, "SI");
                    //Ejecutar el robot
                    //ExecuteFlujos();
                }
            }
            else
            {
                //Crear archivo Txt
                string primera_linea;
                primera_linea = "SI";                
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(primera_linea);
                    tw.Close();
                }
                //Ejecutar el robot
                //ExecuteFlujos();


            }

            WriteFile(path,"NO");


            this.Close();
        }

        private void ExecuteFlujos()
        {
            
            //Creamos el delegado 
            ThreadStart delegado1 = new ThreadStart(SIAF);
            ThreadStart delegado2 = new ThreadStart(CVD1);
            ThreadStart delegado3 = new ThreadStart(RDJ2020V3);
            ThreadStart delegado4 = new ThreadStart(FlujosLite);
            ThreadStart delegado5 = new ThreadStart(CVD2);
            ThreadStart delegado6 = new ThreadStart(CVD3);
            ThreadStart delegado7 = new ThreadStart(Kardex_adicional);
            //Creamos la instancia del hilo 
            Thread hilo1 = new Thread(delegado1);
            Thread hilo2 = new Thread(delegado2);
            Thread hilo3 = new Thread(delegado3);
            Thread hilo4 = new Thread(delegado4);
            Thread hilo5 = new Thread(delegado5);
            Thread hilo6 = new Thread(delegado6);
            Thread hilo7 = new Thread(delegado7);

            //Iniciamos el hilo 
            if (!hilo1.IsAlive)
            {
                hilo1.Start();
                hilo1.IsBackground = false;
            }
            if (!hilo2.IsAlive)
            {
                hilo2.Start();
                hilo2.IsBackground = false;
            }
            if (!hilo3.IsAlive)
            {
                hilo3.Start();
                hilo3.IsBackground = false;
            }
            if (!hilo4.IsAlive)
            {
                hilo4.Start();
                hilo4.IsBackground = false;
            }
            if (!hilo5.IsAlive)
            {
                hilo5.Start();
                hilo5.IsBackground = false;
            }
            if (!hilo6.IsAlive)
            {
                hilo6.Start();
                hilo6.IsBackground = false;
            }
            if (!hilo7.IsAlive)
            {
                hilo7.Start();
                hilo7.IsBackground = false;
            }


            /*
            hilo1.Start();
            hilo1.IsBackground = false;
            hilo2.Start();
            hilo2.IsBackground = false;
            hilo3.Start();
            hilo3.IsBackground = false;
            hilo4.Start();
            hilo4.IsBackground = false;
            hilo5.Start();
            hilo5.IsBackground = false;
            hilo6.Start();
            hilo6.IsBackground = false;
            */
            //if (hilo1.ThreadState == ThreadState.Stopped && hilo2.ThreadState == ThreadState.Stopped && hilo3.ThreadState == ThreadState.Stopped && hilo4.ThreadState == ThreadState.Stopped)
            //if (hilo1.ThreadState != ThreadState.Running )
            hilo7.Join();
            hilo6.Join();
            hilo5.Join();
            hilo4.Join();
            hilo3.Join();
            hilo2.Join();
            hilo1.Join();

            
            var folderPath = @"C:\Users\Luis.Romero.G\AppData\Local\Temp\";
            try
            {
                //recursive false
                Directory.Delete(folderPath, false);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private string ReadFile(string path)
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                //StreamReader sr = new StreamReader("C:\\Sample.txt");
                StreamReader sr = new StreamReader(path);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                /*
                while (line != null)
                {
                    //write the lie to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                */
                //close the file
                sr.Close();
                Console.ReadLine();
                return line;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return "exception";
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
                
            }
        }
        private void WriteFile(string path,string message)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                //StreamWriter sw = new StreamWriter("C:\\Test.txt");
                StreamWriter sw = new StreamWriter(path);
                //Write a line of text
                sw.WriteLine(message);
                //Write a second line of text
                //sw.WriteLine("From the StreamWriter class");
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
        
        private void EjecutarFlujobtn_Click(object sender, EventArgs e)
        {
            string rutaTxtVacios = @"C:\Users\Luis.Romero.G\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\TXT_VACIOS";
            string OringenRdj2020 = @"J:\COMMON\Eddy Fernando TAX\RDJ2020";
            //LM_RRRRRRRRRR_YYYY.TXT
            /* if (fin.name == "LE_" + ruc + "-" + año + ".txt")
                * else
                *Asignar archivo vacio
                     *librom = @"ruta_txt vacio"
            */
            mainFlujos();

        }
        private void ejecutar_flujo_modeler(string comandos)
        {
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";
            try
            {
                //System.Diagnostics.Process process=new System.Diagnostics.Process();
                //System.Diagnostics.ProcessStartInfo startInfo=new System.Diagnostics.ProcessStartInfo();
                //startInfo.WindowStyle=System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.FileName=programa;
                //startInfo.Arguments=comandos;
                //process.StartInfo=startInfo;
                //process.Start();
                #region Ejecutar flujo en modeler
                //Indicamos que deseamos inicializar el proceso cmd.exe junto a un comando de arranque. 
                //(/C, le indicamos al proceso cmd que deseamos que cuando termine la tarea asignada se cierre el proceso).
                //Para mas informacion consulte la ayuda de la consola con cmd.exe /? 
                //System.Diagnostics.ProcessStartInfo procStartInfo=new System.Diagnostics.ProcessStartInfo("cmd", "/c "+ rutaclemb+ " && " + comandos + " foo");

                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + comandos);
                // Indicamos que la salida del proceso se redireccione en un Stream
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                //Indica que el proceso no despliegue una pantalla negra (El proceso se ejecuta en background)
                procStartInfo.CreateNoWindow = true;
                //Inicializa el proceso
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;

                //proc.StartInfo.FileName=@"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin\clemb.exe";
                proc.Start();
                //Consigue la salida de la Consola(Stream) y devuelve una cadena de texto
                string result = proc.StandardOutput.ReadToEnd();
                //Muestra en pantalla la salida del Comando
                //MessageBox.Show("Archivo generado");
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Kardex_adicional()
        {
            //Verificar si las rutas existen
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB");
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\RDJ2020"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\RDJ2020");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS");

            if (!Directory.Exists(@"J:\TL\Digital Tax\Flujos"))
                Directory.CreateDirectory(@"J:\TL\Digital Tax\Flujos");

            //string script = "";
            string comandos = "";
            string ruc, año, mes, dia, tipo_libro;

            //Para probar un script de modeler asignamos una variable segun el formato
            //Paso 1: setear la ruta fija de ejecucion de clembatch
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";
            //Paso 2: generar el sript dinamico para ejecutar el flujo modeler cons us archivos fuentes o inputs
            string flujoKardex = @"C: \Users\Luis.Romero.G\Desktop\Flujos_Modeler\Kardex_Adic\Pry_kardex_2018_21082019_RELAPASAA_v2.str";

            string ubicacion_archivo_entrada = @"J:\COMMON\Eddy Fernando TAX\PLB_Kardex_Adic\";
            string ubicacion_archivo_salida = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string consolidado_html = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba";
            string ubicacion_archivo_log = @"J:\TL\Digital Tax\Flujos\";


            string consolidado_html2 = "";
            string ubicacion_archivo_salida2 = "";
            string ubicacion_archivo_salida3 = "";
            string ubicacion_archivo_salida4 = "";
            string ubicacion_archivo_log2 = "";
            //Kardex
            string establecimiento_anexo = "";
            string kardexene = "";
            string kardexfeb = "";
            string kardexmar = "";
            string kardexabr = "";
            string kardexmay = "";
            string kardexjun = "";
            string kardexjul = "";
            string kardexago = "";
            string kardexset = "";
            string kardexoct = "";
            string kardexnov = "";
            string kardexdic = "";

            //CVD
            string ubicacion_archivo_entrada3 = "";
            string ubicacion_archivo_entrada4 = "";
            string ubicacion_archivo_entrada5 = "";
            string ubicacion_archivo_entrada6 = "";
            string ubicacion_archivo_entrada7 = "";
            string ubicacion_archivo_entrada8 = "";
            string ubicacion_archivo_entrada9 = "";
            string ubicacion_archivo_entrada10 = "";
            string ubicacion_archivo_entrada11 = "";
            string ubicacion_archivo_entrada12 = "";
            string ubicacion_archivo_entrada13 = "";
            string ubicacion_archivo_entrada14 = "";
            string ubicacion_archivo_entrada15 = "";
            string ubicacion_archivo_entrada16 = "";
            string ubicacion_archivo_entrada17 = "";
            string ubicacion_archivo_entrada18 = "";
            string ubicacion_archivo_entrada19 = "";
            string ubicacion_archivo_entrada20 = "";
            string ubicacion_archivo_entrada21 = "";
            string ubicacion_archivo_entrada22 = "";
            string ubicacion_archivo_entrada23 = "";
            string ubicacion_archivo_entrada24 = "";
            string ubicacion_archivo_entrada25 = "";
            string ubicacion_archivo_entrada26 = "";
            string ubicacion_archivo_entrada27 = "";
            string ubicacion_archivo_entrada28 = "";
            string ubicacion_archivo_entrada29 = "";
            string ubicacion_archivo_entrada30 = "";
            string ubicacion_archivo_entrada31 = "";
            string ubicacion_archivo_entrada32 = "";
            string ubicacion_archivo_entrada33 = "";
            string ubicacion_archivo_entrada34 = "";

            string ubicacion_archivo_entrada35 = "";
            string ubicacion_archivo_entrada36 = "";
            string ubicacion_archivo_entrada37 = "";
            string ubicacion_archivo_entrada38 = "";

            DirectoryInfo mainfolder = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\CVD_1");
            string ubicacion_archivo_entrada2 = "";

            
            foreach (var folders in mainfolder.GetDirectories())
            {

                ubicacion_archivo_entrada = folders.FullName + "\\";
                //MessageBox.Show(folders.Name);

                DirectoryInfo di = new DirectoryInfo(folders.FullName);
                //editar campos en LE segun version de formato
                foreach (var fi in di.GetFiles())
                {
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0)
                    {
                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);


                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT

                        tipo_libro = fi.Name.Substring(21, 4);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        if ((año == "2020" && mes == "12") || (int.Parse(año) > 2020))
                        {
                            /*
                            Para el(año 2020 y mes 12) o para el año 2021 en adelante en los txt cargados por los usuarios
                                En el libro ventas: eliminar campo 23 
                                En el libro compras: eliminar campo 22
                                 
                            */
                            if (tipo_libro == "1401")
                            {
                                Editar_registros_txt(22, fi.FullName, fi.FullName);

                            }
                            if (tipo_libro == "0801")
                            {
                                Editar_registros_txt(21, fi.FullName, fi.FullName);

                            }
                        }



                        #endregion


                    }
                }

                #region Identificacion iterativa de archivos en la ruta inicial donde se colocan los Libros electronicos
                foreach (var fi in di.GetFiles())
                {
                    //Nomeclatura de Libros electronicos
                    //http://orientacion.sunat.gob.pe/index.php/empresas-menu/libros-y-registros-vinculados-asuntos-tributarios-empresas/sistema-de-libros-electronicos-ple/6560-05-nomenclatura-de-libros-electronicos
                    //Los nombres de libros electronicos tienen 33 caracteres fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name))
                    {
                        HINICIO = DateTime.Now.ToString();

                        ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;

                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);
                        dia = fi.Name.Substring(19, 2);
                        //Crear carpeta de resultados con el nombre de ticket
                        if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name))
                            Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name);

                        //MessageBox.Show(ubicacion_archivo_entrada);
                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT
                        tipo_libro = fi.Name.Substring(21, 2);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        

                        if (fi.Name.Substring(21, 4) == "0501")
                        {   
                            #region kardex 
                            //Establemiciento anexo
                            if (!File.Exists(ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx") || !File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\Establecimiento_" + ruc + "_" + año + ".xlsx"))
                            {
                                establecimiento_anexo = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\Establecimiento_anexo.xlsx";
                            }
                            else
                            {
                                if (File.Exists(ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx"))
                                {
                                    establecimiento_anexo = ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx";
                                }
                                if (File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\Establecimiento_" + ruc + "_" + año + ".xlsx"))
                                {
                                    establecimiento_anexo = ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx";
                                }

                            }

                            #region meses
                            //enero
                            kardexene = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexene))
                            {
                                kardexene = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //febrero
                            kardexfeb = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexfeb))
                            {
                                kardexfeb = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //marzo
                            kardexmar = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexmar))
                            {
                                kardexmar = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //abril
                            kardexabr = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexabr))
                            {
                                kardexabr = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //mayo
                            kardexmay = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexmay))
                            {
                                kardexmay = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //junio
                            kardexjun = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexjun))
                            {
                                kardexjun = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //julio
                            kardexjul = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexjul))
                            {
                                kardexjul = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //agosto
                            kardexago = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexago))
                            {
                                kardexago = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //setiembre
                            kardexset = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexset))
                            {
                                kardexset = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //octubre
                            kardexoct = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexoct))
                            {
                                kardexoct = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //noviembre
                            kardexnov = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexnov))
                            {
                                kardexnov = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //diciembre
                            kardexdic = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexdic))
                            {
                                kardexdic = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            #endregion

                            #endregion
                            
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoKardexAdic_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogKardexAdic_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + flujoKardex + "\"" +
                                        " -Pkrdxene.full_filename=" + "\"" + kardexene + "\"" +
                                        " -Pkrdxfeb.full_filename=" + "\"" + kardexfeb + "\"" +
                                        " -Pkrdxmar.full_filename=" + "\"" + kardexmar + "\"" +
                                        " -Pkrdxabr.full_filename=" + "\"" + kardexabr + "\"" +
                                        " -Pkrdxmay.full_filename=" + "\"" + kardexmay + "\"" +
                                        " -Pkrdxjun.full_filename=" + "\"" + kardexjun + "\"" +
                                        " -Pkrdxjul.full_filename=" + "\"" + kardexjul + "\"" +
                                        " -Pkrdxago.full_filename=" + "\"" + kardexago + "\"" +
                                        " -Pkrdxset.full_filename=" + "\"" + kardexset + "\"" +
                                        " -Pkrdxoct.full_filename=" + "\"" + kardexoct + "\"" +
                                        " -Pkrdxnov.full_filename=" + "\"" + kardexnov + "\"" +
                                        " -Pkrdxdic.full_filename=" + "\"" + kardexdic + "\"" +
                                        //
                                        " -Ppruebaskrdx.full_filename=" + "\"" + ubicacion_archivo_salida + folders.Name + @"\PRUEBAS_KARDEX_REPORTE_FINAL.xlsx" + "\"" +
                                         " -Pestbkardex.full_filename=" + "\"" + establecimiento_anexo + "\"" +
                                         " -PPERIODO_CUR=" + "\"" + año + "\"" +

                                        //" -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +

                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        
                                        " -Pconsol_adic_kardex.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -Pkardex_prueba_adic.full_filename =" + "\"" + ubicacion_archivo_salida + folders.Name + @"\PRUEBAS_KARDEX_REPORTE_FINAL.xlsx" + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion
                        
                        }

                        if (fi.Name.Substring(21, 4) == "0801")
                        {
                            #region kardex 
                            //Establemiciento anexo
                            if (!File.Exists(ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx") || !File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\Establecimiento_" + ruc + "_" + año + ".xlsx"))
                            {
                                establecimiento_anexo = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\Establecimiento_anexo.xlsx";
                            }
                            else
                            {
                                if (File.Exists(ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx"))
                                {
                                    establecimiento_anexo = ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx";
                                }
                                if (File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\Establecimiento_" + ruc + "_" + año + ".xlsx"))
                                {
                                    establecimiento_anexo = ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx";
                                }

                            }

                            #region meses
                            //enero
                            kardexene = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexene))
                            {
                                kardexene = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //febrero
                            kardexfeb = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexfeb))
                            {
                                kardexfeb = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //marzo
                            kardexmar = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexmar))
                            {
                                kardexmar = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //abril
                            kardexabr = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexabr))
                            {
                                kardexabr = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //mayo
                            kardexmay = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexmay))
                            {
                                kardexmay = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //junio
                            kardexjun = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexjun))
                            {
                                kardexjun = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //julio
                            kardexjul = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexjul))
                            {
                                kardexjul = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //agosto
                            kardexago = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexago))
                            {
                                kardexago = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //setiembre
                            kardexset = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexset))
                            {
                                kardexset = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //octubre
                            kardexoct = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexoct))
                            {
                                kardexoct = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //noviembre
                            kardexnov = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexnov))
                            {
                                kardexnov = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //diciembre
                            kardexdic = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexdic))
                            {
                                kardexdic = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            #endregion

                            #endregion

                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoKardexAdic_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogKardexAdic_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + flujoKardex + "\"" +
                                        " -Pkrdxene.full_filename=" + "\"" + kardexene + "\"" +
                                        " -Pkrdxfeb.full_filename=" + "\"" + kardexfeb + "\"" +
                                        " -Pkrdxmar.full_filename=" + "\"" + kardexmar + "\"" +
                                        " -Pkrdxabr.full_filename=" + "\"" + kardexabr + "\"" +
                                        " -Pkrdxmay.full_filename=" + "\"" + kardexmay + "\"" +
                                        " -Pkrdxjun.full_filename=" + "\"" + kardexjun + "\"" +
                                        " -Pkrdxjul.full_filename=" + "\"" + kardexjul + "\"" +
                                        " -Pkrdxago.full_filename=" + "\"" + kardexago + "\"" +
                                        " -Pkrdxset.full_filename=" + "\"" + kardexset + "\"" +
                                        " -Pkrdxoct.full_filename=" + "\"" + kardexoct + "\"" +
                                        " -Pkrdxnov.full_filename=" + "\"" + kardexnov + "\"" +
                                        " -Pkrdxdic.full_filename=" + "\"" + kardexdic + "\"" +
                                        //
                                        " -Ppruebaskrdx.full_filename=" + "\"" + ubicacion_archivo_salida + folders.Name + @"\PRUEBAS_KARDEX_REPORTE_FINAL.xlsx" + "\"" +
                                         " -Pestbkardex.full_filename=" + "\"" + establecimiento_anexo + "\"" +
                                         " -PPERIODO_CUR=" + "\"" + año + "\"" +

                                        //" -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +

                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +


                                        " -Pconsol_adic_kardex.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -Pkardex_prueba_adic.full_filename =" + "\"" + ubicacion_archivo_salida + folders.Name + @"\PRUEBAS_KARDEX_REPORTE_FINAL.xlsx" + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion

                        }

                        if (fi.Name.Substring(21, 4) == "1401")
                        {
                            #region kardex 
                            //Establemiciento anexo
                            if (!File.Exists(ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx") || !File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\Establecimiento_" + ruc + "_" + año + ".xlsx"))
                            {
                                establecimiento_anexo = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\Establecimiento_anexo.xlsx";
                            }
                            else
                            {
                                if (File.Exists(ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx"))
                                {
                                    establecimiento_anexo = ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx";
                                }
                                if (File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\Establecimiento_" + ruc + "_" + año + ".xlsx"))
                                {
                                    establecimiento_anexo = ubicacion_archivo_entrada + "Establecimiento_" + ruc + "_" + año + ".xlsx";
                                }

                            }

                            #region meses
                            //enero
                            kardexene = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexene))
                            {
                                kardexene = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //febrero
                            kardexfeb = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexfeb))
                            {
                                kardexfeb = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //marzo
                            kardexmar = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexmar))
                            {
                                kardexmar = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //abril
                            kardexabr = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexabr))
                            {
                                kardexabr = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //mayo
                            kardexmay = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexmay))
                            {
                                kardexmay = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //junio
                            kardexjun = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexjun))
                            {
                                kardexjun = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //julio
                            kardexjul = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexjul))
                            {
                                kardexjul = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //agosto
                            kardexago = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexago))
                            {
                                kardexago = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //setiembre
                            kardexset = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexset))
                            {
                                kardexset = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //octubre
                            kardexoct = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexoct))
                            {
                                kardexoct = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //noviembre
                            kardexnov = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexnov))
                            {
                                kardexnov = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //diciembre
                            kardexdic = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(kardexdic))
                            {
                                kardexdic = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            #endregion

                            #endregion

                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoKardexAdic_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogKardexAdic_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + flujoKardex + "\"" +
                                        " -Pkrdxene.full_filename=" + "\"" + kardexene + "\"" +
                                        " -Pkrdxfeb.full_filename=" + "\"" + kardexfeb + "\"" +
                                        " -Pkrdxmar.full_filename=" + "\"" + kardexmar + "\"" +
                                        " -Pkrdxabr.full_filename=" + "\"" + kardexabr + "\"" +
                                        " -Pkrdxmay.full_filename=" + "\"" + kardexmay + "\"" +
                                        " -Pkrdxjun.full_filename=" + "\"" + kardexjun + "\"" +
                                        " -Pkrdxjul.full_filename=" + "\"" + kardexjul + "\"" +
                                        " -Pkrdxago.full_filename=" + "\"" + kardexago + "\"" +
                                        " -Pkrdxset.full_filename=" + "\"" + kardexset + "\"" +
                                        " -Pkrdxoct.full_filename=" + "\"" + kardexoct + "\"" +
                                        " -Pkrdxnov.full_filename=" + "\"" + kardexnov + "\"" +
                                        " -Pkrdxdic.full_filename=" + "\"" + kardexdic + "\"" +
                                        //
                                        " -Ppruebaskrdx.full_filename=" + "\"" + ubicacion_archivo_salida + folders.Name + @"\PRUEBAS_KARDEX_REPORTE_FINAL.xlsx" + "\"" +
                                         " -Pestbkardex.full_filename=" + "\"" + establecimiento_anexo + "\"" +
                                         " -PPERIODO_CUR=" + "\"" + año + "\"" +

                                        //" -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +

                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +


                                        " -Pconsol_adic_kardex.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -Pkardex_prueba_adic.full_filename =" + "\"" + ubicacion_archivo_salida + folders.Name + @"\PRUEBAS_KARDEX_REPORTE_FINAL.xlsx" + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion

                        }
                        #endregion

                        #region LIMPIEZA DE RUTAS UTILIZADAS
                        consolidado_html2 = "";

                        ubicacion_archivo_salida2 = "";
                        ubicacion_archivo_salida3 = "";
                        ubicacion_archivo_salida4 = "";



                        ubicacion_archivo_log2 = "";

                        //Kardex
                        establecimiento_anexo = "";
                        kardexene = "";
                        kardexfeb = "";
                        kardexmar = "";
                        kardexabr = "";
                        kardexmay = "";
                        kardexjun = "";
                        kardexjul = "";
                        kardexago = "";
                        kardexset = "";
                        kardexoct = "";
                        kardexnov = "";
                        kardexdic = "";
                        
                        //CVD
                        ubicacion_archivo_entrada2 = "";
                        ubicacion_archivo_entrada3 = "";
                        ubicacion_archivo_entrada4 = "";
                        ubicacion_archivo_entrada5 = "";
                        ubicacion_archivo_entrada6 = "";
                        ubicacion_archivo_entrada7 = "";
                        ubicacion_archivo_entrada8 = "";
                        ubicacion_archivo_entrada9 = "";
                        ubicacion_archivo_entrada10 = "";
                        ubicacion_archivo_entrada11 = "";
                        ubicacion_archivo_entrada12 = "";
                        ubicacion_archivo_entrada13 = "";
                        ubicacion_archivo_entrada14 = "";
                        ubicacion_archivo_entrada15 = "";
                        ubicacion_archivo_entrada16 = "";
                        ubicacion_archivo_entrada17 = "";
                        ubicacion_archivo_entrada18 = "";
                        ubicacion_archivo_entrada19 = "";
                        ubicacion_archivo_entrada20 = "";
                        ubicacion_archivo_entrada21 = "";
                        ubicacion_archivo_entrada22 = "";
                        ubicacion_archivo_entrada23 = "";
                        ubicacion_archivo_entrada24 = "";
                        ubicacion_archivo_entrada25 = "";
                        ubicacion_archivo_entrada26 = "";
                        ubicacion_archivo_entrada27 = "";
                        ubicacion_archivo_entrada28 = "";
                        ubicacion_archivo_entrada29 = "";
                        ubicacion_archivo_entrada30 = "";
                        ubicacion_archivo_entrada31 = "";
                        ubicacion_archivo_entrada32 = "";
                        ubicacion_archivo_entrada33 = "";
                        ubicacion_archivo_entrada34 = "";
                        ubicacion_archivo_entrada35 = "";
                        ubicacion_archivo_entrada36 = "";
                        ubicacion_archivo_entrada37 = "";
                        ubicacion_archivo_entrada38 = "";

                        #endregion


                    }
                    else
                    {
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name))
                        {
                            //No se reconoce el nombre del archivo como Libro electronico por la longitu de la cadena
                            ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;
                            //MessageBox.Show(fi.Name + "  " +fi.Name.Length);
                            mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            ubicacion_archivo_entrada2 = "";

                        }
                    }

                    #region eliminar carpetas por tickets
                    try
                    {
                        Directory.Delete(folders.FullName, recursive: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.Message);
                    }
                    #endregion

                }

                #endregion


            }

        }
        public void RDJ2020V3()
        {
            //variables definidas para guardar informacion de los libros electronicos por clientes
            string ruc, año, mes, dia, tipo_libro;
            string ubicacion_archivo_entrada2 = "";
            string ubicacion_txtVacio = @"C:\Users\Luis.Romero.G\Desktop\Flujos_Modeler\Flujograma_RDJ_20\TXT_VACIOS\";
            //ubicacion de flujo diseñado en modeler para cada libro electronico


            string ubicacion_flujo = @"C:\Users\luis.romero.g\Desktop\proyecto\RUTA3.str";

            string ubicacion_flujo_af = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Activo_Fijo5.str";
            string ubicacion_flujo_cvd = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\flujograma_sumatoria_compras_2015.str";
            string ubicacion_flujo_kardex = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Pry_kardex_2018_25032019_GOLFFIEL_1.str";
            string ubicacion_ib = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Proy_INVENT_AZ_18_FINALv9_FINAL_16072019.str";


            string ubicacion_archivo_entrada = @"J:\COMMON\Eddy Fernando TAX\RDJ2020\";
            string ubicacion_archivo_salida = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string consolidado_html = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string ubicacion_archivo_log = @"J:\TL\Digital Tax\Flujos\";

            

            //Verificar si las rutas existen
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB");
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\RDJ2020"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\RDJ2020");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS");

            if (!Directory.Exists(@"J:\TL\Digital Tax\Flujos"))
                Directory.CreateDirectory(@"J:\TL\Digital Tax\Flujos");



            string script = "";
            string comandos = "";
            //Para probar un script de modeler asignamos una variable segun el formato
            //Paso 1: setear la ruta fija de ejecucion de clembatch
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";
            //Paso 2: generar el sript dinamico para ejecutar el flujo modeler cons us archivos fuentes o inputs
            string flujoRDJ2020 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FLUJOGRAMA_PRUEBAS_RDJ_2020_VRS_9_14102020_vf_para_script.str";
            string Pventrada1 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190100140100001111.txt";
            string Pventrada2 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190200140100001111.txt";
            string Pventrada3 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190300140100001111.txt";
            string Pventrada4 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190400140100001111.txt";
            string Pventrada5 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190500140100001111.txt";
            string Pventrada6 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190600140100001111.txt";
            string Pventrada7 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190700140100001111.txt";
            string Pventrada8 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190800140100001111.txt";
            string Pventrada9 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190900140100001111.txt";
            string Pventrada10 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120191000140100001111.txt";
            string Pventrada11 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120191100140100001111.txt";
            string Pventrada12 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120191200140100001111.txt";
            string Pcentrada1 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190100080100001111.txt";
            string Pcentrada2 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190200080100001111.txt";
            string Pcentrada3 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190300080100001111.txt";
            string Pcentrada4 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190400080100001111.txt";
            string Pcentrada5 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190500080100001111.txt";
            string Pcentrada6 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190600080100001111.txt";
            string Pcentrada7 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190700080100001111.txt";
            string Pcentrada8 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190800080100001111.txt";
            string Pcentrada9 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190900080100001111.txt";
            string Pcentrada10 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120191000080100001111.txt";
            string Pcentrada11 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120191100080100001111.txt";
            string Pcentrada12 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120191200080100001111.txt";
            string Pnodomic1 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190100080200001111.txt";
            string Pnodomic2 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190200080200001111.txt";
            string Pnodomic3 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190300080200001111.txt";
            string Pnodomic4 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190400080200001111.txt";
            string Pnodomic5 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190500080200001111.txt";
            string Pnodomic6 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190600080200001111.txt";
            string Pnodomic7 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190700080200001111.txt";
            string Pnodomic8 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190800080200001111.txt";
            string Pnodomic9 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190900080200001111.txt";
            string Pnodomic10 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120191000080200001111.txt";
            string Pnodomic11 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120191100080200001111.txt";
            string Pnodomic12 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120191200080200001111.txt";
            string Plibromayor1 = @"C:\Users\Luis.Romero.G\Desktop\Flujos_Modeler\Flujograma_RDJ_20\TXT_VACIOS\LE_FORMATO_LIBRO_MAYOR.txt";
            string Pconsolidadordj = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\CONSOLIDADO_RDJ1.html";
            string Pcompra = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\COMPRAS.txt";
            string Pventa = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\VENTAS.txt";
            string PRDJRP_006_2 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJRP_006_2.xlsx";
            string PRDJRP_006 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJRP_006.xlsx";
            string PRDJLM_001_RESUMEN = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\PRDJLM_001_RESUMEN.xlsx";
            string PRDJLM_001_DETALLE = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_001_DETALLE.xlsx";
            string PRDJLM_001_DETALLE_TXT = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_001_DETALLE_TXT.txt";
            string PRDJLM_002 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_002.xlsx";
            string PRDJLM_003 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_003.xlsx";
            string PRDJLM_004 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_004.xlsx";
            string PRDJLM_005 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_005.xlsx";
            string PRDJLM_005_TXT = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_005_TXT.txt";
            string log = @"J:\TL\Digital Tax\Flujos\RJD2020";

            DirectoryInfo mainfolder = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\RDJ2020");

            foreach (var folders in mainfolder.GetDirectories())
            {
                ubicacion_archivo_entrada = folders.FullName + "\\";
                //MessageBox.Show(folders.Name);

                DirectoryInfo di = new DirectoryInfo(folders.FullName);

                //editar campos en LE segun version de formato
                foreach (var fi in di.GetFiles())
                {
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0)
                    {
                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);


                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT

                        tipo_libro = fi.Name.Substring(21, 4);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        if ((año == "2020" && mes == "12") || (int.Parse(año) > 2020))
                        {
                            /*
                            Para el(año 2020 y mes 12) o para el año 2021 en adelante en los txt cargados por los usuarios
                                En el libro ventas: eliminar campo 23 
                                En el libro compras: eliminar campo 22
                                 
                            */
                            if (tipo_libro == "1401")
                            {
                                Editar_registros_txt(22, fi.FullName, fi.FullName);

                            }
                            if (tipo_libro == "0801")
                            {
                                Editar_registros_txt(21, fi.FullName, fi.FullName);

                            }
                        }



                        #endregion


                    }
                }

                foreach (var fi in di.GetFiles())
                {

                    //Los nombres de libros electronicos tienen 33 caracteres fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0)
                    {
                        //MessageBox.Show(fi.Name,folders.Name);
                        HINICIO = DateTime.Now.ToString();

                        ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;

                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);
                        dia = fi.Name.Substring(19, 2);
                        //Crear carpeta de resultados con el nombre de ticket
                        if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name))
                            Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name);

                        #region Tipo de Libro consumidos
                        tipo_libro = fi.Name.Substring(21, 4);

                        //

                        if (File.Exists(@"J:\COMMON\Eddy Fernando TAX\RDJ2020\" + folders.Name + @"\LM_" + ruc + "_" + año + ".txt"))
                        {
                            Plibromayor1 = @"J:\COMMON\Eddy Fernando TAX\RDJ2020\" + folders.Name + @"\LM_" + ruc + "_" + año + ".txt";
                        }
                        else
                        {
                            if (File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\" + "LM_" + ruc + "_" + año + ".txt"))
                            {
                                Plibromayor1 = @"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS" + "LM_" + ruc + "_" + año + ".txt";

                            }
                        }


                        

                        if (tipo_libro.Equals("1401") || tipo_libro.Equals("0801") || tipo_libro.Equals("0802"))
                        {
                            #region Verificar libros ventas 1401
                            Pventrada1 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada1))
                            {
                                Pventrada1 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada2 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada2))
                            {
                                Pventrada2 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada3))
                            {
                                Pventrada3 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada4))
                            {
                                Pventrada4 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada5))
                            {
                                Pventrada5 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada6))
                            {
                                Pventrada6 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada7))
                            {
                                Pventrada7 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada8))
                            {
                                Pventrada8 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada9))
                            {
                                Pventrada9 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada10))
                            {
                                Pventrada10 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada11))
                            {
                                Pventrada11 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            Pventrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pventrada12))
                            {
                                Pventrada12 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                            }

                            #endregion

                            #region Verificar libros compras 0801
                            Pcentrada1 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada1))
                            {
                                Pcentrada1 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada2 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada2))
                            {
                                Pcentrada2 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada3))
                            {
                                Pcentrada3 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada4))
                            {
                                Pcentrada4 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada5))
                            {
                                Pcentrada5 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada6))
                            {
                                Pcentrada6 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada7))
                            {
                                Pcentrada7 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada8))
                            {
                                Pcentrada8 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada9))
                            {
                                Pcentrada9 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada10))
                            {
                                Pcentrada10 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada11))
                            {
                                Pcentrada11 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            Pcentrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pcentrada12))
                            {
                                Pcentrada12 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            #endregion

                            #region Verificar libros No domicialiados 0802
                            Pnodomic1 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic1))
                            {
                                Pnodomic1 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic2 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic2))
                            {
                                Pnodomic2 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic3))
                            {
                                Pnodomic3 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic4))
                            {
                                Pnodomic4 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic5))
                            {
                                Pnodomic5 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic6))
                            {
                                Pnodomic6 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic7))
                            {
                                Pnodomic7 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic8))
                            {
                                Pnodomic8 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic9))
                            {
                                Pnodomic9 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic10))
                            {
                                Pnodomic10 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic11))
                            {
                                Pnodomic11 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            Pnodomic12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(Pnodomic12))
                            {
                                Pnodomic12 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                            }
                            #endregion

                            #region Archivos Resultados
                            Pconsolidadordj = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\CONSOLIDADO_RDJ1" + "_" + ruc + "_" + año + ".html";
                            Pcompra = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\COMPRAS" + "_" + ruc + "_" + año + ".txt";
                            Pventa = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\VENTAS" + "_" + ruc + "_" + año + ".txt";
                            PRDJRP_006_2 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJRP_006_2" + "_" + ruc + "_" + año + ".xlsx";
                            PRDJRP_006 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJRP_006" + "_" + ruc + "_" + año + ".xlsx";
                            PRDJLM_001_RESUMEN = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\PRDJLM_001_RESUMEN" + "_" + ruc + "_" + año + ".xlsx";
                            PRDJLM_001_DETALLE = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJLM_001_DETALLE" + "_" + ruc + "_" + año + ".xlsx";
                            PRDJLM_001_DETALLE_TXT = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJLM_001_DETALLE_TXT" + "_" + ruc + "_" + año + ".txt";
                            PRDJLM_002 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJLM_002" + "_" + ruc + "_" + año + ".xlsx";
                            PRDJLM_003 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJLM_003" + "_" + ruc + "_" + año + ".xlsx";
                            PRDJLM_004 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJLM_004" + "_" + ruc + "_" + año + ".xlsx";
                            PRDJLM_005 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJLM_005" + "_" + ruc + "_" + año + ".xlsx";
                            PRDJLM_005_TXT = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\RDJLM_005_TXT" + "_" + ruc + "_" + año + ".txt";
                            log = @"J:\TL\Digital Tax\Flujos\RDJ2020_" + folders.Name + "_" + ruc + "_" + año + ".txt";
                            #endregion

                            #region Script flujo modeler


                            script = "clemb -stream " +
                        "\"" + flujoRDJ2020 + "\"" + " -Pventrada1.full_filename=" +
                        "\"" + Pventrada1 + "\"" + " -Pventrada2.full_filename=" +
                        "\"" + Pventrada2 + "\"" + " -Pventrada3.full_filename=" +
                        "\"" + Pventrada3 + "\"" + " -Pventrada4.full_filename=" +
                        "\"" + Pventrada4 + "\"" + " -Pventrada5.full_filename=" +
                        "\"" + Pventrada5 + "\"" + " -Pventrada6.full_filename=" +
                        "\"" + Pventrada6 + "\"" + " -Pventrada7.full_filename=" +
                        "\"" + Pventrada7 + "\"" + " -Pventrada8.full_filename=" +
                        "\"" + Pventrada8 + "\"" + " -Pventrada9.full_filename=" +
                        "\"" + Pventrada9 + "\"" + " -Pventrada10.full_filename=" +
                        "\"" + Pventrada10 + "\"" + " -Pventrada11.full_filename=" +
                        "\"" + Pventrada11 + "\"" + " -Pventrada12.full_filename=" +
                        "\"" + Pventrada12 + "\"" + " -Pcentrada1.full_filename=" +
                        "\"" + Pcentrada1 + "\"" + " -Pcentrada2.full_filename=" +
                        "\"" + Pcentrada2 + "\"" + " -Pcentrada3.full_filename=" +
                        "\"" + Pcentrada3 + "\"" + " -Pcentrada4.full_filename=" +
                        "\"" + Pcentrada4 + "\"" + " -Pcentrada5.full_filename=" +
                        "\"" + Pcentrada5 + "\"" + " -Pcentrada6.full_filename=" +
                        "\"" + Pcentrada6 + "\"" + " -Pcentrada7.full_filename=" +
                        "\"" + Pcentrada7 + "\"" + " -Pcentrada8.full_filename=" +
                        "\"" + Pcentrada8 + "\"" + " -Pcentrada9.full_filename=" +
                        "\"" + Pcentrada9 + "\"" + " -Pcentrada10.full_filename=" +
                        "\"" + Pcentrada10 + "\"" + " -Pcentrada11.full_filename=" +
                        "\"" + Pcentrada11 + "\"" + " -Pcentrada12.full_filename=" +
                        "\"" + Pcentrada12 + "\"" + " -Pnodomic1.full_filename=" +
                        "\"" + Pnodomic1 + "\"" + " -Pnodomic2.full_filename=" +
                        "\"" + Pnodomic2 + "\"" + " -Pnodomic3.full_filename=" +
                        "\"" + Pnodomic3 + "\"" + " -Pnodomic4.full_filename=" +
                        "\"" + Pnodomic4 + "\"" + " -Pnodomic5.full_filename=" +
                        "\"" + Pnodomic5 + "\"" + " -Pnodomic6.full_filename=" +
                        "\"" + Pnodomic6 + "\"" + " -Pnodomic7.full_filename=" +
                        "\"" + Pnodomic7 + "\"" + " -Pnodomic8.full_filename=" +
                        "\"" + Pnodomic8 + "\"" + " -Pnodomic9.full_filename=" +
                        "\"" + Pnodomic9 + "\"" + " -Pnodomic10.full_filename=" +
                        "\"" + Pnodomic10 + "\"" + " -Pnodomic11.full_filename=" +
                        "\"" + Pnodomic11 + "\"" + " -Pnodomic12.full_filename=" +
                        "\"" + Pnodomic12 + "\"" + " -Plibromayor1.full_filename=" +
                        "\"" + Plibromayor1 + "\"" + " -Pconsolidadordj.full_filename=" +
                        "\"" + Pconsolidadordj + "\"" + " -Pcompra.full_filename=" +
                        "\"" + Pcompra + "\"" + " -Pventa.full_filename=" +
                        "\"" + Pventa + "\"" + " -PRDJRP_006_2.full_filename=" +
                        "\"" + PRDJRP_006_2 + "\"" + " -PRDJRP_006.full_filename=" +
                        "\"" + PRDJRP_006 + "\"" + " -PRDJLM_001_RESUMEN.full_filename=" +
                        "\"" + PRDJLM_001_RESUMEN + "\"" + " -PRDJLM_001_DETALLE.full_filename=" +
                        "\"" + PRDJLM_001_DETALLE + "\"" + " -PRDJLM_001_DETALLE_TXT.full_filename=" +
                        "\"" + PRDJLM_001_DETALLE_TXT + "\"" + " -PRDJLM_002.full_filename=" +
                        "\"" + PRDJLM_002 + "\"" + " -PRDJLM_003.full_filename=" +
                        "\"" + PRDJLM_003 + "\"" + " -PRDJLM_004.full_filename=" +
                        "\"" + PRDJLM_004 + "\"" + " -PRDJLM_005.full_filename=" +
                        "\"" + PRDJLM_005 + "\"" + " -PRDJLM_005_TXT.full_filename=" +
                        "\"" + PRDJLM_005_TXT + "\"" + " -execute -log " +
                        "\"" + log + "\"";
                            #endregion
                            comandos = "";
                            comandos = rutaclemb + " && " + script + " ";
                            //comandos=variable;

                            ejecutar_flujo_modeler(comandos);


                            #region Mover Archivos usados en el script

                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada1, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada2, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada3, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada4, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada5, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada6, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada7, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada8, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada9, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada10, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada11, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pventrada12, fi.Name.Substring(0, 17) + "12" + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));



                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada1, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada2, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada3, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada4, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada5, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada6, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada7, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada8, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada9, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada10, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada11, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pcentrada12, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));



                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic1, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic2, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic3, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic4, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic5, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic6, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic7, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic8, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic9, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic10, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic11, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(Pnodomic12, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));


                            #endregion

                        }
                        else
                        {
                            mover_archivo(ubicacion_archivo_entrada2, fi.Name);

                        }
                        #endregion


                        #region Limpieza de variables

                        ubicacion_archivo_entrada2 = "";

                        Pventrada1 = "";
                        Pventrada2 = "";
                        Pventrada3 = "";
                        Pventrada4 = "";
                        Pventrada5 = "";
                        Pventrada6 = "";
                        Pventrada7 = "";
                        Pventrada8 = "";
                        Pventrada9 = "";
                        Pventrada10 = "";
                        Pventrada11 = "";
                        Pventrada12 = "";
                        Pcentrada1 = "";
                        Pcentrada3 = "";
                        Pcentrada4 = "";
                        Pcentrada5 = "";
                        Pcentrada6 = "";
                        Pcentrada7 = "";
                        Pcentrada8 = "";
                        Pcentrada9 = "";
                        Pcentrada10 = "";
                        Pcentrada11 = "";
                        Pcentrada12 = "";
                        Pnodomic1 = "";
                        Pnodomic2 = "";
                        Pnodomic3 = "";
                        Pnodomic4 = "";
                        Pnodomic5 = "";
                        Pnodomic6 = "";
                        Pnodomic7 = "";
                        Pnodomic8 = "";
                        Pnodomic9 = "";
                        Pnodomic10 = "";
                        Pnodomic11 = "";
                        Pnodomic12 = "";
                        Plibromayor1 = "";

                        #endregion
                    }
                    else
                    {
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name))
                        {
                            //No se reconoce el nombre del archivo como Libro electronico por la longitu de la cadena
                            ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;
                            //MessageBox.Show(fi.Name + "  " +fi.Name.Length);
                            mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            ubicacion_archivo_entrada2 = "";

                        }

                    }

                    #region eliminar carpetas por tickets
                    try
                    {
                        Directory.Delete(folders.FullName, recursive: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.Message);
                    }
                    #endregion


                }
            }
        }

        public void SIAF()
        {
            string ubicacion_archivo_entrada = "";
            string ubicacion_archivo_entrada2 = "";
            // VARIABLES PARA EL SCRIPT          

            //parametro CORTE POR MES Y AÑO
            string PPER_MES = "09";
            string PPER_REV = "2020";

            #region Verificar Rutas de Orginen y Destino


            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\SIAF"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\SIAF");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS");

            if (!Directory.Exists(@"J:\TL\Digital Tax\Flujos"))
                Directory.CreateDirectory(@"J:\TL\Digital Tax\Flujos");



            #endregion

            string script = "";
            string comandos = "";
            //Para probar un script de modeler asignamos una variable segun el formato
            //Paso 1: setear la ruta fija de ejecucion de clembatch
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";

            //Declaracion de Variables para el Script
            string flujoDepr2020 = @"C:\Users\Luis.Romero.G\Desktop\Flujos_Modeler\Depreciacion\DEPRECIACION_v20.str";

            string ruc, mes, año;

            string RutaHito0, RutaHito1, RutaHito2, RutaHito3;

            string PData_Tributaria = "";
            string PData_Financiera = "";

            string PCODIGOS_DUPLICADOS_TRIBUTARIO = "";
            string PSI_TRIBUTARIO_NO_FINANCIERO = "";
            string PSI_FINANCIERO_NO_TRIBUTARIO = "";
            string PCODIGOS_DUPLICADOS_FINANCIERO = "";
            string Pconsolidado_hito_1 = "";
            string Pdetalle_hito_1 = "";
            string PH2_AF_OTROS = "";
            string PH2_AF_NO_DEP = "";
            string PGR1_ALTAS_DEL_EJERCIO = "";
            string PGR2_ALTAS_CON_SALDO_INICIAL = "";
            string PGR4_BAJAS_TOTALES = "";
            string PGR3_BAJAS_PARCIALES = "";
            string PGR5_TRASLADOS_y_TRANSFERENCIAS = "";
            string PGR6_DIFERENTES_DEL_GR1_al_GR5 = "";
            string PALERTAS_HITO2 = "";
            string PHITO3_1 = "";
            string PHITO3_2 = "";
            string PHITO3_3 = "";
            string PHITO3_4 = "";
            string PHITO3_5 = "";
            string PHITO3_6 = "";
            string PHITO3_7 = "";
            string log = "";

            //Iteramos dentro de SIAF las carpetas con el nombre de  Ticket respectivas
            //J:\COMMON\Eddy Fernando TAX\SIAF
            DirectoryInfo mainfolder = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\SIAF");
            foreach (var folders in mainfolder.GetDirectories())
            {
                //Ruta de la carpeta con nombre de Ticket
                ubicacion_archivo_entrada = folders.FullName + "\\";

                //Verificamos los archivos en la carpeta Ticket
                DirectoryInfo di = new DirectoryInfo(folders.FullName);
                
                foreach (var fi in di.GetFiles())
                {

                    //Los nombres de libros electronicos tienen 40 caracteres fi.Name.Length == 40 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                    if (fi.Name.Length == 40 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0 && fi.Extension.ToLower() == ".xlsx")
                    {
                        ruc = fi.Name.Substring(0, 11);
                        año = fi.Name.Substring(15, 4);
                        mes = fi.Name.Substring(12, 2);
                        //Crear carpeta de resultados con el nombre de ticket
                        RutaHito0 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\" + "HITO-0";
                        RutaHito1 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\" + "HITO-1";
                        RutaHito2 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\" + "HITO-2";
                        RutaHito3 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name + @"\" + "HITO-3";

                        if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name))
                            Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name);

                        if (!Directory.Exists(RutaHito0))
                        {
                            Directory.CreateDirectory(RutaHito0);
                        }
                        if (!Directory.Exists(RutaHito1))
                        {
                            Directory.CreateDirectory(RutaHito1);
                        }
                        if (!Directory.Exists(RutaHito2))
                        {
                            Directory.CreateDirectory(RutaHito2);
                        }
                        if (!Directory.Exists(RutaHito3))
                        {
                            Directory.CreateDirectory(RutaHito3);
                        }
                        PData_Tributaria = fi.DirectoryName + @"\" + ruc + "_" + mes + "_" + año + "_Data_Tributaria.xlsx";
                        PData_Financiera = fi.DirectoryName + @"\" + ruc + "_" + mes + "_" + año + "_Data_Financiera.xlsx";
                        PPER_MES = mes;
                        PPER_REV = año;
                        PCODIGOS_DUPLICADOS_TRIBUTARIO = RutaHito0 + @"\CODIGOS_DUPLICADOS_TRIBUTARIO.xlsx";
                        PSI_TRIBUTARIO_NO_FINANCIERO = RutaHito0 + @"\SI_TRIBUTARIO_NO_FINANCIERO.xlsx";
                        PSI_FINANCIERO_NO_TRIBUTARIO = RutaHito0 + @"\SI_FINANCIERO_NO_TRIBUTARIO.xlsx";
                        PCODIGOS_DUPLICADOS_FINANCIERO = RutaHito0 + @"\CODIGOS_DUPLICADOS_FINANCIERO.xlsx";
                        Pconsolidado_hito_1 = RutaHito1 + @"\CONSOLIDADO.html";
                        Pdetalle_hito_1 = RutaHito1 + @"\REPORTE_DETALLE_HITO_1.xlsx";

                        PH2_AF_OTROS = RutaHito2 + @"\H2_AF_OTROS.xlsx";
                        PH2_AF_NO_DEP = RutaHito2 + @"\H2_AF_NO_DEP.xlsx";
                        PGR1_ALTAS_DEL_EJERCIO = RutaHito2 + @"\GR1_ALTAS_DEL_EJERCIO.xlsx";
                        PGR2_ALTAS_CON_SALDO_INICIAL = RutaHito2 + @"\GR2_ALTAS_CON_SALDO_INICIAL.xlsx";
                        PGR4_BAJAS_TOTALES = RutaHito2 + @"\GR4_BAJAS_TOTALES.xlsx.xlsx";
                        PGR3_BAJAS_PARCIALES = RutaHito2 + @"\GR3_BAJAS_PARCIALES.xlsx";
                        PGR5_TRASLADOS_y_TRANSFERENCIAS = RutaHito2 + @"\GR5_TRASLADOS_y_TRANSFERENCIAS.xlsx";
                        PGR6_DIFERENTES_DEL_GR1_al_GR5 = RutaHito2 + @"\GR6_DIFERENTES_DEL_GR1_al_GR5.xlsx";
                        PALERTAS_HITO2 = RutaHito2 + @"\ALERTAS_HITO2.xlsx";

                        PHITO3_1 = RutaHito3 + @"\HITO_3_AF_OTROS.xlsx";
                        PHITO3_2 = RutaHito3 + @"\HITO_3_EDIF_CONST.xlsx";
                        PHITO3_3 = RutaHito3 + @"\HITO_3_EDIF_CONST_30264.xlsx";
                        PHITO3_4 = RutaHito3 + @"\HITO_3_EDIF_MINERO_TASA_GLOBAL.xlsx";
                        PHITO3_5 = RutaHito3 + @"\HITO_3_LEAS_EDIF.xlsx";
                        PHITO3_6 = RutaHito3 + @"\HITO_3_LEAS_OTROS.xlsx";
                        PHITO3_7 = RutaHito3 + @"\HITO_3_OTROS_MINERO_TASA_GLOBAL.xlsx";

                        log = @"J:\TL\Digital Tax\Flujos\SIAF_" + folders.Name + "_" + ruc + "_" + año + ".txt";

                        #region script clemb

                        script = "clemb -stream " +
                        "\"" + flujoDepr2020 + "\"" +
                        " -PData_Tributaria.full_filename=" + "\"" + PData_Tributaria + "\"" +
                        " -PData_Financiera.full_filename=" + "\"" + PData_Financiera + "\"" +
                        " -PPER_MES=" + "\"" + PPER_MES + "\"" +
                        " -PPER_REV=" + "\"" + PPER_REV + "\"" +
                        " -PCODIGOS_DUPLICADOS_TRIBUTARIO.full_filename=" + "\"" + PCODIGOS_DUPLICADOS_TRIBUTARIO + "\"" +
                        " -PSI_TRIBUTARIO_NO_FINANCIERO.full_filename=" + "\"" + PSI_TRIBUTARIO_NO_FINANCIERO + "\"" +
                        " -PSI_FINANCIERO_NO_TRIBUTARIO.full_filename=" + "\"" + PSI_FINANCIERO_NO_TRIBUTARIO + "\"" +
                        " -PCODIGOS_DUPLICADOS_FINANCIERO.full_filename=" + "\"" + PCODIGOS_DUPLICADOS_FINANCIERO + "\"" +
                        " -Pconsolidado_hito_1.full_filename=" + "\"" + Pconsolidado_hito_1 + "\"" +
                        " -Pdetalle_hito_1.full_filename=" + "\"" + Pdetalle_hito_1 + "\"" +
                        " -PH2_AF_OTROS.full_filename=" + "\"" + PH2_AF_OTROS + "\"" +
                        " -PH2_AF_NO_DEP.full_filename=" + "\"" + PH2_AF_NO_DEP + "\"" +
                        " -PGR1_ALTAS_DEL_EJERCIO.full_filename=" + "\"" + PGR1_ALTAS_DEL_EJERCIO + "\"" +
                        " -PGR2_ALTAS_CON_SALDO_INICIAL.full_filename=" + "\"" + PGR2_ALTAS_CON_SALDO_INICIAL + "\"" +
                        " -PGR4_BAJAS_TOTALES.full_filename=" + "\"" + PGR4_BAJAS_TOTALES + "\"" +
                        " -PGR3_BAJAS_PARCIALES.full_filename=" + "\"" + PGR3_BAJAS_PARCIALES + "\"" +
                        " -PGR5_TRASLADOS_y_TRANSFERENCIAS.full_filename=" + "\"" + PGR5_TRASLADOS_y_TRANSFERENCIAS + "\"" +
                        " -PGR6_DIFERENTES_DEL_GR1_al_GR5.full_filename=" + "\"" + PGR6_DIFERENTES_DEL_GR1_al_GR5 + "\"" +
                        " -PALERTAS_HITO2.full_filename=" + "\"" + PALERTAS_HITO2 + "\"" +
                        " -PHITO3_1.full_filename=" + "\"" + PHITO3_1 + "\"" +
                        " -PHITO3_2.full_filename=" + "\"" + PHITO3_2 + "\"" +
                        " -PHITO3_3.full_filename=" + "\"" + PHITO3_3 + "\"" +
                        " -PHITO3_4.full_filename=" + "\"" + PHITO3_4 + "\"" +
                        " -PHITO3_5.full_filename=" + "\"" + PHITO3_5 + "\"" +
                        " -PHITO3_6.full_filename=" + "\"" + PHITO3_6 + "\"" +
                        " -PHITO3_7.full_filename=" + "\"" + PHITO3_7 + "\"" +
                        " -execute -log " + "\"" + log + "\"";

                        #endregion

                        comandos = "";
                        //comandos = variable;
                        comandos = rutaclemb + " && " + script;

                        ejecutar_flujo_modeler(comandos);

                        #region Mover Archivos USados en el Script
                        if (File.Exists(fi.DirectoryName + @"\" + ruc + "_" + mes + "_" + año + "_Data_Tributaria.xlsx"))
                            mover_archivo(PData_Tributaria, ruc + "_" + mes + "_" + año + "_Data_Tributaria.xlsx");


                        if (File.Exists(fi.DirectoryName + @"\" + ruc + "_" + mes + "_" + año + "_Data_Financiera.xlsx"))
                            mover_archivo(PData_Financiera, ruc + "_" + mes + "_" + año + "_Data_Financiera.xlsx");

                        #endregion



                    }
                    else
                    {
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name))
                        {
                            //No se reconoce el nombre del archivo como Libro electronico por la longitu de la cadena
                            ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;
                            //MessageBox.Show(fi.Name + "  " +fi.Name.Length);
                            mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            ubicacion_archivo_entrada2 = "";

                        }

                    }

                    #region eliminar carpetas por tickets
                    try
                    {
                        Directory.Delete(folders.FullName, recursive: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.Message);
                    }
                    #endregion

                }
            }



        }


        public void FlujosLite()
        {
            string comandos = "";
            //Multihilos: Opcion ejecutar Flujos pesados contenidos en Carpetas separadas (Kardex,CVD80pruebas,IB) en simultaneo
            //http://eledwin.com/blog/tutorial-de-hilos-en-c-con-ejemplos-parte-1-31




            #region DEFINICION DE RUTAS Y FLUJOS
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";
            //https://docs.microsoft.com/es-es/dotnet/api/system.io.directoryinfo.getfiles?view=netframework-4.8

            //variables definidas para guardar informacion de los libros electronicos por clientes
            string ruc, año, mes, dia, tipo_libro;


            //ubicacion de flujo diseñado en modeler para cada libro electronico

            string ubicacion_flujo_af = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\AF\AF.str";
            string ubicacion_flujo_kardex = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\Kardex.str";
            string ubicacion_ib = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\IB.str";


            string ubicacion_archivo_entrada = @"J:\COMMON\Eddy Fernando TAX\PLB_NO_CVD\";
            string ubicacion_archivo_salida = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string consolidado_html = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string ubicacion_archivo_log = @"J:\TL\Digital Tax\Flujos\";
            //Verificar si las rutas existen
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS");

            if (!Directory.Exists(@"J:\TL\Digital Tax\Flujos"))
                Directory.CreateDirectory(@"J:\TL\Digital Tax\Flujos");

            if (!Directory.Exists(ubicacion_archivo_entrada))
                Directory.CreateDirectory(ubicacion_archivo_entrada);
            //AF
            string ubicacion_archivo_salida2 = "";
            string consolidado_html2 = "";
            string ubicacion_archivo_log2 = "";
            string ubicacion_archivo_entrada2 = "";

            string estableciemitno_anexo = "";

            //CVD
            string ubicacion_archivo_entrada3 = "";
            string ubicacion_archivo_entrada4 = "";
            string ubicacion_archivo_salida3 = "";
            string ubicacion_archivo_salida4 = "";

            //KARDEX
            string ubicacion_archivo_entrada5 = "";
            string ubicacion_archivo_entrada6 = "";
            string ubicacion_archivo_entrada7 = "";
            string ubicacion_archivo_entrada8 = "";
            string ubicacion_archivo_entrada9 = "";
            string ubicacion_archivo_entrada10 = "";
            string ubicacion_archivo_entrada11 = "";
            string ubicacion_archivo_entrada12 = "";
            string ubicacion_archivo_entrada13 = "";
            string ubicacion_archivo_entrada14 = "";

            //IB
            string ubicacion_archivo_entrada15 = "";
            string ubicacion_archivo_entrada16 = "";
            string ubicacion_archivo_entrada17 = "";
            string ubicacion_archivo_entrada18 = "";
            string ubicacion_archivo_entrada19 = "";

            #endregion


            DirectoryInfo mainfolder = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\PLB_NO_CVD");

            foreach (var folders in mainfolder.GetDirectories())
            {
                ubicacion_archivo_entrada = folders.FullName + "\\";

                DirectoryInfo di = new DirectoryInfo(folders.FullName);
                //editar campos en LE segun version de formato
                foreach (var fi in di.GetFiles())
                {
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0)
                    {
                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);


                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT

                        tipo_libro = fi.Name.Substring(21, 4);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        if ((año == "2020" && mes == "12") || (int.Parse(año) > 2020))
                        {
                            /*
                            Para el(año 2020 y mes 12) o para el año 2021 en adelante en los txt cargados por los usuarios
                                En el libro ventas: eliminar campo 23 
                                En el libro compras: eliminar campo 22
                                 
                            */
                            if (tipo_libro == "1401")
                            {
                                Editar_registros_txt(22, fi.FullName, fi.FullName);

                            }
                            if (tipo_libro == "0801")
                            {
                                Editar_registros_txt(21, fi.FullName, fi.FullName);

                            }
                        }



                        #endregion


                    }
                }


                #region Identificacion iterativa de archivos en la ruta inicial donde se colocan los Libros electronicos
                foreach (var fi in di.GetFiles())
                {
                    //Nomeclatura de Libros electronicos
                    //http://orientacion.sunat.gob.pe/index.php/empresas-menu/libros-y-registros-vinculados-asuntos-tributarios-empresas/sistema-de-libros-electronicos-ple/6560-05-nomenclatura-de-libros-electronicos
                    //Los nombres de libros electronicos tienen 33 caracteres fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name))
                    {
                        HINICIO = DateTime.Now.ToString();

                        ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;

                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);
                        dia = fi.Name.Substring(19, 2);
                        //Crear carpeta de resultados con el nombre de ticket
                        if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name))
                            Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name);

                        //MessageBox.Show(ubicacion_archivo_entrada);
                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT
                        tipo_libro = fi.Name.Substring(21, 2);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        /*
                        if (fi.Name.Substring(21, 2) == "01")
                        {
                            consolidado_html = @"J:\COMMON\Eddy Fernando TAX\" + "ConsolidadoCajaBanco" + ruc.ToString() + año.ToString() + mes.ToString() + ".html";
                            MessageBox.Show("ejecutar flujo CAJA Y BANCOS" + " ruc: " + ruc);
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo + "\"" + " -Ptxtentrada.full_filename = " + "\"" + ubicacion_archivo_entrada + "\"" + " -Ptxtsalida.full_filename = " + "\"" + ubicacion_archivo_salida + "\"" + " -execute -nolog";
                            mover_archivo(ubicacion_archivo_entrada, fi.Name);
                            MessageBox.Show(comandos);
                        }
                        */
                        if (fi.Name.Substring(21, 2) == "03")
                        {
                            consolidado_html2 = consolidado_html+ folders.Name + @"\" + "ConsolidadoIB_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida+ folders.Name +@"\" + "IB_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogIB_" +folders.Name+ "_"+ ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            #region Anexos IB
                            //Anexo3_1
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030100" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.1.txt";
                            }
                            //Anexo3_2
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030200" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.2.txt";
                            }
                            //Anexo3_3
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030300" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.3.txt";
                            }
                            //Anexo3_4
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "03040007" + fi.Name.Substring(fi.Name.Length - 8, 8);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.4.txt";
                            }
                            //Anexo3_5
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030500" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.5.txt";
                            }
                            //Anexo3_6
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030600" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.6.txt";
                            }
                            //Anexo3_7
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030700" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.7.txt";
                            }
                            //Anexo3_8
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030800" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.8.txt";
                            }
                            //Anexo3_9
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030900" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.9.txt";
                            }
                            //Anexo3_11
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031100" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.11.txt";
                            }
                            //Anexo3_12
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031200" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.12.txt";
                            }
                            //Anexo3_13
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031300" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.13.txt";
                            }
                            //Anexo3_14
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031400" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.14.txt";
                            }
                            //Anexo3_15
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031500" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.15.txt";
                            }
                            //Anexo3_16_1
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031601" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.16.1.txt";
                            }
                            //Anexo3_16_2
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031602" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.16.2.txt";
                            }
                            //Anexo3_17 y //Anexo3_17_
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031700" + fi.Name.Substring(fi.Name.Length - 10, 10);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\IB\TXT_VACIOS\3.17.txt";
                            }
                            #endregion

                            comandos = ubicacion_ib;
                            comandos = "clemb -stream " + "\"" + ubicacion_ib + "\"" +
                         " -PAnexo3_1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                         " -PAnexo3_2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                         " -PAnexo3_3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                         " -PAnexo3_4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                         " -PAnexo3_5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                         " -PAnexo3_6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                         " -PAnexo3_7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                         " -PAnexo3_8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                         " -PAnexo3_9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                         " -PAnexo3_11.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                         " -PAnexo3_12.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                         " -PAnexo3_13.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                         " -PAnexo3_14.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                         " -PAnexo3_15.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                         " -PAnexo3_16_1.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                         " -PAnexo3_16_2.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                         " -PAnexo3_17.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                         " -PAnexo3_17_.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +

                         " -PPER_EVAL=" + "\"" + año + "\"" +
                         " -Pibconsolidado.full_filename=" + "\"" + consolidado_html2 + "\"" +
                         " -Pibsalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                         " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030100" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 21) + "030100" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030200" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 21) + "030200" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030300" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 21) + "030300" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "03040007" + fi.Name.Substring(fi.Name.Length - 8, 8)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 21) + "03040007" + fi.Name.Substring(fi.Name.Length - 8, 8));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030500" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 21) + "030500" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030600" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 21) + "030600" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030700" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 21) + "030700" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030800" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 21) + "030800" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "030900" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 21) + "030900" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031100" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 21) + "031100" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031200" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 21) + "031200" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031300" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 21) + "031300" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031400" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 21) + "031400" + fi.Name.Substring(fi.Name.Length - 10, 10));


                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031500" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 21) + "031500" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031601" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 21) + "031601" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031602" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 21) + "031602" + fi.Name.Substring(fi.Name.Length - 10, 10));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 21) + "031700" + fi.Name.Substring(fi.Name.Length - 10, 10)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 21) + "031700" + fi.Name.Substring(fi.Name.Length - 10, 10));



                        }
                        /*
                        if (fi.Name.Substring(21, 2) == "04")
                        {
                            MessageBox.Show("ejecutar flujo Retenciones" + " " + ruc);
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo + "\"" + " -Ptxtentrada.full_filename = " + "\"" + ubicacion_archivo_entrada + "\"" + " -Ptxtsalida.full_filename = " + "\"" + ubicacion_archivo_salida + "\"" + " -execute -nolog";
                            mover_archivo(ubicacion_archivo_entrada, fi.Name);
                            MessageBox.Show(comandos);
                        }

                        if (fi.Name.Substring(21, 2) == "06")
                        {
                            MessageBox.Show("ejecutar flujo Mayor" + " ruc: " + ruc);
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo + "\"" + " -Ptxtentrada.full_filename = " + "\"" + ubicacion_archivo_entrada + "\"" + " -Ptxtsalida.full_filename = " + "\"" + ubicacion_archivo_salida + "\"" + " -execute -nolog";
                            mover_archivo(ubicacion_archivo_entrada, fi.Name);
                            MessageBox.Show(comandos);
                        }
                        */
                        if (fi.Name.Substring(21, 4) == "0701")
                        {
                            consolidado_html2 = consolidado_html +folders.Name + @"\" + "ConsolidadoAF_" + ruc + "_" + "_" + año + "_" + mes + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + folders.Name + @"\" + "AF_" + ruc + "_" + "_" + año + "_" + mes + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogAF_" +folders.Name +"_" + ruc + "_" + "_" + año + "_" + mes + ".txt";

                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_af + "\"" + " -Pafentrada.full_filename=" + "\"" + ubicacion_archivo_entrada2 + "\"" + " -Pafsalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" + " -PPERIODO_CUR=" + "\"" + año + "\"" + " -Pconsolidadoaf.full_filename=" + "\"" + consolidado_html2 + "\"" + " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            mover_archivo(ubicacion_archivo_entrada2, fi.Name);


                        }
                        /*
                        if (fi.Name.Substring(21, 2) == "09")
                        {
                            MessageBox.Show("ejecutar flujo Consignaciones" + " ruc: " + ruc);
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo + "\"" + " -Ptxtentrada.full_filename = " + "\"" + ubicacion_archivo_entrada + "\"" + " -Ptxtsalida.full_filename = " + "\"" + ubicacion_archivo_salida + "\"" + " -execute -nolog";

                            MessageBox.Show(comandos);
                        }
                        */
                        if (fi.Name.Substring(21, 2) == "10")
                        {
                            /*
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo + "\"" + 
                                " -Ptxtentrada.full_filename = " + "\"" + ubicacion_archivo_entrada + "\"" + 
                                " -Ptxtsalida.full_filename = " + "\"" + ubicacion_archivo_salida + "\"" + 
                                " -execute -nolog";
                            */
                            
                        }
                        /*
                        if (fi.Name.Substring(21, 2) == "12")
                        {
                            MessageBox.Show("ejecutar flujo Registro de Inventario Permanente en Unidades Físicas " + " ruc: " + ruc);
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo + "\"" + " -Ptxtentrada.full_filename = " + "\"" + ubicacion_archivo_entrada + "\"" + " -Ptxtsalida.full_filename = " + "\"" + ubicacion_archivo_salida + "\"" + " -execute -nolog";
                            mover_archivo(ubicacion_archivo_entrada, fi.Name);
                            MessageBox.Show(comandos);
                        }
                        */
                        if (fi.Name.Substring(21, 4) == "1301")
                        {
                            consolidado_html2 = consolidado_html + folders.Name + @"\" + "ConsolidadoKardex_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + folders.Name + @"\" + "Kardex_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogKardex_" +folders.Name +"_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            //Establemiciento anexo
                            if(!File.Exists(ubicacion_archivo_entrada+"Establecimiento_" + ruc+"_"+año + ".xlsx") || !File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\Establecimiento_" + ruc+"_"+año + ".xlsx"))
                            {
                                estableciemitno_anexo = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\Establecimiento_anexo.xlsx";
                            }
                            else
                            {
                                if (File.Exists(ubicacion_archivo_entrada + "Establecimiento_" + ruc +"_"+año + ".xlsx"))
                                {
                                    estableciemitno_anexo= ubicacion_archivo_entrada + "Establecimiento_" + ruc +"_"+año+ ".xlsx";
                                }
                                if (File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\Establecimiento_" + ruc +"_"+año + ".xlsx"))
                                {
                                    estableciemitno_anexo = ubicacion_archivo_entrada + "Establecimiento_" + ruc+"_"+año + ".xlsx";
                                }

                            }

                            #region meses
                            //enero
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //febrero
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //marzo
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //abril
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //mayo
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //junio
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //julio
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //agosto
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //setiembre
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //octubre
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //noviembre
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            //diciembre
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(fi.Name.Length - 18, 18);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Kardex\TXT_VACIOS\kardex_vacio.txt";
                            }
                            #endregion
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_kardex + "\"" +
                         " -Pkrdxene.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                         " -Pkrdxfeb.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                         " -Pkrdxmar.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                         " -Pkrdxabr.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                         " -Pkrdxmay.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                         " -Pkrdxjun.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                         " -Pkrdxjul.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                         " -Pkrdxago.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                         " -Pkrdxset.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                         " -Pkrdxoct.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                         " -Pkrdxnov.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                         " -Pkrdxdic.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                         " -Ppruebaskrdx.full_filename=" + "\"" + ubicacion_archivo_salida + folders.Name + @"\PRUEBAS_KARDEX_REPORTE_FINAL.xlsx" + "\"" +
                         " -Pestbkardex.full_filename=" + "\"" + estableciemitno_anexo + "\"" +
                         " -PPERIODO_CUR=" + "\"" + año + "\"" +
                         " -Pconsolidadokardex.full_filename=" + "\"" + consolidado_html2 + "\"" +
                         " -Pkrdxsalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                         " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                            {
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(fi.Name.Length - 18, 18));
                                //MessageBox.Show("ENERO");
                            }
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                            {
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(fi.Name.Length - 18, 18));
                                //MessageBox.Show("FEBRERO");
                            }
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                            {
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(fi.Name.Length - 18, 18));
                                //MessageBox.Show("MARZO");
                            }
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(fi.Name.Length - 18, 18));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(fi.Name.Length - 18, 18));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(fi.Name.Length - 18, 18));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(fi.Name.Length - 18, 18));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(fi.Name.Length - 18, 18));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(fi.Name.Length - 18, 18));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(fi.Name.Length - 18, 18));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(fi.Name.Length - 18, 18));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(fi.Name.Length - 18, 18)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(fi.Name.Length - 18, 18));


                        }

                        #endregion

                        #region LIMPIEZA DE RUTAS UTILIZADAS
                        consolidado_html2 = "";

                        ubicacion_archivo_salida2 = "";
                        ubicacion_archivo_salida3 = "";
                        ubicacion_archivo_salida4 = "";



                        ubicacion_archivo_log2 = "";


                        //CVD
                        ubicacion_archivo_entrada2 = "";
                        ubicacion_archivo_entrada3 = "";
                        ubicacion_archivo_entrada4 = "";
                        //Kardex
                        ubicacion_archivo_entrada5 = "";
                        ubicacion_archivo_entrada6 = "";
                        ubicacion_archivo_entrada7 = "";
                        ubicacion_archivo_entrada8 = "";
                        ubicacion_archivo_entrada9 = "";
                        ubicacion_archivo_entrada10 = "";
                        ubicacion_archivo_entrada11 = "";
                        ubicacion_archivo_entrada12 = "";
                        ubicacion_archivo_entrada13 = "";
                        ubicacion_archivo_entrada14 = "";
                        //IB
                        ubicacion_archivo_entrada15 = "";
                        ubicacion_archivo_entrada16 = "";
                        ubicacion_archivo_entrada17 = "";
                        ubicacion_archivo_entrada18 = "";
                        ubicacion_archivo_entrada19 = "";
                        #endregion


                    }
                    else
                    {
                        //No se reconoce el nombre del archivo como Libro electronico por la longitu de la cadena
                        //MessageBox.Show(fi.Name + "  " +fi.Name.Length);
                        mover_archivo(fi.FullName, fi.Name);
                    }

                    //

                }
                #region eliminar carpetas por tickets
                try
                {
                    Directory.Delete(folders.FullName, recursive: true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.Message);
                }
                #endregion


                #endregion


            }



        }
        
        private void CVD1()
        {

            string comandos = "";
            //Multihilos: Opcion ejecutar Flujos pesados contenidos en Carpetas separadas (Kardex,CVD80pruebas,IB) en simultaneo
            //http://eledwin.com/blog/tutorial-de-hilos-en-c-con-ejemplos-parte-1-31



            //comandos = comandos.Replace("\r\n", " ").Replace("  ", "");

            #region DEFINICION DE RUTAS Y FLUJOS
            //Paso 1: setear la ruta fija de ejecucion de clembatch
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";
            //https://docs.microsoft.com/es-es/dotnet/api/system.io.directoryinfo.getfiles?view=netframework-4.8

            //variables definidas para guardar informacion de los libros electronicos por clientes
            string ruc, año, mes, dia, tipo_libro;


            //ubicacion de flujo diseñado en modeler para cada libro electronico


            string ubicacion_flujo_cvd = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\CVD.str";



            string ubicacion_archivo_entrada = @"J:\COMMON\Eddy Fernando TAX\CVD_1\";
            string ubicacion_archivo_salida = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string consolidado_html = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba";
            string ubicacion_archivo_log = @"J:\TL\Digital Tax\Flujos\";
            //Verificar si las rutas existen
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS");

            if (!Directory.Exists(@"J:\TL\Digital Tax\Flujos"))
                Directory.CreateDirectory(@"J:\TL\Digital Tax\Flujos");

            if (!Directory.Exists(ubicacion_archivo_entrada))
                Directory.CreateDirectory(ubicacion_archivo_entrada);
            //AF
            string ubicacion_archivo_salida2 = "";
            string consolidado_html2 = "";
            string ubicacion_archivo_log2 = "";
            string ubicacion_archivo_entrada2 = "";

            //CVD
            string ubicacion_archivo_entrada3 = "";
            string ubicacion_archivo_entrada4 = "";
            string ubicacion_archivo_entrada5 = "";
            string ubicacion_archivo_entrada6 = "";
            string ubicacion_archivo_entrada7 = "";
            string ubicacion_archivo_entrada8 = "";
            string ubicacion_archivo_entrada9 = "";
            string ubicacion_archivo_entrada10 = "";
            string ubicacion_archivo_entrada11 = "";
            string ubicacion_archivo_entrada12 = "";
            string ubicacion_archivo_entrada13 = "";
            string ubicacion_archivo_entrada14 = "";
            string ubicacion_archivo_entrada15 = "";
            string ubicacion_archivo_entrada16 = "";
            string ubicacion_archivo_entrada17 = "";
            string ubicacion_archivo_entrada18 = "";
            string ubicacion_archivo_entrada19 = "";
            string ubicacion_archivo_entrada20 = "";
            string ubicacion_archivo_entrada21 = "";
            string ubicacion_archivo_entrada22 = "";
            string ubicacion_archivo_entrada23 = "";
            string ubicacion_archivo_entrada24 = "";
            string ubicacion_archivo_entrada25 = "";
            string ubicacion_archivo_entrada26 = "";
            string ubicacion_archivo_entrada27 = "";
            string ubicacion_archivo_entrada28 = "";
            string ubicacion_archivo_entrada29 = "";
            string ubicacion_archivo_entrada30 = "";
            string ubicacion_archivo_entrada31 = "";
            string ubicacion_archivo_entrada32 = "";
            string ubicacion_archivo_entrada33 = "";
            string ubicacion_archivo_entrada34 = "";

            string ubicacion_archivo_entrada35 = "";
            string ubicacion_archivo_entrada36 = "";
            string ubicacion_archivo_entrada37 = "";
            string ubicacion_archivo_entrada38 = "";

            string ubicacion_archivo_salida3 = "";
            string ubicacion_archivo_salida4 = "";

            #endregion


            DirectoryInfo mainfolder = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\CVD_1");
                        
            foreach (var folders in mainfolder.GetDirectories())
            {

                ubicacion_archivo_entrada = folders.FullName + "\\";
                //MessageBox.Show(folders.Name);

                DirectoryInfo di = new DirectoryInfo(folders.FullName);
                //editar campos en LE segun version de formato
                foreach (var fi in di.GetFiles())
                {
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0)
                    {
                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);


                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT

                        tipo_libro = fi.Name.Substring(21, 4);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        if ((año == "2020" && mes == "12") || (int.Parse(año) > 2020))
                        {
                            /*
                            Para el(año 2020 y mes 12) o para el año 2021 en adelante en los txt cargados por los usuarios
                                En el libro ventas: eliminar campo 23 
                                En el libro compras: eliminar campo 22

                            */
                            if (tipo_libro == "1401")
                            {
                                Editar_registros_txt(22, fi.FullName, fi.FullName);

                            }
                            if (tipo_libro == "0801")
                            {
                                Editar_registros_txt(21, fi.FullName, fi.FullName);

                            }
                        }



                        #endregion


                    }
                }


                #region Identificacion iterativa de archivos en la ruta inicial donde se colocan los Libros electronicos
                foreach (var fi in di.GetFiles())
                {
                    //Nomeclatura de Libros electronicos
                    //http://orientacion.sunat.gob.pe/index.php/empresas-menu/libros-y-registros-vinculados-asuntos-tributarios-empresas/sistema-de-libros-electronicos-ple/6560-05-nomenclatura-de-libros-electronicos
                    //Los nombres de libros electronicos tienen 33 caracteres fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name))
                    {
                        HINICIO = DateTime.Now.ToString();

                        ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;

                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);
                        dia = fi.Name.Substring(19, 2);
                        //Crear carpeta de resultados con el nombre de ticket
                        if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name))
                            Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name);

                        //MessageBox.Show(ubicacion_archivo_entrada);
                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT
                        tipo_libro = fi.Name.Substring(21, 2);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        

                        if (fi.Name.Substring(21, 4) == "0501")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" +folders.Name +"_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion
                        }

                        if (fi.Name.Substring(21, 4) == "0801")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion

                        }

                        if (fi.Name.Substring(21, 4) == "1401")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_1\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion
                        }
                        #endregion

                        #region LIMPIEZA DE RUTAS UTILIZADAS
                        consolidado_html2 = "";

                        ubicacion_archivo_salida2 = "";
                        ubicacion_archivo_salida3 = "";
                        ubicacion_archivo_salida4 = "";



                        ubicacion_archivo_log2 = "";


                        //CVD
                        ubicacion_archivo_entrada2 = "";
                        ubicacion_archivo_entrada3 = "";
                        ubicacion_archivo_entrada4 = "";
                        ubicacion_archivo_entrada5 = "";
                        ubicacion_archivo_entrada6 = "";
                        ubicacion_archivo_entrada7 = "";
                        ubicacion_archivo_entrada8 = "";
                        ubicacion_archivo_entrada9 = "";
                        ubicacion_archivo_entrada10 = "";
                        ubicacion_archivo_entrada11 = "";
                        ubicacion_archivo_entrada12 = "";
                        ubicacion_archivo_entrada13 = "";
                        ubicacion_archivo_entrada14 = "";
                        ubicacion_archivo_entrada15 = "";
                        ubicacion_archivo_entrada16 = "";
                        ubicacion_archivo_entrada17 = "";
                        ubicacion_archivo_entrada18 = "";
                        ubicacion_archivo_entrada19 = "";
                        ubicacion_archivo_entrada20 = "";
                        ubicacion_archivo_entrada21 = "";
                        ubicacion_archivo_entrada22 = "";
                        ubicacion_archivo_entrada23 = "";
                        ubicacion_archivo_entrada24 = "";
                        ubicacion_archivo_entrada25 = "";
                        ubicacion_archivo_entrada26 = "";
                        ubicacion_archivo_entrada27 = "";
                        ubicacion_archivo_entrada28 = "";
                        ubicacion_archivo_entrada29 = "";
                        ubicacion_archivo_entrada30 = "";
                        ubicacion_archivo_entrada31 = "";
                        ubicacion_archivo_entrada32 = "";
                        ubicacion_archivo_entrada33 = "";
                        ubicacion_archivo_entrada34 = "";
                        ubicacion_archivo_entrada35 = "";
                        ubicacion_archivo_entrada36 = "";
                        ubicacion_archivo_entrada37 = "";
                        ubicacion_archivo_entrada38 = "";

                        #endregion


                    }
                    else
                    {
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name))
                        {
                            //No se reconoce el nombre del archivo como Libro electronico por la longitu de la cadena
                            ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;
                            //MessageBox.Show(fi.Name + "  " +fi.Name.Length);
                            mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            ubicacion_archivo_entrada2 = "";

                        }
                    }

                    #region eliminar carpetas por tickets
                    try
                    {
                        Directory.Delete(folders.FullName, recursive: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.Message);
                    }
                    #endregion

                }

                #endregion


            }
        
        }

        private void CVD2()
        {

            string comandos = "";
            //Multihilos: Opcion ejecutar Flujos pesados contenidos en Carpetas separadas (Kardex,CVD80pruebas,IB) en simultaneo
            //http://eledwin.com/blog/tutorial-de-hilos-en-c-con-ejemplos-parte-1-31



            //comandos = comandos.Replace("\r\n", " ").Replace("  ", "");

            #region DEFINICION DE RUTAS Y FLUJOS
            //Paso 1: setear la ruta fija de ejecucion de clembatch
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";
            //https://docs.microsoft.com/es-es/dotnet/api/system.io.directoryinfo.getfiles?view=netframework-4.8

            //variables definidas para guardar informacion de los libros electronicos por clientes
            string ruc, año, mes, dia, tipo_libro;


            //ubicacion de flujo diseñado en modeler para cada libro electronico


            string ubicacion_flujo_cvd = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\CVD.str";



            string ubicacion_archivo_entrada = @"J:\COMMON\Eddy Fernando TAX\CVD_2\";
            string ubicacion_archivo_salida = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string consolidado_html = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba";
            string ubicacion_archivo_log = @"J:\TL\Digital Tax\Flujos\";
            //Verificar si las rutas existen
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS");

            if (!Directory.Exists(@"J:\TL\Digital Tax\Flujos"))
                Directory.CreateDirectory(@"J:\TL\Digital Tax\Flujos");

            if (!Directory.Exists(ubicacion_archivo_entrada))
                Directory.CreateDirectory(ubicacion_archivo_entrada);
            //AF
            string ubicacion_archivo_salida2 = "";
            string consolidado_html2 = "";
            string ubicacion_archivo_log2 = "";
            string ubicacion_archivo_entrada2 = "";

            //CVD
            string ubicacion_archivo_entrada3 = "";
            string ubicacion_archivo_entrada4 = "";
            string ubicacion_archivo_entrada5 = "";
            string ubicacion_archivo_entrada6 = "";
            string ubicacion_archivo_entrada7 = "";
            string ubicacion_archivo_entrada8 = "";
            string ubicacion_archivo_entrada9 = "";
            string ubicacion_archivo_entrada10 = "";
            string ubicacion_archivo_entrada11 = "";
            string ubicacion_archivo_entrada12 = "";
            string ubicacion_archivo_entrada13 = "";
            string ubicacion_archivo_entrada14 = "";
            string ubicacion_archivo_entrada15 = "";
            string ubicacion_archivo_entrada16 = "";
            string ubicacion_archivo_entrada17 = "";
            string ubicacion_archivo_entrada18 = "";
            string ubicacion_archivo_entrada19 = "";
            string ubicacion_archivo_entrada20 = "";
            string ubicacion_archivo_entrada21 = "";
            string ubicacion_archivo_entrada22 = "";
            string ubicacion_archivo_entrada23 = "";
            string ubicacion_archivo_entrada24 = "";
            string ubicacion_archivo_entrada25 = "";
            string ubicacion_archivo_entrada26 = "";
            string ubicacion_archivo_entrada27 = "";
            string ubicacion_archivo_entrada28 = "";
            string ubicacion_archivo_entrada29 = "";
            string ubicacion_archivo_entrada30 = "";
            string ubicacion_archivo_entrada31 = "";
            string ubicacion_archivo_entrada32 = "";
            string ubicacion_archivo_entrada33 = "";
            string ubicacion_archivo_entrada34 = "";

            string ubicacion_archivo_entrada35 = "";
            string ubicacion_archivo_entrada36 = "";
            string ubicacion_archivo_entrada37 = "";
            string ubicacion_archivo_entrada38 = "";

            string ubicacion_archivo_salida3 = "";
            string ubicacion_archivo_salida4 = "";

            #endregion


            DirectoryInfo mainfolder = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\CVD_2");

            foreach (var folders in mainfolder.GetDirectories())
            {

                ubicacion_archivo_entrada = folders.FullName + "\\";
                //MessageBox.Show(folders.Name);

                DirectoryInfo di = new DirectoryInfo(folders.FullName);
                //editar campos en LE segun version de formato
                foreach (var fi in di.GetFiles())
                {
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0)
                    {
                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);


                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT

                        tipo_libro = fi.Name.Substring(21, 4);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        if ((año == "2020" && mes == "12") || (int.Parse(año) > 2020))
                        {
                            /*
                            Para el(año 2020 y mes 12) o para el año 2021 en adelante en los txt cargados por los usuarios
                                En el libro ventas: eliminar campo 23 
                                En el libro compras: eliminar campo 22

                            */
                            if (tipo_libro == "1401")
                            {
                                Editar_registros_txt(22, fi.FullName, fi.FullName);

                            }
                            if (tipo_libro == "0801")
                            {
                                Editar_registros_txt(21, fi.FullName, fi.FullName);

                            }
                        }



                        #endregion


                    }
                }


                #region Identificacion iterativa de archivos en la ruta inicial donde se colocan los Libros electronicos
                foreach (var fi in di.GetFiles())
                {
                    //Nomeclatura de Libros electronicos
                    //http://orientacion.sunat.gob.pe/index.php/empresas-menu/libros-y-registros-vinculados-asuntos-tributarios-empresas/sistema-de-libros-electronicos-ple/6560-05-nomenclatura-de-libros-electronicos
                    //Los nombres de libros electronicos tienen 33 caracteres fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name))
                    {
                        HINICIO = DateTime.Now.ToString();

                        ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;

                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);
                        dia = fi.Name.Substring(19, 2);
                        //Crear carpeta de resultados con el nombre de ticket
                        if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name))
                            Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name);

                        //MessageBox.Show(ubicacion_archivo_entrada);
                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT
                        tipo_libro = fi.Name.Substring(21, 2);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        
                        if (fi.Name.Substring(21, 4) == "0501")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion
                        }

                        if (fi.Name.Substring(21, 4) == "0801")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion

                        }

                        if (fi.Name.Substring(21, 4) == "1401")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_2\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion
                        }
                        #endregion

                        #region LIMPIEZA DE RUTAS UTILIZADAS
                        consolidado_html2 = "";

                        ubicacion_archivo_salida2 = "";
                        ubicacion_archivo_salida3 = "";
                        ubicacion_archivo_salida4 = "";



                        ubicacion_archivo_log2 = "";


                        //CVD
                        ubicacion_archivo_entrada2 = "";
                        ubicacion_archivo_entrada3 = "";
                        ubicacion_archivo_entrada4 = "";
                        ubicacion_archivo_entrada5 = "";
                        ubicacion_archivo_entrada6 = "";
                        ubicacion_archivo_entrada7 = "";
                        ubicacion_archivo_entrada8 = "";
                        ubicacion_archivo_entrada9 = "";
                        ubicacion_archivo_entrada10 = "";
                        ubicacion_archivo_entrada11 = "";
                        ubicacion_archivo_entrada12 = "";
                        ubicacion_archivo_entrada13 = "";
                        ubicacion_archivo_entrada14 = "";
                        ubicacion_archivo_entrada15 = "";
                        ubicacion_archivo_entrada16 = "";
                        ubicacion_archivo_entrada17 = "";
                        ubicacion_archivo_entrada18 = "";
                        ubicacion_archivo_entrada19 = "";
                        ubicacion_archivo_entrada20 = "";
                        ubicacion_archivo_entrada21 = "";
                        ubicacion_archivo_entrada22 = "";
                        ubicacion_archivo_entrada23 = "";
                        ubicacion_archivo_entrada24 = "";
                        ubicacion_archivo_entrada25 = "";
                        ubicacion_archivo_entrada26 = "";
                        ubicacion_archivo_entrada27 = "";
                        ubicacion_archivo_entrada28 = "";
                        ubicacion_archivo_entrada29 = "";
                        ubicacion_archivo_entrada30 = "";
                        ubicacion_archivo_entrada31 = "";
                        ubicacion_archivo_entrada32 = "";
                        ubicacion_archivo_entrada33 = "";
                        ubicacion_archivo_entrada34 = "";
                        ubicacion_archivo_entrada35 = "";
                        ubicacion_archivo_entrada36 = "";
                        ubicacion_archivo_entrada37 = "";
                        ubicacion_archivo_entrada38 = "";

                        #endregion


                    }
                    else
                    {
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name))
                        {
                            //No se reconoce el nombre del archivo como Libro electronico por la longitu de la cadena
                            ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;
                            //MessageBox.Show(fi.Name + "  " +fi.Name.Length);
                            mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            ubicacion_archivo_entrada2 = "";

                        }
                    }

                    #region eliminar carpetas por tickets
                    try
                    {
                        Directory.Delete(folders.FullName, recursive: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.Message);
                    }
                    #endregion

                }

                #endregion


            }
        }


        private void CVD3()
        {

            string comandos = "";
            //Multihilos: Opcion ejecutar Flujos pesados contenidos en Carpetas separadas (Kardex,CVD80pruebas,IB) en simultaneo
            //http://eledwin.com/blog/tutorial-de-hilos-en-c-con-ejemplos-parte-1-31



            //comandos = comandos.Replace("\r\n", " ").Replace("  ", "");

            #region DEFINICION DE RUTAS Y FLUJOS
            //Paso 1: setear la ruta fija de ejecucion de clembatch
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";
            //https://docs.microsoft.com/es-es/dotnet/api/system.io.directoryinfo.getfiles?view=netframework-4.8

            //variables definidas para guardar informacion de los libros electronicos por clientes
            string ruc, año, mes, dia, tipo_libro;


            //ubicacion de flujo diseñado en modeler para cada libro electronico


            string ubicacion_flujo_cvd = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\CVD.str";



            string ubicacion_archivo_entrada = @"J:\COMMON\Eddy Fernando TAX\CVD_3\";
            string ubicacion_archivo_salida = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string consolidado_html = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba";
            string ubicacion_archivo_log = @"J:\TL\Digital Tax\Flujos\";
            //Verificar si las rutas existen
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS");

            if (!Directory.Exists(@"J:\TL\Digital Tax\Flujos"))
                Directory.CreateDirectory(@"J:\TL\Digital Tax\Flujos");

            if (!Directory.Exists(ubicacion_archivo_entrada))
                Directory.CreateDirectory(ubicacion_archivo_entrada);
            //AF
            string ubicacion_archivo_salida2 = "";
            string consolidado_html2 = "";
            string ubicacion_archivo_log2 = "";
            string ubicacion_archivo_entrada2 = "";

            //CVD
            string ubicacion_archivo_entrada3 = "";
            string ubicacion_archivo_entrada4 = "";
            string ubicacion_archivo_entrada5 = "";
            string ubicacion_archivo_entrada6 = "";
            string ubicacion_archivo_entrada7 = "";
            string ubicacion_archivo_entrada8 = "";
            string ubicacion_archivo_entrada9 = "";
            string ubicacion_archivo_entrada10 = "";
            string ubicacion_archivo_entrada11 = "";
            string ubicacion_archivo_entrada12 = "";
            string ubicacion_archivo_entrada13 = "";
            string ubicacion_archivo_entrada14 = "";
            string ubicacion_archivo_entrada15 = "";
            string ubicacion_archivo_entrada16 = "";
            string ubicacion_archivo_entrada17 = "";
            string ubicacion_archivo_entrada18 = "";
            string ubicacion_archivo_entrada19 = "";
            string ubicacion_archivo_entrada20 = "";
            string ubicacion_archivo_entrada21 = "";
            string ubicacion_archivo_entrada22 = "";
            string ubicacion_archivo_entrada23 = "";
            string ubicacion_archivo_entrada24 = "";
            string ubicacion_archivo_entrada25 = "";
            string ubicacion_archivo_entrada26 = "";
            string ubicacion_archivo_entrada27 = "";
            string ubicacion_archivo_entrada28 = "";
            string ubicacion_archivo_entrada29 = "";
            string ubicacion_archivo_entrada30 = "";
            string ubicacion_archivo_entrada31 = "";
            string ubicacion_archivo_entrada32 = "";
            string ubicacion_archivo_entrada33 = "";
            string ubicacion_archivo_entrada34 = "";

            string ubicacion_archivo_entrada35 = "";
            string ubicacion_archivo_entrada36 = "";
            string ubicacion_archivo_entrada37 = "";
            string ubicacion_archivo_entrada38 = "";

            string ubicacion_archivo_salida3 = "";
            string ubicacion_archivo_salida4 = "";

            #endregion


            DirectoryInfo mainfolder = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\CVD_3");

            foreach (var folders in mainfolder.GetDirectories())
            {

                ubicacion_archivo_entrada = folders.FullName + "\\";
                //MessageBox.Show(folders.Name);

                DirectoryInfo di = new DirectoryInfo(folders.FullName);
                //editar campos en LE segun version de formato
                foreach (var fi in di.GetFiles())
                {
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0)
                    {
                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);


                        #region Tipo de LE
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT

                        tipo_libro = fi.Name.Substring(21, 4);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        if ((año == "2020" && mes == "12") || (int.Parse(año) > 2020))
                        {
                            /*
                            Para el(año 2020 y mes 12) o para el año 2021 en adelante en los txt cargados por los usuarios
                                En el libro ventas: eliminar campo 23 
                                En el libro compras: eliminar campo 22
                                 
                            */
                            if (tipo_libro == "1401")
                            {
                                Editar_registros_txt(22, fi.FullName, fi.FullName);

                            }
                            if (tipo_libro == "0801")
                            {
                                Editar_registros_txt(21, fi.FullName, fi.FullName);

                            }
                        }



                        #endregion


                    }
                }


                #region Identificacion iterativa de archivos en la ruta inicial donde se colocan los Libros electronicos
                foreach (var fi in di.GetFiles())
                {
                    //Nomeclatura de Libros electronicos
                    //http://orientacion.sunat.gob.pe/index.php/empresas-menu/libros-y-registros-vinculados-asuntos-tributarios-empresas/sistema-de-libros-electronicos-ple/6560-05-nomenclatura-de-libros-electronicos
                    //Los nombres de libros electronicos tienen 33 caracteres fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name))
                    {
                        HINICIO = DateTime.Now.ToString();

                        ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;

                        ruc = fi.Name.Substring(2, 11);
                        año = fi.Name.Substring(13, 4);
                        mes = fi.Name.Substring(17, 2);
                        dia = fi.Name.Substring(19, 2);
                        //Crear carpeta de resultados con el nombre de ticket
                        if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name))
                            Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\" + folders.Name);

                        //MessageBox.Show(ubicacion_archivo_entrada);
                        #region Tipo de LEkardex
                        //TIPO DE LIBROS SEGUN SUNAT ver Anexo111-2011 SUNAT
                        tipo_libro = fi.Name.Substring(21, 2);
                        //01:CAJA Y BANCOS
                        //03:IB
                        //04:Retenciones
                        //05:Diario
                        //06:Mayor
                        //07:Activo fijo
                        //08:Compras
                        //09:Consignaciones
                        //10:Costos
                        //12:Registro de Inventario Permanente en Unidades Físicas 
                        //13:IB valorizado(kardex)
                        //14:Ventas e ingresos
                        

                        if (fi.Name.Substring(21, 4) == "0501")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion
                        }

                        if (fi.Name.Substring(21, 4) == "0801")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion

                        }

                        if (fi.Name.Substring(21, 4) == "1401")
                        {
                            #region CVD_detecta primero compras y busca venta y diario
                            //detectar si hay txt ventas entrada
                            #region Esta seccion detecta los libros de Ventas mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada3))
                            {
                                ubicacion_archivo_entrada3 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada4))
                            {
                                ubicacion_archivo_entrada4 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada5))
                            {
                                ubicacion_archivo_entrada5 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada6))
                            {
                                ubicacion_archivo_entrada6 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada7))
                            {
                                ubicacion_archivo_entrada7 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada8))
                            {
                                ubicacion_archivo_entrada8 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada9))
                            {
                                ubicacion_archivo_entrada9 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada10))
                            {
                                ubicacion_archivo_entrada10 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada11))
                            {
                                ubicacion_archivo_entrada11 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada12))
                            {
                                ubicacion_archivo_entrada12 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada13 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada13))
                            {
                                ubicacion_archivo_entrada13 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada14 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada14))
                            {
                                ubicacion_archivo_entrada14 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_VENTA_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt compras entrada
                            #region Esta seccion detecta los libros de Compras mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada15 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada15))
                            {
                                ubicacion_archivo_entrada15 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada16 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada16))
                            {
                                ubicacion_archivo_entrada16 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada17 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada17))
                            {
                                ubicacion_archivo_entrada17 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada18 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada18))
                            {
                                ubicacion_archivo_entrada18 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada19 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada19))
                            {
                                ubicacion_archivo_entrada19 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada20 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada20))
                            {
                                ubicacion_archivo_entrada20 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada21 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada21))
                            {
                                ubicacion_archivo_entrada21 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada22 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada22))
                            {
                                ubicacion_archivo_entrada22 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada23 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada23))
                            {
                                ubicacion_archivo_entrada23 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada24 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada24))
                            {
                                ubicacion_archivo_entrada24 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }

                            ubicacion_archivo_entrada25 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada25))
                            {
                                ubicacion_archivo_entrada25 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada26 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada26))
                            {
                                ubicacion_archivo_entrada26 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_COMPRAS_VACIO.TXT";
                            }
                            #endregion
                            //detectar si hay txt diario entrada
                            #region Esta seccion detecta los libros Diarios mes a mes (ene-dic) si no esta algun libro lo reemplaza por un formato vacio
                            ubicacion_archivo_entrada27 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada27))
                            {
                                ubicacion_archivo_entrada27 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada28 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada28))
                            {
                                ubicacion_archivo_entrada28 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada29 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada29))
                            {
                                ubicacion_archivo_entrada29 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada30 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada30))
                            {
                                ubicacion_archivo_entrada30 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada31 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada31))
                            {
                                ubicacion_archivo_entrada31 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada32 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada32))
                            {
                                ubicacion_archivo_entrada32 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada33 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada33))
                            {
                                ubicacion_archivo_entrada33 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada34 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada34))
                            {
                                ubicacion_archivo_entrada34 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada35 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada35))
                            {
                                ubicacion_archivo_entrada35 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada36 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada36))
                            {
                                ubicacion_archivo_entrada36 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada37 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada37))
                            {
                                ubicacion_archivo_entrada37 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            ubicacion_archivo_entrada38 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12);
                            if (!File.Exists(ubicacion_archivo_entrada38))
                            {
                                ubicacion_archivo_entrada38 = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\CVD_3\TEXT_VACIOS\LE_FORMATO_DIARIO_VACIO.TXT";
                            }
                            #endregion

                            consolidado_html2 = consolidado_html + @"\" + folders.Name + @"\" + "ConsolidadoCVD_" + ruc + "_" + dia + "_" + mes + "_" + año + ".html";

                            ubicacion_archivo_salida2 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_C_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida3 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_V_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            ubicacion_archivo_salida4 = ubicacion_archivo_salida + @"\" + folders.Name + @"\" + "CVD_D_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";

                            ubicacion_archivo_log2 = ubicacion_archivo_log + "LogCVD_" + folders.Name + "_" + ruc + "_" + dia + "_" + mes + "_" + año + ".txt";
                            #region String para ejecutar el flujo de Compras-Ventas-Diario via cmd
                            comandos = "clemb -stream " + "\"" + ubicacion_flujo_cvd + "\"" +
                                        " -Pcomprasentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada15 + "\"" +
                                        " -Pcomprasentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada16 + "\"" +
                                        " -Pcomprasentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada17 + "\"" +
                                        " -Pcomprasentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada18 + "\"" +
                                        " -Pcomprasentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada19 + "\"" +
                                        " -Pcomprasentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada20 + "\"" +
                                        " -Pcomprasentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada21 + "\"" +
                                        " -Pcomprasentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada22 + "\"" +
                                        " -Pcomprasentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada23 + "\"" +
                                        " -Pcomprasentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada24 + "\"" +
                                        " -Pcomprasentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada25 + "\"" +
                                        " -Pcomprasentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada26 + "\"" +
                                        " -Pventaentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada3 + "\"" +
                                        " -Pventaentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada4 + "\"" +
                                        " -Pventaentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada5 + "\"" +
                                        " -Pventaentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada6 + "\"" +
                                        " -Pventaentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada7 + "\"" +
                                        " -Pventaentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada8 + "\"" +
                                        " -Pventaentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada9 + "\"" +
                                        " -Pventaentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada10 + "\"" +
                                        " -Pventaentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada11 + "\"" +
                                        " -Pventaentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada12 + "\"" +
                                        " -Pventaentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada13 + "\"" +
                                        " -Pventaentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada14 + "\"" +
                                        " -Pdiarioentrada1.full_filename=" + "\"" + ubicacion_archivo_entrada27 + "\"" +
                                        " -Pdiarioentrada2.full_filename=" + "\"" + ubicacion_archivo_entrada28 + "\"" +
                                        " -Pdiarioentrada3.full_filename=" + "\"" + ubicacion_archivo_entrada29 + "\"" +
                                        " -Pdiarioentrada4.full_filename=" + "\"" + ubicacion_archivo_entrada30 + "\"" +
                                        " -Pdiarioentrada5.full_filename=" + "\"" + ubicacion_archivo_entrada31 + "\"" +
                                        " -Pdiarioentrada6.full_filename=" + "\"" + ubicacion_archivo_entrada32 + "\"" +
                                        " -Pdiarioentrada7.full_filename=" + "\"" + ubicacion_archivo_entrada33 + "\"" +
                                        " -Pdiarioentrada8.full_filename=" + "\"" + ubicacion_archivo_entrada34 + "\"" +
                                        " -Pdiarioentrada9.full_filename=" + "\"" + ubicacion_archivo_entrada35 + "\"" +
                                        " -Pdiarioentrada10.full_filename=" + "\"" + ubicacion_archivo_entrada36 + "\"" +
                                        " -Pdiarioentrada11.full_filename=" + "\"" + ubicacion_archivo_entrada37 + "\"" +
                                        " -Pdiarioentrada12.full_filename=" + "\"" + ubicacion_archivo_entrada38 + "\"" +

                                        " -Pcomprassalida.full_filename=" + "\"" + ubicacion_archivo_salida2 + "\"" +
                                        " -Pventassalida.full_filename=" + "\"" + ubicacion_archivo_salida3 + "\"" +
                                        " -Pdiariosalida.full_filename=" + "\"" + ubicacion_archivo_salida4 + "\"" +

                                        " -Pconsolidadocvd.full_filename=" + "\"" + consolidado_html2 + "\"" +
                                        " -execute -log " + "\"" + ubicacion_archivo_log2 + "\"";
                            #endregion
                            //MessageBox.Show(comandos);
                            //txtComandos.Text = comandos;
                            //MessageBox.Show(comandos);
                            comandos = rutaclemb + " && " + comandos;
                            ejecutar_flujo_modeler(comandos);

                            //mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            #region mover_archivo_existente_vemtas
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada3, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada4, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada5, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada6, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada7, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada8, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada9, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada10, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada11, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada12, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada13, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada14, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_compra
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada15, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada16, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada17, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada18, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada19, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada20, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada21, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada22, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada23, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada24, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada25, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada26, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion
                            #region mover_archivo_existente_diario
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada27, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada28, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada29, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada30, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada31, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada32, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada33, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada34, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada35, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada36, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada37, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));
                            if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                                mover_archivo(ubicacion_archivo_entrada38, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0501" + fi.Name.Substring(fi.Name.Length - 12, 12));

                            #endregion

                            #endregion
                        }
                        #endregion

                        #region LIMPIEZA DE RUTAS UTILIZADAS
                        consolidado_html2 = "";

                        ubicacion_archivo_salida2 = "";
                        ubicacion_archivo_salida3 = "";
                        ubicacion_archivo_salida4 = "";



                        ubicacion_archivo_log2 = "";


                        //CVD
                        ubicacion_archivo_entrada2 = "";
                        ubicacion_archivo_entrada3 = "";
                        ubicacion_archivo_entrada4 = "";
                        ubicacion_archivo_entrada5 = "";
                        ubicacion_archivo_entrada6 = "";
                        ubicacion_archivo_entrada7 = "";
                        ubicacion_archivo_entrada8 = "";
                        ubicacion_archivo_entrada9 = "";
                        ubicacion_archivo_entrada10 = "";
                        ubicacion_archivo_entrada11 = "";
                        ubicacion_archivo_entrada12 = "";
                        ubicacion_archivo_entrada13 = "";
                        ubicacion_archivo_entrada14 = "";
                        ubicacion_archivo_entrada15 = "";
                        ubicacion_archivo_entrada16 = "";
                        ubicacion_archivo_entrada17 = "";
                        ubicacion_archivo_entrada18 = "";
                        ubicacion_archivo_entrada19 = "";
                        ubicacion_archivo_entrada20 = "";
                        ubicacion_archivo_entrada21 = "";
                        ubicacion_archivo_entrada22 = "";
                        ubicacion_archivo_entrada23 = "";
                        ubicacion_archivo_entrada24 = "";
                        ubicacion_archivo_entrada25 = "";
                        ubicacion_archivo_entrada26 = "";
                        ubicacion_archivo_entrada27 = "";
                        ubicacion_archivo_entrada28 = "";
                        ubicacion_archivo_entrada29 = "";
                        ubicacion_archivo_entrada30 = "";
                        ubicacion_archivo_entrada31 = "";
                        ubicacion_archivo_entrada32 = "";
                        ubicacion_archivo_entrada33 = "";
                        ubicacion_archivo_entrada34 = "";
                        ubicacion_archivo_entrada35 = "";
                        ubicacion_archivo_entrada36 = "";
                        ubicacion_archivo_entrada37 = "";
                        ubicacion_archivo_entrada38 = "";

                        #endregion


                    }
                    else
                    {
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name))
                        {
                            //No se reconoce el nombre del archivo como Libro electronico por la longitu de la cadena
                            ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;
                            //MessageBox.Show(fi.Name + "  " +fi.Name.Length);
                            mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                            ubicacion_archivo_entrada2 = "";

                        }
                    }

                    #region eliminar carpetas por tickets
                    try
                    {
                        Directory.Delete(folders.FullName, recursive: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.Message);
                    }
                    #endregion

                }

                #endregion


            }
        }

        public void RDJ2020V2()
        {
            //variables definidas para guardar informacion de los libros electronicos por clientes
            string ruc, año, mes, dia, tipo_libro;
            string ubicacion_archivo_entrada2 = "";
            string ubicacion_txtVacio = @"C:\Users\Luis.Romero.G\Desktop\Flujos_Modeler\Flujograma_RDJ_20\TXT_VACIOS\";
            //ubicacion de flujo diseñado en modeler para cada libro electronico

            string ubicacion_flujo = @"C:\Users\luis.romero.g\Desktop\proyecto\RUTA3.str";

            string ubicacion_flujo_af = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Activo_Fijo5.str";
            string ubicacion_flujo_cvd = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\flujograma_sumatoria_compras_2015.str";
            string ubicacion_flujo_kardex = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Pry_kardex_2018_25032019_GOLFFIEL_1.str";
            string ubicacion_ib = @"C:\Users\luis.romero.g\Desktop\Flujos_Modeler\Proy_INVENT_AZ_18_FINALv9_FINAL_16072019.str";


            string ubicacion_archivo_entrada = @"J:\COMMON\Eddy Fernando TAX\RDJ2020\";
            string ubicacion_archivo_salida = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string consolidado_html = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\";
            string ubicacion_archivo_log = @"J:\TL\Digital Tax\Flujos\";

            //Verificar si las rutas existen
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\NO_PROCESADO_SIZE_100MB");
            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\RDJ2020"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\RDJ2020");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba");

            if (!Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS"))
                Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS");

            if (!Directory.Exists(@"J:\TL\Digital Tax\Flujos"))
                Directory.CreateDirectory(@"J:\TL\Digital Tax\Flujos");



            string script = "";
            string comandos = "";
            //Para probar un script de modeler asignamos una variable segun el formato
            //Paso 1: setear la ruta fija de ejecucion de clembatch
            string rutaclemb = "cd " + @"C:\Program Files\IBM\SPSS\Modeler\18.2.1\bin";
            //Paso 2: generar el sript dinamico para ejecutar el flujo modeler cons us archivos fuentes o inputs
            string flujoRDJ2020 = @"C:\Users\Luis.Romero.G\Desktop\Flujos_Modeler\Flujograma_RDJ_20\FLUJOGRAMA_PRUEBAS_RDJ_2020_VRS_9_14102020_vf_para_script.str";
            string Pventrada1 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190100140100001111.txt";
            string Pventrada2 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190200140100001111.txt";
            string Pventrada3 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190300140100001111.txt";
            string Pventrada4 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190400140100001111.txt";
            string Pventrada5 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190500140100001111.txt";
            string Pventrada6 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190600140100001111.txt";
            string Pventrada7 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190700140100001111.txt";
            string Pventrada8 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190800140100001111.txt";
            string Pventrada9 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120190900140100001111.txt";
            string Pventrada10 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120191000140100001111.txt";
            string Pventrada11 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120191100140100001111.txt";
            string Pventrada12 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\V\LE2051418433120191200140100001111.txt";
            string Pcentrada1 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190100080100001111.txt";
            string Pcentrada2 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190200080100001111.txt";
            string Pcentrada3 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190300080100001111.txt";
            string Pcentrada4 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190400080100001111.txt";
            string Pcentrada5 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190500080100001111.txt";
            string Pcentrada6 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190600080100001111.txt";
            string Pcentrada7 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190700080100001111.txt";
            string Pcentrada8 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190800080100001111.txt";
            string Pcentrada9 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120190900080100001111.txt";
            string Pcentrada10 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120191000080100001111.txt";
            string Pcentrada11 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120191100080100001111.txt";
            string Pcentrada12 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\C\LE2051418433120191200080100001111.txt";
            string Pnodomic1 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190100080200001111.txt";
            string Pnodomic2 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190200080200001111.txt";
            string Pnodomic3 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190300080200001111.txt";
            string Pnodomic4 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190400080200001111.txt";
            string Pnodomic5 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190500080200001111.txt";
            string Pnodomic6 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190600080200001111.txt";
            string Pnodomic7 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190700080200001111.txt";
            string Pnodomic8 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190800080200001111.txt";
            string Pnodomic9 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120190900080200001111.txt";
            string Pnodomic10 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120191000080200001111.txt";
            string Pnodomic11 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120191100080200001111.txt";
            string Pnodomic12 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\NO_DOMIC\LE2051418433120191200080200001111.txt";
            string Plibromayor1 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\FUENTE\L_MAYOR\LM_20100123981_2020.txt";
            string Pconsolidadordj = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\CONSOLIDADO_RDJ1.html";
            string Pcompra = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\COMPRAS.txt";
            string Pventa = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\VENTAS.txt";
            string PRDJRP_006_2 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJRP_006_2.xlsx";
            string PRDJRP_006 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJRP_006.xlsx";
            string PRDJLM_001_RESUMEN = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\PRDJLM_001_RESUMEN.xlsx";
            string PRDJLM_001_DETALLE = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_001_DETALLE.xlsx";
            string PRDJLM_001_DETALLE_TXT = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_001_DETALLE_TXT.txt";
            string PRDJLM_002 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_002.xlsx";
            string PRDJLM_003 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_003.xlsx";
            string PRDJLM_004 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_004.xlsx";
            string PRDJLM_005 = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_005.xlsx";
            string PRDJLM_005_TXT = @"C:\Users\luis.romero.g\Desktop\PROYECTO\PROY_RDJ_2020_LM\CLEMB_BATCH\RESUL\RDJLM_005_TXT.txt";
            string log = @"J:\TL\Digital Tax\Flujos\RJD2020";

            DirectoryInfo di = new DirectoryInfo(@"J:\COMMON\Eddy Fernando TAX\RDJ2020");

            foreach (var fi in di.GetFiles())
            {
                //Los nombres de libros electronicos tienen 33 caracteres fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0)
                {
                    HINICIO = DateTime.Now.ToString();

                    ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;

                    ruc = fi.Name.Substring(2, 11);
                    año = fi.Name.Substring(13, 4);
                    mes = fi.Name.Substring(17, 2);
                    dia = fi.Name.Substring(19, 2);

                    #region Tipo de Libro consumidos
                    tipo_libro = fi.Name.Substring(21, 4);
                    if (File.Exists((@"J:\COMMON\Eddy Fernando TAX\RDJ2020" + "LM_" + ruc + "_" + año + ".txt")))
                    {
                        Plibromayor1 = @"J:\COMMON\Eddy Fernando TAX\RDJ2020" + "LM_" + ruc + "_" + año + ".txt";
                    }
                    if (File.Exists(@"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\" + "LM_" + ruc + "_" + año + ".txt"))
                    {
                        Plibromayor1 = @"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS" + "LM_" + ruc + "_" + año + ".txt";

                    }

                    
                    if (tipo_libro.Equals("1401") || tipo_libro.Equals("0801") || tipo_libro.Equals("0802"))
                    {
                        #region Verificar libros ventas 1401
                        Pventrada1 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada1))
                        {
                            Pventrada1 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada2 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada2))
                        {
                            Pventrada2 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada3))
                        {
                            Pventrada3 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada4))
                        {
                            Pventrada4 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada5))
                        {
                            Pventrada5 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada6))
                        {
                            Pventrada6 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada7))
                        {
                            Pventrada7 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada8))
                        {
                            Pventrada8 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada9))
                        {
                            Pventrada9 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada10))
                        {
                            Pventrada10 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada11))
                        {
                            Pventrada11 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }
                        Pventrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pventrada12))
                        {
                            Pventrada12 = @"" + ubicacion_txtVacio + "LE_FORMATO_VENTA_VACIO.TXT";
                        }

                        #endregion

                        #region Verificar libros compras 0801
                        Pcentrada1 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada1))
                        {
                            Pcentrada1 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada2 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada2))
                        {
                            Pcentrada2 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada3))
                        {
                            Pcentrada3 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada4))
                        {
                            Pcentrada4 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada5))
                        {
                            Pcentrada5 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada6))
                        {
                            Pcentrada6 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada7))
                        {
                            Pcentrada7 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada8))
                        {
                            Pcentrada8 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada9))
                        {
                            Pcentrada9 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada10))
                        {
                            Pcentrada10 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada11))
                        {
                            Pcentrada11 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }
                        Pcentrada12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pcentrada12))
                        {
                            Pcentrada12 = @"" + ubicacion_txtVacio + "LE_FORMATO_COMPRAS_VACIO.TXT";
                        }

                        #endregion

                        #region Verificar libros No domicialiados 0802
                        Pnodomic1 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic1))
                        {
                            Pnodomic1 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic2 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic2))
                        {
                            Pnodomic2 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic3 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic3))
                        {
                            Pnodomic3 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic4 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic4))
                        {
                            Pnodomic4 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic5 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic5))
                        {
                            Pnodomic5 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic6 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic6))
                        {
                            Pnodomic6 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic7 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic7))
                        {
                            Pnodomic7 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic8 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic8))
                        {
                            Pnodomic8 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic9 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic9))
                        {
                            Pnodomic9 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic10 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic10))
                        {
                            Pnodomic10 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic11 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic11))
                        {
                            Pnodomic11 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        Pnodomic12 = ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12);
                        if (!File.Exists(Pnodomic12))
                        {
                            Pnodomic12 = @"" + ubicacion_txtVacio + "LE_FORMATO_NO_DOMICILIADOS.TXT";
                        }
                        #endregion

                        #region Archivos Resultados
                        Pconsolidadordj = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\CONSOLIDADO_RDJ1" + "_" + ruc + "_" + año + ".html";
                        Pcompra = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\COMPRAS" + "_" + ruc + "_" + año + ".txt";
                        Pventa = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\VENTAS" + "_" + ruc + "_" + año + ".txt";
                        PRDJRP_006_2 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJRP_006_2" + "_" + ruc + "_" + año + ".xlsx";
                        PRDJRP_006 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJRP_006" + "_" + ruc + "_" + año + ".xlsx";
                        PRDJLM_001_RESUMEN = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\PRDJLM_001_RESUMEN" + "_" + ruc + "_" + año + ".xlsx";
                        PRDJLM_001_DETALLE = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJLM_001_DETALLE" + "_" + ruc + "_" + año + ".xlsx";
                        PRDJLM_001_DETALLE_TXT = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJLM_001_DETALLE_TXT" + "_" + ruc + "_" + año + ".txt";
                        PRDJLM_002 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJLM_002" + "_" + ruc + "_" + año + ".xlsx";
                        PRDJLM_003 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJLM_003" + "_" + ruc + "_" + año + ".xlsx";
                        PRDJLM_004 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJLM_004" + "_" + ruc + "_" + año + ".xlsx";
                        PRDJLM_005 = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJLM_005" + "_" + ruc + "_" + año + ".xlsx";
                        PRDJLM_005_TXT = @"J:\COMMON\Eddy Fernando TAX\ResultadoPrueba\RDJLM_005_TXT" + "_" + ruc + "_" + año + ".txt";

                        log = @"J:\TL\Digital Tax\Flujos\RDJ2020_" + ruc + "_" + año + ".txt";
                        #endregion

                        #region Script flujo modeler


                        script = "clemb -stream " +
                    "\"" + flujoRDJ2020 + "\"" + " -Pventrada1.full_filename=" +
                    "\"" + Pventrada1 + "\"" + " -Pventrada2.full_filename=" +
                    "\"" + Pventrada2 + "\"" + " -Pventrada3.full_filename=" +
                    "\"" + Pventrada3 + "\"" + " -Pventrada4.full_filename=" +
                    "\"" + Pventrada4 + "\"" + " -Pventrada5.full_filename=" +
                    "\"" + Pventrada5 + "\"" + " -Pventrada6.full_filename=" +
                    "\"" + Pventrada6 + "\"" + " -Pventrada7.full_filename=" +
                    "\"" + Pventrada7 + "\"" + " -Pventrada8.full_filename=" +
                    "\"" + Pventrada8 + "\"" + " -Pventrada9.full_filename=" +
                    "\"" + Pventrada9 + "\"" + " -Pventrada10.full_filename=" +
                    "\"" + Pventrada10 + "\"" + " -Pventrada11.full_filename=" +
                    "\"" + Pventrada11 + "\"" + " -Pventrada12.full_filename=" +
                    "\"" + Pventrada12 + "\"" + " -Pcentrada1.full_filename=" +
                    "\"" + Pcentrada1 + "\"" + " -Pcentrada2.full_filename=" +
                    "\"" + Pcentrada2 + "\"" + " -Pcentrada3.full_filename=" +
                    "\"" + Pcentrada3 + "\"" + " -Pcentrada4.full_filename=" +
                    "\"" + Pcentrada4 + "\"" + " -Pcentrada5.full_filename=" +
                    "\"" + Pcentrada5 + "\"" + " -Pcentrada6.full_filename=" +
                    "\"" + Pcentrada6 + "\"" + " -Pcentrada7.full_filename=" +
                    "\"" + Pcentrada7 + "\"" + " -Pcentrada8.full_filename=" +
                    "\"" + Pcentrada8 + "\"" + " -Pcentrada9.full_filename=" +
                    "\"" + Pcentrada9 + "\"" + " -Pcentrada10.full_filename=" +
                    "\"" + Pcentrada10 + "\"" + " -Pcentrada11.full_filename=" +
                    "\"" + Pcentrada11 + "\"" + " -Pcentrada12.full_filename=" +
                    "\"" + Pcentrada12 + "\"" + " -Pnodomic1.full_filename=" +
                    "\"" + Pnodomic1 + "\"" + " -Pnodomic2.full_filename=" +
                    "\"" + Pnodomic2 + "\"" + " -Pnodomic3.full_filename=" +
                    "\"" + Pnodomic3 + "\"" + " -Pnodomic4.full_filename=" +
                    "\"" + Pnodomic4 + "\"" + " -Pnodomic5.full_filename=" +
                    "\"" + Pnodomic5 + "\"" + " -Pnodomic6.full_filename=" +
                    "\"" + Pnodomic6 + "\"" + " -Pnodomic7.full_filename=" +
                    "\"" + Pnodomic7 + "\"" + " -Pnodomic8.full_filename=" +
                    "\"" + Pnodomic8 + "\"" + " -Pnodomic9.full_filename=" +
                    "\"" + Pnodomic9 + "\"" + " -Pnodomic10.full_filename=" +
                    "\"" + Pnodomic10 + "\"" + " -Pnodomic11.full_filename=" +
                    "\"" + Pnodomic11 + "\"" + " -Pnodomic12.full_filename=" +
                    "\"" + Pnodomic12 + "\"" + " -Plibromayor1.full_filename=" +
                    "\"" + Plibromayor1 + "\"" + " -Pconsolidadordj.full_filename=" +
                    "\"" + Pconsolidadordj + "\"" + " -Pcompra.full_filename=" +
                    "\"" + Pcompra + "\"" + " -Pventa.full_filename=" +
                    "\"" + Pventa + "\"" + " -PRDJRP_006_2.full_filename=" +
                    "\"" + PRDJRP_006_2 + "\"" + " -PRDJRP_006.full_filename=" +
                    "\"" + PRDJRP_006 + "\"" + " -PRDJLM_001_RESUMEN.full_filename=" +
                    "\"" + PRDJLM_001_RESUMEN + "\"" + " -PRDJLM_001_DETALLE.full_filename=" +
                    "\"" + PRDJLM_001_DETALLE + "\"" + " -PRDJLM_001_DETALLE_TXT.full_filename=" +
                    "\"" + PRDJLM_001_DETALLE_TXT + "\"" + " -PRDJLM_002.full_filename=" +
                    "\"" + PRDJLM_002 + "\"" + " -PRDJLM_003.full_filename=" +
                    "\"" + PRDJLM_003 + "\"" + " -PRDJLM_004.full_filename=" +
                    "\"" + PRDJLM_004 + "\"" + " -PRDJLM_005.full_filename=" +
                    "\"" + PRDJLM_005 + "\"" + " -PRDJLM_005_TXT.full_filename=" +
                    "\"" + PRDJLM_005_TXT + "\"" + " -execute -log " +
                    "\"" + log + "\"";
                        #endregion
                        comandos = "";
                        comandos = rutaclemb + " && " + script + " ";
                        //comandos=variable;

                        ejecutar_flujo_modeler(comandos);


                        #region Mover Archivos usados en el script

                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada1, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada2, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada3, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada4, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada5, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada6, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada7, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada8, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada9, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada10, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada11, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pventrada12, fi.Name.Substring(0, 17) + "12" + "1401" + fi.Name.Substring(fi.Name.Length - 12, 12));



                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada1, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada2, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada3, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada4, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada5, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada6, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada7, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada8, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada9, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada10, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada11, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pcentrada12, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0801" + fi.Name.Substring(fi.Name.Length - 12, 12));



                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic1, fi.Name.Substring(0, 17) + "01" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic2, fi.Name.Substring(0, 17) + "02" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic3, fi.Name.Substring(0, 17) + "03" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic4, fi.Name.Substring(0, 17) + "04" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic5, fi.Name.Substring(0, 17) + "05" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic6, fi.Name.Substring(0, 17) + "06" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic7, fi.Name.Substring(0, 17) + "07" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic8, fi.Name.Substring(0, 17) + "08" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic9, fi.Name.Substring(0, 17) + "09" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic10, fi.Name.Substring(0, 17) + "10" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic11, fi.Name.Substring(0, 17) + "11" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));
                        if (File.Exists(ubicacion_archivo_entrada + fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12)))
                            mover_archivo(Pnodomic12, fi.Name.Substring(0, 17) + "12" + fi.Name.Substring(19, 2) + "0802" + fi.Name.Substring(fi.Name.Length - 12, 12));


                        #endregion

                    }
                    else
                    {
                        mover_archivo(ubicacion_archivo_entrada2, fi.Name);

                    }
                    #endregion


                    #region Limpieza de variables

                    ubicacion_archivo_entrada2 = "";

                    Pventrada1 = "";
                    Pventrada2 = "";
                    Pventrada3 = "";
                    Pventrada4 = "";
                    Pventrada5 = "";
                    Pventrada6 = "";
                    Pventrada7 = "";
                    Pventrada8 = "";
                    Pventrada9 = "";
                    Pventrada10 = "";
                    Pventrada11 = "";
                    Pventrada12 = "";
                    Pcentrada1 = "";
                    Pcentrada3 = "";
                    Pcentrada4 = "";
                    Pcentrada5 = "";
                    Pcentrada6 = "";
                    Pcentrada7 = "";
                    Pcentrada8 = "";
                    Pcentrada9 = "";
                    Pcentrada10 = "";
                    Pcentrada11 = "";
                    Pcentrada12 = "";
                    Pnodomic1 = "";
                    Pnodomic2 = "";
                    Pnodomic3 = "";
                    Pnodomic4 = "";
                    Pnodomic5 = "";
                    Pnodomic6 = "";
                    Pnodomic7 = "";
                    Pnodomic8 = "";
                    Pnodomic9 = "";
                    Pnodomic10 = "";
                    Pnodomic11 = "";
                    Pnodomic12 = "";
                    Plibromayor1 = "";

                    #endregion
                }
                else
                {
                    if (File.Exists(ubicacion_archivo_entrada + fi.Name))
                    {
                        //No se reconoce el nombre del archivo como Libro electronico por la longitu de la cadena
                        ubicacion_archivo_entrada2 = ubicacion_archivo_entrada + fi.Name;
                        //MessageBox.Show(fi.Name + "  " +fi.Name.Length);
                        mover_archivo(ubicacion_archivo_entrada2, fi.Name);
                        ubicacion_archivo_entrada2 = "";

                    }

                }

            }



        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            mainFlujos();
        }
        private void mover_archivo(string ruta_origen, string archivo)
        {
            try
            {
                //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders
                string destinationFile = @"J:\COMMON\Eddy Fernando TAX\TXT_PROCESADOS\" + archivo;

                #region En caso se tenga un archivo con el mismo nombre se elimina para poder mover el archivo traido por parametro
                if (File.Exists(destinationFile))
                    File.Delete(destinationFile);
                #endregion

                System.IO.File.Move(ruta_origen, destinationFile);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainFlujos();
        }
        private void Editar_registros_txt(int nro_columna, string ruta_txt, string nueva_ruta_txt)
        {

            string[] lines = System.IO.File.ReadAllLines(ruta_txt);


            string contenido = "";

            foreach (string line in lines)
            {

                var cols = line.Split('|');
                var numcol = cols.Count();
                //col[0]

                for (int j = 0; j < numcol; j++)
                {
                    if (j != nro_columna)
                    {
                        if (j < numcol)
                        {
                            contenido += cols[j] + '|';
                        }
                        else
                        {
                            contenido += cols[j];
                        }

                    }

                }
                contenido += "\r\n";


            }
            File.WriteAllText(nueva_ruta_txt, contenido);


        }

    }
}

