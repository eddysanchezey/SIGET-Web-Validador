using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliminarCamposLE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int Count_columns_txt(string path  )
        {
            char delimiter = '|';
            int columnCount = -1; // or put the number if it's known

            var errors = File
              .ReadLines(path, Encoding.UTF8) // UTF-8 is default and can be skipped
              .Select((line, index) => {
                  int count = line.Split(delimiter).Length;

                  if (columnCount < 0)
                      columnCount = count;

                  return new
                  {
                      line = line,
                      count = count,
                      index = index
                  };
              })
              .Where(chunk => chunk.count != columnCount)
              .Select(chunk => String.Format("Line #{0} \"{1}\" has {2} items when {3} expected",
                 chunk.index + 1, chunk.line, chunk.count, columnCount));

            // To check if file has any wrong lines:
            if (errors.Any())
            {
                //...
            }

            // To print out a report on wrong lines
            Console.Write(( errors));

            //Number de columns en txt
            Console.Write(columnCount);

            return columnCount;
        }
        private void Editar_registros_txt( int nro_columna, string ruta_txt, string nueva_ruta_txt)
        {

            string[] lines = System.IO.File.ReadAllLines(ruta_txt );


            string contenido = "";

            foreach (string line in lines)
            {
                
                var cols = line.Split('|');
                var numcol=cols.Count();
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
                contenido +=  "\r\n";
                

            }
            File.WriteAllText(nueva_ruta_txt, contenido);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int numColumns= Count_columns_txt(@"C:\Users\VS943JJ\OneDrive - EY\Desktop\T440-Eddy\txt.txt");
            //Editar_registros_txt(1,@"C:\Users\VS943JJ\OneDrive - EY\Desktop\T440-Eddy\txt.txt", @"C:\Users\VS943JJ\OneDrive - EY\Desktop\T440-Eddy\txt2.txt");
            Verificartxt();
        }
        private void Verificartxt()
        {
            var ubicacion_archivo_entrada = "";
            var ruc = "";
            var año = "";
            var mes = "";
            var tipo_libro = "";
            //Iteramos dentro de SIAF las carpetas con el nombre de  Ticket respectivas
            //J:\COMMON\Eddy Fernando TAX\SIAF
            DirectoryInfo mainfolder = new DirectoryInfo(@"C:\Users\VS943JJ\OneDrive - EY\Desktop\T440-Eddy\yaku\periodos - Copy\");
            foreach (var folders in mainfolder.GetDirectories())
            {
                //Ruta de la carpeta con nombre de Ticket
                ubicacion_archivo_entrada = folders.FullName + "\\";

                //Verificamos los archivos en la carpeta Ticket
                DirectoryInfo di = new DirectoryInfo(folders.FullName);
                foreach (var fi in di.GetFiles())
                {

                    //Los nombres de libros electronicos tienen 40 caracteres fi.Name.Length == 40 && File.Exists(ubicacion_archivo_entrada + fi.Name)
                    if (fi.Name.Length == 37 && File.Exists(ubicacion_archivo_entrada + fi.Name) && fi.Length > 0 )
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



                    #region eliminar carpetas por tickets
                    try
                    {
                        //Directory.Delete(folders.FullName, recursive: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.Message);
                    }
                    #endregion

                }
            }

        }
        /*
        private void LecturaExcel()
        {
            byte[] byteArray = File.ReadAllBytes("C:\\temp\\oldName.xltx");
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);
                using (SpreadsheetDocument spreadsheetDoc = SpreadsheetDocument.Open(stream, true))
                {
                    // Do work here
                }
                File.WriteAllBytes("C:\\temp\\newName.xlsx", stream.ToArray());
            }
        }
        */
    }
}
