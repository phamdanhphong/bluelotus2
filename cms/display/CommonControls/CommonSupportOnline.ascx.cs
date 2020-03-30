using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.OtherModul;
using TatThanhJsc.TSql;

public partial class cms_display_CommonControls_CommonSupportOnline : System.Web.UI.UserControl
{
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string app = CodeApplications.SupportOnline;
    private string pic = FolderPic.SupportOnline;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ltrNick.Text = LoadNicks();
    }
    private string  LoadTitle()
    {
        string s="";
        string orderby = GroupsColumns.IgOrder;
        string condition = DataExtension.AndConditon(
            GroupsTSql.GetByLang(lang),
            GroupsTSql.GetByApp(app),
            GroupsTSql.GetByEnable("1")
            );
        DataTable dt = new DataTable();
        dt = Groups.GetGroups("", "*", condition, orderby);
        if (dt.Rows.Count>0)
        {
            string img = ImagesExtension.GetImage(pic, dt.Rows[0][GroupsColumns.VgImage].ToString(), dt.Rows[0][GroupsColumns.VgName].ToString(), "", true, false, ""); ;
            string desc = dt.Rows[0][GroupsColumns.VgDesc].ToString();
            s += @"
            <div class='nguoihoro'>
                <div class='khungAnh'>
                    <a class='khungAnhCrop'>"+img+@"</a>
                </div>
                <div class='tthotro'>
                    <a class='trtt'>" + LanguageItemExtension.GetnLanguageItemTitleByName("Hỗ trợ trực tuyến") + @"</a>
                    <div class='vuilong'>
                        "+desc+@"
                    </div>
                </div>
            </div>";
        }
        return s;
    }

    string LoadNicks()
    {
        string s = "";
        

        string orderby = GroupsItemsColumns.IorderColumn;
        string condition = DataExtension.AndConditon(
            GroupsTSql.GetGroupsByVglang(lang),
            GroupsTSql.GetGroupsByVgapp(app),
            GroupsTSql.GetGroupsByIgenable("1"),
            ItemsTSql.GetItemsByViapp(app),
            ItemsTSql.GetItemsByIienable("1")
            );
        DataTable dt = new DataTable();
        dt = GroupsItems.GetAllData("", "*", condition, orderby);
        string title = ""; string img = ""; string yahoo = "", viber = "", fb = "", zalo = "", skype = "",
            email = "", phone = "";
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            img = ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), dt.Rows[i][ItemsColumns.VititleColumn].ToString(), "", true, false, "");
            title = dt.Rows[i][ItemsColumns.VititleColumn].ToString();
            yahoo = dt.Rows[i][ItemsColumns.ViurlColumn].ToString();
            viber = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 5);
            fb = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 6);
            zalo = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 4);
            skype = dt.Rows[i][ItemsColumns.ViauthorColumn].ToString();
            email = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 3);
            phone = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 1);
            s += @"
            <div class='itemhotro'>
                <a class='title'>" + title + @"</a>
                <div class='hotline'>" + LanguageItemExtension.GetnLanguageItemTitleByName("Hotline") + @": <span>" + phone + @"</span></div>
                <div class='mxh'>
                    <a class='email' href='mailto:" + email + @"'>
                        <img src='Css/pic/email1.png' /></a>
                    <a class='skype' href='skype:call?" + skype + @"'>
                        <img src='Css/pic/skype.png' /></a>
                    <a class='zalo' href='tel:" + zalo + @"'>
                        <img src='Css/pic/zalo.png' /></a>
                </div>
            </div>";
        }
        s = @"
        <div class='hotro' id='popuplh'>
            <a class='close' ></a>
            <div class='noidungtb'>
                <a class='closepopup'></a>
                <div class='nenpopup'>
                     "+LoadTitle()+@"
                    " + s + @"  
                </div>
            </div>
        </div>
        ";
        return s;
    }

    protected string LoadHotline()
    {
        return SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine, lang);
    }
}