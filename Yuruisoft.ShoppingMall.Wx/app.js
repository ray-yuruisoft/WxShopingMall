var http = require('utils/CommonUtil.js')
App({
  ajax: {//网络请求函数
    reqPOST: http.reqPOST
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
    recommentListsNum: 5,
    searchKeyDisplayNum: 10
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
  }
});