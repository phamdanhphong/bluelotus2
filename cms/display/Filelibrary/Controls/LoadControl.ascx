<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadControl.ascx.cs" Inherits="cms_display_Filelibrary_Controls_LoadControl" %>
<%@ Register Src="~/cms/display/ContactUs/SubControls/SubContactMaps.ascx" TagPrefix="uc1" TagName="SubContactMaps" %>
<%@ Register Src="~/cms/display/Adv/AdvSlideTrangCon.ascx" TagPrefix="uc1" TagName="AdvSlideTrangCon" %>
<%@ Register Src="~/cms/display/PhotoAlbum/subControls/subPhotoHomepage.ascx" TagPrefix="uc1" TagName="subPhotoHomepage" %>



<main class="page-index main-page">
    <uc1:AdvSlideTrangCon runat="server" ID="AdvSlideTrangCon" />
    <asp:PlaceHolder ID="plLoadControl" runat="server"></asp:PlaceHolder>
    
    <section class='time-retaurant fade-up'>
        <div class='inner'>
            <div class='time-retaurant__left'>
                <h3 class='time-retaurant__ttl'><%= LanguageItemExtension.GetnLanguageItemTitleByName("giờ mở cửa") %></h3>
                <ul>
                    <li>
                        <span><%= LanguageItemExtension.GetnLanguageItemTitleByName("Thứ 2 - thứ 7") %></span>
                        <span><%= LanguageItemExtension.GetnLanguageItemTitleByName("7:00am - 8:00pm") %></span>
                    </li>
                    <li>
                        <span><%= LanguageItemExtension.GetnLanguageItemTitleByName("Chủ nhật") %></span>
                        <span><%= LanguageItemExtension.GetnLanguageItemTitleByName("7:00am - 10:00pm") %></span>
                    </li>
                    <li>
                        <span><%= LanguageItemExtension.GetnLanguageItemTitleByName("Ngày lễ") %></span>
                        <span><%= LanguageItemExtension.GetnLanguageItemTitleByName("Đóng cửa") %></span>
                    </li>
                </ul>
            </div>
            <div class='time-retaurant__right'>
                <h3 class='time-retaurant__ttl'><%= LanguageItemExtension.GetnLanguageItemTitleByName("liên hệ:") %></h3>
                <ul>
                    <li><%= LanguageItemExtension.GetnLanguageItemTitleByName("Số điện thoại:") %> <a href='tel:<%= LanguageItemExtension.GetnLanguageItemTitleByName("024 627 54598") %>'><%= LanguageItemExtension.GetnLanguageItemTitleByName("024 627 54598") %></a></li>
                    <li><%= LanguageItemExtension.GetnLanguageItemTitleByName("Website:") %> <a href='mailto:coffee@site.com'><%= LanguageItemExtension.GetnLanguageItemTitleByName("coffee@site.com") %></a></li>
                    <li><%= LanguageItemExtension.GetnLanguageItemTitleByName("Địa chỉ:") %><%= LanguageItemExtension.GetnLanguageItemTitleByName("66 Lê Văn Lương, Thanh Xuân, Hà Nội") %></li>
                </ul>
            </div>
        </div>
    </section>
    <uc1:subPhotoHomepage runat="server" ID="subPhotoHomepage" />
    <uc1:SubContactMaps runat="server" ID="SubContactMaps" />
</main>