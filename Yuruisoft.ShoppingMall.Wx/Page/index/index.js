var app = getApp();
var FlagSetOut;

Page({
  data: {
    list: [
      {
        icon:'',
        imageUrl:'../../style/images/replaceIcon/groceries.png',
        id: 'agProducts',
        name: '特色农产品',
        open: false,
        pages: [{ displayName: '小香菜', url: 'produceDetails' }, { displayName: '大头菜', url: 'produceDetails' }],
      },
      {
        icon:'',
        imageUrl:'../../style/images/replaceIcon/fresh-vendor.png',
        id: 'freshProducts',
        name: '特色生鲜',
        open: false,
        pages: [{ displayName: '生态羊肉', url: 'produceDetails' }, { displayName: '生态鸡肉', url: 'produceDetails' }]
      },
      {
        icon: '',
        imageUrl:'../../style/images/replaceIcon/ellipsis.png',
        id: 'findMore',
        name: '更多',
        open: false,
        pages: [{ displayName: '智能提示设置', url: 'produceDetails' }]
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
  KeyListTap: function (e) {//搜索内容点击

    this.setData({
      inputVal: e.currentTarget.dataset.skey,
      SearchKeyList: []
    })

  },
  inputTyping: function (e) {//输入时异步远程查询
    var that = this;

    // try {
    //   var TempObject = app.com.dealUrl(e.detail.value);
    //   if (TempObject.error)
    //     return;
    //   var TempSearchList = TempObject.SearchList;
    //   var Tempkind = TempObject.kind;
    //   var TempSe = TempObject.SeletData;
    //   var TempDa = TempObject.key_data_Temp;
    // }
    // catch (e) {
    //   return
    // }


    // if (FlagSetOut != undefined) {
    //   clearTimeout(FlagSetOut);
    // }
    // if (Tempkind == "EnToCn")//英译汉
    // {
    //   FlagSetOut = setTimeout(function () {
    //     app.ajax.reqPOST('/tinyDic/SearchKey', {
    //       "Searchdata": TempDa,
    //       "TakeNum": app.globalData.autodisplayNum,
    //       "SeletData": TempSe
    //     }, function (res) {
    //       if (!res) {//失败直接返回        
    //         return
    //       }
    //       if (res.error != undefined && res.error == true) {
    //         return;
    //       }
    //       else {//成功
    //         console.log(res);
    //         that.setData({
    //           SearchKeyList: res
    //         })
    //       }
    //     })
    //   }, 500);
    // }
    // else if (Tempkind == "CnToEn")//汉译英
    // {
    //   FlagSetOut = setTimeout(function () {
    //     that.setData({
    //       SearchKeyList: TempSearchList
    //     })
    //   }, 500);
    // }


    this.setData({
      inputVal: e.detail.value
    });
  },
  translating: function (e) {//回车键
    var that = this;

    // try {
    //   var TempObject = app.com.dealUrl(e.detail.value);
    //   if (TempObject.error)
    //     return;
    //   var Tempkind = TempObject.kind;
    //   var TempSe = TempObject.SeletData;
    //   var TempDa = TempObject.key_data_Temp;
    // }
    // catch (e) {
    //   return
    // }


    // if (Tempkind == "EnToCn")// 英译汉
    // {
    //   app.ajax.reqPOST('/tinyDic/Search', {
    //     "Searchdata": TempDa,
    //     "SeletData": TempSe
    //   }, function (res) {
    //     if (!res) {
    //       return
    //     }
    //     //请求数据完成后
    //     that.setData({
    //       articleList: res
    //     })
    //   });
    // }
    // else if (Tempkind == "CnToEn")//汉译英
    // {
    //   app.ajax.reqPOST('/tinyDic/SearchCnToEn', {
    //     "Searchdata": TempDa,
    //     "SeletData": TempSe
    //   }, function (res) {
    //     if (!res) {
    //       return
    //     }
    //     //请求数据完成后
    //     that.setData({
    //       articleList: res
    //     })
    //   });
    // }





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
