using System;
using System.Data;
using System.Web.Script.Serialization;
using TatThanhJsc.Columns;
using TatThanhJsc.ContactModul;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;
using WebApplication2;

public partial class cms_display_ContactUs_Ajax_Ajax : System.Web.UI.Page
{
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    private string app = CodeApplications.Contact;
    string top = "", fields = "", condition = "", orderby = "";
    string action = "";
    JavaScriptSerializer js = new JavaScriptSerializer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) action = Request.QueryString["action"];
        if (Request.Params["action"] != null) action = Request.Params["action"];
        switch (action)
        {
            case "SendContact":
                SendContact();
                break;
            case "SendContact02":
                SendContact02();
                break;

        }
    }

    private void SendContact()
    {
        string s = "Success";
        #region Lấy thông tin
        string name = Request.Params["name"];
        string email = Request.Params["email"];
        string phone = Request.Params["phone"];

        string number = Request.Params["number"];
        string ngay = Request.Params["ngay"];
        string hour = Request.Params["hour"];
        string content = Request.Params["content"];

        string igid = GetFirtIGID();
        #endregion

        TatThanhJsc.Database.GroupsItems.InsertItemsGroupsItems(lang, app+"1", "", name, name,
            content, "",
                                               email, number, ngay, hour, "", "", "", "", "", "",
                                               phone,
                                               "0", "0", "", "", DateTime.Now.ToString(), DateTime.Now.ToString(),
                                               DateTime.Now.ToString(), "", igid,
                                               DateTime.Now.ToString(), DateTime.Now.ToString(),
                                               DateTime.Now.ToString(),
                                               "", "0");
        #region Gửi email thông báo đến
        string emailhethong = SettingsExtension.GetSettingKey(SettingsExtension.KeyMailWebsite, lang);
        string emailkhac = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailPhu, lang) + "," + email;
        string[] listemail = emailkhac.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string date = DateTime.Now.ToString();
        string subject = LanguageItemExtension.GetnLanguageItemTitleByName("Thông báo từ") + UrlExtension.WebisteUrl + " " + date;
        string body =
            @"
<div style='font:bold 14px Arial;padding-bottom:15px'>" + LanguageItemExtension.GetnLanguageItemTitleByName("Xin chào! Bạn có một thư liên hệ") + @" " + UrlExtension.WebisteUrl + @"</div>
<div style='font:bold 12px Arial;padding-bottom:10px'>Chi tiết:</div>
<ul>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Họ tên") + @": " + name + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Email") + @": " + email + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Điện thoại") + @": " + phone + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Số người") + @": " + number + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Ngày") + @": " + ngay + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Giờ") + @": " + hour + @"</li>

<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Gửi lúc") + @": " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + @"</li>
<div style='font:bold 12px Arial;padding-bottom:10px'>" + LanguageItemExtension.GetnLanguageItemTitleByName("Nội dung liên hệ") + @":</div>
<div>"+content+@"</div>

";
        EmailExtension.SendEmail(email, subject, body, listemail);
        #endregion

        string[] strArrayReturn = { s };
        Response.Write(js.Serialize(strArrayReturn));
    }


    private void SendContact02()
    {
        string s = "Success";
        #region Lấy thông tin
        string name = Request.Params["name"];
        string email = Request.Params["email"];
        string phone = Request.Params["phone"];
        string chuDe = Request.Params["chuDe"];
        string content = Request.Params["content"];

        string igid = GetFirtIGID();
        #endregion

        TatThanhJsc.Database.GroupsItems.InsertItemsGroupsItems(lang, app, "", name, name,
                                              content, "",
                                               email, chuDe, "", "", "", "", "", "", "", "",
                                               phone,
                                               "0", "0", "", "", DateTime.Now.ToString(), DateTime.Now.ToString(),
                                               DateTime.Now.ToString(), "", igid,
                                               DateTime.Now.ToString(), DateTime.Now.ToString(),
                                               DateTime.Now.ToString(),
                                               "", "0");
        #region Gửi email thông báo đến
        string emailhethong = SettingsExtension.GetSettingKey(SettingsExtension.KeyMailWebsite, lang);
        string emailkhac = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailPhu, lang) + "," + email;
        string[] listemail = emailkhac.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string date = DateTime.Now.ToString();
        string subject = LanguageItemExtension.GetnLanguageItemTitleByName("Thông báo từ") + UrlExtension.WebisteUrl + " " + date;
        string body =
            @"
<div style='font:bold 14px Arial;padding-bottom:15px'>" + LanguageItemExtension.GetnLanguageItemTitleByName("Xin chào! Bạn có một thư liên hệ") + @" " + UrlExtension.WebisteUrl + @"</div>
<div style='font:bold 12px Arial;padding-bottom:10px'>Chi tiết:</div>
<ul>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Chủ đề") + @": " + chuDe + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Họ tên") + @": " + name + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Email") + @": " + email + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Điện thoại") + @": " + phone + @"</li>
<li>" + LanguageItemExtension.GetnLanguageItemTitleByName("Gửi lúc") + @": " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + @"</li>
<div style='font:bold 12px Arial;padding-bottom:10px'>" + LanguageItemExtension.GetnLanguageItemTitleByName("Nội dung liên hệ") + @":</div>
<div>" + content + @"</div>
";
        EmailExtension.SendEmail(email, subject, body, listemail);
        #endregion

        string[] strArrayReturn = { s };
        Response.Write(js.Serialize(strArrayReturn));
    }
    private string GetFirtIGID()
    {
        string s = "";
        string condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(CodeApplications.Contact),
            GroupsTSql.GetByEnable("1"),
            GroupsTSql.GetByLevel("1"),
            GroupsTSql.GetByLang(lang)
            );
        DataTable dt = Groups.GetGroups("1", GroupsColumns.IgidColumn, condition, GroupsColumns.IgorderColumn);
        if (dt.Rows.Count > 0)
        {
            s = dt.Rows[0][GroupsColumns.IgidColumn].ToString();
        }
        return s;
    }


}