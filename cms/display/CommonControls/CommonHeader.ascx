<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonHeader.ascx.cs" Inherits="cms_display_CommonControls_CommonHeader" %>
<%@ Register Src="~/cms/display/Adv/AdvLogoMain.ascx" TagPrefix="uc1" TagName="AdvLogoMain" %>
<%@ Register Src="~/cms/display/CommonControls/CommonMenuMain.ascx" TagPrefix="uc1" TagName="CommonMenuMain" %>


<header id="header">
    <div class="inner">
        <uc1:AdvLogoMain runat="server" ID="AdvLogoMain" />
        <uc1:CommonMenuMain runat="server" ID="CommonMenuMain" />
        
        <span class="mobile-icon">
            <span></span>
        </span>
    </div>
</header>

