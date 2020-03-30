<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="cms_display_News_Controls_Detail" %>

<%@ Register Src="~/cms/display/CommonControls/CommonCuoiChiTietTin.ascx" TagPrefix="uc1" TagName="CommonCuoiChiTietTin" %>
<%@ Register Src="~/cms/display/CommonControls/CommonTool.ascx" TagPrefix="uc1" TagName="CommonTool" %>
<%@ Register Src="~/cms/display/News/SubControls/SubNewsHot.ascx" TagPrefix="uc1" TagName="SubNewsHot" %>
<%@ Register Src="~/cms/display/News/SubControls/SubNewsOther.ascx" TagPrefix="uc1" TagName="SubNewsOther" %>











<section class="sec-content">
    <div class="inner">
        <h1 class="ttl-comp05 fade-up">
            <asp:Literal ID="ltrTitle" runat="server" /></h1>
        <p class="txt-des fade-up">
            <asp:Literal ID="ltrDesc" runat="server"></asp:Literal>
        </p>
        <uc1:CommonTool runat="server" ID="CommonTool" />

        <div class="main-news detail">
            <div class="main-left">
                <div class="noidung fade-up">
                    <asp:Literal ID="ltrContent" runat="server" />
                </div>
                <uc1:CommonCuoiChiTietTin runat="server" ID="CommonCuoiChiTietTin1" />

            </div>
            <uc1:SubNewsHot runat="server" ID="SubNewsHot" />
        </div>
        <uc1:SubNewsOther runat="server" ID="SubNewsOther" />
    </div>
</section>
