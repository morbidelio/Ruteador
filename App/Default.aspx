<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="App_Inicio" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
<style type="text/css" title="fondo">
 #scrolls
  {
    background-repeat:no-repeat;
    background-size: cover;
 }
 
 </style>

</asp:Content>
<asp:Content ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript">
        function getStyleSheet(unique_title) {
            for (var i = 0; i < document.styleSheets.length; i++) {
                var sheet = document.styleSheets[i];
                if (sheet.title == unique_title) {
                    return sheet;
                }
            }
        }
        const rand = Math.floor(Math.random() * 5) + 1;
        const imagen = '../Img/img_fondo/Route' + rand + '.jpg';
        getStyleSheet('fondo').insertRule("#scrolls { background-image: url('" + imagen + "');}", 0); 

    </script>
</asp:Content>