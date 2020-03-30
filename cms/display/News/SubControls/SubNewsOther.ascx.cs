using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;
public partial class cms_display_News_SubControls_SubNewsOther : System.Web.UI.UserControl

{
    string igid = "";
    string iid = "";

    string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    int rows = 6;

    private string maxItemKey = TatThanhJsc.NewsModul.SettingKey.SoNewsKhacTrenMotTrang;
    string app = TatThanhJsc.NewsModul.CodeApplications.News;
    private string pic = TatThanhJsc.NewsModul.FolderPic.News;

    public string MaxItemKey { set { maxItemKey = value; } }
    public string App { set { app = value; } }
    public string Pic { set { pic = value; } }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null)
            igid = StringExtension.RemoveSqlInjectionChars(Request.QueryString["igid"]);
        if (Request.QueryString["iid"] != null)
            iid = StringExtension.RemoveSqlInjectionChars(Request.QueryString["iid"]);
        if (Request.QueryString["title"] != null)
        {
            if (igid.Length < 1 && Session["igid"] != null)
                igid = Session["igid"].ToString();
            if (iid.Length < 1 && Session["iid"] != null)
                iid = Session["iid"].ToString();
        }

        if (!IsPostBack)
        {
            ltrList.Text = GetList();
            if (ltrList.Text == "")
                this.Visible = false;
        }
    }
    string GetList()
    {
        string s = "";

        string condition = GroupsTSql.GetGroupsByVgapp(app);
        if (igid != "")
            condition = GroupsItemsTSql.GetItemsInGroupCondition(igid, "");

        condition = DataExtension.AndConditon(condition,
            GroupsTSql.GetGroupsByVglang(lang),
            GroupsTSql.GetGroupsByIgenable("1"),
            ItemsTSql.GetItemsByIienable("1"),
            ItemsTSql.GetItemsByViapp(app));
        if (iid != "")
            condition += " and ITEMS.IID<> " + iid + " ";

        string fields = DataExtension.GetListColumns(ItemsColumns.VititleColumn, ItemsColumns.IitotalviewColumn,
            ItemsColumns.VISEOLINKSEARCHColumn, ItemsColumns.DicreatedateColumn, ItemsColumns.ViImage);

        string orderby = ItemsColumns.IiorderColumn + "," + ItemsColumns.DicreatedateColumn + " desc ";

        try
        {
            rows = int.Parse(SettingsExtension.GetSettingKey(maxItemKey, lang));
        }
        catch { }

        DataTable dt = new DataTable();
        dt = GroupsItems.GetAllData(rows.ToString(), "*", condition, orderby);
        if (dt.Rows.Count > 0)
        {
            string link = "";

            s += @"<h2 class='ttl-comp04 fade-up'><span><b>Tin khác</b></span></h2>
            <div class='other-news'>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link =(UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();

              

                s += @"
<div class='other-news__item fade-up'>
        <div class='other-news__img img'>
           <a class='img__crop' href='" + link + @"' title='" + dt.Rows[i][ItemsColumns.VititleColumn].ToString().Replace("'", "") + @"'>
                " + ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), dt.Rows[i][ItemsColumns.VititleColumn].ToString(), "", true, false, "") + @"
            </a>
        </div>
        <h3 class='other-news__ttl'><a href='" + link + @"' title='" + dt.Rows[i][ItemsColumns.VititleColumn].ToString().Replace("'", "") + @"'>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</a></h3>
        <div class='thongke'>
            <div class='thongke__time'><i class='fa fa-clock-o'></i>" + ((DateTime)dt.Rows[i][ItemsColumns.DiCreateDate]).ToString(LanguageItemExtension.GetnLanguageItemTitleByName("dd/MM/yyyy")) + @"</div>
            <div class='thongke__view'><i class='fa fa-eye'></i>" + NumberExtension.FormatNumber(((int)dt.Rows[i][ItemsColumns.IitotalviewColumn] + 1).ToString()) + @" lượt xem</div>
        </div>
        <p class='txtBase'>" + dt.Rows[i][ItemsColumns.VidescColumn] + @"</p>
    </div>";

               
            }

            s += @"</div>";


        }
        return s;
    }
}
