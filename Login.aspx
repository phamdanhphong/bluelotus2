<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="cms/admin/Moduls/CommonControls/AdmControlsLanguages.ascx" TagName="AdmControlsLanguages" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Khu vực đăng nhập hệ thống quản trị website</title>
    <meta name="copyright" content="Tất Thành" />
    <meta name="author" content="Tất Thành" />

    <link href="cms/admin/cs/Common.css" rel="stylesheet" type="text/css" />
    <link href="cms/admin/cs/Login.css" rel="stylesheet" type="text/css" />
    <link href="cms/admin/cs/LoginMedia.css" rel="stylesheet" />

    <script src="cms/admin/js/jquery-1.8.3.min.js" type="text/javascript"></script>

    <%--jAlert - http://labs.abeautifulsite.net/archived/jquery-alerts/demo/--%>
    <script src="cms/admin/js/jAlert/jquery.alerts.js" type="text/javascript"></script>
    <script src="cms/admin/js/jAlert/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="cms/admin/js/jAlert/jquery.alerts.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="top">
                <div class="slogan">
                    <span>Hệ thống quản trị website</span>
                </div>
                <div class="langs">
                    <uc1:AdmControlsLanguages ID="AdmControlsLanguages1" runat="server" />
                </div>
                <div class="cb">
                    <!---->
                </div>
            </div>
            <div class="contentBox">
                <div class="rightCol" style="margin:auto;">
                    <div class="loginBox">
                        <div class="title">
                            Đăng nhập khu vực<br />
                            quản trị website
                        </div>
                        <div class="controls">
                            <div class="item">
                                <div class="text">Tài khoản: </div>
                                <div class="control">
                                    <asp:TextBox ID="tbAccountName" runat="server"></asp:TextBox>
                                    <div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ErrorMessage="<div class='pt5'>Vui lòng nhập tài khoản!</div>" ControlToValidate="tbAccountName"
                                            Display="Dynamic" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="cb">
                                    <!---->
                                </div>
                            </div>
                            <div class="item">
                                <div class="text">Mật khẩu: </div>
                                <div class="control">
                                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ErrorMessage="<div class='pt5'>Vui lòng nhập mật khẩu!</div>" ControlToValidate="tbPassword"
                                            Display="Dynamic" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="cb">
                                    <!---->
                                </div>
                            </div>
                            <asp:Literal ID="ltrLoginLockedAlert" runat="server"></asp:Literal>
                            <asp:Button ID="btLogin" runat="server" Text="Đăng nhập" ToolTip="Click để đăng nhập vào hệ thống" OnClick="btLogin_Click" CssClass="btLogin" />
                        </div>
                        <div class="dn">
                            <div class="title2">Hỗ trợ</div>
                            <ul>
                                <li class="dn">
                                    <asp:LinkButton ID="lbtResetPassword" runat="server" ToolTip="Khôi phục mật khẩu" OnClick="lbtResetPassword_Click" CausesValidation="false">Khôi phục mật khẩu</asp:LinkButton>
                                </li>
                                <li>
                                    <a target="_blank" title="Liên hệ bộ phận hỗ trợ khách hàng" href="http://www.tatthanh.com.vn/">Liên hệ bộ phận hỗ trợ khách hàng</a>
                                </li>
                                <li>
                                    <a target="_blank" title="Liên hệ bộ phận tư vấn" href="http://www.tatthanh.com.vn/">Liên hệ bộ phận tư vấn</a>
                                </li>
                                <li>
                                    <a target="_blank" title="Liên hệ bộ phận kỹ thuật" href="http://www.tatthanh.com.vn/">Liên hệ bộ phận kỹ thuật</a>
                                </li>
                            </ul>
                            <div class="copyright">@ Bản quyền thuộc về Tất Thành</div>
                        </div>
                    </div>
                </div>
                <div class="leftCol dn">
                    <div class="head head1">Ưu điểm của hệ thống quản trị website Tất Thành</div>
                    <div class="taj">
                        <p>
                            <b>Dễ sử dụng:</b> hệ thống quản trị TatThanhCms liên tục được nâng cấp để công việc quản trị website ngày càng dễ dàng hơn.
                        </p>
                        <p>
                            <b>Cấu trúc đồng nhất:</b> các module của website có chung cách bố trí các tính năng. Vì vậy, người quản trị chỉ cần làm quen với việc quản lý một module là có thể dễ dàng sử dụng các module khác.
                        </p>
                        <p>
                            <b>Bảo mật:</b> hệ thống được xây dựng trên công nghệ ASP.NET và cơ sở dữ liệu SQL Server của Micosoft, có tính bảo mật và an toàn dữ liệu cao.
                        </p>
                        <p>
                            <b>Tính năng cao cấp:</b> hệ thống còn cung cấp các tính năng cao cấp như: thống kê báo cáo, thùng rác, thêm mới nhanh dữ liệu, tối ưu website cho các công cụ tìm kiếm,...
                        </p>
                        <br />
                        <div class="head head2">Thông tin hữu ích</div>
                        <p>
                            <a target="_blank" title="Giới thiệu về Tất Thành" href="http://tatthanh.com.vn/gioi-thieu-chung.htm">Giới thiệu về Tất Thành</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                        <a target="_blank" title="Liên hệ xây dựng website" href="http://tatthanh.com.vn/lien-he.htm">Liên hệ xây dựng website</a>
                        </p>
                    </div>
                </div>
                <div class="cb">
                    <!---->
                </div>
            </div>
        </div>
    </form>
</body>
</html>

