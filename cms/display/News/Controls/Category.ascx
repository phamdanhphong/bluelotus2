<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Category.ascx.cs" Inherits="cms_display_News_Controls_Category" %>



<section class="sec-content">
    <div class="inner">
        <asp:Literal ID="ltrList" runat="server" />
       
    </div>
</section>

<style>
    .hide {
        display: none;
    }
</style>


<script>
    function showmoreNews(elm) {
        $(".list-news02__item").removeClass("hide");
        $(elm).addClass("hide");
    }
</script>
