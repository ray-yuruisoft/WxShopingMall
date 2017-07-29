var app = getApp();

Page({
  onLoad: function (options) {
    // 页面初始化 options为页面跳转所带来的参数  
    this.setData({
      autodisplayNum: app.globalData.autodisplayNum
    })   
  },
  saveValue : function(e){
    app.globalData.autodisplayNum = e.detail.value
  },
  onUnload: function (e) {
    // Do something when hide.
    console.log(e)
    wx.setStorage({
      key: "autodisplayNum",
      data: app.globalData.autodisplayNum
    })
  },
});
