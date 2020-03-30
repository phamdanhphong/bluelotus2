<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonCuoiChiTietTin.ascx.cs" Inherits="Cms_Common_CommonCuoiChiTietTin" %>
<%@ Import Namespace="TatThanhJsc.Extension" %>

<div id="CommonCuoiChiTietTin" class=" fade-up">
    <div class="left">
        <a class="prevDBT" href="javascript:history.go(-1)"><i class="fa fa-chevron-left" aria-hidden="true"></i>Về trang trước</a>
        <a href="javascript:void(0)" class="email addthis_button_email"><i class="fa fa-envelope" aria-hidden="true"></i>Gửi email</a>
        <a href="javascript:window.print()" class="print"><i class="fa fa-print" aria-hidden="true"></i>in trang</a>
    </div>
    <div class="addthis_toolbox addthis_default_style addthis_16x16_style">
        <div id="fb-root"></div>
        <script async defer crossorigin="anonymous" src="https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v6.0"></script>
        <div class="fb-like" data-href="<%=UrlExtension.WebisteUrl + Request.RawUrl.Substring(1) %>" data-width="" data-layout="button_count" data-action="like" data-size="small" data-share="true"></div>
        <div class="g-plus" data-action="share" data-annotation="none"></div>
        <script type="text/javascript">
            (function () {
                var po = document.createElement("script");
                po.type = "text/javascript";
                po.async = true;
                po.src = "https://apis.google.com/js/platform.js";
                var s = document.getElementsByTagName("script")[0];
                s.parentNode.insertBefore(po, s);
            })();
        </script>
        <div class="shareItem">
            <a class="addthis_button_facebook"></a>
        </div>
        <div class="shareItem">
            <a class="addthis_button_twitter"></a>
        </div>
        <div class="shareItem">
            <a class="addthis_button_zingme"></a>
        </div>
        <div class="shareItem">
            <a class="addthis_button_compact"></a>
        </div>
        <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#..."></script>
    </div>
    <div class="cb"></div>
</div>
