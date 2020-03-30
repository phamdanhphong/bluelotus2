using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.LanguageModul;

public partial class cms_display_CommonControls_CommonLanguage : System.Web.UI.UserControl
{
    private string top = "";
    private string fields = "";
    private string condition = "";
    private string order = "";

    protected string LanguageIdDisplay = "LanguageIdDisplay";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ltrLang.Text = GetListLanguageNationals();
        }
    }
    string GetListLanguageNationals()
    {
        string s = "";
        top = "";
        fields = "*";
        condition = TatThanhJsc.TSql.LanguageNationalTSql.GetByiLanguageNationalEnable("1");
        order = LanguageNationalColumns.nLanguageNationalDesc;
        DataTable dt = LanguageNational.GetLanguageNational(top, fields, condition, order);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s += @"
    <a href='javascript:;' onclick='SetLangDisplay(" + dt.Rows[i][LanguageNationalColumns.iLanguageNationalId] + ")'>" + ImagesExtension.GetImage(FolderPic.Language, dt.Rows[i][LanguageNationalColumns.nLanguageNationalFlag].ToString(), dt.Rows[i][LanguageNationalColumns.nLanguageNationalName].ToString(), "", false, false, "") + @"</a>
";
            }
        }
        return s;
    }
}
