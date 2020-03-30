using System;
using TatThanhJsc.Extension;
// ReSharper disable All

public partial class cms_display_DisplayLoadControl : System.Web.UI.UserControl
{
    string go = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["go"] != null) go = QueryStringExtension.GetQueryString("go");   
        
        if (go.Length < 1 && Session["go"] != null) go = Session["go"].ToString();      
        if (go ==  RewriteExtension.AboutUs) phLoadControl.Controls.Add(LoadControl("AboutUs/Controls/LoadControl.ascx"));
        else if (go == RewriteExtension.Product) phLoadControl.Controls.Add(LoadControl("Product/Controls/LoadControl.ascx"));
        else if (go == RewriteExtension.News) phLoadControl.Controls.Add(LoadControl("News/Controls/LoadControl.ascx"));
        else if (go == RewriteExtension.ContactUs) phLoadControl.Controls.Add(LoadControl("ContactUs/Controls/LoadControl.ascx"));
        else if (go == RewriteExtension.Service) phLoadControl.Controls.Add(LoadControl("Service/Controls/LoadControl.ascx"));
        else if (go == RewriteExtension.QA) phLoadControl.Controls.Add(LoadControl("QA/Controls/LoadControl.ascx"));
        else if (go == RewriteExtension.FileLibrary2) phLoadControl.Controls.Add(LoadControl("Filelibrary/Controls/LoadControl.ascx"));
        else if (go == RewriteExtension.Customer) phLoadControl.Controls.Add(LoadControl("Customer/Controls/LoadControl.ascx"));
        else if (go == "search") phLoadControl.Controls.Add(LoadControl("Search/Controls/LoadControl.ascx"));
        else if (go == "error")
        {
            
            CommonHeader.Visible = false;
            CommonFooter.Visible = false;
            phLoadControl.Controls.Add(LoadControl("Error/Controls/ErrorLoadControl.ascx"));
        }
        else
        {
            phLoadControl.Controls.Add(LoadControl("HomePage/Controls/LoadControl.ascx"));
        }
       
    }
}