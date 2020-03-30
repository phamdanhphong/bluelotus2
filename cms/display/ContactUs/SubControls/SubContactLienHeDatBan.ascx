<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubContactLienHeDatBan.ascx.cs" Inherits="cms_display_ContactUs_SubControls_SubContactLienHeDatBan" %>


<section class="sec-contact ">
    <div class="inner">
        <div class="contact">
            <h2 class="contact__ttl fade-up">
                <span><%= LanguageItemExtension.GetnLanguageItemTitleByName("Liên hệ đặt bàn") %></span><br>
                <small>Liên hệ ngay:<a href="tel:<%= LanguageItemExtension.GetnLanguageItemTitleByName("+842435535796") %>"><%= LanguageItemExtension.GetnLanguageItemTitleByName("+842435535796") %></a></small>
            </h2>
            <div class="contact__form">
                <div class="contact__form__item fade-up">
                    <i class="fa fa-user" aria-hidden="true"></i>
                    <input type="text" id="txtHoTen" class="required" placeholder="Họ tên"/>
                </div>
                <div class="contact__form__item fade-up">
                    <i class="fa fa-envelope" aria-hidden="true"></i>
                    <input type="text" id="txtEmail" class="required" placeholder="Email"/>
                </div>
                <div class="contact__form__item fade-up">
                    <i class="fa fa-phone" aria-hidden="true"></i>
                    <input type="text" id="txtSoDienThoai" class="required number" placeholder="Số điện thoại"/>
                </div>
                <div class="contact__form__item fade-up">
                    <i class="fa fa-users" aria-hidden="true"></i>
                    <input id="txtNumber" class="required" type="text" placeholder="Số người"/>
                </div>
                <div class="contact__form__item fade-up">
                    <i class="fa fa-calendar" aria-hidden="true"></i>
                    <input id="txtDate"  type="text" placeholder="Ngày" class="select required" id="datepicker"/>
                </div>
                <div class="contact__form__item fade-up">
                    <i class="fa fa-clock-o" aria-hidden="true"></i>
                    <select class="seclect" id="dlhour">
                        <option value="">Giờ</option>
                        <option value="">00:00A.M</option>
                        <option value="">00:30A.M</option>
                        <option value="">01:00A.M</option>
                        <option value="">01:30A.M</option>
                        <option value="">02:00A.M</option>
                        <option value="">02:30A.M</option>
                        <option value="">03:00A.M</option>
                        <option value="">03:30A.M</option>
                        <option value="">04:00A.M</option>
                        <option value="">04:30A.M</option>
                        <option value="">05:00A.M</option>
                        <option value="">05:00A.M</option>
                        <option value="">05:30A.M</option>
                        <option value="">06:00A.M</option>
                        <option value="">06:30A.M</option>
                        <option value="">07:00A.M</option>
                        <option value="">07:30A.M</option>
                        <option value="">80:00A.M</option>
                        <option value="">80:30A.M</option>
                        <option value="">09:00A.M</option>
                        <option value="">09:30A.M</option>
                        <option value="">10:00A.M</option>
                        <option value="">10:30A.M</option>
                        <option value="">11:00A.M</option>
                        <option value="">11:30A.M</option>
                        <option value="">12:00A.M</option>
                        <option value="">00:30P.M</option>
                        <option value="">01:00P.M</option>
                        <option value="">01:30P.M</option>
                        <option value="">02:00P.M</option>
                        <option value="">02:30P.M</option>
                        <option value="">03:00P.M</option>
                        <option value="">03:30P.M</option>
                        <option value="">04:00P.M</option>
                        <option value="">04:30P.M</option>
                        <option value="">05:00P.M</option>
                        <option value="">05:00P.M</option>
                        <option value="">05:30P.M</option>
                        <option value="">06:00P.M</option>
                        <option value="">06:30P.M</option>
                        <option value="">07:00P.M</option>
                        <option value="">07:30P.M</option>
                        <option value="">80:00P.M</option>
                        <option value="">80:30P.M</option>
                        <option value="">09:00P.M</option>
                        <option value="">09:30P.M</option>
                        <option value="">10:00P.M</option>
                        <option value="">10:30P.M</option>
                        <option value="">11:00P.M</option>
                        <option value="">11:30P.M</option>
                        <option value="">12:00P.M</option>
                    </select>
                </div>
                <div class="contact__form__item full fade-up">
                    <textarea id="txtContent" placeholder="Nội dung yêu cầu"></textarea>
                </div>
                <div class="contact__form__item contact__form__item--submit fade-up">
                    <a href="javascript:void(0)" onclick="SendContact();" class="btn-radius">ĐẶT BÀN</a>
                    <a href="javascript:void(0)" onclick="ResetInputContact('.contact02__form');" class="btn-radius">HỦY</a>
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
    function CheckContactForm01() {                
        if (CheckInputContact('.contact__form'))
            if(CheckEmail('#txtEmail','<%=LanguageItemExtension.GetnLanguageItemTitleByName("Email không hợp lệ")%>'))
                return true;

        return false;
    }

    function SendContact() {

      
       

        if (CheckContactForm01() ) {
            loading(true);
          
            var name = $("#txtHoTen").val();
            var email = $("#txtEmail").val();
            var phone = $("#txtSoDienThoai").val();
            var number = $("#txtNumber").val();
            var ngay = $("#txtDate").val();
            var hour = $("#dlhour").children("option:selected").text();
            var content = $("#txtContent").val();
     
            jQuery.ajax({
                url: weburl + "cms/Display/ContactUs/Ajax/Ajax.aspx",
                type: "POST",
                dataType: "json",
                data: {
                    "action": "SendContact",
                    "name": name,
                    "email": email,
                    "phone": phone,
                    "number": number,
                    "ngay": ngay,
                    "hour": hour,
                    "content": content
                },
                success: function (res) {
                    loading(false);
                    alert("thành công");
                    ResetInputContact('.contact__form');
                    //window.location.href = "/gui-lien-he-thanh-cong.htm";
                },
                error: function (error) {//Lỗi xảy ra
                    loading(false);
                    alert('<%=LanguageItemExtension.GetnLanguageItemTitleByName("Hệ thống đang bận, bạn vui lòng thử lại sau!") %>');
                }
            });
        }
    };
</script>

