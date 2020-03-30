using System;
using System.Data;
using System.Web.Script.Serialization;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.MemberModul;
using TatThanhJsc.TSql;

public partial class cms_display_Ajax_RegisEmail : System.Web.UI.Page
{

    string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    string email = "", nhanthu = "", condition = "";
    string[] cutEmail;
    JavaScriptSerializer js = new JavaScriptSerializer();
    protected void Page_Load(object sender, EventArgs e)
    {
        InserContactUs();
    }
    bool ExistedEmail(string email, string app)
    {
        condition = DataExtension.AndConditon(
            MembersTSql.GetMembersByVmemberemail(email),
            MembersTSql.GetMembersByVproperty(app));
        DataTable dt = new DataTable();
        dt = Members.GetMembersCondition("", MembersColumns.ImidColumn, condition, "");
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;
    }
    void InserContactUs()
    {
        string email = Request.Params["email"];
        bool hopLe = true;
        if (ExistedEmail(email, CodeApplications.MemberNewsletter))
        {
            hopLe = false;
        }
        if (hopLe)
        {
            string trangThai = "1";

            #region Thêm tài khoản

            //Thêm tài khoản
            Members.InsertMembers(
                CodeApplications.MemberNewsletter, "", "", "", "", "", email, DateTime.Now.ToString(), "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", trangThai, "");

            #endregion

            #region Gửi email thông báo đến email hệ thống

            string emailhethong = SettingsExtension.GetSettingKey(SettingsExtension.KeyMailWebsite, lang);
            string emailkhac = SettingsExtension.GetSettingKey("MailBanTin", lang) + "," + email;
            string[] listemail = emailkhac.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
            string date = DateTime.Now.ToString();
            string subject = "Thông báo từ " + UrlExtension.WebisteUrl + " " + date;
            string body =
                @"
<div style='font:bold 14px Arial;padding-bottom:15px'>Xin chào! Bạn có đăng ký nhận bản tin từ " +
                UrlExtension.WebisteUrl + @"</div>
<div style='font:bold 12px Arial;padding-bottom:10px'>Chi tiết:</div>
<ul>
<li>Email: " + email + @"</li>
<li>Gửi lúc: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + @"</li>
</ul>";
            EmailExtension.SendEmail(emailhethong, subject, body, listemail);

            //#region Gửi email tới KH
            //EmailExtension.SendEmail(email, subject, body);
            //#endregion

            #endregion

            #region Thông báo hoàn thành và reset các texbox

            string[] reply = {"success"};
            Response.Output.Write(js.Serialize(reply));
            return;

            #endregion
        }
        else
        {
            string[] reply = { "error" };
            Response.Output.Write(js.Serialize(reply));
        }
    }
}