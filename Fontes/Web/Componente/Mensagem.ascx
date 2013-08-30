<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mensagem.ascx.cs" Inherits="Web.Mensagem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head>
<title> SimpleModal Confirm Modal Dialog </title>
<link href="../Componente/Aviso.css" rel="stylesheet" media="all" />
</head>
<body>
    <div id="DialogMensagem">
        
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnOkay"
            TargetControlID="LabelModalPopup" PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader"
            Drag="true" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
        <asp:Label ID="LabelModalPopup" runat="server" Text=""></asp:Label>
        <asp:Panel ID="Panel1" Style="display: none" runat="server">
            <div class="HellowWorldPopup">
                <div class="header" id="PopupHeader">
                    <p><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></p>
                </div>
                <div class="message">
                    <p>
                        <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="buttons2">
                    <div id="btnOkay">OK</div>
                </div>
            </div>
        </asp:Panel>
    </div>

</body>
</html>