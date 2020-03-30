using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Extension;

public partial class cms_display_LienHe_Controls_GuiThanhCong : System.Web.UI.UserControl
{
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();  
    protected string keythongbaothanhcong = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            keythongbaothanhcong=SettingsExtension.GetSettingKey(SettingsExtension.KeyThongBaoSauKhiGuiLienHe, lang);
        }
    }
}