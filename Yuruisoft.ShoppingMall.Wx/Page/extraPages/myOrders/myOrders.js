// myOrders.js
var sliderWidth = 96; // 需要设置slider的宽度，用于计算中间位置
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    tabs: ["所有订单", "待付款", "配送中", "待评价"],//商品收藏Tab
    activeIndex: 0,//商品详情Tab
    sliderOffset: 0,//商品详情Tab
    sliderLeft: 0,//商品详情Tab
  },
  /*商品详情Tab 开始*/
  tabClick: function (e) {
    this.setData({
      sliderOffset: e.currentTarget.offsetLeft,
      activeIndex: e.currentTarget.id
    });
  },
  /*商品详情Tab 结束*/
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    /*商品详情Tab 开始*/
    wx.getSystemInfo({
      success: function (res) {
        that.setData({
          sliderLeft: (res.windowWidth / that.data.tabs.length - sliderWidth) / 2,
          sliderOffset: res.windowWidth / that.data.tabs.length * that.data.activeIndex
        });
      }
    });
    /*商品详情Tab 结束*/
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    var that = this;
    var thirdSessionKey = wx.getStorageSync('thirdSessionKey');
    if (thirdSessionKey != null) {
      app.ajax.reqPost('/shoppingMall/orderInfoGet', {
        "thirdSessionKey": thirdSessionKey
      }, function (res) {
        if (!res || res.error == true) {//失败直接返回        
          return;
        }
        // 格式化
        var tempArr = res.myOrders;
        tempArr.forEach(item => {
          var temp;
          if (item.orderStatus == 0) {
            temp = '待付款';
          }
          else if (item.orderStatus == 1) {
            temp = '待发货';
          }
          else if (item.orderStatus == 1) {
            temp = '待确认收货';
          }
          else if (item.orderStatus == 1) {
            temp = '待评价';
          }
          else if (item.orderStatus == 1) {
            temp = '待再次购买';
          }
          item.orderStatus = temp;
        })

        that.setData({
          myOrders: tempArr
        })
      });
    }

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})