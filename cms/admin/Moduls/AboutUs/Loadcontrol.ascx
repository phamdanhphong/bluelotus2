﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Loadcontrol.ascx.cs" Inherits="cms_admin_AboutUs_Loadcontrol" %>
<%@ Register src="Leftmenu.ascx" tagname="AdmLeftmenu" tagprefix="uc1" %>
<link href="_cs/_cs.css" rel="stylesheet" />
<div id="AboutUsModul">
    <div class="container">
        <uc1:AdmLeftmenu ID="AdmLeftmenu" runat="server" />
        <div class="colRight">
            <asp:PlaceHolder ID="phControl" runat="server"></asp:PlaceHolder>
        </div>
        <div class="cb"><!----></div>
    </div>
</div>