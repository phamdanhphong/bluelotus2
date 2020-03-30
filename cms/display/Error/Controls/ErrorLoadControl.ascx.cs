using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cms_display_Error_Controls_ErrorLoadControl : System.Web.UI.UserControl
{
    private string code = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["code"] != null)
            code = Request.QueryString["code"];

        switch(code)
        {
            case "404":
                plLoadControl.Controls.Add(LoadControl("Error404.ascx"));
                break;

            default:
                plLoadControl.Controls.Add(LoadControl("Error404.ascx"));
                break;
        }
    }
}