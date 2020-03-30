<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="cms_display_ContactUs_Controls_Index" %>





<section class="sec-content">
    <div class="inner">
        <div class="contact02">
            <div class="contact02__address">
                <asp:Literal ID="ltrOfficeInfo" runat="server"></asp:Literal>
            </div>
            <div class="contact02__form">
                <h2 class="contact02__ttl fade-up"><%= LanguageItemExtension.GetnLanguageItemTitleByName("gửi nội dung liên hệ") %></h2>
                <div class="box-form">
                    <div class="box-form__item fade-up">
                        <input id="txtChuDe" type="text" placeholder="<%= LanguageItemExtension.GetnLanguageItemTitleByName("Chủ đề") %>"/>
                    </div>
                    <div class="box-form__item fade-up">
                        <input id="tbHoTen" type="text" placeholder="Họ và tên"/>
                    </div>
                    <div class="box-form__item fade-up">
                        <input class="required" id="tbEmail" type="text" placeholder="Email"/>
                    </div>
                    <div class="box-form__item fade-up">
                        <input class="required number" id="tbDienThoai" type="text" placeholder="Số điện thoại"/>
                    </div>
                    <div class="box-form__item box-form__item--full fade-up">
                        <textarea id="tbContent" placeholder="Nội dung hỗ trợ"></textarea>
                    </div>
                    <div class="box-form__item box-form__item--full fade-up">
                        <a href="javascript:void(0)" onclick="SendContact01()" class="btn on"><%= LanguageItemExtension.GetnLanguageItemTitleByName("gửi liên hệ") %></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>


<style>
    .notfilled {
        border: 1px solid red !important;
    }
</style>

<script>
    function CheckContactForm02() {                
        if (CheckInputContact('.contact02__form'))
            if(CheckEmail('#tbEmail','<%=LanguageItemExtension.GetnLanguageItemTitleByName("Email không hợp lệ")%>'))
                return true;

        return false;
    }

    function SendContact01() {
        if (CheckContactForm02() ) {
            loading(true);
            var chuDe = $("#txtChuDe").val();
            var name = $("#tbHoTen").val();
            var email = $("#tbEmail").val();
            var phone = $("#tbDienThoai").val();
            var content = $("#tbContent").val();
     
            jQuery.ajax({
                url: weburl + "cms/Display/ContactUs/Ajax/Ajax.aspx",
                type: "POST",
                dataType: "json",
                data: {
                    "action": "SendContact02",
                    "name": name,
                    "email": email,
                    "phone": phone,
                    "chuDe": chuDe,
                    "content": content
                },
                success: function (res) {
                    loading(false);
                    ResetInputContact('.contact02__form');
                    window.location.href = "/gui-lien-he-thanh-cong.htm";
                },
                error: function (error) {//Lỗi xảy ra
                    loading(false);
                    alert('<%=LanguageItemExtension.GetnLanguageItemTitleByName("Hệ thống đang bận, bạn vui lòng thử lại sau!") %>');
                }
            });
        }
    };
</script>
