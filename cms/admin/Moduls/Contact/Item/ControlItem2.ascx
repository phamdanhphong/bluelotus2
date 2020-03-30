<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlItem2.ascx.cs" Inherits="cms_admin_Moduls_ContactUs_Item_ControlItem2" %>
<%@ Import Namespace="TatThanhJsc.Extension" %>
<%@ Register TagPrefix="cc1" Namespace="CssJscriptOptimizer.Controls" Assembly="CssJscriptOptimizer" %>
<cc1:StyleSheetCombiner ID="sheetCombiner" runat="server"><link href="~/cms/admin/Moduls/Contact/Item/ControlItem/_cs.css" rel="stylesheet" type="text/css" /></cc1:StyleSheetCombiner>

<asp:HiddenField ID="hd_time" runat="server" />
<div id="admitem">
    <div class="BgTabTool">        
        <a href="javascript:DeleteListItems()" class="LinkDelete"><%=Developer.ContactKeyword.Xoa%></a>                   
        <div class="right">            
            <div class="ColPagging"><div class="AdminPagging"><asp:Literal ID="LtPaggingTop" runat="server"></asp:Literal></div></div>
            <div class="ColShowItem">            
                <div class="TextShow">Hiển thị</div>
                <div class="BoxShow">
                    <asp:DropDownList ID="DdlListShowItemTop" runat="server" Width="50px" Height="19px" CssClass="TextInBox" AutoPostBack="True" 
                    onselectedindexchanged="DdlListShowItemTop_SelectedIndexChanged">
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="cb"><!----></div>
            </div>                                         
        </div>        
    </div>

    <div class="BgTabTitle" align="center">
        <div class="cot1 pt5" align="center"><input id="checkAll" type="checkbox" onchange="CheckAllCheckBox('CbItem',this)"/></div>
        <div class="split">|</div>                    
        <div class="cot2" align="left">        
            <asp:LinkButton ID="lbtName" runat="server" onclick="lbtName_Click" CssClass="arrowSort" ToolTip="Click để sắp xếp danh sách theo trường này"><%=Developer.ContactKeyword.TieuDeBaiViet %></asp:LinkButton>
        </div>
        <div class="split">|</div>                    
        <div class="cot3">
            <asp:LinkButton ID="lbtDate" runat="server" onclick="lbtDate_Click" CssClass="arrowSort" ToolTip="Click để sắp xếp danh sách theo trường này"><%=Developer.ContactKeyword.NgayDang %></asp:LinkButton>
        </div>                        
        <div class="split">|</div>                    
        <div class="cot5">
            <asp:LinkButton ID="lbtStatus" runat="server" onclick="lbtStatus_Click" CssClass="arrowSort" ToolTip="Click để sắp xếp danh sách theo trường này"><%=Developer.ContactKeyword.TrangThai %></asp:LinkButton>
        </div>                        
        <div class="split">|</div>                    
        <div class="fr pr5"><%=Developer.ContactKeyword.CongCu %></div>                    
        <div class="cb"><!----></div>
    </div>
    
    <div align="center" class="content">
        <asp:Repeater ID="rp_mn_users" runat="server" onitemcommand="rp_mn_users_ItemCommand">
            <ItemTemplate>
                <div id="Item-<%#Eval("IID").ToString()%>">
                    <div class="bgItem">
                        <div class="cot1"><input id="CbItem_<%#Eval("IID").ToString() %>" type="checkbox" /></div>                            
                        <div class="split">|</div>                            
                        <div class="cot2" align="left">
                            <div class="pt5">- <b>Họ tên:</b> <%#Eval("VITITLE").ToString() %></div>
                            <div class="pt5">- <b>Email:</b> <%#Eval("VIDESC").ToString() %></div>
                            <div class="pt5">- <b>Điện thoại:</b> <%#Eval("VIURL").ToString()%></div>
                            <div class="pt5">- <b>Số người:</b> <%#Eval("VISEOTITLE").ToString()%></div>
                            <div class="pt5">- <b>Ngày:</b> <%#Eval("VICONTENT").ToString()%></div>
                            <div class="pt5">- <b>Giờ:</b> <%#Eval("VISEOLINK").ToString()%></div>
                            <div class="pt5">- <b>Nội dung:</b> <%#Eval("VICONTENT").ToString()%></div>
                            <%--<div class="pt5">- <b>Gửi đến:</b> <%#Eval("vgname").ToString() %></div>--%>
                        </div>
                        <div class="split">|</div>                            
                        <div class="cot3" align="left">                            
                            <%#TimeExtension.FormatTime(Eval(TatThanhJsc.Columns.ItemsColumns.DicreatedateColumn),6)%>                        
                        </div>                                                    
                        <div class="split">|</div>                            
                        <div class="cot5" align="center">
                            <a id="nc<%#Eval("IID").ToString()%>" href="javascript:UpdateEnableItem(<%#Eval("IID").ToString()%>)" class="EnableIcon<%#Eval("IIENABLE").ToString()%>">&nbsp;</a>
                        </div>
                        <div class="split">|</div>                            
                        <div class="fr tool pr5">                            
                            <a title='Click để xem chi tiết thư liên hệ này' href="javascript:void(0)" onclick="NewWindow_('cms/admin/Moduls/Contact/Item/Popup/ViewDetail.aspx?iid=<%#Eval("IID")%>','ImageList','900','500','yes','yes')"><span class='iconInfo'><!----></span></a>
                            <a href="javascript:DeleteItem('<%#Eval("IID").ToString()%>','<%#Eval("VITITLE").ToString()%>')"><span class='iconDelete'><!----></span></a>                            
                        </div>
                        <div class="cbh0"><!----></div>                        
                    </div>
                </div>          
            </ItemTemplate>
            <SeparatorTemplate><div class="vien"><!----></div></SeparatorTemplate>
        </asp:Repeater>
    </div>
    <div class="cb h25"><!----></div>    
</div>

<div id="FooterRightControl">
    <div class="pdFooterR">
        <div class="ColPagging"><div class="AdminPagging"><asp:Literal ID="LtPagging" runat="server"></asp:Literal></div></div>
        <div class="ColShowItem">            
            <div class="TextShow">Hiển thị</div>
            <div class="BoxShow">
                <asp:DropDownList ID="DdlListShowItem" runat="server" Width="50px" 
                    Height="19px" CssClass="TextInBox" AutoPostBack="True" 
                    onselectedindexchanged="DdlListShowItem_SelectedIndexChanged">
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="cb"><!----></div>
        </div>        
        <div class="cbh0"><!----></div>
    </div>
</div>

<div id="SubItemSearch">
    <div class="frame">
        <div class="frame2">
            <div class="fl pr10">
                <asp:TextBox ID="tbTitleSearch" runat="server" placeholder="Tiêu đề thư"></asp:TextBox>
            </div>
            <div class="fl pr10">
                <asp:TextBox ID="tbKeySearch" runat="server" placeholder="Người gửi"></asp:TextBox>
            </div>
            <div class="fl">
                <asp:DropDownList ID="ddlCateSearch" runat="server">
                </asp:DropDownList>
            </div>
            <div class="fr tar">
                <asp:LinkButton ID="ltrSearch" runat="server" CssClass="lbtSearch" 
                    onclick="ltrSearch_Click">&nbsp;</asp:LinkButton>
            </div>
            <div class="cb"><!----></div>
        </div>
    </div>
    <script type="text/javascript">
        $(window).load(function () {
            var height = ($("#ContactUsModul").outerHeight() + $("#SubItemSearch").outerHeight());
            $(".PositionLeftControl").css("height", height + "px");
            $(".PositionRightControl").css("height", height + "px");
        });
        $(window).scroll(function () {
            if (($("#SubItemSearch").offset().top + $("#SubItemSearch").outerHeight()) > ($("#AdmFooter").offset().top + 20))
                $("#SubItemSearch").css("bottom", "43px");

            if (($("#SubItemSearch").offset().top + $("#SubItemSearch").outerHeight()) <= ($("#AdmFooter").offset().top - 20))
                $("#SubItemSearch").css("bottom", "0");
        });
    </script>
</div>