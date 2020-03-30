<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlsUserTiemTang.ascx.cs" Inherits="cms_admin_ModulMain_Email_Controls_AdmControlsUserTiemTang" %>
<%@ Import Namespace="TatThanhJsc.Columns" %>
<%@ Import Namespace="TatThanhJsc.Extension" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:HiddenField ID="hd_modulid" runat="server" />
<asp:HiddenField ID="hd_parent" runat="server" />
<div id="ProductAdmControlsCategory">
<div class="PositionRightControl">
    <div class="BgTabTool">
        <div class="pdTool">             
            <div><asp:LinkButton CssClass="LinkDelete" ID="lnk_delete_user_checked" runat="server" onclick="lnk_delete_user_checked_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn xoá các tài khoản này?');">Xóa các tài khoản đang chọn</asp:LinkButton></div>
            <div class="FormatTextBox">
                <div class="cb h4"><!----></div>
                <asp:TextBox ID="txtKeySearch" runat="server" 
                    Width="250px" Height="14px" CssClass="TextInBox"
                    onclick="if(this.value=='Nhập tên tài khoản cần tìm') this.value=''" 
                    onblur="if(this.value.length<1) this.value='Nhập tên tài khoản cần tìm'" 
                    AutoPostBack="True" ontextchanged="txtKeySearch_TextChanged">Nhập tên tài khoản cần tìm</asp:TextBox>
            </div>
            <div class="cbh0"><!----></div>
        </div>
    </div>
    <div class="cbh0"><!----></div>
    <div class="BgTabTitle" align="center">
        <div class="FormatCheckBox" align="center"><asp:CheckBox ID="chk_list" runat="server" AutoPostBack="true" oncheckedchanged="chk_list_CheckedChanged" /></div>
        <div class="SplitBar">|</div>
        <div class="FormatTitle" align="left">
            <asp:LinkButton ID="lbtName" runat="server" onclick="lbtName_Click" CssClass="arrowSort" ToolTip="Click để sắp xếp danh sách theo trường này">Tên tài khoản</asp:LinkButton>
        </div>
        <div class="SplitBar">|</div>     
        <div class="FormatTitle" style="width:250px" align="left">
            Thông tin cơ bản
        </div>
        <div class="SplitBar">|</div>     
        <div class="FormatStatus">
            <asp:LinkButton ID="lbtStatus" runat="server" onclick="lbtStatus_Click" CssClass="arrowSort" ToolTip="Click để sắp xếp danh sách theo trường này">Trạng thái</asp:LinkButton>
        </div>
        <div class="SplitBar">|</div>
        <div class="FormatOnTabCollumTool">Công cụ</div>
        <div class="cbh0"><!----></div>
    </div>
    <div class="BgContainContent" align="center">
        <asp:Repeater ID="rp_mn_users" runat="server" onitemcommand="rp_mn_users_ItemCommand">
            <ItemTemplate>
                <div class="FormatCellItem">
                    <div class="pdCellItem">
                        <div class="FormatCheckBox"><asp:CheckBox ID="chk_item" runat="server" ToolTip='<%#Eval("IMID")%>' /></div>
                        <div class="SplitBar">|</div>
                        <div class="FormatTitle" align="left">
                            <%#Eval(MembersColumns.VmemberaccountColumn).ToString()%>              
                        </div>
                        <div class="SplitBar">|</div>
                        <div class="FormatTitle" style="width:250px" align="left">
                            - Họ tên: <%#Eval(MembersColumns.VmembernameColumn) %><br />
                            - Điện thoại:<%#Eval(MembersColumns.VmemberphoneColumn)%><br />
                            - Email:<%#Eval(MembersColumns.VmemberemailColumn)%><br />
                            - Ngày sinh:  <%#StringExtension.LayChuoi(Eval(MembersColumns.VmembercommentColumn).ToString(), TatThanhJsc.AdminModul.Keyword.ParamsSpilitItems, 4).ToString() %><br />
                            - Địa chỉ:<%#Eval(MembersColumns.VmemberaddressColumn)%><br />                            
                            - Quốc gia:  <%#StringExtension.LayChuoi(Eval(MembersColumns.VmemberimageColumn).ToString(), TatThanhJsc.AdminModul.Keyword.ParamsSpilitItems, 1) %><br />
                            - Thành phố:  <%#StringExtension.LayChuoi(Eval(MembersColumns.VmemberimageColumn).ToString(), TatThanhJsc.AdminModul.Keyword.ParamsSpilitItems, 2)%><br />
                            - Quận:  <%#StringExtension.LayChuoi(Eval(MembersColumns.VmemberimageColumn).ToString(), TatThanhJsc.AdminModul.Keyword.ParamsSpilitItems, 3)%><br />
                            - Ngày tạo:<%#TatThanhJsc.Extension.TimeExtension.FormatTime(Eval(MembersColumns.DmembercreatedateColumn),6)%><br />
                        </div>
                        <div class="SplitBar">|</div>                   
                        <div class="FormatStatus" align="center">
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="EditStatus" ToolTip="Click vào để thay đổi trạng thái" CommandArgument='<%#Eval("IMID").ToString() %>'>
                                <div class="EnableIcon<%#Eval(TatThanhJsc.Columns.MembersColumns.ImemberisapprovedColumn).ToString()%>"><!----></div>                                
                            </asp:LinkButton>
                        </div>
                        <div class="SplitBar">|</div>
                        <div class="fr">
                            <div class="IconTool"><asp:LinkButton ID="LnkDel" runat="server" CommandName="delete" CommandArgument='<%#Eval("IMID").ToString() %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xoá tài khoản vừa chọn?');" ToolTip="Click vào để xóa tài khoản này"><div class='iconDelete'><!----></div></asp:LinkButton></div>
                            <div class="IconTool"><asp:LinkButton ID="lbtDoiMatKhau" runat="server" CommandName="editPassword" CommandArgument='<%#Eval("IMID").ToString() %>' ToolTip="Click vào để đổi mật khẩu cho tài khoản này"><div class='iconChangePassword'><!----></div></asp:LinkButton></div>
                            <div class="cbh0"><!----></div>
                        </div>
                        <div class="cbh0"><!----></div>
                    </div>
                </div>
            </ItemTemplate>
            <SeparatorTemplate>
                <div class="pdSpaceItem"><div class="SpaceItem"><!----></div></div>
            </SeparatorTemplate>
        </asp:Repeater>
    </div>  
</div>
    <div id="FormatFooterRightControl">
        <div class="ColShowItem">
            <div class="TextShow">Hiển thị</div>
            <div class="BoxShow">
                <asp:DropDownList ID="DdlListShowItem" runat="server" Width="50px" Height="19px" CssClass="TextInBox" onselectedindexchanged="DdlListShowItem_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="cbh0"><!----></div>
        </div>
        <div class="ColPagging"><div id="AdminPagging"><asp:Literal ID="LtPagging" runat="server"></asp:Literal></div></div>
        <div class="cbh0"><!----></div>
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>