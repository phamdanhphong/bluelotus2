using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;

using TatThanhJsc.CustomerModul;
using TatThanhJsc.TSql;

public partial class cms_display_Customer_Controls_Index : System.Web.UI.UserControl
{

    #region Các thông số cần chỉnh theo từng modul (Customer, Customer, Customer...)
    private string app = CodeApplications.Customer;
    protected string rewrite = RewriteExtension.Customer;
    private string pic = FolderPic.Customer;
    private string maxItemKey = SettingKey.SoCustomerTrenTrangChu;
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

        string orderby = GroupsColumns.VGSEOMETAPARAMSColumn + " DESC";

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
            link =(UrlExtension.WebisteUrl + dt.Rows[i][GroupsColumns.VGSEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();


            if (dt.Rows[i][GroupsColumns.VGSEOMETAPARAMSColumn].ToString() == "3")
            {
                list = GetList(dt.Rows[i][GroupsColumns.IgidColumn].ToString(), rows.ToString());
                s += @"
<section class='sec-drink'>
    <div class='inner'>
        <h2 class='ttl-comp03 ttl-comp03--md fade-up'>" + dt.Rows[i][GroupsColumns.VgnameColumn] + @"</h2>
        <div class='list-drink'>
            " + list + @"  
        </div>
    </div>
</section>
";
            }
            else if (dt.Rows[i][GroupsColumns.VGSEOMETAPARAMSColumn].ToString() == "0")
            {
                list = GetList2(dt.Rows[i][GroupsColumns.IgidColumn].ToString(), rows.ToString());
                if (list.Length > 0)
                    s += list;
            }

        }


        return s;
    }
    string GetList(string igid, string top)
    {
        string s = "";

        #region Condition, orderby, fields
        string condition = "";

        if (igid != "")
            condition = GroupsItemsTSql.GetItemsInGroupCondition(igid, "");
        else
            condition = GroupsTSql.GetGroupsByVgapp(app);
        condition = DataExtension.AndConditon(
            condition,
            GroupsTSql.GetGroupsByVglang(lang),
            GroupsTSql.GetGroupsByIgenable("1"),
            ItemsTSql.GetItemsByIienable("1"),
            ItemsTSql.GetItemsByViapp(app));

        if (key.Length > 0)
            condition = DataExtension.AndConditon(condition, SearchTSql.GetSearchMathedCondition(key, ItemsColumns.VititleColumn, ItemsColumns.VikeyColumn, ItemsColumns.FipriceColumn, ItemsColumns.FisalepriceColumn));

        string orderby = ItemsColumns.IiorderColumn + "," + ItemsColumns.DicreatedateColumn + " desc ";

        string fields = DataExtension.GetListColumns(ItemsColumns.VititleColumn,
            ItemsColumns.ViimageColumn, ItemsColumns.VISEOLINKSEARCHColumn, ItemsColumns.VidescColumn, ItemsColumns.DiCreateDate, ItemsColumns.IiTotalView);
        #endregion

        DataTable dt = GroupsItems.GetAllData("", fields, condition, orderby);

        #region Lấy ra danh sách bài viết
        if (dt.Rows.Count > 0)
        {
            string link = (UrlExtension.WebisteUrl + dt.Rows[0][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link =
                    (UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions)
                        .ToLower();
                s += @"
            <div class='list-drink__item fade-up'>
                <div class='img'>
                   <a href='#' title='" + dt.Rows[i][ItemsColumns.VititleColumn] + @"' class='img__crop'>
                   " + ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), dt.Rows[i][ItemsColumns.VititleColumn].ToString(), "", true, false, "") + @"
                </a>
                </div>
                <h2 class='list-drink__ttl'><a href='#' title='" + dt.Rows[i][ItemsColumns.VititleColumn] + @"'><span>"+(i+1).ToString("D2")+@"</span>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</a></h2>
                <p class='txtBase'>  " + dt.Rows[i][ItemsColumns.ViDesc] + @"</p>
            </div>";
            }
        }
        #endregion

        return s;
    }
    string GetList2(string igid, string top)
    {
        string s = "";

        #region Condition, orderby, fields
        string condition = "";

        if (igid != "")
            condition = GroupsItemsTSql.GetItemsInGroupCondition(igid, "");
        else
            condition = GroupsTSql.GetGroupsByVgapp(app);
        condition = DataExtension.AndConditon(
            condition,
            GroupsTSql.GetGroupsByVglang(lang),
            GroupsTSql.GetGroupsByIgenable("1"),
            ItemsTSql.GetItemsByIienable("1"),
            ItemsTSql.GetItemsByViapp(app));

        if (key.Length > 0)
            condition = DataExtension.AndConditon(condition, SearchTSql.GetSearchMathedCondition(key, ItemsColumns.VititleColumn, ItemsColumns.VikeyColumn, ItemsColumns.FipriceColumn, ItemsColumns.FisalepriceColumn));

        string orderby = ItemsColumns.IiorderColumn + "," + ItemsColumns.DicreatedateColumn + " desc ";

        string fields = DataExtension.GetListColumns(ItemsColumns.VititleColumn,
            ItemsColumns.ViimageColumn, ItemsColumns.VISEOLINKSEARCHColumn, ItemsColumns.VidescColumn, ItemsColumns.DiCreateDate, ItemsColumns.IiTotalView);
        #endregion

        DataTable dt = GroupsItems.GetAllData("2","*", condition, orderby);

        #region Lấy ra danh sách bài viết
        if (dt.Rows.Count > 0)
        {
            string link = (UrlExtension.WebisteUrl + dt.Rows[0][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link = (UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();

                if (i==0)
                {
                    s += @"
<section class='sec-content'>
    <div class='inner'>
        <h2 class='ttl-comp03 ttl-comp03--md fade-up'>" + dt.Rows[i][ItemsColumns.VititleColumn].ToString() + @"</h2>
        <div class='box-tab'>
        " + GetOtherImages(dt.Rows[i][ItemsColumns.IidColumn].ToString()) + @"
        </div>
    </div>
</section>
";
                }
                else
                {
                    s += @"
<section class='sec-picdrink fade-up'>
    <div class='inner'>
        <h2 class='ttl-comp03 ttl-comp03--md fade-up'>" + dt.Rows[i][ItemsColumns.VititleColumn].ToString() + @"</h2>
    </div>
    <div class='list-picdrink'>
        " + GetOtherImages2(dt.Rows[i][ItemsColumns.IidColumn].ToString()) + @"
    </div>
</section>
";
                }

               
            }
        }
        #endregion

        return s;
    }
    private string GetOtherImages(string iid)
    {
        string s = "";
        string s1 = "";
        string pic = TatThanhJsc.ProductModul.FolderPic.Product;
        string condition = DataExtension.AndConditon
        (
            SubitemsTSql.GetSubitemsByIid(iid),
            SubitemsTSql.GetSubitemsByVskey(TatThanhJsc.ProductModul.CodeApplications.ProductImagesOther),
            SubitemsTSql.GetByEnable("1")
        );

        string orderby = SubitemsColumns.VsatuthorColumn;
        DataTable dt = new DataTable();
        dt = Subitems.GetSubItems("", "*", condition, orderby);
        if (dt.Rows.Count > 0)
        {
            s += "<ul class='nav-tabs fade-up'>";
            s1 += " <div class='tab-content fade-up'>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s += @"<li class='tab-link " + (i == 0 ? "on" : "") + @"' data-tab='tab-" + (i + 1) + @"'><span>" + dt.Rows[i][SubitemsColumns.VstitleColumn].ToString() + @"</span> </li>";

                s1 += @"
                <div class='tab-item " + (i == 0 ? "on" : "") + @"' id='tab-" + (i + 1) + @"'>
                    <div class='noidung'>
                       " + ImagesExtension.GetImage(pic, dt.Rows[i][SubitemsColumns.VsimageColumn].ToString(), dt.Rows[i][SubitemsColumns.VstitleColumn].ToString(), "", true, false, "", false) + @"
                    </div>
                </div>";

            }

            s += @"</ul>";
            s1 += @"</div>";
        }

        return s + s1;
    }
    private string GetOtherImages2(string iid)
    {
        string s = "";
        string s1 = "";
        string condition = DataExtension.AndConditon
        (
            SubitemsTSql.GetSubitemsByIid(iid),
            SubitemsTSql.GetSubitemsByVskey(TatThanhJsc.ProductModul.CodeApplications.ProductImagesOther),
            SubitemsTSql.GetByEnable("1")
        );

        string pic = TatThanhJsc.ProductModul.FolderPic.Product;

        string orderby = SubitemsColumns.VsatuthorColumn;
        DataTable dt = new DataTable();
        dt = Subitems.GetSubItems("", "*", condition, orderby);
        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                s1 += @"


<div class='list-picdrink__item'>
            <div class='img'>
                <a href='" + UrlExtension.WebisteUrl + pic + "/" + dt.Rows[i][SubitemsColumns.VsimageColumn] + @"' data-fancybox='gallery' class='img__crop'>
                               " + ImagesExtension.GetImage(pic, dt.Rows[i][SubitemsColumns.VsimageColumn].ToString(), dt.Rows[i][SubitemsColumns.VstitleColumn].ToString(), "", true, false, "", false) + @"
                        </a>
            </div>
        </div>";

            }


        }

        return s1;
    }



}