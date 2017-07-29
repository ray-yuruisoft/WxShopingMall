INSERT INTO [Yuruisoft.RouteStatistics.DB].[dbo].[ActionInfo]
(ActionInfoName,ControllerName,ActionMethodName,Url,ActionTypeEnum,MenuIcon,IconWidth,IconHeight,HttpMethod,SubTime,ModifiedOn,Remark,DelFlag,Sort)
VALUES
('显示-系统主页','Home'      ,'HomePage'     ,'/home/homepage'           ,0,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-显示系统主页面的权限'  ,0,'1' ),
('显示-权限界面','ActionInfo','Index'        ,'/actioninfo/index'        ,1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-显示权限管理界面的权限',0,'2' ),
('显示-角色界面','RoleInfo'  ,'Index'        ,'/roleinfo/index'          ,1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-显示角色管理界面的权限',0,'3'),
('显示-用户界面','UserInfo'  ,'Index'        ,'/userinfo/index'          ,1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-显示用户管理界面的权限',0,'4'),

('获取-权限信息','ActionInfo','GetActionInfo','/actioninfo/getactioninfo',0,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-获取权限信息数据的权限',0,'5' ),
('获取-角色信息','RoleInfo'  ,'GetRoleInfo'  ,'/roleinfo/getroleinfo'    ,0,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-获取角色信息数据的权限',0,'6'),
('获取-用户信息','UserInfo'  ,'GetUserInfo'  ,'/userinfo/getuserinfo'    ,0,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-获取用户信息数据的权限',0,'7'),

('添加-权限信息','ActionInfo','AddActionInfo','/actioninfo/addactioninfo',1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-添加权限信息的权限，对应按钮',0,'8'),
('添加-角色信息','RoleInfo'  ,'AddRoleInfo'  ,'/roleInfo/addroleinfo'    ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-添加角色信息的权限，对应按钮',0,'9'),
('添加-用户信息','UserInfo'  ,'AddUserInfo'  ,'/userinfo/adduserinfo'    ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-添加用户信息的权限，对应按钮',0,'10'),

('显示-修改权限页面','ActionInfo','ShowEditInfo'  ,'/actioninfo/showeditinfo'  ,1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-显示修改权限信息的权限，对应对话框',0,'11'),
('提交-修改权限页面','ActionInfo','EditActionInfo','/actioninfo/editactioninfo',1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-提交修改权限信息的对话框，对应按钮',0,'12'),
('显示-修改角色页面','RoleInfo'  ,'ShowEditInfo'  ,'/roleinfo/showeditinfo'    ,1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-显示修改角色信息的权限，对应对话框',0,'13'),
('提交-修改角色页面','RoleInfo'  ,'EditInfo'      ,'/roleinfo/editinfo'        ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-提交修改角色信息的对话框，对应按钮',0,'14'),
('显示-修改用户页面','UserInfo'  ,'ShowEditInfo'  ,'/userinfo/showeditinfo'    ,1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-显示修改用户信息的权限，对应对话框',0,'15'),
('提交-修改用户页面','UserInfo'  ,'EditUserInfo'  ,'/userinfo/edituserinfo'    ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-提交修改用户信息的对话框，对应按钮',0,'16'),

('获取-给权限分配角色信息','ActionInfo','SetActionRole'    ,'/actioninfo/setactionrole'  ,1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-获取给权限分配角色信息的数据',0,'17'),
('提交-给权限分配角色信息','ActionInfo','SetActionRole'    ,'/actioninfo/setactionrole'  ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-提交给权限分配角色信息的按钮',0,'18'),
('获取-给用户分配角色信息','UserInfo'  ,'SetUserRoleInfo'  ,'/userinfo/setuserroleinfo'  ,1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-获取给用户分配角色信息的数据',0,'19'),
('提交-给用户分配角色信息','UserInfo'  ,'SetUserRoleInfo'  ,'/userinfo/setuserroleinfo'  ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-提交给用户分配角色信息的按钮',0,'20'),
('获取-给用户分配权限信息','UserInfo'  ,'SetUserActionInfo','/userinfo/setuseractioninfo',1,'空',400,400,'GET' ,@DateTime_now,@DateTime_now,'系统默认添加-获取给用户分配权限信息的数据',0,'21'),
('提交-给用户分配权限信息','UserInfo'  ,'SetActionForUser' ,'/userinfo/setactionforuser' ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-提交给用户分配权限信息的按钮',0,'22'),

('删除-权限信息','ActionInfo','DeleteActionInfo','/actioninfo/deleteactioninfo',1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-删除权限信息的按钮',0,'23'),
('删除-角色信息','RoleInfo'  ,'DeleteRoleInfo'  ,'/roleinfo/deleteroleinfo'    ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-删除角色信息的按钮',0,'24'),
('删除-用户信息','UserInfo'  ,'DeleteUserInfo'  ,'/userinfo/deleteuserinfo'    ,1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-删除用户信息的按钮',0,'25'),

('找出-用户菜单','Home','GetMenuItems','/home/getmenuitems',1,'空',400,400,'GET',@DateTime_now,@DateTime_now,'系统默认添加-找出对应用户菜单',0,'26'),

('获取-文件数据','ActionInfo','GetMenuIcon','/actioninfo/getmenuicon',1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-获取图像文件方法',0,'27'),

('检查-用户名重复','UserInfo','CheckUserName','/userinfo/checkusername',0,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-检查用户名是否重复功能',0,'28'),

('删除-给用户分配权限信息','UserInfo','ClearActionUser','/userinfo/clearactionuser',1,'空',400,400,'POST',@DateTime_now,@DateTime_now,'系统默认添加-删除给用户分配的权限信息',0,'29')

insert into [Yuruisoft.RouteStatistics.DB].[dbo].[UserInfo]
  (UName,UPwd,TUPwd,UEmail,UPhoneNumber,SubTime,ModifiedOn,Remark,DelFlag,Sort)
  values
  ('admin','admin','admin','417853832@qq.com','15308202328',@DateTime_now,@DateTime_now,'系统默认添加-超级管理员权限',0,'1')