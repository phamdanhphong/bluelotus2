using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.PhotoAlbumModul;
using TatThanhJsc.TSql;


public partial class cms_display_PhotoAlbum_subControls_subPhotoHomepage : System.Web.UI.UserControl

{
    private string app = CodeApplications.PhotoAlbum;
    private string appGroup = CodeApplications.PhotoAlbumGroupItem;
    private string pic = FolderPic.PhotoAlbum;
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string rewrite = RewriteExtension.PhotoAlbum;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ltrGroups.Text = GetGroups("0");
            if (ltrGroups.Text == "")
                this.Visible = false;
        }
    }

    /// <summary>
    /// Lấy danh sách các nhóm
    /// </summary>
    /// <returns></returns>
    private string GetGroups(string position)
    {
        string s = "";

        string condition = DataExtension.AndConditon(
            GroupsTSql.GetGroupsByIgenable("1"),
            GroupsTSql.GetGroupsByVgapp(appGroup),
            GroupsTSql.GetGroupsByVglang(lang),
            GroupsTSql.GetGroupsByVgparams(position)
            );

        string fields = DataExtension.GetListColumns(GroupsColumns.IgidColumn, GroupsColumns.VgnameColumn,
            GroupsColumns.VGSEOLINKSEARCHColumn, GroupsColumns.IgtotalitemsColumn);

        DataTable dt = Groups.GetGroups("", fields, condition, GroupsColumns.IgorderColumn);
        string list = "";
        string link = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            list = GetList(dt.Rows[i][GroupsColumns.IgidColumn].ToString(),
                dt.Rows[i][GroupsColumns.IgtotalitemsColumn].ToString());
            if (list.Length > 0)
            {
                link = UrlExtension.WebisteUrl + dt.Rows[i][GroupsColumns.VGSEOLINKSEARCHColumn].ToString().ToLower() +
                       RewriteExtension.Extensions;
            }
            else//Nếu không có tin --> lấy tự động tin mới
            {
                link = UrlExtension.WebisteUrl + rewrite + RewriteExtension.Extensions;

                list = GetLastest(dt.Rows[i][GroupsColumns.IgtotalitemsColumn].ToString());
            }


            s += @"
  <h2 class='ttl-comp03 fade-up'>" + dt.Rows[i][GroupsColumns.VgnameColumn] + @"</h2>
    <div class='list-carpark'>
        " + list + @"
    </div>
";
        }

        return s;
    }

    /// <summary>
    /// Lấy danh sách tin mới nhất
    /// </summary>
    /// <param name="maxRow"></param>
    /// <returns></returns>

    private string GetLastest(string maxRow)
    {
        string condition = DataExtension.AndConditon(
            ItemsTSql.GetItemsByIienable("1"),
            ItemsTSql.GetItemsByViapp(app),
            ItemsTSql.GetItemsByVilang(lang),
            GroupsTSql.GetGroupsByVgapp(app),
            GroupsTSql.GetGroupsByIgenable("1"),
            GroupsTSql.GetGroupsByVglang(lang)
            );

        string orderby = ItemsColumns.IiorderColumn + "," + ItemsColumns.DicreatedateColumn + " desc ";

        DataTable dt = GroupsItems.GetAllData(maxRow, "*", condition, orderby);
        return BindItemsToHTML(dt);
    }

    /// <summary>
    /// Lấy danh sách tin trong một nhóm
    /// </summary>
    /// <param name="igid"></param>
    /// <param name="maxRow"></param>
    /// <returns></returns>
    private string GetList(string igid, string maxRow)
    {
        string condition = DataExtension.AndConditon(
            ItemsTSql.GetItemsByIienable("1"),
            ItemsTSql.GetItemsByViapp(app),
            ItemsTSql.GetItemsByVilang(lang),
            GroupsItemsTSql.GetItemsInGroupCondition(igid, "")
            );

        string orderby = ItemsColumns.IiorderColumn + "," + ItemsColumns.DicreatedateColumn + " desc ";

        DataTable dt = GroupsItems.GetAllData(maxRow, "*", condition, orderby);
        return BindItemsToHTML(dt);
    }

    /// <summary>
    /// Hiện thị danh sách tin ra html
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    private string BindItemsToHTML(DataTable dt)
    {
        string s = "";
        string link = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            link = (UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();
            s += GetImagesInAlbum(dt.Rows[i][ItemsColumns.Iid].ToString());
        }
        return s;
    }

    private object GetImagesInAlbum(string albumId)
    {
        string s = "";

        string condition = DataExtension.AndConditon(
                SubitemsTSql.GetSubitemsByIid(albumId),
                SubitemsTSql.GetSubitemsByIsenable("1"),
                SubitemsTSql.GetSubitemsByVskey(CodeApplications.PhotoAlbumImagesOther));
        DataTable dt = new DataTable();
        string fields = DataExtension.GetListColumns(SubitemsColumns.VsimageColumn, SubitemsColumns.VstitleColumn);
        string orderby = SubitemsColumns.VsemailColumn + " asc," + SubitemsColumns.VsatuthorColumn + " asc";

        dt = Subitems.GetSubItems("6", "*", condition, orderby);

        if (dt.Rows.Count>0)
        {
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s += @"
   <div class='list-carpark__item fade-up'>
        <div class='img'>
            <a href='' class='img__crop'>
               " + ImagesExtension.GetImage(pic, dt.Rows[i][SubitemsColumns.VsimageColumn].ToString(),
                         dt.Rows[i][SubitemsColumns.VstitleColumn].ToString(), "", true, false, "",false) + @"
            </a>
        </div>
        <div class='list-carpark__content'>
            <h3 class='list-carpark__ttl'>
                <a href=''>"+ dt.Rows[i][SubitemsColumns.VstitleColumn].ToString() + @"</a>
            </h3>
            <p class='txtBase'>" + dt.Rows[i][SubitemsColumns.VscontentColumn].ToString() + @"</p>
        </div>
    </div>";
            }

        }

        return s;
    }
}