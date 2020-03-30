using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.ProductModul;
using TatThanhJsc.TSql;

public partial class cms_display_Product_Controls_Index : System.Web.UI.UserControl
{

    #region Các thông số cần chỉnh theo từng modul (Product, Product, Product...)
    private string app = CodeApplications.Product;
    protected string rewrite = RewriteExtension.Product;
    private string pic = FolderPic.Product;
    private string maxItemKey = SettingKey.SoProductTrenTrangChu;
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


            if (dt.Rows[i][GroupsColumns.VGSEOMETAPARAMSColumn].ToString() == "1")
            {
                s += @"
 <div class='itemM itemM--style02 fade-up'>
            <div class='itemM__content'>
                 <h2 class='itemM__ttl'>" + dt.Rows[i][GroupsColumns.VgnameColumn] + @"</h2>
                <p class='txtBase'>
                     " + StringExtension.LayChuoi(dt.Rows[i][GroupsColumns.VgcontentColumn].ToString(), "", 1) + @"
                </p>
            </div>
            <div class='itemM__img img'>
                <div class='img__crop'>
                   " + ImagesExtension.GetImage(pic, dt.Rows[i][GroupsColumns.VgimageColumn].ToString(), dt.Rows[i][GroupsColumns.VgnameColumn].ToString(), "", true, false, "") + @"
                </div>
            </div>
        </div>
";
            }
            else if (dt.Rows[i][GroupsColumns.VGSEOMETAPARAMSColumn].ToString() == "2")
            {
                list = GetList(dt.Rows[i][GroupsColumns.IgidColumn].ToString(), rows.ToString());
                if (list.Length > 0)
                    s += @"
<h2 class=' ttl-comp03 ttl-comp03--md fade-up'>" + dt.Rows[i][GroupsColumns.VgnameColumn] + @"</h2>
  <div class='list-service'>
         " + list + @"  
  </div>
";
            }
            else if (dt.Rows[i][GroupsColumns.VGSEOMETAPARAMSColumn].ToString() == "3")
            {
                list = GetList2(dt.Rows[i][GroupsColumns.IgidColumn].ToString(), rows.ToString());
                if (list.Length > 0)
                    s += list;
            }

            else if (dt.Rows[i][GroupsColumns.VGSEOMETAPARAMSColumn].ToString() == "4")
            {
                list = GetList3(dt.Rows[i][GroupsColumns.IgidColumn].ToString(), rows.ToString());
                if (list.Length > 0)
                    s += @"
    <div class='price-album'>
      <h2 class='ttl-comp03 ttl-comp03--md fade-up'>album ảnh cưới tại blue lotus</h2>
    <div class='list-price'>
             " + list + @"  
      </div>
  </div>
";
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

        DataTable dt = GroupsItems.GetAllData(top, fields, condition, orderby);

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
            <div class='list-service__item fade-up'>
                <div class='img'>
                    <a href='#' title='" + dt.Rows[i][ItemsColumns.VititleColumn] + @"' class='img__crop'>
                   " + ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), dt.Rows[i][ItemsColumns.VititleColumn].ToString(), "", true, false, "") + @"
                </a>
                </div>
                 <h3 class='list-service__ttl'><a href='#' title='" + dt.Rows[i][ItemsColumns.VititleColumn] + @"'>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</a></h3>
                <p class='txtBase txt-center'>
                      " + dt.Rows[i][ItemsColumns.ViDesc] + @"
                </p>
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

        DataTable dt = GroupsItems.GetAllData("1","*", condition, orderby);

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
 <h2 class='ttl-comp03 ttl-comp03--md fade-up'>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</h2>
        <div class='list-album'>
            "+ GetOtherImages(dt.Rows[i][ItemsColumns.IidColumn].ToString()) + @"
            </div>";
            }
        }
        #endregion

        return s;
    }
    private string GetOtherImages(string iid)
    {
        string s = "";
        string condition = DataExtension.AndConditon
        (
            SubitemsTSql.GetSubitemsByIid(iid),
            SubitemsTSql.GetSubitemsByVskey(CodeApplications.ProductImagesOther),
            SubitemsTSql.GetByEnable("1")
        );

        string orderby = SubitemsColumns.VsatuthorColumn;
        DataTable dt = new DataTable();
        dt = Subitems.GetSubItems("", "*", condition, orderby);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                if (i==6)
                {
                    s += @"
  <div class='list-album__item list-album__item--w60'>
                " + ImagesExtension.GetImage(pic, dt.Rows[i][SubitemsColumns.VsimageColumn].ToString(), dt.Rows[i][SubitemsColumns.VstitleColumn].ToString(), "", true, false, "", false) + @"
            </div>";
                }
                else
                {
                    s += @"
  <div class='list-album__item'>
                " + ImagesExtension.GetImage(pic, dt.Rows[i][SubitemsColumns.VsimageColumn].ToString(), dt.Rows[i][SubitemsColumns.VstitleColumn].ToString(), "", true, false, "", false) + @"
            </div>";
                }
                

              
            }
        }

        return s;
    }


    string GetList3(string igid, string top)
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

        DataTable dt = GroupsItems.GetAllData("3", "*", condition, orderby);

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
                <div class='list-price__item fade-up'>
                    <h3 class='list-price__ttl'>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</h3>
                    <p class='list-price__price'>  " + dt.Rows[i][ItemsColumns.ViDesc] + @"</p>
                     " +  StringExtension.LayChuoi(dt.Rows[i][ItemsColumns.VicontentColumn].ToString(), "", 1) + @"
                </div>";
            }
        }
        #endregion

        return s;
    }
}