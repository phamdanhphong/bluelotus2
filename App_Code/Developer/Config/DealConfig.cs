
/// <summary>
/// Lưu các cấu hình cho modul deal
/// </summary>
public class DealConfig
{
    private string[] values;
    private string[] text;

    public DealConfig()
    {
        text = new string[] { "Giao sản phẩm", "Giao voucher" };
        values = new string[] { "0", "1" };
    }
    /// <summary>
    /// Lưu chuỗi: Giao sản phẩm, Giao voucher,...
    /// </summary>
    public string[] Text
    {
        get { return text; }
    }
    public string[] Values
    {
        get { return values; }
    }

    public const bool KeyHienThiQuanLyThuocTinhDeal = false;
    public const bool KeyHienThiThuocTinhLocDeal = true;
    public const bool KeyHienThiQuanLyPhanHoiDeal = true; 
    public const bool KeyHienThiThongKeBaoCaoDeal = true;
    public const bool KeyHienThiThemNhieuAnhChoDeal = true;
    public const bool KeyHienThiNhieuAnhDealTheoMau = true;
    public const bool KeyHienThiAddNickChoDeal = false;
    public const bool KeyHienThiHangSanXuat = false;

    public const bool KeyHienThiThemNhieuVideoChoDeal = true;
    public const bool KeyHienThiThemNhieuSubitemChoDeal = true;

    public const bool KeyHienThiQuanLySoLuongDeal = true;
    public const bool KeyHienThiQuanLyThoiGianDeal = true;
}