var app = getApp();
var FlagSetOut;

Page({
  data: {
    list: [
      {
        icon:'icon-book',
        id: 'dic',
        name: '词典',
        open: false,
        pages: [{ displayName: '英汉翻译', url: 'Dictionary'}, { displayName: '汉英翻译', url: 'Dictionary' }],
      },
      {
        icon:'icon-edit',
        id: 'feedback',
        name: '设置',
        open: false,
        pages: [{ displayName: '智能提示设置', url: 'SetAutoNum' }]
      }
    ],

    inputShowed: false,//搜索
    inputVal: ""
  },
  /***
   * 搜索
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

    try {
      var TempObject = app.com.dealUrl(e.detail.value);
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

  onLoad: function (options) {
    // 页面初始化 options为页面跳转所带来的参数  
    var that = this;
    wx.getStorage({
      key: 'autodisplayNum',
      success: function (res) {
        console.log(res.data)
        app.globalData.autodisplayNum = res.data
      }
    })
  }

});
