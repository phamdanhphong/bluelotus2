<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Error404.ascx.cs" Inherits="cms_display_Error_Controls_Error404" %>
<%@ Import Namespace="TatThanhJsc.Extension" %>



<div class="errorPage">
    <div class="wrapper">
        <div class="wrapperImg">
            <img src="/cms/display/Error/Controls/icon404.png" />
        </div>
        <span class="title">
            <%= LanguageItemExtension.GetnLanguageItemTitleByName("Trang bạn tìm hiện không có hoặc vì một lý do nào đó không tồn tại trên website...!") %>
        </span>
        <div class="text">
            <%= LanguageItemExtension.GetnLanguageItemTitleByName("Trở về trang chủ hoặc liên hệ với chúng tôi nếu bạn cần thêm thông tin.")%>
        </div>
        <a href="/" title="<%= LanguageItemExtension.GetnLanguageItemTitleByName("Trở về trang chủ")%>" class="btn"><%= LanguageItemExtension.GetnLanguageItemTitleByName("Trở về trang chủ")%></a>
    </div>
</div>
<%= LanguageItemExtension.GetnLanguageItemTitleByName("") %>

<style>
    .btn {
        display: inline-block;
        font-weight: 400;
        color: #212529;
        text-align: center;
        vertical-align: middle;
        user-select: none;
        background-color: transparent;
        border: 1px solid transparent;
        padding: .375rem .75rem;
        font-size: 1rem;
        line-height: 1.5;
        border-radius: .25rem;
        transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
    }
    .errorPage {
        background: url(/cms/display/Error/Controls/43-404.png) no-repeat center center;
        background-size: cover;
        padding: 170px 0 150px 0;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
    }

    @media(max-width: 767px) {
        .errorPage {
            padding: 50px 10px;
        }

        .errorPage .wrapperImg {
            max-width: 100%;
        }

        .errorPage span.title {
            max-width: 100%;
        }

        .errorPage .text {
            max-width: 100%;
        }
    }

    .errorPage .wrapper {
        width: 100%;
    }

    .errorPage .wrapperImg {
        width: 350px;
        margin: 0 auto;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-bottom: 30px;
    }

    .errorPage .wrapperImg img {
        max-width: 100%;
    }

    .errorPage span.title {
        display: inline-block;
        font-family: ""Roboto"";
        color: #333;
        font-size: 18px;
        line-height: 24px;
        width: 100%;
        text-align: center;
        margin-bottom: 10px;
        display: inline-block;
    }

    .errorPage .text {
        margin-bottom: 43px;
        text-align: center;
        color: #333;
    }

    .errorPage a.btn {
        font-family: "RobotoCondensedBold";
        color: #fff;
        font-size: 15px;
        line-height: 38px;
        padding: 0 35px;
        transition: .4s;
        background: #0342ac;
        border-radius: 50px;
    }

    .errorPage a.btn:hover {
        background: #fff;
        color: #333;
        border: solid 1px #ffbf09;
    }

    .errorPage a.btn {
        position: relative;
        left: 50%;
        transform: translateX(-50%);
    }
</style>
