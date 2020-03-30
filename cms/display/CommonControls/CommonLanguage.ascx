<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonLanguage.ascx.cs" Inherits="cms_display_CommonControls_CommonLanguage" %>

<div class="language">
     <asp:Literal runat="server" ID="ltrLang"></asp:Literal>
</div>


<script type="text/javascript">
    function SetLangDisplay(langId) {
        setCookie('<%=LanguageIdDisplay%>', langId, '3', '/', '', '');
        window.location = weburl;
    }
</script>

