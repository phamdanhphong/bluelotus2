using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TatThanhJsc.Columns;
using TatThanhJsc.ContactModul;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;

public partial class cms_display_ContactUs_Controls_Index : System.Web.UI.UserControl
{
    protected string lat = "";
    protected string lng = "";
    protected string infoWindow = "";

    protected string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string app = CodeApplications.Contact;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GetMapInfo();
        }
    }


    #region Phần thông tin phòng ban
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


            ltrOfficeInfo.Text = @"
<h2 class='contact02__ttl fade-up'>" + dt.Rows[0][GroupsColumns.VgnameColumn].ToString() + @"</h2>
    <ul>
        <li class=' fade-up'>
            <span class='icon'>
                <img src='/img/contact/ico-01.png' alt=''>
            </span>
            <p><span>Địa chỉ</span><br>
                " + StringExtension.LayChuoi(content, "", 1) + @"
            </p>
        </li>
        <li class=' fade-up'>
            <span class='icon'><img src='/img/contact/ico-02.png' alt=''></span>
            <p><span>Email</span><br>
                <a href='mailto:" + StringExtension.LayChuoi(content, "", 4) + @"'>" + StringExtension.LayChuoi(content, "", 4) + @"</a>
            </p>
        </li>
        <li class=' fade-up'>
            <span class='icon'><img src='/img/contact/ico-03.png' alt=''></span>
            <p><span>Hotline</span><br>
                <a href='tel:" + StringExtension.LayChuoi(content, "", 8) + @"'>" + StringExtension.LayChuoi(content, "", 8) + @"</a>
            </p>
        </li>
     </ul>
";
        }
    }



    #endregion
}