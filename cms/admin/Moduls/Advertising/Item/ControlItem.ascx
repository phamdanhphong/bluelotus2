<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlItem.ascx.cs" Inherits="cms_admin_Moduls_Advertising_Item_ControlItem" %>
<%@ Import Namespace="TatThanhJsc.Extension" %>

<%@ Register TagPrefix="cc1" Namespace="CssJscriptOptimizer.Controls" Assembly="CssJscriptOptimizer" %>
<cc1:StyleSheetCombiner ID="sheetCombiner" runat="server">
    <link href="~/cms/admin/Moduls/Advertising/Item/ControlItem/_cs.css" rel="stylesheet" type="text/css" />
</cc1:StyleSheetCombiner>

<asp:HiddenField ID="hd_time" runat="server" />
<div id="admitem">
    <div class="BgTabTool">                
        <a href="<%=LinkCreate() %>" class="LinkCreate"><%=Developer.AdvertisingKeyword.ThemMoi%></a>
        &nbsp;|&nbsp;
        <a href="javascript:DeleteListItems()" class="LinkDelete"><%=Developer.AdvertisingKeyword.Xoa%></a>                   
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
            <asp:LinkButton ID="lbtName" runat="server" onclick="lbtName_Click" CssClass="arrowSort" ToolTip="Click để sắp xếp danh sách theo trường này"><%=Developer.AdvertisingKeyword.AnhQuangCao %></asp:LinkButton>
        </div>
        <div class="split">|</div>                    
        <div class="cot3">
            <asp:LinkButton ID="lbtDate" runat="server" onclick="lbtDate_Click" CssClass="arrowSort" ToolTip="Click để sắp xếp danh sách theo trường này"><%=Developer.AdvertisingKeyword.NgayDang %></asp:LinkButton>
        </div>                        
        <div class="split">|</div>                    
        <div class="cot5">
            <asp:LinkButton ID="lbtStatus" runat="server" onclick="lbtStatus_Click" CssClass="arrowSort" ToolTip="Click để sắp xếp danh sách theo trường này"><%=Developer.AdvertisingKeyword.TrangThai %></asp:LinkButton>
        </div>                        
        <div class="split">|</div>                    
        <div class="fr pr5"><%=Developer.AdvertisingKeyword.CongCu %></div>                    
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
                            <div class="pb5">                   
                                <%#GetImageAdv(pic, Eval("FIPRICE").ToString(), Eval("VIIMAGE").ToString(), "SizeImage") %>
                            </div>
                            <div>
                                <%#Eval("VITITLE").ToString() %>
                            </div>
                        </div>                            
                        <div class="split">|</div>                            
                        <div class="cot3"><%#TimeExtension.FormatTime(Eval("DCREATEDATE"),"dd/MM/yyyy - HH:mm")%></div>                                            
                        <div class="split">|</div>
                        <div class="cot5" align="center">
                            <a id="nc<%#Eval("IID").ToString()%>" href="javascript:UpdateEnableItem(<%#Eval("IID").ToString()%>)" class="EnableIcon<%#Eval("IIENABLE").ToString()%>">&nbsp;</a>
                        </div>
                        <div class="split">|</div>                            
                        <div class="fr tool pr5">
                            <a href="<%#LinkUpdate(Eval("IID").ToString())%>"><span class="iconEdit"><!----></span></a>                        
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
        <div class="frame2 tar">
            <div class="di pr10 pt5">
                <asp:TextBox ID="tbTitleSearch" runat="server" placeholder="Tiêu đề quảng cáo"></asp:TextBox>
            </div>
            <div class="di pr10 pt5">
                <asp:DropDownList ID="ddlCateSearch" runat="server">
                </asp:DropDownList>
            </div>
            <div class="di btsearchp">
                <asp:LinkButton ID="ltrSearch" runat="server" CssClass="lbtSearch" 
                    onclick="ltrSearch_Click">&nbsp;</asp:LinkButton>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(window).load(function () {
            var height = ($("#AdvertisingModul").outerHeight() + $("#SubItemSearch").outerHeight());
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