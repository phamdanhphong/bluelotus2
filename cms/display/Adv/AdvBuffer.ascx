<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvBuffer.ascx.cs" Inherits="cms_display_Adv_AdvBuffer" %>


<section class='sec-buffer'>
    <div class='inner'>
        <h2 class='ttl-comp01 fade-up'><%= LanguageItemExtension.GetnLanguageItemTitleByName("Buffer sen xanh") %><small><%= LanguageItemExtension.GetnLanguageItemTitleByName("Thực đơn buffer độc đáo - hấp dẫn") %></small></h2>
        <div class='list-foods'>
            <asp:Literal ID="ltrAdv" runat="server"></asp:Literal>
        </div>
        <div class='txt-center fade-up'>
            <a href="javascript:void(0)" onclick="showMoreBuffer(this)" class='btn' tabindex='0'>Xem thêm</a>
        </div>
    </div>
</section>

<style>
    .hide {
        display: none;
    }
</style>

<script>
    function showMoreBuffer(elm) {
        $(".sec-buffer .list-foods__item").removeClass("hide");
        $(elm).addClass("hide");
    }
</script>
