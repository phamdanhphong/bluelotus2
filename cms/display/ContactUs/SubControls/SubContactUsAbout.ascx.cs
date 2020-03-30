using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.ContactModul;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;

public partial class cms_display_ContactUs_SubControls_SubContactUsAbout : System.Web.UI.UserControl
{
    protected string Lat = "";
    protected string Lng = "";
    protected string InfoWindow = "";

    string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string app = CodeApplications.Contact;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetMapInfo();
        }
    }

    /// <summary>
    /// Lấy thông tin bản đồ. Hiện Hoà đang cho lấy bản đồ của phòng ban đầu tiên
    /// </summary>
    void GetMapInfo()
    {
        string condition = DataExtension.AndConditon(
            GroupsTSql.GetGroupsByVgapp(app),
            GroupsTSql.GetGroupsByIgenable("1"),
            GroupsTSql.GetGroupsByVglang(lang),
            GroupsTSql.GetGroupsByIgparentid("0")
            );
        string order = GroupsColumns.IgorderColumn;

        DataTable dt = Groups.GetGroups("1", "*", condition, order);

        if (dt.Rows.Count > 0)
        {
            string content = dt.Rows[0][GroupsColumns.VgcontentColumn].ToString();
            Lng = StringExtension.LayChuoi(content, "", 5);
            Lat = StringExtension.LayChuoi(content, "", 6);
            InfoWindow = dt.Rows[0][GroupsColumns.VgName].ToString();

            ltrInfos.Text = @"
 <h3 class='footer__main__ttl'>"+ dt.Rows[0][GroupsColumns.VgName].ToString() + @"</h3>
<ul>
    <li class='map'>Địa chỉ: " + StringExtension.LayChuoi(content, "", 1) + @"</li>
    <li class='phone'>Hotline: <a href='tel:" + StringExtension.LayChuoi(content, "", 8) + @"'>" + StringExtension.LayChuoi(content, "", 8) + @"</a></li>
</ul>";
        }
    }

    private string XuLyHotline(string hotlines)
    {
        string s = "";
        foreach (string hotline in hotlines.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries))
        {
            s += "<a href='tel:" + hotline.Trim() + "'>" + hotline.Trim() + "</a> - ";
        }
        if (s.EndsWith(" - "))
            s = s.Remove(s.Length - " - ".Length);

        return s;
    }
}