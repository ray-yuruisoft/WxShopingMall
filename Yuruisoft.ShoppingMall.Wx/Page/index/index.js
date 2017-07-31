var app = getApp();
var flagSetOut;
var searchKeyObject;
Page({
  data: {
    list: [
      {
        icon: '',
        imageUrl: '../../style/images/replaceIcon/groceries.png',
        id: 'agProducts',
        name: '特色农产品',
        open: false,
        pages: [{ displayName: '小香菜', url: 'produceDetails' }, { displayName: '大头菜', url: 'produceDetails' }],
      },
      {
        icon: '',
        imageUrl: '../../style/images/replaceIcon/fresh-vendor.png',
        id: 'freshProducts',
        name: '特色生鲜',
        open: false,
        pages: [{ displayName: '生态羊肉', url: 'produceDetails' }, { displayName: '生态鸡肉', url: 'produceDetails' }]
      },
      {
        icon: '',
        imageUrl: '../../style/images/replaceIcon/ellipsis.png',
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
  keyListTap: function (e) {//搜索内容点击,函数主要清空作用
    console.log(e)
    this.setData({
      searchKeyList: []
    })
  },
  inputTyping: function (e) {//输入时异步本地查询
    var that = this;
    var searchKeyArr = searchKeyObject.searchKeyArray;

    if (flagSetOut != undefined) {
      clearTimeout(flagSetOut);
    }
    flagSetOut = setTimeout(function () {
      if (e.detail.value == '') {
        return
      }
      var searchKeyList = that.searchKeyListGet(searchKeyArr, e.detail.value);
      that.setData({
        searchKeyList: searchKeyList
      })
    }, 500);
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
    app.ajax.reqPOST('/shoppingMall/recommentListsGet', {//TODO:这里可以做大数据扩展
      "userInfo": "",//TODO:用户信息,调整推荐策略
      "takeNum": app.globalData.recommentListsNum,
      "prefers": ""  //TODO:用户喜好,调整推荐策略
    }, function (res) {
      if (!res || res.error == true) {//失败直接返回        
        return
      }
      that.setData({
        recommentLists: res
      })

    });
    this.searchKeyObjectGet(that);//搜索栏关键字获取
  },
  searchKeyObjectGet: function (that) {//搜索栏关键字对象获取
    wx.getStorage({
      key: 'searchKeyObject',
      success: function (res) {
        searchKeyObject = res.data
      },
      fail: function () {
        app.ajax.reqPOST('/shoppingMall/searchKeyTreeGet', {//TODO:这里可以做大数据扩展    
        }, function (res) {
          if (!res || res.error == true) {//失败直接返回        
            return
          }
          searchKeyObject = res.data;
          wx.setStorage({
            key: 'searchKeyObject',
            data: res,
          })
        });
      }
    })
  },
  searchKeyListGet: function (searchKeyArr, input) {//搜索栏关键字列表获取
    var TempArr = searchKeyArr.map(item => {
      return {   
        id : item.id,
        listImageUrl : item.listImageUrl,
        listTitle : item.listTitle,
        evaluationCount : item.evaluationCount,
        evaluationPercent : item.evaluationPercent,
        price : item.price,
        unit : item.unit,
        sort: (item.listTitle.indexOf(input) == -1) ? 99999 : item.listTitle.indexOf(input)      
      }
    })
    var returnTemp = TempArr.sort(function (value1, value2) {
      if (value1.sort > value2.sort) {
        return 1;
      } else if (value1.sort < value2.sort) {
        return -1;
      } else {
        return 0;
      }
    }).slice(0, app.globalData.searchKeyDisplayNum);

    for (var i = 0; i <= returnTemp.length; i++) {//排除包含不匹配的情况
      if (returnTemp[i].sort == 99999) {
        if (i == 0) {
          return []
        }
        return returnTemp.slice(0, i)
      }
    }
    return returnTemp
  }
});
