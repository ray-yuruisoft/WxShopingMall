//增加Tabs选项卡
function f_addTab(title, ContentString) {
    if ($("#tt_tabs_Yurui").tabs('exists', title)) {
        $('#tt_tabs_Yurui').tabs('select', title);
    } else {
        $('#tt_tabs_Yurui').tabs('add', {
            title: title,
            content: ContentString,
            closable: true,
            cache: true,
            fit: true,
            border: false,
            plain: true
        });
    }
}
//关闭TAB选项卡
function removePanel() {
    var tab = $('#tt_tabs_Yurui').tabs('getSelected');
    if (tab) {
        var index = $('#tt_tabs_Yurui').tabs('getTabIndex', tab);
        if (index != 0)
            $('#tt_tabs_Yurui').tabs('close', index);
    }
}
//加载用户管理信息
function LoadAdminInfo() {
    var ContentString = '<iframe id="Adminiframe" src ="/UserInfo/Index" scrolling="no" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('用户管理', ContentString);
}
//加载权限管理信息
function LoadActionInfo() {
    var ContentString = '<iframe id="ActionInfoframe" src ="/ActionInfo/Index" scrolling="no" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('权限管理', ContentString);
}
//加载角色管理信息
function LoadRoleInfo() {
    var ContentString = '<iframe id="RoleInfoframe" src ="/RoleInfo/Index" scrolling="no" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('角色管理', ContentString);
}
//加载用户管理说明书
function LoadUserInfo_Instructions() {
    var ContentString = '<iframe id="UserInfo_Instructions" src ="/Instructions/UserInfoInstructions.html" scrolling="no" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('用户管理说明书', ContentString);
}
//加载角色管理说明书
function LoadRoleInfo_Instructions() {
    var ContentString = '<iframe id="RoleInfo_Instructions" src ="/Instructions/RoleInfoInstructions.html" scrolling="no" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('角色管理说明书', ContentString);
}
//加载权限管理说明书
function LoadActionInfo_Instructions() {
    var ContentString = '<iframe id="ActionInfo_Instructions" src ="/Instructions/ActionInfoInstructions.html" scrolling="no" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('权限管理说明书', ContentString);
}
//加载报名配置页面
function LoadEnterConfigPage()
{
    var ContentString = '<iframe id="EnterConfigPage" src ="/Enter/Index" scrolling="auto" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('报名页面', ContentString);
}

//加载推广链接管理
function LoadRouteStatisticsLinksInfo() {
    var ContentString = '<iframe id="RouteStatisticsLinksframe" src ="/RouteStatisticsLinks/Index" scrolling="no" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('推广链接管理', ContentString);
}

//加载推广员管理
function LoadExtensionAgentsInfo() {
    var ContentString = '<iframe id="ExtensionAgentsframe" src ="/ExtensionAgents/Index" scrolling="no" frameborder="0" width="100%" height="100%"></iframe>';
    f_addTab('推广员管理', ContentString);
}