using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.NewsModul;
using TatThanhJsc.TSql;

public partial class cms_display_News_SubControls_SubNewsHot : System.Web.UI.UserControl
{
    private string app = CodeApplications.News;
    private string appGroup = CodeApplications.NewsGroupItem;
    private string pic = FolderPic.News;
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           GetGroups("2");
        }
    }

    /// <summary>
    /// Lấy danh sách các nhóm
    /// </summary>
    /// <returns></returns>
    private string GetGroups(string position)
    {
        string s = "";
        string fields = "", condition = "", orderby = "";
        condition = DataExtension.AndConditon(
            GroupsTSql.GetByParams(position),
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
                list = GetList(dt.Rows[i][GroupsColumns.IgidColumn].ToString(), "5");
                // tieu de
                 string link = "", name = "";
                link = (UrlExtension.WebisteUrl + dt.Rows[i][GroupsColumns.VGSEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();
                name = dt.Rows[i][GroupsColumns.VgnameColumn].ToString();                                       
                if (list.Length>0)
                {
                    ltrList.Text = @"
                    <div class='main-right'>
                        <span class='btn-comp01 fade-up'><b>" + name + @"</b></span>
                        <div class='list-news03'>
                        "+list+ @"
                        </div>
                    </div>";
                }
               
            }
        }
        return s;
    }

    private string GetList(string id, string top)
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
        DataTable dt = GroupsItems.GetAllData(rows.ToString(), fields, condition, orderby);
        if (dt.Rows.Count > 0)
        {
            s = "";
            string link = "", titleItem = "", desc = "", img = "", dateIT = "", countView = "", listOrther = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                link = (UrlExtension.WebisteUrl + dt.Rows[i][ItemsColumns.VISEOLINKSEARCHColumn] + RewriteExtension.Extensions).ToLower();
                titleItem = dt.Rows[i][ItemsColumns.VititleColumn].ToString();
                img = ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), titleItem, "", true, false, "",false);
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