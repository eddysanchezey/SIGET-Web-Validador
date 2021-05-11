using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB_APP
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            #region carpetas requeridas para mover los archivos donde el robot procesador de Libros electronicos pueda procesarlos
            if (!System.IO.Directory.Exists(@"J:\COMMON\Eddy Fernando TAX"))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX");
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }

            }
            if (!System.IO.Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\PLB_Kardex_Adic"))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\PLB_Kardex_Adic");
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }

            }
            if (!System.IO.Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\PLB_NO_CVD"))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\PLB_NO_CVD");
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }

            }
            if (!System.IO.Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\CVD_1"))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\CVD_1");
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }

            }
            if (!System.IO.Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\CVD_2"))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\CVD_2");
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }

            }
            if (!System.IO.Directory.Exists(@"J:\COMMON\Eddy Fernando TAX\CVD_3"))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(@"J:\COMMON\Eddy Fernando TAX\CVD_3");
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }

            }
            #endregion
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                bool Autenticado = false;
                

                ServiceReference1.WSSoapClient wS = new ServiceReference1.WSSoapClient();


                //devolver correo de un usuario con estado activo (1) según los datos registrados en Login
                string correo_devuelto = (wS.RetornarAcceso(Login1.UserName, Login1.Password).ToString());
                Autenticado = LoginCorrecto(correo_devuelto, Login1.UserName);
               

                //Muestra en el formulario si el usuario y contraseña son validos
                e.Authenticated = Autenticado;
                if (Autenticado)
                {
                    Response.Redirect("Formulario_web.aspx?UserName_ey=" + Login1.UserName);//Paso como valor el user_name del loging
                                                                                            //En el formulario Formulario_WEb recupero el valor pasado en page_load 
                                                                                            //String UserName_ey = request.QueryString["UserName_ey"]
                                                                                            //TextBox2.Text = UserName_ey;
                                                                                            //Response.Redirect("Formulario_web.aspx");

                }
            }
            catch
            {

            }
            
        }

        bool LoginCorrecto(string correo_devuelto, string UserName)
        {
            if (correo_devuelto == UserName)
                return true;
            else
                return false;
        }
    }
       
}