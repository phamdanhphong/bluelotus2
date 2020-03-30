<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="cms_display_Product_Controls_Index" %>
<%@ Register Src="~/cms/display/Adv/AdvInstagram.ascx" TagPrefix="uc1" TagName="AdvInstagram" %>



<section class="sec-wedding sec-content">
    <div class="inner">
        <asp:Literal ID="ltrList" runat="server"></asp:Literal>
        <uc1:AdvInstagram runat="server" ID="AdvInstagram" />
    </div>
</section>
