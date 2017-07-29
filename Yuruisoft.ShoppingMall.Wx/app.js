var http = require('utils/CommonUtil.js')
var Sele = require('utils/DealTheUrlUtil.js')
App({
  ajax: {//网络请求函数
    reqPOST: http.reqPOST
  },
  com:{
    dealUrl: Sele.SelectUrl
  },
  onLaunch: function () {
    console.log('App Launch')
  },
  onShow: function () {
    console.log('App Show')
  },
  onHide: function () {
    console.log('App Hide')
  },
  globalData: {
    hasLogin: false,
    autodisplayNum: 5
  }
});