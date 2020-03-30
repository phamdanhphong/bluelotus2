using System;
using System.Data;
using System.Web;
using System.Web.UI;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;

public partial class Login : System.Web.UI.Page
{
    private string LoginSetting = "LoginSetting";
    private string loginFailCountName = "LoginFailCount";
    private int lockMinute = 5;//Thời gian khóa khi đăng nhập lỗi
    protected void Page_Load(object sender, EventArgs e)
    {        
        if(Session[loginFailCountName] != null && (int)Session[loginFailCountName] > 3)
        {
            if(Session[loginFailCountName + "Time"] != null &&
               (DateTime.Now - (DateTime)Session[loginFailCountName + "Time"]).TotalMinutes <= 0)
            {
                tbAccountName.Text = "*";
                tbAccountName.Enabled = false;

                tbPassword.Text = "*";
                tbPassword.Enabled = false;

                btLogin.Visible = false;

                double thoiGianCho = ((DateTime)Session[loginFailCountName + "Time"] - DateTime.Now).TotalMinutes;
                
                ltrLoginLockedAlert.Text =
                    "<div class='loginLocked'>Thông báo: Bạn đã đăng nhập sai quá 3 lần, vui lòng thử lại sau " + thoiGianCho.ToString("N1") + " phút nữa.</div>";
            }
            else
            {
                Session[loginFailCountName] = 0;
                Session[loginFailCountName + "Time"] = DateTime.Now.AddMinutes(-1);
            }
        }

        tbAccountName.Focus();
        if(!IsPostBack)
        {
            if(Session[loginFailCountName] == null)            
                Session[loginFailCountName] = 0;

            if(Session[loginFailCountName + "Time"] == null)
                Session[loginFailCountName + "Time"] = DateTime.Now.AddMinutes(-1);
        }
    }

    protected void btLogin_Click(object sender, EventArgs e)
    {
        if (Session[loginFailCountName]!=null && (int)Session[loginFailCountName] > 3)
        {
            Session[loginFailCountName + "Time"] = DateTime.Now.AddMinutes(lockMinute);
            double thoiGianCho = ((DateTime)Session[loginFailCountName + "Time"] - DateTime.Now).TotalMinutes;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Bạn đã đăng nhập sai quá 3 lần, vui lòng thử lại sau " + thoiGianCho.ToString("N1") + " phút nữa.','Thông báo');", true);
            return;
        }

        DataTable dt = new DataTable();
        dt = Users.GetUsersByUserNameAndPassword(tbAccountName.Text, tbPassword.Text);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][UsersColumns.UserisapprovedColumn].ToString() == "1")
            {
                Session[loginFailCountName] = 0;

                CookieExtension.SaveCookies(LoginSetting, dt.Rows[0][UsersColumns.UsernameColumn].ToString());

                #region UserName

                CookieExtension.SaveCookies("UserName", dt.Rows[0][UsersColumns.UsernameColumn].ToString());
                CookieExtension.SaveCookies("UserPassword", dt.Rows[0][UsersColumns.UserpasswordColumn].ToString());

                #endregion

                #region UserId

                CookieExtension.SaveCookies("UserId", dt.Rows[0][UsersColumns.UseridColumn].ToString());

                #endregion

                #region Roles

                //Luu mo ta quyen vao cookies
                DataTable dtRoles = new DataTable();

                dtRoles = Roles.GetRolesByRoleId(dt.Rows[0]["RoleId"].ToString());
                string RoleDescription = dtRoles.Rows[0]["RoleDescription"].ToString();
                CookieExtension.SaveCookies("RolesUser", RoleDescription);

                #endregion

                #region Cập nhật lần đăng nhập cuối

                string values = UsersTSql.GetUsersByUserlastlogindate(DateTime.Now.ToString());
                string conditionUpdate = UsersTSql.GetUsersByUsername(tbAccountName.Text);
                Users.UpdateUsers(values, conditionUpdate);

                #endregion


                #region Logs

                string logAuthor = CookieExtension.GetCookies("LoginSetting");
                string logCreateDate = DateTime.Now.ToString();
                Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", logAuthor, logAuthor, "",
                    logCreateDate + ": " + logAuthor + " đăng nhập vào hệ thống quản trị");

                #endregion

                if (Request.Cookies["RefererUrl"] != null)
                    Response.Redirect(Request.Cookies["RefererUrl"].Value);
                else
                    Response.Redirect("admin.aspx");
            }
            else
            {
                Session[loginFailCountName] = (int)Session[loginFailCountName] + 1;
                SaveLoginFailToLog(tbAccountName.Text, "0");

                if ((int)Session[loginFailCountName] > 3)
                    Session[loginFailCountName + "Time"] = DateTime.Now.AddMinutes(lockMinute);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Tài khoản của bạn đang bị khoá.\\nLưu ý: bạn đã đăng nhập sai " + Session[loginFailCountName] + " lần.\\nĐăng nhập sai quá 3 lần đăng nhập sai thì bạn sẽ không thể đăng nhập nữa.','Thông báo');", true);
                return;
            }
        }
        else
        {
            Session[loginFailCountName] = (int)Session[loginFailCountName] + 1;
            SaveLoginFailToLog(tbAccountName.Text, "1");

            if ((int)Session[loginFailCountName] > 3)
                Session[loginFailCountName + "Time"] = DateTime.Now.AddMinutes(lockMinute);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Bạn đã nhập sai tài khoản hoặc mật khẩu.\\nLưu ý: bạn đã đăng nhập sai " + Session[loginFailCountName] + " lần.\\nĐăng nhập sai quá 3 lần đăng nhập sai thì bạn sẽ không thể đăng nhập nữa.','Thông báo');", true);
            return;
        }
    }

    /// <summary>
    /// Lưu thông tin đăng nhập lỗi vào log
    /// </summary>
    /// <param name="acountName">Tên tài khoản được nhập vào form đăng nhập</param>
    /// <param name="status">0: tài khoản bị khóa, 1: sai tài khoản</param>
    void SaveLoginFailToLog(string acountName, string status)
    {
        #region Get IP Network
        //Get IP Network
        string clientIP = "";
        clientIP = HttpContext.Current.Request.UserHostAddress;
        #endregion
        #region netword ip
        string ipAddress = "";
        ipAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString();
        #endregion
        #region Get Computer Name
        //Get Computer Name
        string strClientName = "";
        strClientName = System.Net.Dns.GetHostName();
        #endregion

        #region Logs
        string logAuthor = acountName;
        string logCreateDate = DateTime.Now.ToString();

        if(status == "0")
            Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", logAuthor, logAuthor, "",
                logCreateDate + ": " + logAuthor + " đã cố gắng đăng nhập vào hệ thống bằng tài khoản đã bị khóa.");
        else if(status == "1")
            Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", logAuthor, logAuthor, "",
                logCreateDate + ": " + logAuthor +
                " đã cố gắng đăng nhập vào hệ thống với thông tin tài khoản không chính xác.");
        #endregion
    }

    protected void lbtResetPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect(UrlExtension.WebisteUrl + "cms/admin/OtherControls/ResetPassword.aspx");
    }
}
