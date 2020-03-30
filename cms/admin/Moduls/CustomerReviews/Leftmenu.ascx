<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Leftmenu.ascx.cs" Inherits="cms_admin_CustomerReviews_Leftmenu" %>
<%@ Import Namespace="Developer" %>
<%@ Import Namespace="TatThanhJsc.CustomerReviewsModul" %>
<div id="Leftmenu" class="colLeft">
    <a class="head" href="admin.aspx?uc=<%= uc %>"><%= CustomerReviewsKeyword.TongQuanModul %><span><!----></span></a>

    <div class="subHead"><%= CustomerReviewsKeyword.DanhMucQuanLy %></div>
    <a class="tool tCate <%= SetCurrent(suc, TypePage.Cate) %>" href="<%= Link.LnkMnCustomerReviewsCate() %>"><%= CustomerReviewsKeyword.QuanLyDanhMuc %></a>    
    <a class="tool tGroup <%= SetCurrent(suc, TypePage.GroupItem) %>" href="<%= Link.LnkMnCustomerReviewsGroupItem() %>"><%= CustomerReviewsKeyword.QuanLyNhom %></a>
    <a class="tool tItem <%= SetCurrent(suc, TypePage.Item) %>" href="<%= Link.LnkMnCustomerReviewsItem() %>"><%= CustomerReviewsKeyword.QuanLyDanhSach %></a>
    <div class="cb h15"><!----></div>

    <div class="subHead"><%= CustomerReviewsKeyword.CongCu %></div>
    <a class="tool tCate plus <%= SetCurrent(suc, TypePage.CreateCate) %>" href="<%= Link.LnkMnCustomerReviewsCateCreate() %>"><%= CustomerReviewsKeyword.ThemMoiDanhMuc %></a>   
    <a class="tool tGroup plus <%= SetCurrent(suc, TypePage.CreateGroupItem) %>" href="<%= Link.LnkMnCustomerReviewsGroupItemCreate() %>"><%= CustomerReviewsKeyword.ThemMoiNhom %></a>
    <a class="tool tItem plus <%= SetCurrent(suc, TypePage.CreateItem) %>" href="<%= Link.LnkMnCustomerReviewsItemCreate() %>"><%= CustomerReviewsKeyword.ThemMoi %></a>
    <div class="cb h15"><!----></div>

    <div class="subHead"><%= CustomerReviewsKeyword.TinhNangKhac %></div>
    <a class="tool tConfig <%= SetCurrent(suc, TypePage.Configuration) %>" href="<%= Link.LnkMnCustomerReviewsConfig() %>"><%= CustomerReviewsKeyword.CauHinh %></a>
    <a class="tool tRecycle" href="javascript://"><%= CustomerReviewsKeyword.ThungRac %></a>
    <a class="subtool <%= SetCurrent(suc, TypePage.RecycleCate) %>" href="<%= Link.LnkMnCustomerReviewsCateRec() %>">- <%= CustomerReviewsKeyword.DanhMuc %></a>
    <a class="subtool <%= SetCurrent(suc, TypePage.RecycleGroupItem) %>" href="<%= Link.LnkMnCustomerReviewsGroupItemRec() %>">- <%= CustomerReviewsKeyword.Nhom %></a>
    <a class="subtool <%= SetCurrent(suc, TypePage.RecycleItem) %>" href="<%= Link.LnkMnCustomerReviewsItemRec() %>">- <%= CustomerReviewsKeyword.DanhSach %></a>
</div>