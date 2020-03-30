﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Leftmenu.ascx.cs" Inherits="cms_admin_Moduls_Advertising_Leftmenu" %>
<%@ Import Namespace="TatThanhJsc.AdvertisingModul" %>
<div id="AdvertisingAdmLeftMenu">
    <div class="BgTabTongQuan"><a class="TextInTabTongQuan" href="admin.aspx?uc=<%=CodeApplications.Advertising %>"><%=Developer.AdvertisingKeyword.TongQuanModul %></a></div>
    <div class="DanhMucQuanLy"><%=Developer.AdvertisingKeyword.DanhMucQuanLy %></div>
    <div class="PdSpaceCate"><div class='<%=SetEnableSpaceCate() %>'><div class="SpaceCate"><!----></div></div></div>
    <div class="ArroundCate<%=SetSelectedCate(TypePage.Cate) %>">
        <div class="PdIconInfomation"><div class="iconDanhMuc"><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnAdvertisingCate() %>"><%=Developer.AdvertisingKeyword.QuanLyViTriQuangCao %></a>
        <div class="cbh8"><!----></div>
    </div>
    <div class="ArroundCate<%=SetSelectedCate(TypePage.Item) %>">
        <div class="PdIconInfomation"><div class='iconDanhSach'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnAdvertisingItem() %>"><%=Developer.AdvertisingKeyword.QuanLyAnhQuangCao %></a>
        <div class="cbh8"><!----></div>
    </div>    
    <div class="cbh8"><!----></div>
    <asp:PlaceHolder ID="PhManagerApi" runat="server"></asp:PlaceHolder>
    <div class="cbh20"><!----></div>
        
    <div class="DanhMucQuanLy">Công cụ</div>
    <div class="PdSpaceCate"><div class='<%=SetEnableTool() %>'><div class="SpaceCate"><!----></div></div></div>
    <div class="ArroundCate<%=SetSelectedCate(TypePage.CreateCate) %>">
        <div class="PdIconInfomation"><div class='iconDanhMucThemMoi'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnAdvertisingCateCreate() %>"><%=Developer.AdvertisingKeyword.ThemMoiViTriQuangCao %></a>
        <div class="cbh0"><!----></div>
    </div>
    <div class="ArroundCate<%=SetSelectedCate(TypePage.CreateItem) %>">
        <div class="PdIconInfomation"><div class='iconDanhSachThemMoi'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnAdvertisingItemCreate() %>"><%=Developer.AdvertisingKeyword.ThemMoiQuangCao %></a>
        <div class="cbh0"><!----></div>
    </div>    
    
    <div class="cbh20"><!----></div>
    <div class="ArroundCate<%=SetSelectedRecycleBin() %>">
        <div class="PdIconInfomation"><div class='iconRecycle'><!----></div></div>
        <div class="TextCateManager"><%=Developer.AdvertisingKeyword.ThungRac %></div>
        <div class="cbh0"><!----></div>
    </div>    
    <div class="cbh5"><!----></div>
    <div class="PdSubIconRecycleBin">-</div>
    <a class="TextCateManager <%=SetSelectedCate(TypePage.RecycleCate) %>" href="<%=Link.LnkMnAdvertisingCateRec() %>"><%=Developer.AdvertisingKeyword.ViTriQuangCao %></a>
    <div class="cbh5"><!----></div>
    <div class="PdSubIconRecycleBin">-</div>
    <a class="TextCateManager <%=SetSelectedCate(TypePage.RecycleItem) %>" href="<%=Link.LnkMnAdvertisingItemRec() %>"><%=Developer.AdvertisingKeyword.DanhSachQuangCao %></a>
    <div class="cbh0"><!----></div>            
</div>