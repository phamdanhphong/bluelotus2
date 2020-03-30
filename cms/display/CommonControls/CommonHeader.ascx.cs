using System;
using TatThanhJsc.Extension;

public partial class cms_display_CommonControls_CommonHeader : System.Web.UI.UserControl
{
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {     

            GetHotline();
        }
    }
    private void GetHotline()
    {
        string s = "";
        string hotlines = SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine, lang);
        string nameWeb = SettingsExtension.GetSettingKey(SettingsExtension.KeyKeyWebsite, lang);
       
    }
  
}