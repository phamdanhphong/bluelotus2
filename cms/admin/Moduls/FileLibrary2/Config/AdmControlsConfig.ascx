﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdmControlsConfig.ascx.cs" Inherits="cms_admin_FileLibrary2_Controls_AdmControlsConfiguration" %>
<%@ Register TagPrefix="cc1" Namespace="CssJscriptOptimizer.Controls" Assembly="CssJscriptOptimizer" %>
<cc1:StyleSheetCombiner ID="sheetCombiner" runat="server">
    <link href="~/cms/admin/Moduls/FileLibrary2/Config/AdmControlsConfig/_cs.css" rel="stylesheet" type="text/css" />
</cc1:StyleSheetCombiner>
<div id="AdmControlsConfig">
    <asp:HiddenField ID="hdLogoImage" runat="server" Value=""/>
    <div class="fwb pb10">Cấu hình số lượng</div>   
     <div class="cbh20"><!----></div>
    <div class="fl pl20">Số dữ liệu trên trang chủ:</div>                
    <div class="fl fr20">
        <asp:TextBox ID="tbSoFileLibrary2TrenTrangChu" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
            ErrorMessage="Vui lòng số tự nhiên(vd: 6)" ControlToValidate="tbSoFileLibrary2TrenTrangChu" 
            Display="Dynamic" SetFocusOnError="True" ValidationExpression="\d*"></asp:RegularExpressionValidator>
    </div>                
    <div class="cbh20"><!----></div>
    <div class="fl pl20">Số dữ liệu trên trang danh mục:</div> 
    <div class="fl pr20">
        <asp:TextBox ID="tbSoFileLibrary2TrenTrangDanhMuc" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
            ErrorMessage="Vui lòng số tự nhiên(vd: 6)" ControlToValidate="tbSoFileLibrary2TrenTrangDanhMuc" 
            Display="Dynamic" SetFocusOnError="True" ValidationExpression="\d*"></asp:RegularExpressionValidator>
    </div>
    <div class="cbh20"><!----></div>
    <div class="fl pl20">Số dữ liệu khác trên một trang:</div>                
    <div class="fl pr20">
        <asp:TextBox ID="tbSoFileLibrary2KhacTrenMotTrang" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
            ErrorMessage="Vui lòng số tự nhiên(vd: 6)" ControlToValidate="tbSoFileLibrary2KhacTrenMotTrang" 
            Display="Dynamic" SetFocusOnError="True" ValidationExpression="\d*"></asp:RegularExpressionValidator>
    </div>
   
    <div class="cbh20"><!----></div>
    <div class="fwb pb10">Cấu hình hình ảnh</div>
    <div class="pl20">                
        <asp:CheckBox ID="cbDongDauAnh" runat="server" Text="Đóng dấu ảnh"/>
        <div class="khungThuocTinh">
            <div class="cot1">Ảnh làm dấu:</div>
            <div class="cot2">
                <div>
                    <asp:Literal ID="ltrLogoImage" runat="server"></asp:Literal>
                </div>
                <div>
                    <asp:FileUpload ID="fulDongDauAnh" runat="server" /> (Nên chọn ảnh .png hoặc .gif kích thước nhỏ vừa phải)
                </div>
            </div>
            <div class="cb h5"><!----></div>
            <div class="cot1">Vị trí dấu:</div>
            <div class="fl">
                <asp:RadioButtonList ID="rbViTriDongDau" runat="server">
                    <asp:ListItem Text="Giữa ảnh" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Góc trên-trái" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Góc trên-phải" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Góc dưới-trái" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Góc dưới-phải" Value="4" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>            
            </div>
            <div class="cb h5"><!----></div>
            <div class="cot1">Cách lề:</div>            
            <div class="fl">
                <asp:TextBox ID="tbLeX" runat="server" ToolTip="Khoảng cách từ lề ngang tới ảnh con dấu"></asp:TextBox>&nbsp;px&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="tbLeY" runat="server" ToolTip="Khoảng cách từ lề dọc tới ảnh con dấu"></asp:TextBox>&nbsp;px&nbsp;&nbsp;&nbsp;(Lề ngang - dọc)
            </div>
            <div class="cb h5"><!----></div>
            <div class="cot1">Tỷ lệ dấu/ảnh(%)</div>
            <div class="fl">
                <asp:TextBox ID="tbPhanTram" runat="server" ToolTip="Ảnh con dấu sẽ giãn theo tỷ lệ với ảnh nền nhưng không vượt quá kích thước thật của nó"></asp:TextBox> (Bỏ trống nếu muốn giữ nguyên kích thước ảnh dấu)
            </div>
            <div class="cb h5"><!----></div>
            <div class="cot1">Độ trong suốt(%)</div>
            <div class="fl">
                <asp:TextBox ID="tbTrongSuot" runat="server" ToolTip="Nhập giá trị từ 0 đến 100"></asp:TextBox>
            </div>
            <div class="cb h5"><!----></div>
        </div>
        <div class="cb h10"><!----></div>

        <asp:CheckBox ID="cbHanCheKichThuoc" runat="server" Text="Hạn chế kích thước tối đa cho ảnh đại diện"/>
        <div class="khungThuocTinh">
            Rộng <asp:TextBox ID="tbHanCheW" runat="server" ToolTip="Chiều rộng lớn nhất có thể của ảnh đại diện, nếu ảnh có kích thước lớn hơn nó sẽ tự co lại"></asp:TextBox>&nbsp;px&nbsp;&nbsp;&nbsp;
            Cao <asp:TextBox ID="tbHanCheH" runat="server" ToolTip="Chiều cao lớn nhất có thể của ảnh đại diện, nếu ảnh có kích thước lớn hơn nó sẽ tự co lại"></asp:TextBox>&nbsp;px
        </div>

        <div class="cb h10"><!----></div>
        <asp:CheckBox ID="cbTaoAnhNho" runat="server" Text="Tạo ảnh nhỏ cho ảnh đại diện(thumbnails)"/>
        <div class="khungThuocTinh">
        Rộng <asp:TextBox ID="tbAnhNhoW" runat="server" ToolTip="Chiều rộng của ảnh nhỏ. Ảnh nhỏ dùng để hiển thị thay thế cho ảnh đại diện nhằm giảm tải dữ liệu phải tải về máy khách khi hiển thị"></asp:TextBox>&nbsp;px&nbsp;&nbsp;&nbsp;
        Cao <asp:TextBox ID="tbAnhNhoH" runat="server" ToolTip="Chiều cao của ảnh nhỏ. Ảnh nhỏ dùng để hiển thị thay thế cho ảnh đại diện nhằm giảm tải dữ liệu phải tải về máy khách khi hiển thị"></asp:TextBox>&nbsp;px
        </div>             
    </div>
    
    <div class="cbh20"><!----></div>
    <div class="fwb pb5">Cấu hình trang chủ trang quản trị</div>
    <div>
        Tích chọn các mục bạn muốn hiển thị tại trang quản trị của modul này và nhập thứ tự để sắp xếp chúng
    </div> 
    <div class="cbh20"><!----></div>
    <asp:Panel ID="pnCauHinhTrangChu" runat="server" CssClass="pl20">
        <div class="khungThuocTinh">
            <asp:Panel ID="Panel1" runat="server" CssClass="pb5">                    
                <asp:HiddenField ID="HiddenField1" runat="server" Value="cms/admin/Moduls/FileLibrary2/Index.ascx->cms/admin/Moduls/FileLibrary2/Item/SubControl/SubControlComment.ascx"/>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Phản hồi mới"/>                    
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" CssClass="pb5">                    
                <asp:HiddenField ID="HiddenField2" runat="server" Value="cms/admin/Moduls/FileLibrary2/Index.ascx->cms/admin/Moduls/FileLibrary2/Item/SubControl/SubControlItemLastest.ascx"/>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:CheckBox ID="CheckBox2" runat="server" Text="Dữ liệu mới cập nhật"/>                    
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" CssClass="pb5">                    
                <asp:HiddenField ID="HiddenField3" runat="server" Value="cms/admin/Moduls/FileLibrary2/Index.ascx->cms/admin/Moduls/FileLibrary2/Item/SubControl/SubControlItemHostest.ascx"/>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <asp:CheckBox ID="CheckBox3" runat="server" Text="Dữ liệu xem nhiều"/>                    
            </asp:Panel>            
        </div>                 
    </asp:Panel>

    <div class="cb h20"><!----></div>
    <div class="tac">
        <asp:Button ID="btSave" runat="server" onclick="btSave_Click" Width="120px" Text="Đồng ý" OnClientClick="return CheckInput()"/>
    </div>
    <div class="cb h10"><!----></div>
</div>