<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonFooter.ascx.cs" Inherits="cms_display_CommonControls_CommonFooter" %>
<%@ Register Src="~/cms/display/CommonControls/CommonMenuFooter.ascx" TagPrefix="uc1" TagName="CommonMenuFooter" %>
<%@ Register Src="~/cms/display/Adv/AdvLogoFooter.ascx" TagPrefix="uc1" TagName="AdvLogoFooter" %>
<%@ Register Src="~/cms/display/ContactUs/SubControls/SubContactUsAbout.ascx" TagPrefix="uc1" TagName="SubContactUsAbout" %>




<footer>
    <div class="footer__link">
        <div class="inner">
            <uc1:CommonMenuFooter runat="server" ID="CommonMenuFooter" />
            <ul class="footer__social">
                <li><a href="<%= LanguageItemExtension.GetnLanguageItemTitleByName("Link_fb") %>"><i class="fa fa-facebook" aria-hidden="true"></i></a></li>
                <li><a href="<%= LanguageItemExtension.GetnLanguageItemTitleByName("Link_tw") %>"><i class="fa fa-twitter" aria-hidden="true"></i></a></li>
                <li><a href="<%= LanguageItemExtension.GetnLanguageItemTitleByName("Link_ins") %>"><i class="fa fa-instagram" aria-hidden="true"></i></a></li>
                <li><a href="<%= LanguageItemExtension.GetnLanguageItemTitleByName("Link_yt") %>"><i class="fa fa-youtube-play" aria-hidden="true"></i></a></li>
            </ul>
        </div>
    </div>
    <div class="footer__main">
        <div class="inner">
            <div class="footer__main__info">
               
                <div class="footer__main__logo">
                    <uc1:AdvLogoFooter runat="server" ID="AdvLogoFooter" />
                </div>
                <uc1:SubContactUsAbout runat="server" id="SubContactUsAbout" />
            </div>
            <div class="footer__main__copyright">
                <asp:Literal ID="ltrFooterContent" runat="server"></asp:Literal>
            </div>
        </div>

    </div>
</footer>
