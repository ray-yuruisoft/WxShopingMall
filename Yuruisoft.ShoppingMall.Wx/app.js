var http = require('utils/CommonUtil.js')
var search = require('utils/searchUtil.js')
var calculate = require('utils/calculate.js')
App({
  ajax: {//网络请求函数
    reqPost: http.reqPost
  },
  search: {//关于搜索组件的函数
    searchKeyListGet: search.searchKeyListGet
  },
  com: {//一般工具
    add: calculate.add,
    sub: calculate.sub,
    mul: calculate.mul,
    div: calculate.div,
    regexEmail: http.regexEmail,
    regexAccount: http.regexAccount,
    regexPassword: http.regexPassword,
    regexPhoneNum: http.regexPhoneNum,
    regexNumber: http.regexNumber
  },
  onLaunch: function () {
    console.log('App Launch')
    this.init(this);//初始化工作

  },
  onShow: function () {
    console.log('App Show')
  },
  onHide: function () {
    console.log('App Hide')
  },
  globalData: {
    account: '',
    servsers: 'http://www.yurusoft.net',
    hasLogin: false,
    userInfo: {},
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
        http.reqPost('/shoppingMall/searchKeyTreeGet', {//TODO:这里可以做大数据扩展    
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
        http.reqPost('/shoppingMall/verifyVersion', {
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
  init: function (that) {
    that.checkVersion();//先检查版本是否正确，不正确自动删除,并调用searchKeyObjectGet
    wx.checkSession({//1、首先调用微信API，检查登录态是否过期
      success: function (e) {
        if (e.errMsg.split(':')[1] == 'ok') {

          //2、然后服务端通信，检查自定义登录态是否过期
          var thirdSessionKey = wx.getStorageSync('thirdSessionKey');
          if (thirdSessionKey != '') {
            http.reqPost('/shoppingMall/sessionCheck', {
              thirdSessionKey: thirdSessionKey
            }, res => {
              if (!res || res.error || !res.exist) {
                that.login(that);//（4）服务端对比失败，重新登录
              }
              else {//3、微信登录态并且自定义登录未过期，获取用户信息
                that.userInfoGet(that);
              }
            })
          } else {
            that.login(that);//（3）未获取到缓存，重新登录
          }
        } else {
          that.login(that);//（2）check成功,但未获取到成功回调信息，重新登录
        }
      },
      fail: function () {//（1）微信检查失败，重新登录
        that.login(that);
      }
    });
    that.globalVarInit(that);
  },
  login: function (that) {//丢弃所有SessionKey,重新登录
    wx.login({
      success: function (res) {
        if (res.code) {
          var code = res.code;
          wx.getUserInfo({
            success: function (res2) {
              that.globalData.userInfo = res2.userInfo;
              http.Login(code, res2.encryptedData, res2.iv, res2.rawData, res2.signature);
            }
          })
        }
      }
    });
  },
  userInfoGet: function (that) {
    wx.getUserInfo({
      success: function (res) {
        that.globalData.userInfo = res.userInfo
        console.log(that.globalData.userInfo);
      }
    })
  },

  globalVarInit: function (that) {

    var account = wx.getStorageSync('account');
    if (account)
      that.globalData.account = account;
  }
});