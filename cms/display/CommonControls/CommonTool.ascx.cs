using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;

public partial class cms_display_CommonControls_CommonTool : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDetail();
        }
    }
    void LoadDetail()
    {
        DataTable dt = (DataTable)Session["dataByTitle"];//Thông tin chi tiết về Items hoặc Groups đã được gán ở Defualt.aspx vào session
        if (dt.Rows.Count > 0)
        {
            ltrDate.Text = ((DateTime)dt.Rows[0][ItemsColumns.DiupdateColumn]).ToString(LanguageItemExtension.GetnLanguageItemTitleByName("dd/MM/yyyy - hh:mm tt"));
            ltrViewCount.Text = NumberExtension.FormatNumber(((int)dt.Rows[0][ItemsColumns.IitotalviewColumn] + 1).ToString()); 
        }
    }
}