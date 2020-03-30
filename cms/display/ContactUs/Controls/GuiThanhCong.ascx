<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GuiThanhCong.ascx.cs" Inherits="cms_display_LienHe_Controls_GuiThanhCong" %>


<section class="sec-content">
    <div class="inner">
        <div class="guiThanhCong">
            <i class="fa fa-check" aria-hidden="true"></i>
            <p class="title vang bold text-cent er"><%= LanguageItemExtension.GetnLanguageItemTitleByName("Gửi liên hệ thành công!") %></p>
            <p class="first text-center"><%= LanguageItemExtension.GetnLanguageItemTitleByName("Cảm ơn quý khách đã liên hệ với BLUE LOTUS") %></p>
            <div class="text txt-center"><%= LanguageItemExtension.GetnLanguageItemTitleByName("Chúng tôi sẽ phản hồi quý khách trong thời gian sớm nhất") %></div>
            <a href="/" class="back"><%= LanguageItemExtension.GetnLanguageItemTitleByName("Về trang chủ") %></a>
        </div>
    </div>
</section>