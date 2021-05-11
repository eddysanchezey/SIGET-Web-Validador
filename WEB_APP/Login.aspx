<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEB_APP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIGET</title>
    <link href="css/Estilos_login.css" rel="stylesheet" />
</head>
<body style="background-color:#191919;">
    <form id="form1" runat="server">
        
        <div class="box" style="width:300px;padding:40px;position:absolute;top:50%;left:50%;transform:translate(-50%,-50%);background:#191919;text-align:center;color:#ffffff">
            
            <img  border="0" src="Imagen/EY_transparente.png" style="height:15vh;"<%--style="height:1.116in;width:1.291in" v:shapes="Picture_x0020_1" width="124"--%> /><![endif]>

            <h3>Sistema integrado de gestión electrónica de tickets</h3>

            <div class="aps_login" >
                
            <asp:Login ID="Login1" runat="server" onauthenticate="Login1_Authenticate" LoginButtonText="Ingresar" PasswordLabelText="Contraseña" RememberMeText="Recordarme la proxima vez." TitleText=""
            UserNameLabelText="Correo"  Width="295px" BorderColor="#3498DB" > </asp:Login>
            </div>

        </div>
        

    </form>
</body>
</html>
