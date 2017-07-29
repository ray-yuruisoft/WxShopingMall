//加载DataGrid数据
function loadData() {
    $('#tt').datagrid({
        url: '/ActionInfo/GetActionInfo',
        height: 520,
        rownumbers: true,
        remoteSort: false,
        multiSort: true,
        collapsible: true,
        fitColumns: true, //列自适应
        nowrap: true,
        pagination: true,
        idField: 'ID',//主键列的列明
        loadMsg: '正在加载权限信息...',
        pagination: true,//是否有分页
        singleSelect: false,//是否单行选择
        pageSize: 15,//页大小，一页多少条数据
        pageNumber: 1,//当前页，默认的
        pageList: [15, 30, 45],
        queryParams: {},//往后台传递参数
        columns: [[
            { field: 'ck', checkbox: true, align: 'left', width: 50 },
            { field: 'ID', title: '编号', width: 50 },
            { field: 'ActionInfoName', title: '权限名称', width: 200 },
            { field: 'Sort', title: '排序', width: 20 },
            { field: 'Remark', title: '备注', width: 300 },
            { field: 'Url', title: '请求地址', width: 300 },
            { field: 'HttpMethod', title: '请求方式', width: 60 },
            {
                field: 'ActionTypeEnum', title: '权限类型', width: 100,
                formatter: function (value, row, index) {
                    if (value == 1) {
                        return "菜单权限";
                    } else {
                        return "普通权限";
                    }
                }
            },
            {
                field: 'SubTime', title: '时间', width: 100, align: 'right',
                formatter: function (value, row, index) {
                    return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-M-d");
                }
            },
            {
                field: 'Operator', title: '操作', width: 100, align: 'right',
                formatter: function (value, row, index) {
                    var str = "<a href='javascript:void(0)' ids='" + row.ID + "' class='deleteLink'>删除</a>";
                    str += "&nbsp;&nbsp;<a href='javascript:void(0)' ids='" + row.ID + "' class='editLink'>修改</a>";
                    return str;
                }
            },

        ]],
        toolbar: [{
            id: 'btnAdd',
            text: '添加',
            iconCls: 'icon-add',
            handler: function () {
                addInfo();
            }
        }, {
            id: 'btnDelete',
            text: '删除',
            iconCls: 'icon-remove',
            handler: function () {
                deleteInfo();
            }
        }, {
            id: 'btnEdit',
            text: '编辑',
            iconCls: 'icon-edit',
            handler: function () {
                editInfo();
            }
        }, {
            id: 'btnSetRole',
            text: '给权限分配角色',
            iconCls: 'icon-edit',
            handler: function () {
                setActionRoleInfo();
            }
        }],
        onLoadSuccess: function (e, field) {
            $(".deleteLink").click(function () {
                alert($(this).attr("ids"));
            });
        },
    });
}
//给权限类型下拉框加上改变事件
function bindselectActionTypeChange() {
    $("#selectActionType").change(function () {
        if ($(this).val() == "0") {//如果不是选择的"菜单权限"类型隐藏，否则显示
            $("#imageIconTr").fadeOut("slow");
        } else {
            $("#imageIconTr").fadeIn("slow");
        }
    });
}
//文件异步上传
function bindFileUpload() {
    $("#btnUpload").click(function () {
        if ($("#imgIcon").val() == "") {
            $.messager.alert("提示", "请选择图片文件");
            return;
        }
        $("#addForm").ajaxSubmit({
            type: 'post',
            url: '/ActionInfo/GetMenuIcon',
            success: function (data) {
                var serverData = data.split(':');
                if (serverData[0] == "ok") {
                    $("#hidImage").attr("value", serverData[1]);//将服务端返回的图片路径赋给隐藏域
                    $("#menuIconShow").append("<img src='" + serverData[1] + "' width='40px' height='40px' />");
                } else {   //这里需要压缩图片 压缩成40*40
                    $.messager.alert("提示", "图片上传错误!");
                }
            }
        });
    });
}
//设置权限的角色
function setActionRoleInfo() {
    var rows = $('#tt').datagrid('getSelections');
    if (rows.length != 1) {
        $.messager.alert("提示", "您的选择错误！", "error");
        return;
    }
    $("#setActionRoleDiv").css("display", "block");
    $("#setActionRoleFrame").attr("src", "/ActionInfo/SetActionRole/?id=" + rows[0].ID);
    $('#setActionRoleDiv').dialog({
        title: "设置权限的角色信息",
        modal: true,
        collapsible: true,
        width: 400,
        height: 500,
        buttons: [{
            text: 'Ok',
            iconCls: 'icon-ok',
            handler: function () {
                //自己完成表单校验
                //$("#editForm").submit();
                var chilidWindow = $("#setActionRoleFrame")[0].contentWindow;//获取子窗体window对象.
                chilidWindow.subEditForm();
            }
        }, {
            text: 'Cancel',
            handler: function () {
                $('#setActionRoleDiv').dialog('close');

            }
        }]
    });
}
//修改信息
function editInfo() {
    var rows = $('#tt').datagrid('getSelections');
    if (rows.length != 1) {
        $.messager.alert("提示", "您的选择错误！", "error");
        return;
    }
    $("#editDiv").css("display", "block");
    $("#editFrame").attr("src", "/ActionInfo/ShowEditInfo/?id=" + rows[0].ID);
    $('#editDiv').dialog({
        title: "编辑信息",
        modal: true,
        collapsible: true,
        width: 400,
        height: 500,
        buttons: [{
            text: 'Ok',
            iconCls: 'icon-ok',
            handler: function () {
                var chilidWindow = $("#editFrame")[0].contentWindow;//获取子窗体window对象.
                chilidWindow.subEditForm();
            }
        }, {
            text: 'Cancel',
            handler: function () {
                $('#editDiv').dialog('close');

            }
        }]
    });
}
//修改完成后调用该方法
function afterEdit() {
    $('#editDiv').dialog('close');
    $('#tt').datagrid('reload');//重新加载。
}
//权限配置完角色后调用该方法
function afterSetAction_div() {
    $('#setActionRoleDiv').dialog('close');
    $('#tt').datagrid('reload');//重新加载。
}
//添加信息
function addInfo() {
    $("#addDiv").css("display", "block");
    $('#addDiv').dialog({
        title: "添加信息",
        modal: true,
        collapsible: true,
        width: 350,
        height: 400,
        buttons: [{
            text: 'Ok',
            iconCls: 'icon-ok',
            handler: function () {
                //自己完成表单校验
                $("#addForm").submit();//提交表单.
            }
        }, {
            text: 'Cancel',
            handler: function () {
                $('#addDiv').dialog('close');

            }
        }]
    });
}
//添加完成以后调用该方法
function afterAdd() {
    $('#addDiv').dialog('close');
    $('#tt').datagrid('reload');//重新加载。
    $("#addForm input").val("");
}
//删除信息.
function deleteInfo() {
    var rows = $('#tt').datagrid('getSelections');
    if (!rows || rows.length == 0) {
        $.messager.alert("提醒", "请选择要删除的记录!", "error");
        return;
    }
    if ($.messager.confirm("提示", "确定要删除吗?", function (r) {
        if (r) {//如果成立表示用户单击了确定
        //获取要删除的记录的编号.
        var strId = "";
        for (var i = 0; i < rows.length; i++) {
                strId = strId + rows[i].ID + ",";//1,3,4,
    }
        strId = strId.substr(0, strId.length - 1);//去掉最后的逗号.
        $.post("/ActionInfo/DeleteActionInfo", { "strId": strId }, function (data) {
                    if (data == "ok") {
                    $('#tt').datagrid('reload');//重新加载。
                    $('#tt').datagrid('clearSelections');
    } else {
    }
    });
    }

    }));
}
//将序列化成json格式后日期(毫秒数)转成日期格式
function ChangeDateFormat(cellval) {
    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}