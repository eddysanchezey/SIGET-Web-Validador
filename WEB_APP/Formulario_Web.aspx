<%@ Page Language="C#"  CodeBehind="Formulario_Web.aspx.cs" Inherits="WEB_APP.Formulario_Web" %>

<!DOCTYPE html>
<script runat="server">
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            CheckBoxList3.Items[0].Selected = false;
            CheckBoxList3.Items[1].Selected = false;
            CheckBoxList3.Items[2].Selected = false;

            foreach (ListItem items in RadioButtonList2.Items)
            {
                if (items.Selected == true)
                {
                    items.Selected = false;
                }
            }
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            CheckBoxList3.Items[0].Selected = false;
            CheckBoxList3.Items[1].Selected = false;
            CheckBoxList3.Items[2].Selected = false;

            foreach (ListItem items in RadioButtonList1.Items)
            {
                if (items.Selected == true)
                {
                    items.Selected = false;
                }
            }
        }
    }
    protected void CheckBoxList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (CheckBoxList3.Items[0].Selected == true || CheckBoxList3.Items[1].Selected == true || CheckBoxList3.Items[2].Selected == true)
            {
                //CheckBoxList4.Items[0].Selected = false;
                //CheckBoxList4.Items[1].Selected = false;
                //CheckBoxList4.Items[2].Selected = false;
                //CheckBoxList4.Items[3].Selected = false;
                //CheckBoxList4.Items[4].Selected = false;

                CheckBoxList2.Items[0].Selected = false;
                CheckBoxList2.Items[1].Selected = false;
                CheckBoxList2.Items[2].Selected = false;
                CheckBoxList2.Items[3].Selected = false;
                CheckBoxList2.Items[4].Selected = false;

                RadioButtonList1.Items[0].Selected = false;
                RadioButtonList1.Items[1].Selected = false;
                RadioButtonList1.Items[2].Selected = false;
                RadioButtonList1.Items[3].Selected = false;
                RadioButtonList1.Items[4].Selected = false;

                RadioButtonList2.Items[0].Selected = false;
                RadioButtonList2.Items[1].Selected = false;
                RadioButtonList2.Items[2].Selected = false;
                RadioButtonList2.Items[3].Selected = false;
                RadioButtonList2.Items[4].Selected = false;
                RadioButtonList2.Items[5].Selected = false;
                RadioButtonList2.Items[6].Selected = false;
                RadioButtonList2.Items[7].Selected = false;

            }
        }
        //foreach (ListItem item in CheckBoxList3.Items)
        //{
        //    if (item.Selected == true)
        //    {
        //        CheckBoxList4.Items[0].Selected = false;
        //        CheckBoxList4.Items[1].Selected = false;
        //        CheckBoxList4.Items[2].Selected = false;
        //        CheckBoxList4.Items[3].Selected = false;
        //        CheckBoxList4.Items[4].Selected = false;

        //        CheckBoxList2.Items[0].Selected = false;
        //        CheckBoxList2.Items[1].Selected = false;
        //        CheckBoxList2.Items[2].Selected = false;
        //        CheckBoxList2.Items[3].Selected = false;
        //    }
        //}
    }

    protected void CheckBoxList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (CheckBoxList4.Items[0].Selected == true || CheckBoxList4.Items[1].Selected == true || CheckBoxList4.Items[2].Selected == true || CheckBoxList4.Items[3].Selected == true || CheckBoxList4.Items[4].Selected == true)
            {
                CheckBoxList2.Items[0].Selected = false;
                CheckBoxList2.Items[1].Selected = false;
                CheckBoxList2.Items[2].Selected = false;
                CheckBoxList2.Items[3].Selected = false;
                CheckBoxList2.Items[4].Selected = false;

                CheckBoxList3.Items[0].Selected = false;
                CheckBoxList3.Items[1].Selected = false;
                CheckBoxList3.Items[2].Selected = false;

                if (CheckBoxList4.Items[0].Selected == true)
                {
                    CheckBoxList4.Items[1].Selected = false;
                    CheckBoxList4.Items[2].Selected = false;
                    CheckBoxList4.Items[3].Selected = false;
                    CheckBoxList4.Items[4].Selected = false;
                }
                if (CheckBoxList4.Items[1].Selected == true)
                {
                    CheckBoxList4.Items[0].Selected = false;
                    CheckBoxList4.Items[2].Selected = false;
                    CheckBoxList4.Items[3].Selected = false;
                    CheckBoxList4.Items[4].Selected = false;
                }
                if (CheckBoxList4.Items[2].Selected == true)
                {
                    CheckBoxList4.Items[0].Selected = false;
                    CheckBoxList4.Items[1].Selected = false;
                    CheckBoxList4.Items[3].Selected = false;
                    CheckBoxList4.Items[4].Selected = false;
                }
                if (CheckBoxList4.Items[3].Selected == true)
                {
                    CheckBoxList4.Items[0].Selected = false;
                    CheckBoxList4.Items[1].Selected = false;
                    CheckBoxList4.Items[2].Selected = false;
                    CheckBoxList4.Items[4].Selected = false;
                }
                if (CheckBoxList4.Items[4].Selected == true)
                {
                    CheckBoxList4.Items[0].Selected = false;
                    CheckBoxList4.Items[1].Selected = false;
                    CheckBoxList4.Items[2].Selected = false;
                    CheckBoxList4.Items[3].Selected = false;
                }

            }

        }
        foreach (ListItem item in CheckBoxList4.Items)
        {

            if (item.Selected == true)
            {

                // this is the one selected before the user click on current checkbox
                CheckBoxList2.Items[0].Selected = false;
                CheckBoxList2.Items[1].Selected = false;
                CheckBoxList2.Items[2].Selected = false;
                CheckBoxList2.Items[3].Selected = false;
                CheckBoxList2.Items[4].Selected = false;

                CheckBoxList3.Items[0].Selected = false;
                CheckBoxList3.Items[1].Selected = false;
                CheckBoxList3.Items[2].Selected = false;
            }
        }
    }

    protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {//Cambio 24/09/2019
            if (CheckBoxList2.Items[0].Selected == true || CheckBoxList2.Items[1].Selected == true || CheckBoxList2.Items[2].Selected == true || CheckBoxList2.Items[3].Selected == true || CheckBoxList2.Items[4].Selected == true)
            {
                CheckBoxList2.Items[1].Selected = true;
                CheckBoxList2.Items[2].Selected = true;
                CheckBoxList2.Items[3].Selected = true;
                CheckBoxList2.Items[4].Selected = true;


                CheckBoxList4.Items[0].Selected = false;
                CheckBoxList4.Items[1].Selected = false;
                CheckBoxList4.Items[2].Selected = false;
                CheckBoxList4.Items[3].Selected = false;
                CheckBoxList4.Items[4].Selected = false;

                CheckBoxList3.Items[0].Selected = false;
                CheckBoxList3.Items[1].Selected = false;
                CheckBoxList3.Items[2].Selected = false;

                if (CheckBoxList2.Items[0].Selected == true)
                {
                    CheckBoxList2.Items[1].Selected = false;
                    CheckBoxList2.Items[2].Selected = false;
                    CheckBoxList2.Items[3].Selected = false;
                    CheckBoxList2.Items[4].Selected = false;
                }
                if (CheckBoxList2.Items[1].Selected == true || CheckBoxList2.Items[2].Selected == true || CheckBoxList2.Items[3].Selected == true || CheckBoxList2.Items[4].Selected == true)
                { CheckBoxList2.Items[0].Selected = false; }


            }
        }
        //foreach (ListItem item in CheckBoxList2.Items)
        //{
        //    if (item.Selected == true)
        //    {
        //        CheckBoxList4.Items[0].Selected = false;
        //        CheckBoxList4.Items[1].Selected = false;
        //        CheckBoxList4.Items[2].Selected = false;
        //        CheckBoxList4.Items[3].Selected = false;
        //        CheckBoxList4.Items[4].Selected = false;

        //        CheckBoxList3.Items[0].Selected = false;
        //        CheckBoxList3.Items[1].Selected = false;
        //        CheckBoxList3.Items[2].Selected = false;
        //    }
        //}
    }

    public void textbox_KeyUp(KeyAttribute e)
    {
       
        if (e.TypeId.Equals(13))
        {
            VistaBuscarTicket();
            BuscarTicket();
        }
        
    }
    

</script>
<script type="text/javascript">
    function textbox_KeyUpC(e) {
        /*para browsers antiguos IE 10
        if (e.keyCode == 13) {
            e.preventDefault();
        }
        */
        //console.log('hola');
        if (e.which == 13) {
            //BuscarTicket();
            return false;
            
        }

        return true;

        /*
         if (event.which == 13 || event.keyCode == 13) {
        //code to execute here
        return false;
        }
        return true;
        */
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIGET</title>

    <link href="css/Estilos.css" rel="stylesheet" />
    <link href="css/popup.css" rel="stylesheet" type="text/css" />

    
</head>
    
     

<body>
    <form id="form1" runat="server">
        <div id="Cabecera" class="Cabecera_class">
             <div id="logo" class="logo_class">
                 <img  border="0" src="Imagen/EY_transparente.png" style="height:15vh;"<%--style="height:1.116in;width:1.291in" v:shapes="Picture_x0020_1" width="124"--%> />
             </div>   
             <div id="logotext" class="logotextclass" >
                <asp:Label ID="Label1" runat="server"    Text="Sistema integrado de gestión electrónica de tickets" style="height:5vh"></asp:Label>
                 <div></div>
                 <asp:Label ID="Label2" runat="server"    Text="" style="height:5vh"></asp:Label>

            </div>
                       

        </div>
        <div class="Lienzo">

            <div id="Menu" class="Menu_class">
            
            <div class="BtnCrear">
                <div class="BtnCrear_">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Registrar ticket"  Width="100%"  />
                </div>
                <div class="BtnConsultar_">
                    
                    <asp:Button ID="Button2" runat="server" Text="Consultar estado"  Width="100%" OnClick="Button2_Click1"  />
                </div>
                <div class="BtnEditar_">
                    
                    <asp:Button ID="Button5" runat="server" Text="Editar Estado "  Width="100%" OnClick="Button5_Click"   />
                </div>
            
            </div>
            
        </div><div id="Cuerpo" class="Cuerpo_class">
            
            <div class="Titulo_buscar"  runat="server" id="Titulo_registrar_valor" >
                 <div class="Titulo_registrar_valor">Registrar ticket</div>

            </div>
            <div class="CrearTicket" runat="server" id="CrearTicketid">
                                
                <div class="Usuario">
                    <div>
                        Sub service line
                    </div>
                    <div>
                        <asp:TextBox ID="TextBox6" runat="server" Enabled="False"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="TextBox6" ErrorMessage="*Ingrese solo letras"
                                ForeColor="Red"
                                ValidationExpression="^[A-Za-z]*$">
                            </asp:RegularExpressionValidator>
                    </div>
                    
                </div>
                <div class="Gerente">
                    <div class="Gerente_label">
                        Autorizado por:</div>
                    <%--<div class="Gerente_text">
                        <asp:TextBox ID="TextBox8" runat="server" Enabled="False"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ControlToValidate="TextBox8" ErrorMessage="*Ingrese solo letras"
                                ForeColor="Red"
                                ValidationExpression="^[A-Za-z\s,\@]*$">
                            </asp:RegularExpressionValidator>
                    
                </div>--%>
                    <select id="Select1" runat="server" onchange="Cambiarlabel" name="D1">

                            </select>
                 </div>
                <div class="Job">
                    <div class="Job_label">
                    Job activo
                    </div>
                    
                    <div class="Job_text">
                        <%--<asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="TextBox7" ErrorMessage="*Ingrese Valores Numericos"
                                ForeColor="Red"
                                ValidationExpression="^[0-9]*">
                            </asp:RegularExpressionValidator>--%>
                        <div>
                            <select id="Select2" runat="server" onchange="Cambiarlabel" name="D1">

                            </select>
                        <asp:Button ID="Button7" runat="server" Text="Ver jobs" OnClick="Button7_Click" />
                        </div>
                    </div>
                    
                </div>
                <div class="Senior">
                    <div class="Senior_label">
                        Solicitado por:
                    </div>
                    <div class="Senior_text">
                        <asp:TextBox ID="TextBox9" runat="server" Enabled="False"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                ControlToValidate="TextBox9" ErrorMessage="*Ingrese solo letras"
                                ForeColor="Red"
                                ValidationExpression="^[A-Za-z\s-\s_\-\s,\@\.]*$">
                            </asp:RegularExpressionValidator>
                    </div>
                    
                </div>

<%--                <div class="FechIng">
                </div>--%>

                <div class="TipoLib">
                    
                    <div class="libro_mensual" aria-disabled="False"> 
                        <p style="margin-bottom: 9px">Revisión digital de Libros mensuales</p>
                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" OnSelectedIndexChanged="CheckBoxList3_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem>Compras</asp:ListItem>
                            <asp:ListItem>Ventas</asp:ListItem>
                            <asp:ListItem>Diario</asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                    </div>
                    <div class="libro_anual"> 
                        <p style="margin-bottom: 9px">Revision digital de Libros anuales</p>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem>Activo Fijo</asp:ListItem>
                            <asp:ListItem>Kardex</asp:ListItem>
                            <asp:ListItem>Inventario y Balance</asp:ListItem>
                            <asp:ListItem>Costo</asp:ListItem>
                            <asp:ListItem Value="Kardex Adicionales"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CheckBoxList ID="CheckBoxList4" runat="server" OnSelectedIndexChanged="CheckBoxList4_SelectedIndexChanged" AutoPostBack="true" Visible="False">
                            <asp:ListItem>Activo Fijo</asp:ListItem>
                            <asp:ListItem>Kardex</asp:ListItem>
                            <asp:ListItem>Inventario y Balance</asp:ListItem>
                            <asp:ListItem>Costo</asp:ListItem>
                            <asp:ListItem>Kardex Adicionales</asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                    </div>
                    <div class="libro_rdj"> 
                        <p style="margin-bottom: 9px">Pruebas&nbsp; digitales&nbsp; para RDJ&nbsp;&nbsp; </p>
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Enabled="False">RDJ 5 pruebas/eficiencia</asp:ListItem>
                            <asp:ListItem>RDJ versión 2020</asp:ListItem>
                            <asp:ListItem>SIAF</asp:ListItem>
                            <asp:ListItem>Otras Pruebas DJ</asp:ListItem>
                            <asp:ListItem Enabled="True">Lectura XML Ventas/Compras</asp:ListItem>
                            <asp:ListItem>Lectura XML Ventas(LE)*</asp:ListItem>
                            <asp:ListItem>Lectura XML Compras(LE)*</asp:ListItem>
                            <asp:ListItem>Cruce FV y LE</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" OnSelectedIndexChanged="CheckBoxList2_SelectedIndexChanged" AutoPostBack="true" Visible="False">
                        <asp:ListItem>RDJ_eficiencia</asp:ListItem>
                            <asp:ListItem>RDJ_extendida Compras</asp:ListItem>
                            <asp:ListItem>RDJ_extendida diario 5.1</asp:ListItem>
                            <asp:ListItem>RDJ_extendida diario 5.3</asp:ListItem>
                            <asp:ListItem>RDJ_extendida Ventas</asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                    </div>
                
                
                </div>
                <%-- <div class="separacion">--%><%--  --%>
                    <div id="popup" class="overlay">
                       <div id="popupBody">
                           <h3>Atención</h3>
                           <a id="cerrar" href="#">&times;</a>
                           <div class="popupContent">
                                                  
                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Enabled="True">Comunicado!!!!

Sino cargas los Libros electrónicos luego de 90 minutos de generado el ticket este será anulado ( el ticket no se podrá usar).Debes de crear otro ticket. 
Los libros electrónicos a cargar debe de ser txt con el formato PLE y no zipiados.
                                    
Pruebas digitales para RDJ!!

La cantidad de Libros electrónicos de Compras / Ventas deben ser iguales y del mismo periodo. 
Cargar como mínimo un libro diario 5.1 y 5.2 de lo contrario no se procesará el ticket y será anulado.

Pruebas digitales para RDJ eficiencia!!

Ejecuta 5 pruebas.

Pruebas digitales para RDJ Compras-Ventas-Diario!!
Ejecuta 20 pruebas previa coordinacion con en equipo de Digital.

                                </asp:TextBox>
                    
                           </div>
                       </div>
                    </div>
                <%--  --%><%-- </div>--%>
                <div class="FechIng">
                      <p><a href="#popup">Tips para generar tickets </a></p>                    
                      <%--                    <asp:Button ID="Button5" runat="server"  OnClick="Button5_Click" Text="Verificar " target="#popup" Enabled="True" Width="130px" />
                    <a id="Ver" runat="server" href="#popup">  Ver</a>--%>

                </div>
                <div class="CodTicket">
                    
                    <asp:Button ID="Button4" runat="server" Text="Registrar ticket" OnClick="Button8_Click" />
                    <asp:TextBox ID="TextBox10" runat="server" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                    
                </div>

            </div>
            <div class="Titulo_registrar"  runat="server" id="Titulo_buscar_valor">
                 <div class="Titulo_buscar_valor" >Consultar estado de ticket</div>

            </div>
            
            <div class="BuscartTicket" runat="server" id="BuscarTicketid">
                
                <div class="BtnBuscar">
                    <asp:Button ID="Button3" runat="server" Text="Buscar ticket" OnClick="Button3_Click" />
                    <asp:TextBox ID="TextBox11" runat="server" placeholder="DG-####" onkeypress="return textbox_KeyUpC(event)"   keydown="textbox_KeyUp"  ToolTip="Escriba un ticket de la forma: DG-####"  ></asp:TextBox>
                </div>
                <div class="texto">El último ticket por procesar es: 
                    
                    <asp:Label ID="TicketProcesando" runat="server" Text="Ticket Procesando" OnInit="TicketProcesando_Init"></asp:Label>
                    
                </div>
                <div class="texto">El número de tickets procesando es: 
                    
                    <asp:Label ID="NumeroProcesando" runat="server" Text="" OnInit="TicketProcesando_Init"></asp:Label>
                    
                </div>
                <div class="TextBuscar">
                    <br />
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                    <br />
                    <br />
                    <p>***En caso la fecha prevista fuese un día no laborable, los resultados son entregados el siguiente primer dia laborable a primera hora.***</p>
                    </div>
            </div>

            <div class="Titulo_editarestado"  runat="server" id="Titulo_editarestado_valor">
                 <div class="Titulo_buscar_valor" >Editar estado de ticket</div>

            </div>
            <div class="EditarEstado" runat="server" id="EditarEstadoid">
                
                <div class="BtnBuscar">
                    <asp:Button ID="Button6" runat="server" Text="Editar ticket" OnClick="Button6_Click" />
                    <asp:TextBox ID="TextBox2" runat="server" placeholder="DG-####" onkeypress="return textbox_KeyUpC(event)"   keydown="textbox_KeyUp"  ToolTip="Escriba un ticket de la forma: DG-####"  ></asp:TextBox>
                    <asp:DropDownList ID="listaOpcionesEstados" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="188px">
                        <asp:ListItem>Anulado</asp:ListItem>
                        <asp:ListItem>Procesando</asp:ListItem>
                        <asp:ListItem>Terminado</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox12" runat="server" placeholder="mm/dd/yyyy" TextMode="Date"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        </div>
        <div class="Pie_class"></div>
    </form>
</body>
</html>
