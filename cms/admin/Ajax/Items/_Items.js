function UpdateEnableItem(iid) {
    var val = "";
    var ElementCssClass = document.getElementById("nc" + iid).className;
    val = ElementCssClass.substring(ElementCssClass.length - 1, ElementCssClass.length)

    $.post(WebsiteUrl + "cms/admin/Ajax/Items/UpdateEnableItem.aspx", { "iid": iid, "iienable": val }, function (result) {
        if (val == 0) {
            $("#nc" + iid).removeClass("EnableIcon0");
            $("#nc" + iid).addClass("EnableIcon1");
        }
        else if (val == 1) {
            $("#nc" + iid).removeClass("EnableIcon1");
            $("#nc" + iid).addClass("EnableIcon0");
        }
    });
}

function DeleteItem(iid, titleItem) {
    var msg = "<b>Bạn có chắc chắn muốn xóa bản ghi này không?</b>";
    var msgSuccess = "<b>Bạn đã xóa thành công ''" + titleItem + "''</b>";
    jConfirm(msg, 'Thông báo', function (r) {
        if (r) {
            $.post(WebsiteUrl + "cms/admin/Ajax/Items/DeleteItem.aspx", { "iid": iid }, function (result) {

                $("#Item-" + iid).slideUp();
                $("#DeleteItem").html(result);
                ThongBao('3000', msgSuccess);
            });
        }
    });
}

function DeleteListItems() {
    var msg = "<b>Bạn có chắc chắn muốn xóa các bản ghi này không?</b>";
    var msgSuccess = "<b>Bạn đã xóa thành công các bản ghi vừa chọn</b>";
    var alertMes = "";
    var id = "";
    jQuery(".content input[type=checkbox]").each(function () {
        if (this.checked) {
            id = this.id.substring(this.id.lastIndexOf("_") + 1);
            alertMes += id + ",";
        }
    });
    jConfirm(msg, 'Thông báo', function (r) {
        if (r) {
            $.post(WebsiteUrl + "cms/admin/Ajax/Items/DeleteListItems.aspx", { "listigid": alertMes }, function (result) {
                ThongBao('3000', msgSuccess);
            });
        }

        jQuery(".content input[type=checkbox]").each(function () {
            if (this.checked) {
                id = this.id.substring(this.id.lastIndexOf("_") + 1);
                $("#Item-" + id).slideUp();
            }
        });
    });
}

function DeleteRecItem(iid, titleItem,pic) {
    var msg = "<b>Bạn có chắc chắn muốn xóa bản ghi này không?</b>";
    var msgSuccess = "<b>Bạn đã xóa thành công ''" + titleItem + "''</b>";
    jConfirm(msg, 'Thông báo', function (r) {
        if (r) {
            $("#DeleteCate").html("");
            $.post(WebsiteUrl + "cms/admin/Ajax/Items/DeleteRecItem.aspx", { "iid": iid,"pic":pic }, function (result) {
                $("#Item-" + iid).slideUp();
                ThongBao('3000', msgSuccess);
            });
        }
    });
}

function DeleteRecListItems(pic) {
    var msg = "<b>Bạn có chắc chắn muốn xóa các bản ghi này không?</b>";
    var msgSuccess = "<b>Bạn đã xóa thành công các bản ghi vừa chọn</b>";
    var alertMes = "";
    var id = "";
    jQuery(".content input[type=checkbox]").each(function () {
        if (this.checked) {
            id = this.id.substring(this.id.lastIndexOf("_") + 1);
            alertMes += id + ",";
        }
    });

    jConfirm(msg, 'Thông báo', function (r) {
        if (r) {
            $.post(WebsiteUrl + "cms/admin/Ajax/Items/DeleteRecListItems.aspx", { "listigid": alertMes,"pic":pic }, function (result) {
                ThongBao('3000', msgSuccess);
            });
        }

        jQuery(".content input[type=checkbox]").each(function () {
            if (this.checked) {
                id = this.id.substring(this.id.lastIndexOf("_") + 1);
                $("#Item-" + id).slideUp();
            }
        });
    });
}

function RestoreItem(iid, titleItem) {
    var msg = "<b>Bạn có chắc chắn muốn khôi phục bản ghi này không?</b>";
    var msgSuccess = "Bạn đã khôi phục thành công '" + titleItem + "' ";
    
    jConfirm(msg, 'Thông báo', function (r) {
        if (r) {
            $.post(WebsiteUrl + "cms/admin/Ajax/Items/RestoreItem.aspx", { "iid": iid }, function (result) {
                $("#Item-" + iid).slideUp();
                ThongBao('3000', msgSuccess);
            });
        }
    });
}