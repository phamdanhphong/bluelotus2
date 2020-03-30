using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.NewsModul;
using TatThanhJsc.TSql;


public partial class cms_display_News_SubControls_SubSuKienHome : System.Web.UI.UserControl

{
    private string app = CodeApplications.News;
    private string appGroup = CodeApplications.NewsGroupItem;
    private string pic = FolderPic.News;
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string rewrite = RewriteExtension.News;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ltrGroups.Text = GetGroups("1");
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
<div class='list-program'>
    " + list + @"
</div>";
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
        if (dt.Rows.Count > 0)
        {
            link = (UrlExtension.WebisteUrl + dt.Rows[0][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();

            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link = (UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();
                s += @"
 <div class='list-program__item fade-up'>
        <div class='list-program__img img'>
            <a  href='" + link + @"' title='" + dt.Rows[i][ItemsColumns.VititleColumn] + @"' class='img__crop'>
                " + ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), dt.Rows[i][ItemsColumns.VititleColumn].ToString(), "", true, false, "",false) + @"            
            </a>
        </div>
        <div class='list-program__content'>
            <div class='thongke'>
                <div class='thongke__time'><i class='fa fa-clock-o'></i> " + ((DateTime)dt.Rows[i][ItemsColumns.DiCreateDate]).ToString(
                         LanguageItemExtension.GetnLanguageItemTitleByName("dd/MM/yyyy")) + @"</div>
                <div class='thongke__view'><i class='fa fa-eye'></i> " + NumberExtension.FormatNumber(((int)dt.Rows[i][ItemsColumns.IitotalviewColumn] + 1).ToString()) + @"  lượt xem</div>
            </div>
            <h3 class='list-program__ttl'><a href=''>" + dt.Rows[i][ItemsColumns.VititleColumn] + @"</a></h3>
            <p class='txtBase'>" + dt.Rows[i][ItemsColumns.VidescColumn] + @"</p>
            <a href='" + link + @"' title='" + dt.Rows[i][ItemsColumns.VititleColumn] + @"' class='view-more'>xem thêm</a>
        </div>
    </div>";
            }


        }
        return s;
    }
}