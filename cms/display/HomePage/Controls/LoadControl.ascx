<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadControl.ascx.cs" Inherits="cms_display_HomePage_Controls_LoadControl" %>
<%@ Register Src="~/cms/display/Adv/AdvSlideHome.ascx" TagPrefix="uc1" TagName="AdvSlideHome" %>
<%@ Register Src="~/cms/display/Adv/AdvGioiThieuBlueLotus.ascx" TagPrefix="uc1" TagName="AdvGioiThieuBlueLotus" %>
<%@ Register Src="~/cms/display/Adv/AdvDsHoiThao.ascx" TagPrefix="uc1" TagName="AdvDsHoiThao" %>
<%@ Register Src="~/cms/display/Adv/AdvCoffeeAndTea.ascx" TagPrefix="uc1" TagName="AdvCoffeeAndTea" %>
<%@ Register Src="~/cms/display/Adv/AdvBuffer.ascx" TagPrefix="uc1" TagName="AdvBuffer" %>
<%@ Register Src="~/cms/display/ContactUs/SubControls/SubContactMaps.ascx" TagPrefix="uc1" TagName="SubContactMaps" %>
<%@ Register Src="~/cms/display/ContactUs/SubControls/SubContactLienHeDatBan.ascx" TagPrefix="uc1" TagName="SubContactLienHeDatBan" %>







<main class="page-index main-page">
    <uc1:AdvSlideHome runat="server" ID="AdvSlideHome" />
    <section class="sec-intro">
        <div class="inner">
            <uc1:AdvGioiThieuBlueLotus runat="server" ID="AdvGioiThieuBlueLotus" />
            <uc1:AdvDsHoiThao runat="server" ID="AdvDsHoiThao" />
        </div>
    </section>
    <uc1:AdvCoffeeAndTea runat="server" ID="AdvCoffeeAndTea" />
    <uc1:AdvBuffer runat="server" ID="AdvBuffer" />
    <uc1:SubContactLienHeDatBan runat="server" ID="SubContactLienHeDatBan" />
    <uc1:SubContactMaps runat="server" ID="SubContactMaps" />
</main>