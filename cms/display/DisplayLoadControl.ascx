<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DisplayLoadControl.ascx.cs" Inherits="cms_display_DisplayLoadControl" %>
<%@ Register Src="~/cms/display/CommonControls/CommonHeader.ascx" TagPrefix="uc1" TagName="CommonHeader" %>

<%@ Register Src="~/cms/display/CommonControls/CommonFooter.ascx" TagPrefix="uc1" TagName="CommonFooter" %>

<uc1:CommonHeader runat="server" ID="CommonHeader" />
<asp:PlaceHolder ID="phLoadControl" runat="server"></asp:PlaceHolder>
<uc1:CommonFooter runat="server" ID="CommonFooter" />


