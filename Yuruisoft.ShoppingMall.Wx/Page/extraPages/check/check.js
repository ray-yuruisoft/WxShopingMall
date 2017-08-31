// check.js
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    myOrders: {},

    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n',
  },
  showToastWrong: function (title) {
    wx.showToast({
      title: title,
      image: '../../../style/images/replaceIcon/exclamation-sign.png'
    })
  },
  orderCheck: function () {
    var myOrders = this.data.myOrders;
    var orderAddress = this.data.userAddress;
    myOrders['carriage'] = '15.00';
    myOrders['deliveryCity'] = orderAddress.city;
    myOrders['deliveryAddress'] = orderAddress.address;
    myOrders['deliveryName'] = orderAddress.name;
    myOrders['deliveryPhoneNumber'] = orderAddress.phoneNumber;
    return myOrders;
  },
  weiChatPay: function () {
    if (this.data.userAddress == undefined) {
      this.showToastWrong('还没有设置收货地址');
      return;
    }
    var myOrders = this.orderCheck();
    var thirdSessionKey = wx.getStorageSync('thirdSessionKey');
    app.ajax.reqPost('/shoppingMall/placeAnOrder', {
      myOrders: myOrders,
      thirdSessionKey: thirdSessionKey
    }, function (res) {
      if (!res || res.error == true) {//失败直接返回        
        return
      }
    });
  },
  COD: function () { },


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
      }
    }
    // 订单数据结构 构造开始
    var detail = temp.detail.map(item => {
      var tempData = item.produceArr.filter((itemBottom) => {
        if (itemBottom.choosedFlag)
          return {};
      });
      if (tempData.length != 0)
        return {
          allItemCount: item.chooseItemCount,
          merchantId: item.merchantId,
          merchantName: item.merchantName,
          feeSum: item.feeSum,
          produceArr: tempData
        };
    });
    detail = detail.filter(item => {
      if (item != undefined)
        return {}
    });
    detail.forEach(item => {
      item.produceArr = item.produceArr.map(itemBottom => {
        return {
          id: itemBottom.id,
          itemCount: itemBottom.itemCount,
          listTitle: itemBottom.listTitle,
          listImageUrl: itemBottom.listImageUrl,
          price: itemBottom.price
        }
      })
    })
    var myOrders = {
      allItemCount: temp.chooseItemCount,
      feeSum: temp.feeSum,
      detail: detail
    }
    that.setData({
      myOrders: myOrders
    })
    // 订单数据结构 构造结束
    var tempAddress = app.globalData.userAddress;
    if (tempAddress != undefined && tempAddress.length != 0) {
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