<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="aserradero.aspx.vb" Inherits="WebMRD.aserradero" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title runat="server" id="PageTitle">Centro Productivo - Maderer&iacute;a R&iacute;o Dorado</title>
    <meta name="description" content="Las mejores casas de madera de México, con garantía de 40 años contra polillas y pudrición. " /> 
    <meta name="keywords" content="casa, cabaña, hogar, vivienda, hotel, salon para eventos, techo de madera, techumbre de madera, casa de madera, cabaña de madera, vivienda de madera, hotel de madera, vivienda digna, casas, cabañas, hogares, vivivendas, casas de madera, cabañas de madera, viviendas de madera, techos de madera, techumbres de madera, salones para eventos, viviendas dignas, mexico, méxico, calidad, bonita" /> 
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
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkCasas" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White" 
                            ToolTip="Viviendas y Oficinas de gran estilo" PostBackUrl="index.aspx">Casas R&iacute;o Dorado
                        </asp:LinkButton>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkInteriores" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White" 
                            ToolTip="Viviendas y Oficinas de gran estilo" PostBackUrl="interiores.aspx">Vista Interior
                        </asp:LinkButton>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkTechumbres" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White" ToolTip="Techos, Cubiertas y Vigas de Madera" PostBackUrl="techos.aspx">Cubiertas y Vigas</asp:LinkButton>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkPuertas" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White" ToolTip="Puertas, Ventanas, Barandales, Pisos, Mezzanines y todo lo que puedas imaginar..." PostBackUrl="otros.aspx">Puertas y más...</asp:LinkButton>
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
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkSobreMadera" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White"  
                            ToolTip="Acerca de la Madera de Pino y su tratamiento" PostBackUrl="madera.aspx">Acerca de la Madera</asp:LinkButton>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkProcesoConstructivo" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White"  
                            ToolTip="Sobre el Proceso Constructivo" PostBackUrl="proceso.aspx">Proceso Constructivo</asp:LinkButton>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkSobreGarantia" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White"  
                            ToolTip="Acerca de la Garant&iacute;a de 40 a&ntilde;os" PostBackUrl="garantia.aspx">Acerca de la Garantía</asp:LinkButton>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkSobreRioDorado" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White"  
                            ToolTip="Acerca de R&iacute;o Dorado" PostBackUrl="nosotros.aspx">Acerca de la Empresa</asp:LinkButton>
                            &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkSobreAserradero" runat="server" Font-Size="9pt" 
                            Font-Underline="False" ForeColor="White"  
                            ToolTip="Acerca del Centro Productivo" PostBackUrl="aserradero.aspx">Acerca del Centro Productivo</asp:LinkButton>
                            &nbsp;
                        <br />
                        <br />
                        <br />
                        <asp:Image ID="option3" runat="server" ImageUrl="images/option.png" ImageAlign="AbsMiddle" />
                        &nbsp;
                        <asp:LinkButton ID="lnkParaInversionistas" runat="server" Font-Size="9pt" 
                        Font-Bold="true" Font-Underline="True" ForeColor="White"  
                        ToolTip="Información sobre Inversiones con Nosotros" PostBackUrl="inversionistas.aspx">Para Inversionistas</asp:LinkButton>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlSlogan" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
               <ContentTemplate>
               
                   <asp:Panel ID="pnlLogo" runat="server" Width="165px" Height="68px" Style="z-index: 100; top: 8px; left: 235px; position: absolute; vertical-align:middle; text-align:left;">
                       <asp:Image ID="imgLogo" runat="server" ImageUrl="images/logo.jpg" 
                       AlternateText="Logo R&iacute;o Dorado" ToolTip="Ir a P&aacute;gina Principal" />
                   </asp:Panel>
            
                   <asp:Panel ID="pnlSlogan" runat="server" BackImageUrl="images/web_08.jpg" ToolTip="Construimos tu Espacio, Tu Hogar, Tu Patrimonio" 
                       Style="z-index: 100; top: 76px; left: 235px; position: absolute; height: 67px; width: 559px; vertical-align:middle; text-align:justify; right: 217px;">
                   </asp:Panel>
                   
                   <asp:Panel ID="pnlSlogan2" runat="server" BackImageUrl="images/web_09.jpg" 
                   Style="z-index: 100; top: 76px; left: 793px; position: absolute; height: 47px; width: 210px; vertical-align:middle; text-align:justify; background-repeat:no-repeat;">
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
                    <asp:Panel ID="pnlBrown" runat="server" Height="328px" Width="558px" BackImageUrl="images/web_11.jpg" 
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
                        <asp:Label ID="lblRestrictions" runat="server" ForeColor="Red" Font-Size="8pt">¿Tienes problemas para ver las fotos? <a href="aserraderogallery.aspx" target="_self">Haz Click Aqu&iacute;</a></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
                  
            
            <asp:UpdatePanel ID="updPnlSlideShow" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    
                    <asp:Panel ID="pnlSlideShow" runat="server" Width="448px" Height="299px" Style="z-index: 102; top: 165px; left: 240px; position: absolute; vertical-align:text-bottom; text-align:left;">
                        
                        <asp:Image ID="imgSldShow" runat="server" ImageUrl="productionfacilityimages/A1.jpg" ImageAlign="Top" ToolTip="" AlternateText="" />
                        
                        <asp:SlideShowExtender ID="SlideShowExtender1" runat="server" 
                            TargetControlID="imgSldShow" 
                            SlideShowServiceMethod="GetSlides" AutoPlay="true" 
                            NextButtonID="btnNext" 
                            PreviousButtonID="btnPrev" PlayButtonID="btnPlay" PlayButtonText="Iniciar"
                            ImageTitleLabelID="lblImgSldShow" 
                            StopButtonText="Parar" Loop="False" PlayInterval="4000">
                        </asp:SlideShowExtender>
                        
                        <script type="text/javascript" language="javascript">
                        
                            function popupImage() {
                                // This is where you set how wide you want your popup window
                                var awidth = 800;
                                // This is where you set how high you want your popup window
                                var aheight = 600;
                                // This calculates the middle of the screen Horizontally
                                var leftVal = (screen.width / 2) - (awidth / 2);
                                // This calculates the middle of the screen Vertically
                                var topVal = (screen.height / 2) - (aheight / 2);

                                // This is where you set it to show scrollbars.
                                //var showScrollbars = 0; // Hide Scrollbars
                                var showScrollbars = 1; // Show Scrollbars

                                // This is where you set it to be resizable or not.
                                //var isResizable = 0; //Not resizable
                                var isResizable = 1; //Is resizable

                                // This is where you set it to show Toolbars.
                                var showToolbar = 0; // Hide Toolbar
                                //var showToolbar = 1; // Show Toolbar

                                // This is where you set it to show Status or not.
                                var showStatus = 0; // Hide Status
                                //var showStatus = 1; // Show Status

                                // This is where you tell the window to popup
                                //window.open($get("<%=imgSldShow.ClientID %>").src
                                window.open($get("<%=imgSldShow.ClientID%>").src.replace("productionfacilityimages", "productionfacilityimages/larger")
                                    , ''
                                    , 'scrollbars = ' + showScrollbars +
                                     ', height = ' + aheight +
                                     ', width = ' + awidth +
                                     ', resizable = ' + isResizable +
                                     ', toolbar = ' + showToolbar +
                                     ', status = ' + showStatus +
                                     ', left = ' + leftVal +
                                     ', top = ' + topVal);

                            }
                            function pageLoad() {
                                // Set the image so it can accept a click,
                                // and tell it to call "popupImage"
                                $addHandler($get("<%=imgSldShow.ClientID%>"), "click", popupImage);
                            }
                        </script>
                        
                       <asp:Panel ID="pnlLblImgSldShow" runat="server" 
                       Style="z-index: 102; top: 0px; left: 500px; position: absolute; vertical-align:middle; text-align:left;">
                            <asp:Label ID="lblImgSldShow" runat="server" Font-Size="6pt" ForeColor="Black" Text="1"></asp:Label>
                       </asp:Panel>
                        
                       <asp:Panel ID="pnlSlideShowControls" runat="server" 
                       Style="z-index: 102; top: 105px; left: 480px; position: absolute; vertical-align:middle; text-align:center;">
                            <asp:Button ID="btnPrev" runat="server" Font-Size="8pt" Text="<" />
                            <br />
                            <br />
                            <asp:Button ID="btnPlay" runat="server" Font-Size="8pt" Text="" />
                            <br />
                            <br />
                            <asp:Button ID="btnNext" runat="server" Font-Size="8pt" Text=">" />
                        </asp:Panel>
                        
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlSheet" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
               <ContentTemplate>
                   <asp:Panel ID="pnlSheet" runat="server" BackImageUrl="images/web_10.jpg"
                       Style="z-index: 100; top: 123px; left: 793px; position: absolute; height: 347px; width: 206px; vertical-align:middle; text-align:justify; background-repeat:no-repeat;">
                   </asp:Panel>
               </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlSheetInfo" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    
                    <asp:Panel ID="pnlSheetInfo" runat="server" 
                        Style="z-index: 101; top: 130px; left: 798px; position: absolute; vertical-align:middle; text-align:justify; width: 192px;">
                        
                        <br />
                        <asp:Image ID="check1" runat="server" ImageUrl="images/checkmark.png" ImageAlign="AbsBottom" />
                        <asp:Label ID="lblCheck1" runat="server" Font-Italic="true" Font-Size="9pt" Font-Bold="true" ForeColor="Black" Text="Dise&ntilde;o con base en sus Necesidades"></asp:Label>
                        <br />
                        <br />
                        <asp:Image ID="check2" runat="server" ImageUrl="images/checkmark.png" ImageAlign="AbsBottom" />
                        <asp:Label ID="lblCheck2" runat="server" Font-Italic="true" Font-Size="9pt" Font-Bold="true" ForeColor="Black" Text="De R&aacute;pida Construcci&oacute;n"></asp:Label>
                        <br />
                        <br />
                        <asp:Image ID="check3" runat="server" ImageUrl="images/checkmark.png" ImageAlign="AbsBottom" />
                        <asp:Label ID="lblCheck3" runat="server" Font-Italic="true" Font-Size="9pt" Font-Bold="true" ForeColor="Black" Text="Segura"></asp:Label>
                        <br />
                        <br />
                        <asp:Image ID="check4" runat="server" ImageUrl="images/checkmark.png" ImageAlign="AbsBottom" />
                        <asp:Label ID="lblCheck4" runat="server" Font-Italic="true" Font-Size="9pt" Font-Bold="true" ForeColor="Black" Text="Durable"></asp:Label>
                        <br />
                        <br />
                        <asp:Image ID="check5" runat="server" ImageUrl="images/checkmark.png" ImageAlign="AbsBottom" />
                        <asp:Label ID="lblCheck5" runat="server" Font-Italic="true" Font-Size="9pt" Font-Bold="true" ForeColor="Black" Text="Y sobre todo... Bonitas!"></asp:Label>
                        <br />
                        <br />
                        
                    </asp:Panel>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlGreen" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                
                    <asp:Panel ID="pnlGreen" runat="server" BackImageUrl="images/web_12.jpg" Width="763px" Height="95px" Style="z-index: 100; top: 470px; left: 235px; position: absolute; vertical-align:middle; text-align:justify; background-repeat:repeat-x;">
                        
                    </asp:Panel>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlInfo" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlInfo" runat="server" Width="755px" Height="82px" Style="z-index: 101; top: 475px; left: 238px; position: absolute; vertical-align:middle; text-align:justify;">
                        <asp:Label ID="lblInfo" runat="server" ForeColor="White" Text="R&iacute;o Dorado cuenta con la tecnolog&iacute;a m&aacute;s moderna para la creaci&oacute;n de las Casas. <br />Nuestra planta de impregnaci&oacute;n y secado permite tener 43 m3 de madera en una sola tanda, y nuestra m&aacute;quina laminadora nos permite hacer hasta 40 vigas laminadas por d&iacute;a para usarlas en las cubiertas de las Casas."></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlCopyrightBar" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlCopyrightBar" runat="server" Width="764px" BackImageUrl="images/web_14.jpg" 
                        Style="z-index: 100; top: 565px; left: 234px; position: absolute; vertical-align:middle; text-align:justify; height: 42px;">
                        
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
            
            <asp:UpdatePanel ID="updPnlCopyright" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlCopyright" runat="server" Width="761px" Height="30px" Style="z-index: 101; top: 571px; left: 234px; position: absolute; vertical-align:middle; text-align:center;">
                        <asp:Label ID="lblAddress" runat="server" Font-Size="8pt" ForeColor="White" Text="Libramiento Sur Poniente 239, Tuxtla Guti&eacute;rrez, Chiapas, M&eacute;xico. C&oacute;digo Postal 29000."></asp:Label>
                        <br />
                        <asp:Label ID="lblTelephones" runat="server" Font-Size="8pt" ForeColor="White" Text="+52 961 612 5507&nbsp;&nbsp;|&nbsp;&nbsp;+52 961 612 6351"></asp:Label>
                        <br />
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
