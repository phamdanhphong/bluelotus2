<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadControl.ascx.cs" Inherits="cms_display_Service_Controls_LoadControl" %>
<%@ Register Src="~/cms/display/ContactUs/SubControls/SubContactMaps.ascx" TagPrefix="uc1" TagName="SubContactMaps" %>
<%@ Register Src="~/cms/display/Adv/AdvSlideTrangCon.ascx" TagPrefix="uc1" TagName="AdvSlideTrangCon" %>


<main class="page-index main-page">
    <uc1:AdvSlideTrangCon runat="server" ID="AdvSlideTrangCon" />
    <asp:PlaceHolder ID="plLoadControl" runat="server"></asp:PlaceHolder>
    <uc1:SubContactMaps runat="server" ID="SubContactMaps" />
</main>
