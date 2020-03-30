<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="cms_display_AboutUs_Controls_Index" %>
<%@ Register Src="~/cms/display/News/SubControls/SubSuKienHome.ascx" TagPrefix="uc1" TagName="SubSuKienHome" %>


<section class="sec-introduction sec-content">
    <div class="inner">
        <asp:Literal ID="ltrList" runat="server"></asp:Literal>
        <uc1:SubSuKienHome runat="server" ID="SubSuKienHome" />
    </div>
</section>
