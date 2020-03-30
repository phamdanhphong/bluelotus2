<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Leftmenu.ascx.cs" Inherits="cms_admin_Service_AdmLeftmenu" %>
<%@ Import Namespace="TatThanhJsc.ServiceModul" %>
<div id="ServiceAdmLeftMenu">
    <div class="BgTabTongQuan"><a class="TextInTabTongQuan" href="admin.aspx?uc=<%=uc %>"><%=Developer.ServiceKeyword.TongQuanModul %></a></div>
    <div class="DanhMucQuanLy"><%=Developer.ServiceKeyword.DanhMucQuanLy %></div>
    <div class="PdSpaceCate"><div class='<%=SetEnableSpaceCate() %>'><div class="SpaceCate"><!----></div></div></div>
    <div class="ArroundCate<%=SetSelectedCate(TypePage.Cate) %>">
        <div class="PdIconInfomation"><div class='iconDanhMuc'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceCate() %>"><%=Developer.ServiceKeyword.QuanLyDanhMuc %></a>
        <div class="cbh8"><!----></div>
    </div>
    <div class="ArroundCate<%=SetSelectedCate(TypePage.Item) %>">
        <div class="PdIconInfomation"><div class='iconDanhSach'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceItem() %>"><%=Developer.ServiceKeyword.QuanLyDanhSach %></a>
        <div class="cbh8"><!----></div>
    </div>

    <div class="ArroundCate<%=SetSelectedCate(TypePage.GroupItem) %>">
        <div class="PdIconInfomation"><div class='iconNhom'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceGroupItem() %>"><%=Developer.ServiceKeyword.QuanLyNhom %></a>
        <div class="cbh8"><!----></div>
    </div>
   
    <asp:Panel ID="pnThuocTinhDichVu" runat="server">
     <div class="ArroundCate<%=SetSelectedCate(TypePage.Property) %>">
        <div class="PdIconInfomation"><div class='iconThuocTinh'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceProperty() %>"><%=Developer.ServiceKeyword.QuanLyThuocTinh%></a>
        <div class="cbh8"><!----></div>
    </div>
    </asp:Panel>        
    
    <asp:Panel ID="pnDanhSachPhanHoi" runat="server">
    <div class="ArroundCate<%=SetSelectedCate(TypePage.Comment) %>">
        <div class="PdIconInfomation"><div class='iconPhanHoi'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceComment() %>"><%=Developer.ServiceKeyword.DanhSachPhanHoi%></a>
        <div class="cbh8"><!----></div>
    </div>
    </asp:Panel>

    <asp:PlaceHolder ID="PhManagerApi" runat="server"></asp:PlaceHolder>
    <div class="cbh20"><!----></div>

    <div class="cbh20"><!----></div>
    <div class="DanhMucQuanLy"><%=Developer.ServiceKeyword.CongCu %></div>
    <div class="PdSpaceCate"><div class='<%=SetEnableTool() %>'><div class="SpaceCate"><!----></div></div></div>
    <div class="ArroundCate<%=SetSelectedCate(TypePage.CreateCate) %>">
        <div class="PdIconInfomation"><a href="?uc=<%=uc %>&suc=ImportCategory" title="Click để nhập dịch vụ từ Excel"><div class='iconDanhSachThemMoi'><!----></div></a></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceCateCreate() %>"><%=Developer.ServiceKeyword.ThemMoiDanhMuc %></a>
        <div class="cbh0"><!----></div>
    </div>
    <div class="ArroundCate<%=SetSelectedCate(TypePage.CreateItem) %>">
        <div class="PdIconInfomation"><a href="?uc=<%=uc %>&suc=ImportItem" title="Click để nhập dịch vụ từ Excel"><div class='iconDanhSachThemMoi'><!----></div></a></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceItemCreate() %>"><%=Developer.ServiceKeyword.ThemMoi %></a>
        <div class="cbh0"><!----></div>
    </div>    

    <div class="ArroundCate<%=SetSelectedCate(TypePage.CreateGroupItem) %>">
        <div class="PdIconInfomation"><div class='iconNhomThemMoi'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceGroupItemCreate() %>"><%=Developer.ServiceKeyword.ThemMoiNhom %></a>
        <div class="cbh0"><!----></div>
    </div>
<%--    <div class="ArroundCate<%=SetSelectedCate("ImportItem") %>">
        <div class="PdIconInfomation"><a href="#" title="Click để nhập dịch vụ từ Excel"><div class='iconExcel'><!----></div></a></div>
        <a class="TextCateManager" href="?uc=<%=uc %>&suc=ImportItem"><%=Developer.ServiceKeyword.CapNhatQuaExcel %></a>
        <div class="cbh0"><!----></div>
    </div>   --%>   
    <asp:Panel ID="pnThuocTinhDichVu_ThemMoi" runat="server">
    <div class="ArroundCate<%=SetSelectedCate(TypePage.CreateProperty) %>">
        <div class="PdIconInfomation"><div class='iconThuocTinh'><!----></div></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServicePropertyCreate() %>"><%=Developer.ServiceKeyword.ThemMoiThuocTinh%></a>
        <div class="cbh0"><!----></div>
    </div>   
    </asp:Panel>    
    
    <div class="cbh20"><!----></div>
    <div class="DanhMucQuanLy"><%=Developer.ServiceKeyword.TinhNangKhac %></div>
    <div class="PdSpaceCate"><div class='<%=SetCustomizeOther() %>'><div class="SpaceCate"><!----></div></div></div>
    <asp:Panel ID="pnThongKeBaoCao" runat="server">
    <%--<div class="ArroundCate<%=SetSelectedCate(TypePage.Report) %>">
        <div class="PdIconInfomation"><div class='iconReport'><!----></div></div>
        <a class="TextCateManager" href="<%=LinkManageReport %>"><%=Developer.ServiceKeyword.ThongKeBaoCao%></a>
        <div class="cbh0"><!----></div>
    </div>--%>
    </asp:Panel>    
    <div class="ArroundCate<%=SetSelectedCate(TypePage.Configuration) %>">
        <div class="PdIconInfomation"><a href="#"><div class='iconConfig'><!----></div></a></div>
        <a class="TextCateManager" href="<%=Link.LnkMnServiceConfig() %>"><%=Developer.ServiceKeyword.CauHinh %></a>
        <div class="cbh0"><!----></div>
    </div>
    <div class="ArroundCate<%=SetSelectedRecycleBin() %>">
        <div class="PdIconInfomation"><div class='iconRecycle'><!----></div></div>
        <div class="TextCateManager"><%=Developer.ServiceKeyword.ThungRac %></div>
        <div class="cbh0"><!----></div>
    </div>    
    <div class="cbh5"><!----></div>
    <div class="PdSubIconRecycleBin">-</div>
    <a class="TextCateManager <%=SetSelectedCate(TypePage.RecycleCate) %>" href="<%=Link.LnkMnServiceCateRec() %>"><%=Developer.ServiceKeyword.DanhMuc %></a>
    <div class="cbh0"><!----></div>
    <div class="PdSubIconRecycleBin">-</div>
    <a class="TextCateManager <%=SetSelectedCate(TypePage.RecycleItem) %>" href="<%=Link.LnkMnServiceItemRec() %>"><%=Developer.ServiceKeyword.DanhSach %></a>
    <div class="cbh0"><!----></div>
    <div class="PdSubIconRecycleBin">-</div>
    <a class="TextCateManager <%=SetSelectedCate(TypePage.RecycleGroupItem) %>" href="<%=Link.LnkMnServiceGroupItemRec() %>"><%=Developer.ServiceKeyword.Nhom %></a>
    <div class="cbh0"><!----></div>
    <asp:Panel ID="pnThuocTinhDichVu_ThungRac" runat="server">    
    <div class="PdSubIconRecycleBin">-</div>
    <a class="TextCateManager <%=SetSelectedCate(TypePage.RecycleProperty) %>" href="<%=Link.LnkMnServicePropertyRec() %>"><%=Developer.ServiceKeyword.ThuocTinh%></a>
    <div class="cbh0"><!----></div>
    </asp:Panel>                
</div>