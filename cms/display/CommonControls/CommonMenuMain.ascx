<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonMenuMain.ascx.cs" Inherits="cms_display_CommonControls_CommonMenuMain" %>


<nav class="nav-menu">
   
    <ul class='show-menu'>
        <asp:Literal ID="ltrList" runat="server"></asp:Literal>
    </ul>
    <ul class='show-menu'>
        <asp:Literal ID="ltrList2" runat="server"></asp:Literal>
    </ul>
</nav>
<script type="text/javascript">
    //Script đánh dấu menu hiện tại theo modul (chỉ đúng cho trường hợp menu dẫn tới trang chính modul)
    var cRewrite = "<%=cRewrite%>";
    var cHrefInUrl = XuLyLink(document.URL);

    jQuery("#menu li.litop").removeClass("active");
    jQuery("#menu li.litop a").each(function () {
        var href = jQuery(this).attr("href");
        if (href) {
            href = XuLyLink(href);
            
            if (href === cHrefInUrl || href === cRewrite)
                jQuery(this).parent().addClass("active");

            if (href === "thu-vien") {
                var active = false;
                var listSubRewrite = ["hinh-anh", "video", "tai-lieu"];
                for (var i = 0; i < listSubRewrite.length; i++) {
                    href = listSubRewrite[i];

                    if (href) {
                        if (href.lastIndexOf("/") > -1) href = href.substring(href.lastIndexOf("/") + 1);
                        if (href.lastIndexOf(".") > -1) href = href.substring(0, href.lastIndexOf("."));
                        if (href === "/") href = "";
                        if (href === cRewrite) active = true;
                    }
                }

                if (active)
                    jQuery(this).parent().addClass("active");
            }
        }
    });

    function XuLyLink(href) {
        if (href.lastIndexOf("/") > -1)
            href = href.substring(href.lastIndexOf("/") + 1);

        if (href.lastIndexOf(".") > -1)
            href = href.substring(0, href.lastIndexOf("."));
        if (href === "/") href = "";

        return href;
    }
</script>

