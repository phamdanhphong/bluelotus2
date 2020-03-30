﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using TatThanhJsc.AdminModul;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.DestinationModul;
using TatThanhJsc.TSql;


public partial class cms_admin_Moduls_Destination_Item_RecycleItemPhoto : System.Web.UI.UserControl
{
    private string app = CodeApplications.DestinationPhoto;
    private string appcate = CodeApplications.Destination;
    protected string pic = FolderPic.Destination;
    private string language = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueAdmin();
    private string sortCookiesName = SortKey.SortDestinationItems;
    private string p = "1";
    private string NumberShowItem = "10";

    private string top = "";
    private string fields = "";
    private string condition = "";
    private string orderBy = "";

    private string igid = "";

    private string conditionItems = "";
    private string key = "";
    private string SearchCondition = "";
    private string ArrayId = "";

    private string strdisplay = "Nhập từ khóa tìm kiếm";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null)
            p = Request.QueryString["p"];
        if (Request.QueryString["igid"] != null)
            igid = Request.QueryString["igid"];
        if (Request.QueryString["key"] != null)
            key = Request.QueryString["key"];
        if (Request.QueryString["NumberShowItem"] != null)
            NumberShowItem = Request.QueryString["NumberShowItem"].ToString();

        if (!IsPostBack)
        {
            txt_key.Text = key;
            GetParentCate();
            GetDestination("");
        }
    }

    protected string LinkUpdate(string iid)
    {
        if (!NumberShowItem.Equals("") && !p.Equals(""))
        {
            return LinkAdmin.GoAdminItem(CodeApplications.Destination, TypePage.UpdateItem, iid, NumberShowItem, p) + "&app=" + app;
        }
        else
        {
            return LinkAdmin.GoAdminItem(CodeApplications.Destination, TypePage.UpdateItem, iid) + "&app=" + app;
        }
    }

    private string LinkCreate()
    {
        string igidUpdate = "";
        if (!ddl_group_ontab.SelectedValue.Equals(""))
        {
            igidUpdate = ddl_group_ontab.SelectedValue;
        }
        return LinkAdmin.GoAdminCategory(CodeApplications.Destination, TypePage.CreateItem, igidUpdate) + "&app=" + app;
    }

    void GetDestination(string order)
    {
        DdlListShowItem.SelectedValue = NumberShowItem;
        if (!igid.Equals(""))
        {
            ddl_group_ontab.SelectedValue = igid;
            conditionItems = GroupsItemsTSql.GetItemsInGroupCondition(ddl_group_ontab.SelectedValue, ItemsTSql.GetItemsByViapp(app));
        }
        else
        {
            conditionItems = DataExtension.AndConditon(
                "VGAPP = '" + appcate + "'",
                GroupsTSql.GetGroupsByVglang(language), ItemsTSql.GetItemsByViapp(app), ItemsTSql.GetItemsByVilang(language));
        }
        conditionItems = DataExtension.AndConditon(conditionItems, ItemsTSql.GetItemsByViapp(app));
        conditionItems += " AND IIENABLE = '2' ";
        if (txt_key.Text.Length > 0 && !txt_key.Text.Equals(strdisplay))
        {
            SearchCondition = " AND " + SearchTSql.GetSearchMathedCondition(txt_key.Text, ItemsColumns.VititleColumn);
        }
        if (txt_key.Text.Equals(strdisplay))
        {
            txt_key.Text = "";
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
        ds = GroupsItems.GetAllDataPagging(p, NumberShowItem, conditionItems + SearchCondition, orderBy);
        DataTable dt = new DataTable();
        dt = ds.Tables[1];

        LtPagging.Text = PagingExtension.SpilitPages(Convert.ToInt32(dt.Rows[0]["TotalRows"]),
                                                      Convert.ToInt16(NumberShowItem), Convert.ToInt32(p),
                                                      LinkAdmin.UrlAdmin(CodeApplications.Destination, TypePage.RecycleItem,
                                                                   ddl_group_ontab.SelectedValue, txt_key.Text,
                                                                   NumberShowItem) + "&app=" + app, "currentPS", "otherPS", "firstPS",
                                                      "lastPS", "previewPS", "nextPS");
        rp_mn_users.DataSource = ds.Tables[0];
        rp_mn_users.DataBind();
    }

    void GetParentCate()
    {
        DataTable dt = new DataTable();
        fields = "*";
        condition = DataExtension.AndConditon(
            GroupsTSql.GetGroupsByVglang(language),
            GroupsTSql.GetGroupsByVgapp(appcate),
            " IGENABLE <> 2 ");
        orderBy = "";
        dt = Groups.GetAllGroups(fields, condition, orderBy);
        if (dt.Rows.Count > 0)
        {
            ddl_group_ontab.Items.Add(new ListItem(Developer.DestinationKeyword.TatCaDanhMuc, ""));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl_group_ontab.Items.Add(new ListItem(TatThanhJsc.Extension.DropDownListExtension.FormatForDdl(dt.Rows[i]["IGLEVEL"].ToString()) + dt.Rows[i]["VGNAME"].ToString(), dt.Rows[i]["IGID"].ToString()));
            }
        }
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
        //condition = " IID in(" + ArrayId + ") ";
        //Đoàn sửa
        char[] splitItem = { ',' };
        foreach (string itemID in ArrayId.Split(splitItem, StringSplitOptions.RemoveEmptyEntries))
        {
            string[] fieldsDelItem = { "IIENABLE", ItemsColumns.DiupdateColumn };
            string[] valuesDelItem = { "2", "'" + DateTime.Now.ToString() + "'" };
            Items.UpdateItems(DataExtension.UpdateTransfer(fieldsDelItem, valuesDelItem), ItemsTSql.GetItemsByIid(itemID));
        }

        GetDestination("");
    }
    protected void TextChanged(object sender, EventArgs e)
    {
        TextBox textbox = sender as TextBox;
        string[] fields = { "IORDER" };
        string[] values = { textbox.Text };
        string condition = " IGIID = '" + textbox.ToolTip + "' ";
        GroupsItems.UpdateGroupsItemsCondition(DataExtension.UpdateTransfer(fields, values), condition);
        GetDestination("");
    }
    protected void SetTextBoxSearch(object sender, System.EventArgs e)
    {
        txt_key.Text = "Nhập từ khóa tìm kiếm";
        ((TextBox)sender).Attributes["onfocus"] = "if (this.value=='" + strdisplay + "') this.value='';";
        ((TextBox)sender).Attributes["onblur"] = "if (this.value=='') this.value='" + strdisplay + "';";
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
                GetDestination("");
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
                GetDestination("");
                break;
            #endregion
            #region Edit
            case "edit":
                Response.Redirect(LinkUpdate(p));
                break;
            #endregion
        }
    }
    protected void lnk_create_product_Click(object sender, EventArgs e)
    {
        Response.Redirect(LinkCreate());
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        p = "1";
        igid = ddl_group_ontab.SelectedValue;
        GetDestination("");
    }
    protected void DdlListShowItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        NumberShowItem = DdlListShowItem.SelectedValue;
        igid = ddl_group_ontab.SelectedValue;
    }
    protected void lbtName_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        string order = CookieExtension.SetCookiesSort(ItemsColumns.VititleColumn, sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại        
        GetDestination(order);
    }
    protected void lbtDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        string order = CookieExtension.SetCookiesSort(ItemsColumns.DicreatedateColumn, sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetDestination(order);
    }
    protected void lbtView_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        string order = CookieExtension.SetCookiesSort(ItemsColumns.IitotalviewColumn, sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetDestination(order);
    }
    protected void lbtStatus_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        string order = CookieExtension.SetCookiesSort(ItemsColumns.IienableColumn, sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetDestination(order);
    }

    protected void LbSeaerch_Click(object sender, EventArgs e)
    {
        igid = ddl_group_ontab.SelectedValue;
        GetDestination("");
    }
}