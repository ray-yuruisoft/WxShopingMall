var http = require('utils/CommonUtil.js')
var search = require('utils/searchUtil.js')
var calculate = require('utils/calculate.js')
App({
  ajax: {//网络请求函数
    reqPOST: http.reqPOST
  },
  search: {//关于搜索组件的函数
    searchKeyListGet: search.searchKeyListGet
  },
  com: {//一般工具
    add: calculate.add,
    sub: calculate.sub,
    mul: calculate.mul,
    div: calculate.div
  },
  onLaunch: function () {
    console.log('App Launch')
    this.checkVersion();//先检查版本是否正确，不正确自动删除,并调用searchKeyObjectGet
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
  searchKeyObjectGet: function (that) {//搜索栏关键字对象获取
    wx.getStorage({
      key: 'searchKeyObject',
      success: function (res) {
        that.globalData.searchKeyObject = res.data
      },
      fail: function () {
        http.reqPOST('/shoppingMall/searchKeyTreeGet', {//TODO:这里可以做大数据扩展    
        }, function (res) {
          if (!res || res.error == true) {//失败直接返回
            return
          }

          that.globalData.searchKeyObject = res;
          wx.setStorage({
            key: 'searchKeyObject',
            data: res,
          })
        });
      }
    })
  },
  checkVersion: function () {//比较缓存版本是否更新，更新了则丢弃原来的
    var that = this;
    wx.getStorage({
      key: 'searchKeyObject',
      success: function (resStorage) {
        http.reqPOST('/shoppingMall/verifyVersion', {
        }, function (res) {
          if (!res || res.error == true) {//失败直接返回  
            that.searchKeyObjectGet(that);
            return
          }
          if (resStorage.data.searchKeyArrayVersion != res.version) {
            wx.removeStorageSync("searchKeyObject")
          }
          that.searchKeyObjectGet(that);
        });
      },
      fail: function () {
        that.searchKeyObjectGet(that);
      }
    })
  },
  init: function () {

  }
});