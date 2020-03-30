﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubControlItemLastest.ascx.cs" Inherits="cms_admin_Moduls_Blog_Item_SubControl_SubControlItemLastest" %>
<%@ Import Namespace="TatThanhJsc.AdminModul" %>
<%@ Import Namespace="TatThanhJsc.BlogModul" %>
<%@ Register TagPrefix="cc1" Namespace="CssJscriptOptimizer.Controls" Assembly="CssJscriptOptimizer" %>
<cc1:StyleSheetCombiner ID="sheetCombiner" runat="server">    
    <link href="~/cms/admin/Moduls/Blog/Item/SubControl/SubControlItemLastest/_cs.css" rel="stylesheet" type="text/css" />   
</cc1:StyleSheetCombiner>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div id="SubBlogItemLastest">    
    <div class="header">
        <div class="tab"><%=subControlsTitle%></div>
<%--        <div class="icon"><a><div class='iconClose'><!----></div></a></div>
        <div class="icon"><a><div class='iconRefresh'><!----></div></a></div>  --%>      
        <div class="icon"><a><div class='iconClose'><!----></div></a></div>
        <div class="icon">
            <asp:LinkButton ID="lbtRefresh" runat="server" onclick="lbtRefresh_Click"><div class='iconRefresh'><!----></div></asp:LinkButton>
        </div>
        <div class="cb"><!----></div>
    </div>    
    <div class="content">        
        <div class="head">
            <div class="cot1"><div class="pl15">Tiêu đề</div></div>
            <div class="fr tool"><div class="pr5">Công cụ</div></div>
            <div class="cot3"><div class="pl15">Ngày cập nhật</div></div>
            <div class="cot2">Lượt xem</div>
            <div class="cb"><!----></div>
        </div>
        <div class="cb h1"><!----></div>
        <asp:Repeater ID="RpItems" runat="server">
            <ItemTemplate>
                <div class="Item" id="Item-<%#Eval("IID").ToString()%>">
                    <div class="cot1"><div class="pl15"><%#Eval("VITITLE").ToString()%></div></div>                                            
                    <div class="fr tool">      
                        <a title="Ẩn / Hiện" id="nc<%#Eval("IID").ToString()%>" href="javascript:UpdateEnableItem(<%#Eval("IID").ToString()%>)" class="EnableIcon<%#Eval("IIENABLE").ToString() %>">&nbsp;</a>
                        <a title="Cập nhật" href="<%#LinkAdmin.GoAdminItem(CodeApplications.Blog, TypePage.UpdateItem, Eval("IID").ToString())%>"><span class="iconEdit"><!----></span></a>
                        <a title="Xóa" href="javascript:DeleteItem('<%#Eval("IID").ToString()%>','<%#Eval("VITITLE").ToString()%>')"><span class='iconDelete'><!----></span></a>
                    </div>
                    <div class="cot3"><div class="pl15"><%#((DateTime)Eval("DUPDATE")).ToString(dinhDangNgay)%></div></div>
                    <div class="cot2"><%#TatThanhJsc.Extension.NumberExtension.FormatNumber(Eval("IITOTALVIEW").ToString())%></div>
                    <div class="cb"><!----></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>        
    </div>    
</div>
</ContentTemplate>
</asp:UpdatePanel>