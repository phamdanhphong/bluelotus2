<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import Namespace="TatThanhJsc.Extension" %>

<%@ Register Src="~/cms/display/DisplayLoadControl.ascx" TagPrefix="uc1" TagName="DisplayLoadControl" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal></title>
    <meta name="MobileOptimized" content="device-width" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <asp:Literal ID="ltrMetaOther" runat="server"></asp:Literal>
    <asp:Literal ID="ltrMetaShare" runat="server"></asp:Literal>
    <asp:Literal ID="ltrFavicon" runat="server"></asp:Literal>
    <asp:Literal ID="ltrGA" runat="server"></asp:Literal>
    <meta name="MobileOptimized" content="device-width" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <%--khai báo weburl sử dụng cho js--%>
    <script type="text/javascript">
        var webUrl = "<%= UrlExtension.WebisteUrl %>";
        var weburl = webUrl;
        if (document.URL.indexOf("www.") > -1)
            window.location = document.URL.replace("www.", "");
    </script>

    <meta charset="utf-8" />
    <link type="text/css" rel="stylesheet" href="/fonts/font-awesome/css/font-awesome.min.css" />
    <link type="text/css" rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css" />
    <link type="text/css" rel="stylesheet" href="/css/jquery.fancybox.min.css" />
    <link type="text/css" rel="stylesheet" href="/js/slick/slick.css" />
    <link type="text/css" rel="stylesheet" href="/css/styles.css" />
    <script src="/js/jquery-3.3.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:DisplayLoadControl runat="server" ID="DisplayLoadControl" />
    </form>
  
    <script src="/js/jquery.matchHeight-min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script src="/js/slick/slick.min.js"></script>
    <script src="/js/jquery.fancybox.min.js"></script>
    <script src="/js/ofi.min.js"></script>
    <script src="/js/masonry.pkgd.min.js"></script>
    <script src="/js/setting.js"></script>
    <script src="/js/cookie.js"></script>
    <script src="/js/loading.js"></script>
    <script src="/js/common_code.js"></script>
</body>
</html>

