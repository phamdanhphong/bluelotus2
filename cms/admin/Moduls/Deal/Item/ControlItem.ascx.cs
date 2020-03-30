﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using TatThanhJsc.AdminModul;
using TatThanhJsc.Extension;
using TatThanhJsc.DealModul;
using TatThanhJsc.TSql;
using TatThanhJsc.Database;
using TatThanhJsc.Columns;

public partial class cms_admin_Moduls_Deal_Item_ControlItem : System.Web.UI.UserControl
{
    protected string app = CodeApplications.Deal;
    protected string pic = FolderPic.Deal;
    private string language = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueAdmin();
    private string sortCookiesName = SortKey.SortDealItems;
    private string p = "1";
    private string NumberShowItem = "10";

    private string top = "";
    private string fields = "";
    private string condition = "";
    private string orderBy = "";

    private string igid = "";
    
    private string key = "";
    private string name = "";
    
    private string ArrayId = "";    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null)
            p = Request.QueryString["p"];
        if (Request.QueryString["igid"] != null)
            igid = Request.QueryString["igid"];
        if (Request.QueryString["key"] != null)
            key = Request.QueryString["key"];
        if (Request.QueryString["name"] != null)
            name = Request.QueryString["name"];
        if (Request.QueryString["NumberShowItem"] != null)
            NumberShowItem = Request.QueryString["NumberShowItem"].ToString();
        if (!IsPostBack)
        {
            tbKeySearch.Text = key;
            tbTitleSearch.Text = name;
            if (NumberShowItem.Length > 0)
            {
                DdlListShowItem.SelectedValue = NumberShowItem;
                DdlListShowItemTop.SelectedValue = NumberShowItem;
            }

            GetGroupsInDdl();
            GetNews("");
        }
    }

    protected string SetEnableQuantity()
    {
        if (!DealConfig.KeyHienThiQuanLySoLuongDeal)
            return "style='display:none;'";
        else
            return "";
    }
    protected string SetEnableTime()
    {
        if (!DealConfig.KeyHienThiQuanLyThoiGianDeal)
            return "style='display:none;'";
        else
            return "";
    }

    protected string LinkUpdate(string iid)
    {
        if (!NumberShowItem.Equals("") && !p.Equals(""))
        {
            return LinkAdmin.GoAdminItem(CodeApplications.Deal, TypePage.UpdateItem, iid, NumberShowItem, p);
        }
        else
        {
            return LinkAdmin.GoAdminItem(CodeApplications.Deal, TypePage.UpdateItem, iid);
        }
    }

    protected string LinkCreate()
    {
        string igidUpdate = "";
        if (!ddlCateSearch.SelectedValue.Equals(""))
        {
            igidUpdate = ddlCateSearch.SelectedValue;
        }
        return LinkAdmin.GoAdminCategory(CodeApplications.Deal, TypePage.CreateItem, igidUpdate);
    }

    void GetNews(string order)
    {        
        if (!igid.Equals(""))
        {
            ddlCateSearch.SelectedValue = igid;
            condition = GroupsItemsTSql.GetItemsInGroupCondition(ddlCateSearch.SelectedValue, "");
        }
        else
        {
            condition = DataExtension.AndConditon(
                "VGAPP = '" + app + "'",
                GroupsTSql.GetGroupsByVglang(language));
        }
        condition += " AND IIENABLE <> '2' ";
        if (tbKeySearch.Text.Length>0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(tbKeySearch.Text, ItemsColumns.VikeyColumn);
        }
        if (tbTitleSearch.Text.Length > 0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(tbTitleSearch.Text, ItemsColumns.VititleColumn);
        }

        if (order.Length > 0)
            orderBy = order;
        else
        {
            orderBy = CookieExtension.GetCookiesSort(sortCookiesName);
            if (orderBy.Length < 1)
                orderBy = " DCREATEDATE DESC ";
        }

        DataSet ds = new DataSet();
        ds = GroupsItems.GetAllDataPagging(p, DdlListShowItem.SelectedValue, condition, orderBy);
        DataTable dt = new DataTable();
        dt = ds.Tables[1];
        string key = tbKeySearch.Text + "&name=" + tbTitleSearch.Text;
        LtPagging.Text = PagingExtension.SpilitPages(Convert.ToInt32(dt.Rows[0]["TotalRows"]),
                                                      Convert.ToInt16(DdlListShowItem.SelectedValue), Convert.ToInt32(p),
                                                      LinkAdmin.UrlAdmin(CodeApplications.Deal, TypePage.Item,
                                                                   ddlCateSearch.SelectedValue, key,
                                                                   DdlListShowItem.SelectedValue), "currentPS", "otherPS", "firstPS",
                                                      "lastPS", "previewPS", "nextPS");
        LtPaggingTop.Text = LtPagging.Text;
        rp_mn_users.DataSource = ds.Tables[0];
        rp_mn_users.DataBind();
    }

    void GetGroupsInDdl()
    {
        DataTable dt = new DataTable();
        fields = "*";
        condition = DataExtension.AndConditon(
            GroupsTSql.GetGroupsByVglang(language),
            GroupsTSql.GetGroupsByVgapp(app),
            " IGENABLE <> 2 ");
        orderBy = "";
        dt = Groups.GetAllGroups(fields, condition, orderBy);
 
        ddlCateSearch.Items.Add(new ListItem(Developer.DealKeyword.TatCaDanhMuc, ""));            
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ddlCateSearch.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i]["IGLEVEL"].ToString()) + dt.Rows[i]["VGNAME"].ToString(), dt.Rows[i]["IGID"].ToString()));
        }
        ddlCateSearch.SelectedValue = igid;
    }

    protected void lnk_delete_user_checked_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= rp_mn_users.Items.Count - 1; i++)
        {
            CheckBox chkDelete = (CheckBox)rp_mn_users.Items[i].FindControl(("chk_item"));
            if (chkDelete != null)
            {
                if (chkDelete.Checked)
                {
                    ArrayId += chkDelete.ToolTip;
                    ArrayId += ",";
                }
            }
            else
            {
                return;
            }
        }
        if (ArrayId.Length > 0)
        {
            ArrayId = ArrayId.Substring(0, (ArrayId.Length - 1));
        }
        else
        {
            return;
        }
        char[] splitItem = { ',' };
        foreach (string itemID in ArrayId.Split(splitItem, StringSplitOptions.RemoveEmptyEntries))
        {
            string[] fieldsDelItem = { "IIENABLE", ItemsColumns.DiupdateColumn };
            string[] valuesDelItem = { "2", "'" + DateTime.Now.ToString() + "'" };
            Items.UpdateItems(DataExtension.UpdateTransfer(fieldsDelItem, valuesDelItem), ItemsTSql.GetItemsByIid(itemID));
        }

        GetNews("");
    }
    protected void TextChanged(object sender, EventArgs e)
    {
        TextBox textbox = sender as TextBox;
        string[] fields = { "IORDER" };
        string[] values = { textbox.Text };
        string condition = " IGIID = '" + textbox.ToolTip + "' ";
        GroupsItems.UpdateGroupsItemsCondition(DataExtension.UpdateTransfer(fields, values), condition);
        GetNews("");
    }
   
    protected void rp_mn_users_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string c = e.CommandName.Trim();
        string p = e.CommandArgument.ToString().Trim();
        fields = "*";
        condition = ItemsTSql.GetItemsCondition(p, app);

        switch (c)
        {
            #region Delete
            case "delete":
                string[] fieldsDelItem = { "IIENABLE", ItemsColumns.DiupdateColumn };
                string[] valuesDelItem = { "2", "'" + DateTime.Now.ToString() + "'" };

                Items.UpdateItems(DataExtension.UpdateTransfer(fieldsDelItem, valuesDelItem), condition);
                GetNews("");
                break;
            #endregion
            #region Edit Enable
            case "EditEnable":
                DataTable dt = new DataTable();
                dt = GroupsItems.GetAllData(top, fields, condition, orderBy);

                string[] fieldsEnable = { "IIENABLE" };
                string[] valuesEnable = { "" };
                if (dt.Rows[0]["IIENABLE"].ToString().Equals("0"))
                {
                    valuesEnable[0] = "1";
                    Items.UpdateItems(DataExtension.UpdateTransfer(fieldsEnable, valuesEnable), condition);
                }
                else
                {
                    valuesEnable[0] = "0";
                    Items.UpdateItems(DataExtension.UpdateTransfer(fieldsEnable, valuesEnable), condition);
                }
                GetNews("");
                break;
            #endregion
            #region Edit
            case "edit":
                Response.Redirect(LinkUpdate(p));
                break;
            #endregion
        }
    }

    protected void lbtName_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        string order = CookieExtension.SetCookiesSort(ItemsColumns.VititleColumn, sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại        
        GetNews(order);
    }
    protected void lbtDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        string order = CookieExtension.SetCookiesSort(ItemsColumns.DicreatedateColumn, sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetNews(order);
    }
    protected void lbtView_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        string order = CookieExtension.SetCookiesSort(ItemsColumns.IitotalviewColumn, sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetNews(order);
    }
    protected void lbtStatus_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        string order = CookieExtension.SetCookiesSort(ItemsColumns.IienableColumn, sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetNews(order);
    }    

    protected void ltrSearch_Click(object sender, EventArgs e)
    {
        PostSearch();
    }

    protected void DdlListShowItem_SelectedIndexChanged(object sender, EventArgs e)
    {        
        PostSearch();
    }

    protected void DdlListShowItemTop_SelectedIndexChanged(object sender, EventArgs e)
    {
        DdlListShowItem.SelectedValue = DdlListShowItemTop.SelectedValue;
        PostSearch();
    }
    void PostSearch()
    {
        string key = tbKeySearch.Text + "&name=" + tbTitleSearch.Text;
        Response.Redirect(LinkAdmin.GoAdminCategory(CodeApplications.Deal, TypePage.Item, ddlCateSearch.SelectedValue,
                                                    "&NumberShowItem=" + DdlListShowItem.SelectedValue, "1", key));
    }


    protected string CalculateChart(string total, string current)
    {
        string s = "";
        float width = 100;
        float currentWidth = 0;
        float tong = float.Parse(total);
        float hientai = float.Parse(current);
        if (tong != 0)
        {
            float tile = (hientai / tong);
            currentWidth = tile * width;
        }
        s = "<div style='margin-top:2px;width:" + width + @"px; height:10px;border:solid 1px #4CB313'><div class='hientai' style='width:" + currentWidth + @"px;height:10px;background:#4CB313'></div></div>";

        return s;
    }

    protected string SoldItems(string id)
    {        
        int number = 0;
        int total = 0;
        condition = DataExtension.AndConditon(
            SubitemsTSql.GetSubitemsByVslang(id),//Lấy chi tiết đơn hàng theo id của item
            SubitemsTSql.GetSubitemsByVskey(CodeApplications.DealCart));
        DataTable dt = Subitems.GetSubItems("", SubitemsColumns.IsidColumn, condition, "");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                number = (int.Parse(dt.Rows[i][SubitemsColumns.VsatuthorColumn].ToString()));
                total += (1 * number);
            }
        }
        return total.ToString();
    }
}