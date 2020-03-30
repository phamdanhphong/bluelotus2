<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="cms_display_Filelibrary_Controls_Category" %>
<%@ Register Src="~/cms/display/Filelibrary/SubControls/SubFilelibraryOrther.ascx" TagPrefix="uc1" TagName="SubFilelibraryOrther" %>
<%@ Register Src="~/cms/display/CommonControls/CommonCuoiChiTietTin.ascx" TagPrefix="uc1" TagName="CommonCuoiChiTietTin" %>
<%@ Register Src="~/cms/display/CommonControls/CommonTool.ascx" TagPrefix="uc1" TagName="CommonTool" %>



<div id="content">
    <div class="khoi1200">
        <div class="PageContentLoadControl">
            <div class="khoi1200">
                <div class="khoitin">
                    <a class="topic">
                        <asp:Literal ID="ltrTitle" runat="server" /></a>
                    
                    <uc1:CommonTool runat="server" ID="CommonTool" />
                    <div class="thongtinchitiet">
                        <asp:Literal ID="ltrContent" runat="server" />
                        
                    </div>
                    <uc1:CommonCuoiChiTietTin runat="server" ID="CommonCuoiChiTietTin1" />
                    
                    <uc1:SubFilelibraryOrther runat="server" id="SubFilelibraryOrther" />
                </div>
            </div>
        </div>
    </div>
</div>
