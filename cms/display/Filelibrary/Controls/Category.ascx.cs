using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.FileLibrary2Modul;
using TatThanhJsc.TSql;

public partial class cms_display_Filelibrary_Controls_Category : System.Web.UI.UserControl
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

    #region Các thông số cần chỉnh theo từng modul (FileLibrary2, FileLibrary2, FileLibrary2...)
    private string app = TatThanhJsc.FileLibrary2Modul.CodeApplications.FileLibrary2;
    private string pic = TatThanhJsc.FileLibrary2Modul.FolderPic.FileLibrary2;
    private string maxItemKey = TatThanhJsc.FileLibrary2Modul.SettingKey.SoFileLibrary2TrenTrangDanhMuc;
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
            ItemsTSql.GetItemsByIienable("1"),
            ItemsTSql.GetItemsByViapp(app));

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
        if (dt.Rows.Count > 0)
        {
            string link = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link = (UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();

                if (i == 0)
                {
                    ltrList.Text += @"
        <div class='itemM fade-up'>
            <div class='itemM__content'>
                <h2 class='itemM__ttl'>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</h2>
                <p class='txtBase'>
                  " + dt.Rows[i][ItemsColumns.VicontentColumn] + @"
                </p>
            </div>
            <div class='itemM__img img'>
                <div class='img__crop'>
                    " + ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), dt.Rows[i][ItemsColumns.VititleColumn].ToString(), "", true, false, "", false) + @"
                </div>
            </div>
        </div>";
                }
                else if (i == 1)
                {
                    ltrList.Text += @"
  <h2 class='ttl-comp03 ttl-comp03--md fade-up active'>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</h2>
    <div class='box-tab'>
            <div class='img-left'>
                <img src='/img/seminor/img-left.jpg' alt=''>
            </div>
            " + GetOtherImages(dt.Rows[i][ItemsColumns.IidColumn].ToString()) + @"
            <div class='img-right'>
                <img src='/img/seminor/img-right.jpg' alt=''>
            </div>
    </div>
";
                }
                else
                {
                    ltrList.Text += @"
 <div class='picture-restaurant'>
    <h2 class='ttl-comp03 ttl-comp03--md fade-up active'>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</h2>
    <div class='list-picture'>
        " + GetOtherImages2(dt.Rows[i][ItemsColumns.IidColumn].ToString()) + @"
    </div>
</div>";
                }



            }
        }

    }
    #endregion

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
                <div class='list-picture__item fade-up'>
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