<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Leftmenu.ascx.cs" Inherits="cms_admin_AboutUs_Leftmenu" %>
<%@ Import Namespace="Developer" %>
<%@ Import Namespace="TatThanhJsc.AboutUsModul" %>
<div id="Leftmenu" class="colLeft">
    <a class="head" href="admin.aspx?uc=<%= uc %>"><%= AboutUsKeyword.TongQuanModul %><span><!----></span></a>

    <div class="subHead"><%= AboutUsKeyword.DanhMucQuanLy %></div>
    <a class="tool tCate <%= SetCurrent(suc, TypePage.Cate) %>" href="<%= Link.LnkMnAboutUsCate() %>"><%= AboutUsKeyword.QuanLyDanhMuc %></a>    
    <div class="dn">
        <a class="tool tGroup <%= SetCurrent(suc, TypePage.GroupItem) %>" href="<%= Link.LnkMnAboutUsGroupItem() %>"><%= AboutUsKeyword.QuanLyNhom %></a>
    </div>
    <a class="tool tItem <%= SetCurrent(suc, TypePage.Item) %>" href="<%= Link.LnkMnAboutUsItem() %>"><%= AboutUsKeyword.QuanLyDanhSach %></a>
    <div class="cb h15"><!----></div>

    <div class="subHead"><%= AboutUsKeyword.CongCu %></div>
    <a class="tool tCate plus <%= SetCurrent(suc, TypePage.CreateCate) %>" href="<%= Link.LnkMnAboutUsCateCreate() %>"><%= AboutUsKeyword.ThemMoiDanhMuc %></a>   
    <div class="dn">
        <a class="tool tGroup plus <%= SetCurrent(suc, TypePage.CreateGroupItem) %>" href="<%= Link.LnkMnAboutUsGroupItemCreate() %>"><%= AboutUsKeyword.ThemMoiNhom %></a>
    </div>
    <a class="tool tItem plus <%= SetCurrent(suc, TypePage.CreateItem) %>" href="<%= Link.LnkMnAboutUsItemCreate() %>"><%= AboutUsKeyword.ThemMoi %></a>
    <div class="cb h15"><!----></div>

    <div class="subHead"><%= AboutUsKeyword.TinhNangKhac %></div>
    <a class="tool tConfig <%= SetCurrent(suc, TypePage.Configuration) %>" href="<%= Link.LnkMnAboutUsConfig() %>"><%= AboutUsKeyword.CauHinh %></a>
    <a class="tool tRecycle" href="javascript://"><%= AboutUsKeyword.ThungRac %></a>
    <a class="subtool <%= SetCurrent(suc, TypePage.RecycleCate) %>" href="<%= Link.LnkMnAboutUsCateRec() %>">- <%= AboutUsKeyword.DanhMuc %></a>
    <div class="dn">
        <a class="subtool <%= SetCurrent(suc, TypePage.RecycleGroupItem) %>" href="<%= Link.LnkMnAboutUsGroupItemRec() %>">- <%= AboutUsKeyword.Nhom %></a>
    </div>
    <a class="subtool <%= SetCurrent(suc, TypePage.RecycleItem) %>" href="<%= Link.LnkMnAboutUsItemRec() %>">- <%= AboutUsKeyword.DanhSach %></a>
</div>