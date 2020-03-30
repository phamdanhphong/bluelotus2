using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;
public partial class cms_display_News_Controls_Category : System.Web.UI.UserControl
{
    #region Các thông số chung
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string title = "";
    private string go = "";

    string igid = "";
    string p = "1";
    int rows = 10;
    string key = "";
    #endregion

    #region Các thông số cần chỉnh theo từng modul (News, News, News...)
    private string appGroup = TatThanhJsc.NewsModul.CodeApplications.NewsGroupItem;
    private string app = TatThanhJsc.NewsModul.CodeApplications.News;
    private string pic = TatThanhJsc.NewsModul.FolderPic.News;
    private string maxItemKey = TatThanhJsc.NewsModul.SettingKey.SoNewsTrenTrangDanhMuc;
    private string noResultText = LanguageItemExtension.GetnLanguageItemTitleByName("Nội dung các bài viết thuộc chuyên mục này sẽ được chúng tôi cập nhật sớm. Cảm ơn quý khách đã quan tâm!");
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null)
            igid = QueryStringExtension.GetQueryString("igid");

        if (Request.QueryString["go"] != null)
            go = QueryStringExtension.GetQueryString("go");

        #region title
        if (Request.QueryString["title"] != null)
        {
            title = Request.QueryString["title"];
            //Lấy igid từ session ra vì nó đã dược lưu khi xét tại Default.aspx
            if (igid.Length < 1 && Session["igid"] != null)
                igid = Session["igid"].ToString();

            if (go.Length < 1 && Session["go"] != null)
                go = Session["go"].ToString();
        }
        #endregion

        if (Request.QueryString["p"] != null)
            p = QueryStringExtension.GetQueryString("p");

        if (Request.QueryString["key"] != null)
            key = QueryStringExtension.GetQueryString("key");

        if (!IsPostBack)
        {
            GetList();
        }
    }

    private void GetCateInfo()
    {
        if (Session["dataByTitle_Cate"] != null)
        {
            DataTable dt = (DataTable)Session["dataByTitle_Cate"];
            if (dt.Rows.Count > 0)
            {
                //ltrCateName.Text = @"
                //<div class='ten_xemthem'>
                //    <h2><a class='tendanhmuc'>" + dt.Rows[0][GroupsColumns.VgName].ToString() + @"</a></h2>
                //</div>";                                    
            }
        }
    }

    #region Get list item
    void GetList()
    {
        #region Condition, orderby
        string condition = "";

        if (igid != "")
            condition = GroupsItemsTSql.GetItemsInGroupCondition(igid, "");
        else
            condition = GroupsTSql.GetGroupsByVgapp(app);
        condition = DataExtension.AndConditon(
            condition,
            GroupsTSql.GetGroupsByVglang(lang),
            GroupsTSql.GetGroupsByIgenable("1"),
            ItemsTSql.GetItemsByIienable("1"));

        if (key.Length > 0)
            condition = DataExtension.AndConditon(condition, SearchTSql.GetSearchMathedCondition(key, ItemsColumns.VititleColumn, ItemsColumns.VikeyColumn, ItemsColumns.FipriceColumn, ItemsColumns.FisalepriceColumn));

        string orderby = ItemsColumns.IiorderColumn + "," + ItemsColumns.DicreatedateColumn + " desc ";

        try
        {
            rows = int.Parse(SettingsExtension.GetSettingKey(maxItemKey, lang));
        }
        catch { }
        #endregion

        DataTable dt = GroupsItems.GetAllData("", "*", condition, orderby);
        #region Lấy ra danh sách bài viết

        string s = "";
        string s1 = "";
        string s2 = "";
        if (dt.Rows.Count > 0)
        {
            string link = "";
            string titleIT = "";
            string img = "";
            string detail = "";
            string dateIT = "";
            string countView = "";

            string ds_tintucnoibat_right = "";
          
            s += @"<div class='head_news'>
            <div class='list-news01'>";

            

            s2 += @"<div class='main-left'>
                <div class='list-news02'>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link = (UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();
                img = ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), dt.Rows[i][ItemsColumns.VititleColumn].ToString(), "", true, false, "");
                titleIT = dt.Rows[i][ItemsColumns.VititleColumn].ToString().Replace("'", "");
                detail = dt.Rows[i][ItemsColumns.ViDesc].ToString();
                dateIT = ((DateTime)dt.Rows[i][ItemsColumns.DiCreateDate]).ToString(LanguageItemExtension.GetnLanguageItemTitleByName("dd/MM/yyyy"));
                countView = NumberExtension.FormatNumber(((int)dt.Rows[i][ItemsColumns.IitotalviewColumn]).ToString());

                if (i<6)
                {
                    s += @"
                    <div class='list-news01__item fade-up'>
                        <div class='img'>
                            <a class='img__crop' href='" + link + "' title='" + titleIT + @"'>" + img + @"</a>
                        </div>
                        <h2 class='list-news01__ttl'><a  href='" + link + "' title='" + titleIT + @"'>" + titleIT + @"</a></h2>
                    </div> ";
                }

                if (i>5)
                {
                    s2 += @"
                    <div class='list-news02__item fade-up "+(i>8?"hide":"")+@"'>
                        <div class='list-news02__img img'>
                             <a class='img__crop' href='" + link + "' title='" + titleIT + @"'>" + img + @"</a>
                        </div>
                        <div class='list-news02__content'>
                            <h3 class='list-news02__ttl'><a href='" + link + "' title='" + titleIT + @"'>" + titleIT + @" </a></h3>
                            <p class='txtBase'>"+ detail + @"</p>
                        </div>
                    </div>";
                }

            }

           
            s += @"</div></div>";
           
            s2 += @"</div> <a href='javascript:void(0)' onclick='showmoreNews(this)' class='btn-view fade-up'>Xem thêm tin</a></div>";
        }
        #endregion
        s1 += @" <h2 class='ttl-comp04 fade-up'><span><b>Tin khác</b></span></h2>
        <div class='main-news'>";
        s1 += s2;
        s1 += GetGroups2("");
        s1 += @"</div></div>";

        ltrList.Text = s + s1;
    }
    #endregion



    private string GetGroups2(string position)
    {
        string s = "";
        string fields = "", condition = "", orderby = "";
        condition = DataExtension.AndConditon(
            GroupsTSql.GetByParams("2"),
            GroupsTSql.GetByEnable("1"),
            GroupsTSql.GetByApp(appGroup),
            GroupsTSql.GetByLang(lang)
        );
        fields = DataExtension.GetListColumns(
            GroupsColumns.IgidColumn,
            GroupsColumns.VgnameColumn,
            GroupsColumns.VgimageColumn,
            GroupsColumns.VGSEOLINKSEARCHColumn,
            GroupsColumns.IgtotalitemsColumn
        );
        orderby = GroupsColumns.IgorderColumn;
        DataTable dt = Groups.GetGroups("", fields, condition, orderby);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string list = "";
                string cateName = "";
                list = GetList2(dt.Rows[i][GroupsColumns.IgidColumn].ToString(), "5");
                // tieu de
                string link = "", name = "";
                link = (UrlExtension.WebisteUrl + dt.Rows[i][GroupsColumns.VGSEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();
                name = dt.Rows[i][GroupsColumns.VgnameColumn].ToString();
                if (list.Length > 0)
                {
                    s = @"
                <div class='main-right'>
                    <span class='btn-comp01 fade-up'><b>" + name + @"</b></span>
                    <div class='list-news03'>
                        " + list + @"
                    </div>
                </div>";
                }

            }
        }
        return s;
    }

    private string GetList2(string id, string top)
    {
        var s = string.Empty;
        string fields = "*", condition = "", orderby = "";
        int rows = 6;
        try
        {
            rows = int.Parse(top);
        }
        catch
        {

        }
        condition = DataExtension.AndConditon(
            ItemsTSql.GetByEnable("1"),
            ItemsTSql.GetByApp(app)
            );
        condition = GroupsItemsTSql.GetItemsInGroupCondition(id, condition);
        orderby = GroupsItemsColumns.IorderColumn;
        DataTable dt = GroupsItems.GetAllData("", fields, condition, orderby);
        if (dt.Rows.Count > 0)
        {
            s = "";
            string link = "", titleItem = "", desc = "", img = "", dateIT = "", countView = "", listOrther = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link = (UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();
                titleItem = dt.Rows[i][ItemsColumns.VititleColumn].ToString();
                img = ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), titleItem, "", true, false, "", false);
                desc = dt.Rows[i][ItemsColumns.VidescColumn].ToString();
                dateIT = ((DateTime)dt.Rows[i][ItemsColumns.DicreatedateColumn]).ToString("dd/MM/yyyy");
                countView = NumberExtension.FormatNumber((int.Parse(dt.Rows[i][ItemsColumns.IitotalviewColumn].ToString())).ToString());
                s += @"
                    <div class='list-news03__item fade-up'>
                        <div class='list-news03__img img'>
                            <a class='img__crop' href='" + link + "' title='" + titleItem + @"'> " + img + @"</a>
                        </div>
                        <div class='list-news03__content'>
                            <h3 class='list-news03__ttl'><a href='" + link + "' title='" + titleItem + @"'>" + titleItem + @"</a></h3>
                        </div>
                    </div>";

            }
           
        }
        return s;
    }
}

