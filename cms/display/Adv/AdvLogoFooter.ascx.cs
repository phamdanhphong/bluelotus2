using System;
using System.Data;
using TatThanhJsc.AdvertisingModul;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.TSql;
using TatThanhJsc.Extension;

public partial class cms_display_Adv_AdvLogoFooter : System.Web.UI.UserControl
{
    private string app = CodeApplications.Advertising;
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string pic = FolderPic.Advertising;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadListSlider("1", "");
            if (ltrAdv.Text == "")
                this.Visible = false;
        }

    }

    private void LoadListSlider(string position, string cssImage)
    {
        //get data
        // lấy danh sách group có vị trí xác định
        string fields = DataExtension.GetListColumns(GroupsColumns.IgidColumn, GroupsColumns.VgnameColumn);
        string orderBy = GroupsColumns.IgorderColumn;
        string condition = DataExtension.AndConditon(GroupsTSql.GetByApp(app),
            GroupsTSql.GetByLang(lang),
            GroupsTSql.GetGroupsByIgenable("1"),
            GroupsTSql.GetGroupsByVgparams(position),
            GroupsTSql.GetByLang(lang));
        DataTable dt = Groups.GetGroups("1", fields, condition, orderBy);
        if (dt.Rows.Count.Equals(0))
        {
            return;
        }
        string igid = "";

        // items info
        string title = "";
        string img = "";
        string href = "";
        string viparams = "";
        string target = "";
        string content = "";

        string strList = "";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            igid = dt.Rows[i][GroupsColumns.IgidColumn].ToString();
            // getListItem
            DataTable dataItems = GroupsItems.GetAllData("1", " * ",
                GroupsItemsTSql.GetItemsInGroupCondition(igid,
                ItemsTSql.GetItemsByIienable("1")),
                GroupsItemsColumns.IorderColumn);
            if (dt.Rows.Count.Equals(0))
            {
                continue;
            }
            else
            {
                string s = "";
                for (int j = 0; j < dataItems.Rows.Count; j++)
                {
                    title = dataItems.Rows[j][ItemsColumns.VititleColumn].ToString();
                    viparams = dataItems.Rows[j][ItemsColumns.ViparamsColumn].ToString();
                    if (viparams.Equals("1")) target = "target='_blank'";
                    else target = "";
                    href = dataItems.Rows[j][ItemsColumns.ViurlColumn].ToString();
                    img = ImagesExtension.SetTypeImageAdvertising("1", pic, dataItems.Rows[j][ItemsColumns.ViimageColumn].ToString(), title, "", "", cssImage, false);
                    content = dataItems.Rows[j][ItemsColumns.VISEOTITLEColumn].ToString();
                    ltrAdv.Text = @"
                    <a href='" + href + "' title='" + title + @"' >
                        " + img + @"
                    </a>";
                }
            }
        }
        //// render
        //ltrAdv.Text = "<ul class='khoi1170'>" + strList + "</ul>";

    }
}
