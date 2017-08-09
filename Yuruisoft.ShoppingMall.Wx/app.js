var http = require('utils/CommonUtil.js')
var search = require('utils/searchUtil.js')
App({
  ajax: {//网络请求函数
    reqPOST: http.reqPOST
  },
  search: {//关于搜索组件的函数
    searchKeyListGet: search.searchKeyListGet
  },
  onLaunch: function () {
    console.log('App Launch')
    this.checkVersion();
  },
  onShow: function () {
    console.log('App Show')
  },
  onHide: function () {
    console.log('App Hide')
  },
  globalData: {
    hasLogin: false,

    shoppingCart: wx.getStorageSync('shoppingCart'),//全局购物车
    recommentListsNum: 5,//推荐栏数量
    searchKeyDisplayNum: 10,//搜索关键字提示数量，主页带图片
    searchListPageSize: 6,//搜索结果分页显示，单页长度
    searchKeyObject: {}, //搜索关键字的对象
    searchKeyListNum: 5,//搜索关键字提示数量，搜索页纯单词
  },

  checkVersion: function () {//比较缓存版本是否更新，更新了则丢弃原来的
    wx.getStorage({
      key: 'searchKeyObject',
      success: function (resStorage) {
        http.reqPOST('/shoppingMall/verifyVersion', {
        }, function (res) {
          if (!res || res.error == true) {//失败直接返回        
            return
          }
          if (resStorage.data.searchKeyArrayVersion != res.version) {
            wx.removeStorageSync("searchKeyObject")
          }
        });
      },
    })
  },
  init: function(){  

  }
});