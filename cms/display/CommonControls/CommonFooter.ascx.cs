using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;


public partial class cms_display_CommonControls_CommonFooter : System.Web.UI.UserControl
{
    protected string lat = "";
    protected string lng = "";
    protected string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string app = TatThanhJsc.ContactModul.CodeApplications.Contact;
    string appMenu = TatThanhJsc.MenuModul.CodeApplications.MenuBottom;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ltrFooterContent.Text =  SettingsExtension.GetSettingKey(SettingsExtension.KeyContentFooterWebsite , lang);
        }
    }
   
  
}