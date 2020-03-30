<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Category.ascx.cs" Inherits="cms_display_QA_Controls_Category" %>


<section class="sec-wedding sec-content">
    <div class="inner">
        <asp:Literal ID="ltrList" runat="server" />
    </div>
</section>

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
