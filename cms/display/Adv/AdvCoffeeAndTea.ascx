<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvCoffeeAndTea.ascx.cs" Inherits="cms_display_Adv_AdvCoffeeAndTea" %>


<section class='sec-drink'>
    <div class='inner'>
        <h2 class='ttl-comp01 fade-up'><%= LanguageItemExtension.GetnLanguageItemTitleByName("Coffee and Tea") %><small><%= LanguageItemExtension.GetnLanguageItemTitleByName("Chuyên nghiệp - Uy tín - Chất lượng") %></small></h2>

        <div class='box-drink'>
            <asp:Literal ID="ltrAdv" runat="server"></asp:Literal>
        </div>
    </div>
</section>
