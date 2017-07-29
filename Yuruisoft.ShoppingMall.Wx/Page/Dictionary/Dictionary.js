var app = getApp();
var FlagSetOut;

var reg = /^[A-Za-z0-9\,\-\s+]+$/ //只包含数字，字母，逗号等
var reg_han = /[\u4E00-\u9FA5\uF900-\uFA2D]/ //只包含汉字


Page({
  data: {
    inputShowed: false,
    inputVal: ""
  },
  temp: {},
  /***********************************
   * 翻译逻辑 以下
   * 
   * 
   * 
   * */
  showInput: function () {
    this.setData({
      inputShowed: true
    });
  },
  hideInput: function () {
    this.setData({
      inputVal: "",
      inputShowed: false,
      articleList: []
    });
  },
  clearInput: function () {
    this.setData({
      inputVal: "",
      SearchKeyList: []
    });
  },
  KeyListTap: function (e) {

    this.setData({
      inputVal: e.currentTarget.dataset.skey,
      SearchKeyList: []
    })

    var that = this;
    try {
      var TempObject = app.com.dealUrl(e.currentTarget.dataset.skey);
      if (TempObject.error)
        return;
      var Tempkind = TempObject.kind;
      var TempSe = TempObject.SeletData;
      var TempDa = TempObject.key_data_Temp;
    }
    catch (e) {
      return
    }
    if (Tempkind == "EnToCn")// 英译汉
    {
      app.ajax.reqPOST('/tinyDic/Search', {
        "Searchdata": TempDa,
        "SeletData": TempSe
      }, function (res) {
        if (!res) {
          return
        }
        //请求数据完成后
        that.setData({
          articleList: res
        })
      });
    }
    else if (Tempkind == "CnToEn")//汉译英
    {
      app.ajax.reqPOST('/tinyDic/SearchCnToEn', {
        "Searchdata": TempDa,
        "SeletData": TempSe
      }, function (res) {
        if (!res) {
          return
        }
        //请求数据完成后
        that.setData({
          articleList: res
        })
      });
    }
  },
  inputTyping: function (e) {
    var that = this;
    this.temp.inputdata = e.detail.value;
    /***
     * 单独增加，客户要求为区分英汉 汉英 以下
     */
    if (this.data.displayName == '英汉翻译')//英译汉
    {
      if (reg_han.test(e.detail.value))//有汉字就滚
        return
    }
    else if (this.data.displayName == '汉英翻译') {//汉译英
      if (reg.test(e.detail.value))//有英文就滚
        return
    }
    else
      return
    /***
     * 单独增加，客户要求为区分英汉 汉英 以上
     */
    try {
      var TempObject = app.com.dealUrl(e.detail.value);
      if (TempObject.error)
        return;
      var TempSearchList = TempObject.SearchList;
      var Tempkind = TempObject.kind;
      var TempSe = TempObject.SeletData;
      var TempDa = TempObject.key_data_Temp;
    }
    catch (e) {
      return
    }


    if (FlagSetOut != undefined) {
      clearTimeout(FlagSetOut);
    }
    if (Tempkind == "EnToCn")//英译汉
    {
      FlagSetOut = setTimeout(function () {
        app.ajax.reqPOST('/tinyDic/SearchKey', {
          "Searchdata": TempDa,
          "TakeNum": app.globalData.autodisplayNum,
          "SeletData": TempSe
        }, function (res) {
          if (!res) {//失败直接返回        
            return
          }
          if (res.error != undefined && res.error == true) {
            return;
          }
          else {//成功
            console.log(res);
            that.setData({
              SearchKeyList: res
            })
          }
        })
      }, 500);
    }
    else if (Tempkind == "CnToEn")//汉译英
    {
      FlagSetOut = setTimeout(function () {
        that.setData({
          SearchKeyList: TempSearchList
        })
      }, 500);
    }


    this.setData({
      inputVal: e.detail.value
    });
  },
  translating: function (e) {
    var that = this;
    if (e.detail.value == undefined)
      var Tempvalue = this.temp.inputdata;
    else
      var Tempvalue = e.detail.value;
    /***
     * 单独增加，客户要求为区分英汉 汉英 以下
     */
    if (this.data.displayName == '英汉翻译')//英译汉
    {
      if (reg_han.test(e.detail.value))//有汉字就滚
        return
    }
    else if (this.data.displayName == '汉英翻译') {//汉译英
      if (reg.test(e.detail.value))//有英文就滚
        return
    }
    else
      return
    /***
     * 单独增加，客户要求为区分英汉 汉英 以上
     */
    try {
      var TempObject = app.com.dealUrl(Tempvalue);
      if (TempObject.error)
        return;
      var Tempkind = TempObject.kind;
      var TempSe = TempObject.SeletData;
      var TempDa = TempObject.key_data_Temp;
    }
    catch (e) {
      return
    }


    if (Tempkind == "EnToCn")// 英译汉
    {
      app.ajax.reqPOST('/tinyDic/Search', {
        "Searchdata": TempDa,
        "SeletData": TempSe
      }, function (res) {
        if (!res) {
          return
        }
        //请求数据完成后
        that.setData({
          articleList: res
        })
      });
    }
    else if (Tempkind == "CnToEn")//汉译英
    {
      app.ajax.reqPOST('/tinyDic/SearchCnToEn', {
        "Searchdata": TempDa,
        "SeletData": TempSe
      }, function (res) {
        if (!res) {
          return
        }
        //请求数据完成后
        that.setData({
          articleList: res
        })
      });
    }





  },


  kindToggle: function (e) {//辅助选项栏
    var id = e.currentTarget.id, list = this.data.list;
    for (var i = 0, len = list.length; i < len; ++i) {
      if (list[i].id == id) {
        list[i].open = !list[i].open
      } else {
        list[i].open = false
      }
    }
    this.setData({
      list: list
    });
  },

  /***
     * 翻译逻辑 以上
     * 
     * 
     * 
     * ********************************/





  onLoad: function (options) {
    // 页面初始化 options为页面跳转所带来的参数  

    this.setData({
      displayName: options.id
    })
    if (options.id == '英汉翻译') {
      this.setData({
        description: '该英汉词典词源来自于21世纪英汉词典'
      })
    }
    else if (options.id == '汉英翻译') {
      this.setData({
        description: '该汉英词典词源来自于21世纪汉英词典'
      })
    }
  }
});