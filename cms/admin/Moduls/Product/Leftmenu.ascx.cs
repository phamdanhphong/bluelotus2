using System;

public partial class cms_admin_Product_AdmLeftmenu : System.Web.UI.UserControl
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

        PhManagerApi.Controls.Add(LoadControl("../../../api/Product/Leftmenu.ascx"));

        if (!IsPostBack)
            SetEnableControls();
    }

    /// <summary>
    /// Hiển thị hoặc ẩn các chức năng theo thiết lập trong bảng Settings
    /// </summary>
    void SetEnableControls()
    {
        //Ẩn hiện menu Quản lý thuộc tính tin tức
        if (ProductConfig.KeyHienThiQuanLyThuocTinhSanPham)
        {
            pnThuocTinhSanPham.Visible = true;
            pnThuocTinhSanPham_ThemMoi.Visible = true;
            pnThuocTinhSanPham_ThungRac.Visible = true;
        }
        else
        {
            pnThuocTinhSanPham.Visible = false;
            pnThuocTinhSanPham_ThemMoi.Visible = false;
            pnThuocTinhSanPham_ThungRac.Visible = false;
        }
        //Ẩn hiện menu Quản lý thuộc tính lọc sản phẩm
        if (ProductConfig.KeyHienThiThuocTinhLocSanPham)
        {
            pnThuocTinhLocSanPham.Visible = true;
            pnThuocTinhLocSanPham_ThemMoi.Visible = true;
            pnThuocTinhLocSanPham_ThungRac.Visible = true;
        }
        else
        {
            pnThuocTinhLocSanPham.Visible = false;
            pnThuocTinhLocSanPham_ThemMoi.Visible = false;
            pnThuocTinhLocSanPham_ThungRac.Visible = false;
        }
        //Ẩn hiện menu Quản lý màu sản phẩm
        if (ProductConfig.KeyHienThiNhieuAnhProductTheoMau)
        {
            PnColor.Visible = true;
            PnColor_ThemMoi.Visible = true;
            PnColor_ThungRac.Visible = true;
        }
        else
        {
            PnColor.Visible = false;
            PnColor_ThemMoi.Visible = false;
            PnColor_ThungRac.Visible = false;
        }

        //Ẩn hiện menu Nhà cung cấp
        if (ProductConfig.KeyHienThiHangSanXuat)
        {
            PnProvider.Visible = true;
            pnProvider_ThemMoi.Visible = true;
            pnProvider_ThungRac.Visible = true;
        }
        else
        {
            PnProvider.Visible = false;
            pnProvider_ThemMoi.Visible = false;
            pnProvider_ThungRac.Visible = false;
        }

        //Ẩn hiện menu Quản lý phản hồi
        if (ProductConfig.KeyHienThiQuanLyPhanHoiSanPham) pnDanhSachPhanHoi.Visible = true;
        else pnDanhSachPhanHoi.Visible = false;

        //Ẩn hiện menu Thống kê báo cáo
        if (ProductConfig.KeyHienThiThongKeBaoCaoSanPham) pnThongKeBaoCao.Visible = true;
        else pnThongKeBaoCao.Visible = false;

        pnDonHang.Visible = ProductConfig.KeyHienThiDonHang;


       
        pnNhom.Visible = ProductConfig.KeyHienThiNhom;
        pnNhom_ThemMoi.Visible = ProductConfig.KeyHienThiNhom;
        pnNhom_ThungRac.Visible = ProductConfig.KeyHienThiNhom;
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
