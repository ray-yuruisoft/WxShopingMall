//加载DataGrid数据
function loadData(pars) {
    var pager = $('#tt').datagrid({
        url: '/ExtensionAgents/GetAgentInfo',
        height: 520,
        rownumbers: true,
        remoteSort: false,
        multiSort: true,
        collapsible: true,
        fitColumns: true, //列自适应
        nowrap: true,
        pagination: true,
        idField: 'ID',//主键列的列明
        loadMsg: '正在加载用户的信息...',
        pagination: true,//是否有分页
        singleSelect: false,//是否单行选择
        pageSize: 5,//页大小，一页多少条数据
        pageNumber: 1,//当前页，默认的
        pageList: [5, 10, 15],
        queryParams: pars,//往后台传递参数
        columns: [[//c.UserName, c.UserPass, c.Email, c.RegTime
            { field: 'ck', checkbox: true, align: 'left', width: 50 },
            { field: 'ID', title: "ID", sortable: true, width: 80 },
            { field: 'LName', title: "推广员姓名", sortable: true, width: 120 },
            { field: 'UrlName', title: "需要推广的链接名", sortable: true, width: 120 },
            { field: 'ExtensionUrl', title: "发送给推广员的链接", sortable: true, width: 180 },
            { field: 'ExtensionScore', title: "推广合格次数", sortable: true, width: 120 },
            { field: 'Remark', title: "备注", sortable: true, width: 120 },
            {
                field: 'SubTime', title: "创建时间", sortable: true, width: 100, align: 'right',
                formatter: function (value, row, index) {
                    return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-M-d");
                }
            },
            {
                field: 'ModifiedOn', title: "修改时间", sortable: true, width: 100, align: 'right',
                formatter: function (value, row, index) {
                    return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-M-d");
                }
            }
        ]],
        toolbar: '#toolbar_Admin',
        onBeforeLoad: function () {
            $('#tt').datagrid('getPager').pagination({
                buttons: [{
                    iconCls: 'icon-configure',
                    handler: function () {
                        alert('search');
                    }
                }, {
                    iconCls: 'icon-add',
                    handler: function () {
                        alert('add');
                    }
                }, {
                    iconCls: 'icon-edit',
                    handler: function () {
                        alert('edit');
                    }
                }]
            })
        },
        onLoadSuccess: function () {
        },
    });
    return pager;
}
//简单查询按钮
function submitForm() {
    var pars = {
        DoTheSearch: true,
        name: $("#txtUName").val(),
        remark: $("#txtURemark").val()
    };
    loadData(pars);
}
function clearForm() {
    $('#ff_easySearch').form('clear');
}
//修改信息
function editInfo() {
    var rows = $('#tt').datagrid('getSelections');
    if (rows.length != 1) {
        $.messager.alert("提示", "您的选择错误！", "error");
        return;
    }
    $("#editDiv").css("display", "block");
    $("#editFrame").attr("src", "/UserInfo/ShowEditInfo/?id=" + rows[0].ID);
    $('#editDiv').dialog({
        title: "编辑信息",
        modal: true,
        collapsible: true,
        width: 400,
        height: 450,
        buttons: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                var chilidWindow = $("#editFrame")[0].contentWindow;//获取子窗体window对象.
                chilidWindow.subEditForm();
            }
        }, {
            text: '取消',
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
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                //自己完成表单校验 使用了MVC校验
                $("#addForm").submit();//提交表单.
            }
        }, {
            text: '取消',
            handler: function () {
                $('#addDiv').dialog('close');
            }
        }]
    });

}
//添加完成以后调用该方法,回调函数
function afterAdd() {
    $('#addDiv').dialog('close');
    $('#tt').datagrid('reload');//重新加载。
    $("#addForm input").val("");
}
//删除用户信息.
function deleteInfo() {
    var rows = $('#tt').datagrid('getSelections');
    if (!rows || rows.length == 0) {
        //alert("请选择要修改的商品！");
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
        $.post("/ExtensionAgents/DeleteAgentInfo", { "strId": strId }, function (data) {
                    if (data == "ok") {
        // loadData();
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