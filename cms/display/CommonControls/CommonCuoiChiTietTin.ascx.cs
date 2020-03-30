using System;
using TatThanhJsc.Extension;

public partial class Cms_Common_CommonCuoiChiTietTin : System.Web.UI.UserControl
{
    protected string link = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        link = UrlExtension.WebisteUrl + Request.RawUrl.Substring(1);
    }
}