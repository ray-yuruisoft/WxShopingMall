// check.js
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    shoppingCart: {},

    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n',
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function (e) {
    var that = this;
    var temp;
    var shoppingCartTemp = app.globalData.shoppingCartTemp;
    if (shoppingCartTemp != undefined && shoppingCartTemp != '') {
      temp = shoppingCartTemp;
      app.globalData.shoppingCartTemp = '';
    }
    else {
      if (app.globalData.shoppingCart != '') {
        temp = app.globalData.shoppingCart;
        temp.detail.forEach(item => {
          item.produceArr.forEach(itemBottom => {
            if (itemBottom.choosedFlag) {
              item.choosedFlag = true;
            }
          })
        })
      }
    }

    that.setData({
      shoppingCart: temp
    })

//这里很奇怪！估计是微信小程序的一个BUG，app.globalData.shoppingCart没有被赋值，仍然会被修改，这里只能强行改回来！
    app.globalData.shoppingCart = app.com.checkAllFee(app.globalData.shoppingCart);

    var tempAddress = app.globalData.userAddress;
    if (tempAddress != [] && tempAddress != undefined) {
      var tempData;
      tempAddress.forEach(item => {
        if (item.checked) {
          tempData = item;
        }
      })
      var tempPhoneNumber = tempData.phoneNumber;
      tempPhoneNumber = tempPhoneNumber.slice(0, 3) + '****' + tempPhoneNumber.slice(7, 11);
      that.setData({
        tempPhoneNumber: tempPhoneNumber,
        userAddress: tempData
      })
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