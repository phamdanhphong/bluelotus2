using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.ServiceModul;
using TatThanhJsc.TSql;

public partial class cms_display_Service_Controls_Index : System.Web.UI.UserControl
{

    #region Các thông số cần chỉnh theo từng modul (Service, Service, Service...)
    private string app = CodeApplications.Service;
    protected string rewrite = RewriteExtension.Service;
    private string pic = FolderPic.Service;
    private string maxItemKey = SettingKey.SoServiceTrenTrangChu;
    #endregion

    #region Các thông số chung
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    protected string title = "";
    string igid = "";
    string p = "1";
    int rows = 6;
    string key = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null)
            igid = QueryStringExtension.GetQueryString("igid");
        #region title
        if (Request.QueryString["title"] != null)
        {
            title = Request.QueryString["title"];

            //Lấy igid từ session ra vì nó đã dược lưu khi xét tại Default.aspx
            if (igid.Length < 1 && Session["igid"] != null)
                igid = Session["igid"].ToString();
        }
        #endregion

        if (Request.QueryString["p"] != null)
            p = QueryStringExtension.GetQueryString("p");

        if (Request.QueryString["key"] != null)
            key = QueryStringExtension.GetQueryString("key");

        if (!IsPostBack)
        {
            ltrList.Text = GetCate();
        }
    }


    string GetCate()
    {
        string s = "";

        #region Condition, orderby, fields
        string condition = "";

        if (igid != "")
            condition = GroupsTSql.GetGroupsByIgid(igid);
        else
            condition = DataExtension.AndConditon(GroupsTSql.GetGroupsByIgparentid("0"),
                GroupsTSql.GetGroupsByVgapp(app));
        condition = DataExtension.AndConditon(
            condition,
            GroupsTSql.GetGroupsByVglang(lang),
            GroupsTSql.GetGroupsByIgenable("1"));

        string orderby = GroupsColumns.VGSEOMETAPARAMSColumn;

        try
        {
            rows = int.Parse(SettingsExtension.GetSettingKey(maxItemKey, lang));
        }
        catch { }

        string fields = DataExtension.GetListColumns(GroupsColumns.IgidColumn, GroupsColumns.VgnameColumn, GroupsColumns.VGSEOLINKSEARCHColumn);
        #endregion

        DataTable dt = Groups.GetGroups("", "*", condition, orderby);

        string link = "";
        string list = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            link =
                (UrlExtension.WebisteUrl + dt.Rows[i][GroupsColumns.VGSEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();

            s += @"
	<div class='item-seminor'>
			<div class='inner'>
				<div class='item-seminor__img fade-up'>
					<div class='img'>
						<div class='img__crop'>
							  " + ImagesExtension.GetImage(pic, dt.Rows[i][GroupsColumns.VgimageColumn].ToString(), dt.Rows[i][GroupsColumns.VgnameColumn].ToString(), "", true, false, "",false) + @"
						</div>
					</div>
				</div>
				<div class='item-seminor__content fade-up'>
					<h2 class='item-seminor__ttl'>
						<span>" + dt.Rows[i][GroupsColumns.VgnameColumn] + @"</span>
					</h2>
					<p class='txtBase'><span>" + dt.Rows[i][GroupsColumns.VgdescColumn] + @"</span></p>
					<div class='txt-center'>
						<a href='"+ link + @"' class='btn on'>Xem chi tiết</a>
					</div>
				</div>
			</div>
		</div>
";
        }


        return s;
    }

}