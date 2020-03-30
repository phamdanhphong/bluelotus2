using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.OtherModul;
using TatThanhJsc.TSql;

public partial class cms_display_CommonControls_CommonSupportOnline_ProductDetail : System.Web.UI.UserControl
{
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string app = CodeApplications.SupportOnline;
    private string pic = FolderPic.SupportOnline;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ltrNick.Text=LoadNicks();
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
            email="",phone="";
        if (dt.Rows.Count==0)
        {
            return "";
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            img = ImagesExtension.GetImage(pic, dt.Rows[i][ItemsColumns.ViimageColumn].ToString(), dt.Rows[i][ItemsColumns.VititleColumn].ToString(), "tall", true, false, "");
            title = dt.Rows[i][ItemsColumns.VititleColumn].ToString();
            yahoo = dt.Rows[i][ItemsColumns.ViurlColumn].ToString();
            viber = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 5);
            fb = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 6);
            zalo = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 4);
            skype = dt.Rows[i][ItemsColumns.ViauthorColumn].ToString();
            email = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 3);
            phone = StringExtension.LayChuoi(dt.Rows[i]["VIPARAMS"].ToString(), "", 1);
            s += @"
            <div class='item'>
                <div class='item_box'>
                    <div class='khungAnh'>
                        <a href='javascript://' title='"+title+@"' class='khungAnhCrop' tabindex='-1'>
                            "+img+@"
                        </a>
                    </div>
                    <div class='item_details'>
                        <div class='item_title'>"+title+@"</div>
                        <div class='item_mxh'>
                        <ul class='icon'>
                
                            " + (skype.Length > 0 ? @"
                                <li>
                                <a href='skype:call?" + skype + @"'>
                                    <img src='Css/Pic/sk.png'></a>
                            </li>" : "") + @"
                 
                            " + (zalo.Length > 0 ? @"
                                <li>
                                <a href='tel:" + zalo + @"'>
                                    <img src='Css/Pic/zl.png'></a>
                            </li>" : "") + @"
                            " + (viber.Length > 0 ? @"
                                <li>
                                <a href='tel:" + viber + @"'>
                                    <img src='~/Css/Pic/vb.png'></a>
                            </li>" : "") + @"
                            " + (fb.Length > 0 ? @"
                                <li>
                                <a href='" + fb + @"'>
                                    <img src='Css/Pic/fb.png'></a>
                            </li>" : "") + @"                                
                        </ul>
                        </div>
                    </div>
                    <ul class='infocontact'>
                        <li class='mail' href='mailto:" + email + "'>" + email + @"</li>
                        <li class='phone' href='tel:" + phone + "'>" + phone + @"</li>
                    </ul>
                </div>
            </div>";                     
        }
        s = @"
        <div class='hotro_1'>
            <div class='slick_1'>
                "+s+@"
            </div>
        </div>";
        return s;
    }

    protected string LoadHotline()
    {
        return SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine, lang);
    }
}