using System;

public partial class cms_admin_Service_AdmLeftmenu : System.Web.UI.UserControl
{    
    protected string suc = "";
    protected string uc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["uc"] != null)
        {
            uc = Request.QueryString["uc"];
        }
        if (Request.QueryString["suc"] != null)
        {
            suc = Request.QueryString["suc"];
        }

        PhManagerApi.Controls.Add(LoadControl("../../../api/Service/Leftmenu.ascx"));

        if (!IsPostBack)
            SetEnableControls();
    }

    /// <summary>
    /// Hiển thị hoặc ẩn các chức năng theo thiết lập trong bảng Settings
    /// </summary>
    void SetEnableControls()
    {
        //Ẩn hiện menu Quản lý thuộc tính dịch vụ
        if (ServiceConfig.KeyHienThiQuanLyThuocTinhDichVu)
        {
            pnThuocTinhDichVu.Visible = true;
            pnThuocTinhDichVu_ThemMoi.Visible = true;
            pnThuocTinhDichVu_ThungRac.Visible = true;
        }
        else
        {
            pnThuocTinhDichVu.Visible = false;
            pnThuocTinhDichVu_ThemMoi.Visible = false;
            pnThuocTinhDichVu_ThungRac.Visible = false;
        }               
        //Ẩn hiện menu Quản lý phản hồi
        if (ServiceConfig.KeyHienThiQuanLyPhanHoiDichVu) pnDanhSachPhanHoi.Visible = true;
        else pnDanhSachPhanHoi.Visible = false;

        //Ẩn hiện menu Thống kê báo cáo
        if (ServiceConfig.KeyHienThiThongKeBaoCaoDichVu) pnThongKeBaoCao.Visible = true;
        else pnThongKeBaoCao.Visible = false;
    }

    protected string SetSelectedCate(string Values)
    {
        if (suc.Equals(Values))
        {
            return "Selected";
        }
        else
        {
            return "";
        }
    }

    protected string SetSelectedRecycleBin()
    {
        if (suc.Equals("RecycleCategory") || suc.Equals("RecycleItem") || suc.Equals("RecycleGroup"))
        {
            return "Selected";
        }
        else
        {
            return "";
        }
    }

    protected string SetEnableSpaceCate()
    {
        if (suc.Equals("c"))
        {
            return "InvisibleSpaceCate";
        }
        else
        {
            return "";
        }
    }

    protected string SetEnableTool()
    {
        if (suc.Equals("CreateCategory"))
        {
            return "InvisibleSpaceCate";
        }
        else
        {
            return "";
        }
    }

    protected string SetCustomizeOther()
    {
        if (suc.Equals("Report"))
        {
            return "InvisibleSpaceCate";
        }
        else
        {
            return "";
        }
    }
}
