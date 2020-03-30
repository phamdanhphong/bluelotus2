

using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;

public partial class cms_display_CommonControls_CommonMenuFooter : System.Web.UI.UserControl
{
    string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    string app = TatThanhJsc.MenuModul.CodeApplications.MenuBottom;
    protected string cRewrite = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["rewrite"] != null)
            cRewrite = Session["rewrite"].ToString();

        if (!IsPostBack)
            LoadMenu();
    }

    protected void LoadMenu()
    {
        string top = "";
        string field = "*";
        string condition = DataExtension.AndConditon(
            GroupsTSql.GetGroupsByVgapp(app),
            GroupsTSql.GetGroupsByIgparentid("0"),
            GroupsTSql.GetGroupsByIgenable("1"),
            GroupsTSql.GetGroupsByVglang(lang));

        string orderby = GroupsColumns.IgorderColumn;
        DataTable dt = Groups.GetGroups(top, field, condition, orderby);
        string s = "";

        if (dt.Rows.Count > 0)
        {
            string link = "";
            string subMenus = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link = RewriteExtension.GetLinkMenu(dt.Rows[i][GroupsColumns.VgdescColumn].ToString());

                ltrList.Text += @"
<li class='litop '>
    <a href='" + link + "' " +
                                    MenuExtension.GetTarget(dt.Rows[i][GroupsColumns.VgparamsColumn].ToString()) + @" title='" +
                                    dt.Rows[i][GroupsColumns.VgnameColumn] + @"'>" +
                                    dt.Rows[i][GroupsColumns.VgnameColumn] + @"
    </a>
</li>";



            }
        }

    }
   
}