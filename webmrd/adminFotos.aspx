<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adminFotos.aspx.vb" Inherits="WebMRD.adminFotos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title runat="server" id="PageTitle">Administracion de Fotos de Rio Dorado</title>
    <meta name="viewport" content="width=device-width; initial-scale=0.5; maximum-scale=2.0;user-scalable=true;" />
    <link rel="apple-touch-icon" href="images/bookmarkfavicon.png"/>
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico" /> 
    <link rel="icon" type="image/x-icon" href="images/favicon.ico" />
    <link href="styles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
    
        var isIE4up = (document.all) ? 1 : 0;
		var isIE5 = (isIE4up && navigator.appVersion.indexOf("MSIE 5") != -1) ? 1 : 0;
		var isIE6 = (isIE4up && navigator.appVersion.indexOf("MSIE 6") != -1) ? 1 : 0;
    
        function pageLoad() {
            updateOrientation(); 
        }

        function changePointerToHourglass() {

            document.body.style.cursor = "wait";
        }

        function changePointerToNormal() {
            document.body.style.cursor="auto";
        }

		function validateInput(e, strPattern)
		{
			var chr = (isIE4up || isIE6 || isIE6)?e.keyCode:e.which;
			var ch = String.fromCharCode(chr);
     
			if (chr != 13 && chr !=  8 )
			{
				var re = new RegExp(strPattern);
     
				if (ch.search(re) == -1)
				{
					if(isIE4up || isIE6 || isIE6)
					{
						e.returnValue = false;
					}
					else
					{
						e.preventDefault();
					}
				}
			}
		}
		
        function resizeOuterTo(w,h) {
            if (parseInt(navigator.appVersion)>3) {
                if (navigator.appName=="Netscape") {
                    top.outerWidth=w;
                    top.outerHeight=h;
                }
                else top.resizeTo(w,h);
            }
        }
        
        function fav() {

            window.external.AddFavorite(location.href,document.title);

        }
        
        function updateOrientation(){  
            var contentType = "show_";  
            switch(window.orientation){  
                case 0:  
                contentType += "normal";  
                break;  
          
                case -90:  
                contentType += "right";  
                break;  
          
                case 90:  
                contentType += "left";  
                break;  
          
                case 180:  
                contentType += "flipped";  
                break;  
            }  
            document.getElementById("pnlContent").setAttribute("class", contentType);
        }  
        
        function openNewWin(url) {

            var x = window.open(url, 'mynewwin', 'width=600, height=600,toolbar=1, menubar=1, resizable=1, fullscreen=yes, scrollbars=auto');

            x.focus();

        }
        
        top.window.moveTo(0,0);
        top.window.resizeTo(window.screen.availWidth, window.screen.availHeight);
    
    </script>
    
</head>

<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onorientationchange="updateOrientation();" >

    <form id="form1" runat="server">
        
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        
        <asp:Panel ID="pnlContent" runat="server" 
            Style="z-index: 1; top: 0px; left: 0px; position: relative; width: 1002px;" 
            Height="620px" HorizontalAlign ="Center">   
        
        
            <asp:UpdatePanel ID="updPnlMenuLeft" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                
                    <asp:Panel ID="pnlMenuLeft" runat="server" 
                        HorizontalAlign="Left" 
                        Style="z-index: 100; top: 76px; 
                        left: 0px; position: absolute; width: 234px; height: 531px;  
                        background-image:url('images/web_07.jpg'); background-repeat:repeat-x; ">
                        
                        <br />
                        <asp:Image ID="option1" runat="server" ImageUrl="images/option.png" ImageAlign="AbsMiddle" />
                        &nbsp;
                        <asp:Label ID="lblProductos" runat="server" Font-Size="10pt" ForeColor="White" 
                        Font-Bold="true" Font-Underline="true" Text="Productos">
                        </asp:Label>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkCasas" runat="server" Target="_blank" NavigateUrl="index.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Casas R&iacute;o Dorado</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkInteriores" runat="server" Target="_blank" NavigateUrl="interiores.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Vista Interior</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkTechumbres" runat="server" Target="_blank" NavigateUrl="techos.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Cubiertas y Vigas</asp:HyperLink>
                        &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="lnkPuertas" runat="server" Target="_blank" NavigateUrl="otros.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Puertas y más...</asp:HyperLink>
                            &nbsp;
                        <br />
                        <br />
                        <br />
                        <asp:Image ID="option2" runat="server" ImageUrl="images/option.png" ImageAlign="AbsMiddle" />
                        &nbsp;
                        <asp:Label ID="lblInfoCorporativa" runat="server" Font-Size="10pt" ForeColor="White" 
                        Font-Bold="true" Font-Underline="true" Text="Informaci&oacute;n Corporativa"></asp:Label>&nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="lnkSobreMadera" runat="server" Target="_blank" NavigateUrl="madera.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Acerca de la Madera</asp:HyperLink>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="lnkProcesoConstructivo" runat="server" Target="_blank" NavigateUrl="proceso.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Proceso Constructivo</asp:HyperLink>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="lnkSobreGarantia" runat="server" Target="_blank" NavigateUrl="garantia.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Acerca de la Garantía</asp:HyperLink>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="lnkSobreRioDorado" runat="server" Target="_blank" NavigateUrl="nosotros.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Acerca de la Empresa</asp:HyperLink>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="lnkSobreAserradero" runat="server" Target="_blank" NavigateUrl="aserradero.aspx" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White">Acerca del Centro Productivo</asp:HyperLink>
                            &nbsp;
                        <br />
                        <br />
                        <br />
                        <asp:Image ID="option3" runat="server" ImageUrl="images/option.png" ImageAlign="AbsMiddle" />
                        &nbsp;
                        <asp:HyperLink ID="lnkParaInversionistas" runat="server" Target="_blank" NavigateUrl="inversionistas.aspx" Font-Size="9pt" 
                        Font-Underline="False" ForeColor="White">Para Inversionistas</asp:HyperLink>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlSlogan" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
               <ContentTemplate>
               
                   <asp:Panel ID="pnlLogo" runat="server" Width="165px" Height="68px" Style="z-index: 100; top: 8px; left: 235px; position: absolute; vertical-align:middle; text-align:left;">
                       <asp:Image ID="imgLogo" runat="server" ImageUrl="images/logo.jpg" 
                       AlternateText="Logo R&iacute;o Dorado" ToolTip="Ir a P&aacute;gina Principal" />
                   </asp:Panel>
            
                   <asp:Panel ID="pnlSlogan" runat="server" BackImageUrl="images/web_08_complete.jpg" ToolTip="Construimos tu Espacio, Tu Hogar, Tu Patrimonio" 
                       Style="z-index: 100; top: 76px; left: 235px; position: absolute; height: 67px; width: 765px; vertical-align:middle; text-align:justify; right: 217px;">
                   </asp:Panel>
                   
                   <asp:Panel ID="pnlLogout" runat="server" Style="z-index: 100; top: 85px; left: 845px; position: absolute; height: 25px; width: 150px; vertical-align:middle; text-align:right;">
                        <asp:LinkButton ID="lnkLogout" runat="server" Font-Size="8pt" ForeColor="Black" Font-Bold="true">Salir de la Sesi&oacute;n</asp:LinkButton>
                   </asp:Panel>
                                     
                   
               </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlLanguage" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlLanguage" runat="server" Width="213px" Height="20px" Style="z-index: 100; top: 8px; left: 787px; position: absolute; text-align:right; vertical-align:top;">
                        <asp:Label ID="lblChangeLanguage" runat="server" Font-Size="7pt" ForeColor="White" Text="Cambiar Idioma:"></asp:Label>
                        <asp:DropDownList ID="ddLanguage" runat="server" Width="100px" Font-Size="7pt" AutoPostBack="True"></asp:DropDownList>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlBrown" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlBrown" runat="server" Width="765px" height="25px" BackImageUrl="images/web_11.jpg" 
                        Style="z-index: 101; top: 143px; left: 235px; position: absolute; vertical-align:middle; text-align:justify;">
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlRestrictions" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlRestrictions" runat="server" Width="583px" Style="z-index: 102; top: 144px; left: 240px; position: absolute; vertical-align:middle; text-align:justify;">
                        &nbsp;
                        <asp:Image ID="imgBrowser" runat="server" ImageAlign="AbsMiddle" />
                        &nbsp;
                        <asp:Label ID="lblRestrictions" runat="server" ForeColor="Red" Font-Size="8pt">Esta p&aacute;gina funciona mejor sobre Internet Explorer</asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            

            <asp:Panel ID="pnlUpload" runat="server" Style="z-index: 103; top: 150px; left: 740px; position: absolute; width: 250px; vertical-align:middle; text-align:left;">
                <asp:Label ID="lblUpload" runat="server" Text="Subir Nuevas Fotografias : "></asp:Label><br />
                <br />
                <asp:Label ID="lblMultipleWindowsWarning" runat="server" ForeColor="Red" Text="No intentes subir fotos en mas de 1 ventana!"></asp:Label><br />
                <br /><br />
                <asp:FileUpload ID="fUpload" runat="server" ClientIDMode="AutoID" /><br />
                <asp:FileUpload ID="fUpload2" runat="server" ClientIDMode="AutoID" /><br />
                <asp:FileUpload ID="fUpload3" runat="server" ClientIDMode="AutoID" /><br />
                <asp:FileUpload ID="fUpload4" runat="server" ClientIDMode="AutoID" /><br />
                <br /><br />
                <asp:Button ID="btnUpload" runat="server" Text="Subir" />
                &nbsp;
                <asp:Label ID="lblUploadStatus" runat="server" Text=""></asp:Label>
            </asp:Panel>

            
            <asp:UpdatePanel ID="updPnlSlideShow" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    
                    <asp:Panel ID="pnlSlideShow" runat="server" Width="765px" BackImageUrl="images/web_11.jpg" Style="z-index: 102; top: 160px; left: 235px; position: absolute; vertical-align:middle; text-align:justify;">
                        
                        <asp:Panel ID="Panel1" runat="server" Width="765px" BackImageUrl="images/web_11.jpg" Style="z-index: 102; top: 8px; left: 0px; position: absolute; text-align:left; vertical-align:top;">
                            
                            <asp:Label ID="lblGallery" runat="server" Font-Size="7pt" ForeColor="Black" Text="Galer&iacute;a a Ordenar"></asp:Label>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddGalleries" runat="server" Width="150px" Font-Size="7pt" AutoPostBack="True"></asp:DropDownList>
                            <br />
                            <br />

                            
                            <asp:GridView ID="gvImages" runat="server" Width="50%" Font-Size="XX-Small" Font-Names="Verdana" DataKeyNames="iimageid" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
						                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Font-Size="8pt" Text="X" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Orden" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="iimageid" />
                                <asp:TemplateField HeaderText="Ordenar" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <br />
						                <asp:Button ID="btnUp" runat="server" CommandName="Up" Font-Size="8pt" Text="Subir/Antes" />
							            <asp:Button ID="btnDown" runat="server" CommandName="Down" Font-Size="8pt" Text="Bajar/Despu&eacute;s" />
                                        <br />
                                        <br />
                                        <asp:Label ID="lblMoveTo" runat="server" Text="Mover a otra Galeria:"></asp:Label>
                                        <asp:DropDownList ID="ddGalleriesToMove" runat="server" Width="150px" Font-Size="7pt" ></asp:DropDownList>
                                        <asp:Button ID="btnMove" runat="server" CommandName="Move" Font-Size="8pt" Text="Mover" />
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ImageField HeaderText="Imagen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataImageUrlField="simageurl">
                                </asp:ImageField>
                            </Columns>
                            </asp:GridView>
                            
                        </asp:Panel>
                                                
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            
            <asp:UpdatePanel ID="updPnlCCLicence" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlCCLicence" runat="server" Width="80px" Height="15px" Style="z-index:101; top: 592px; left:0px; position:absolute; vertical-align:middle; text-align:justify;"> 
                        <a rel="license" target="_blank" href="http://creativecommons.org/licenses/by-nc-nd/2.5/mx/">
                            <asp:Image ID="imgCCLicense" runat="server" ImageURL="http://i.creativecommons.org/l/by-nc-nd/2.5/mx/80x15.png" AlternateText="This work by Maderer&iacute;a R&iacute;o Dorado S.A. de C.V is licensed under a Creative Commons Attribution-Noncommercial-No Derivative Works 2.5 Mexico License" ToolTip="This work by Maderer&iacute;a R&iacute;o Dorado S.A. de C.V is licensed under a Creative Commons Attribution-Noncommercial-No Derivative Works 2.5 Mexico License" />
                        </a>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
         </asp:Panel>
               
    </form>
    
    
</body>
</html>
