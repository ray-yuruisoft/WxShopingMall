
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheJsonTest.TestWebPage" %>  
  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  
  
<html xmlns="http://www.w3.org/1999/xhtml" >  
<head id="Head1" runat="server">  
    <title></title>  
    <style type="text/css">  
        body{ font-family:Arial,微软雅黑; font-size:14px;}  
        a{ text-decoration:none; color:#333;}  
        a:hover{ text-decoration:none; color:#f00;}  
    </style>  
</head>  
<body>      
    <form id="form2" runat="server">  
        <h3>序列化对象</h3>  
        表现1：<br />  
        <%=TestJsonSerialize()%>  备注：序列化 -Newtonsoft.Json.JsonConvert.SerializeObject(,)-单个对象序列化<br />
        表现2：<br />  
        <%=TestListJsonSerialize() %>  备注：序列化 -Newtonsoft.Json.JsonConvert.SerializeObject(,)-泛集合（嵌套）序列化<br />
        <hr />  
        <h3>反序列化对象</h3>  
        <p>单个对象</p>  
        <%=TestJsonDeserialize() %>  
        <p>多个对象</p>  
        <%=TestListJsonDeserialize() %>      
        <p>反序列化成数据字典Dictionary</p>  
        <%=TestDeserialize2Dic() %>  
        <hr />      
        <h3>自定义反序列化</h3>  
        <%=TestListCustomDeserialize()%>  
        <hr />  
        <h3>序列化输出的忽略特性</h3>  
        NullValueHandling特性忽略=><br />  
        <%=CommonSerialize() %><br />  
        <%=IgnoredSerialize()%><br /><br />  
        属性标记忽略=><br />  
        <%=OutIncluded() %><br />  
        <%=OutIncluded2() %>  
        <hr />  
        <h3>Serializing Partial JSON Fragments</h3>  
        <%=SerializingJsonFragment() %>  
        <hr />  
        <h3>ShouldSerialize</h3>  
        <%=ShouldSerializeTest() %><br />  
        <%=JJJ() %><br /><br />  
        <%=TestReadJsonFromFile() %>  
    </form>  
</body>  
</html>  