// myOrders.js
var sliderWidth = 96; // 需要设置slider的宽度，用于计算中间位置
var app = getApp();
//JSON.parse(str); //由JSON字符串转换为JSON对象
//JSON.stringify(obj);//由JSON对象转换为JSON字符串
Page({

  /**
   * 页面的初始数据
   */
  data: {
    tabs: ["所有订单", "待付款", "配送中", "待评价"],//商品收藏Tab
    activeIndex: 0,//商品详情Tab
    sliderOffset: 0,//商品详情Tab
    sliderLeft: 0,//商品详情Tab

    startX: 0, //开始坐标
    startY: 0
  },
  /*商品详情Tab 开始*/
  tabClick: function (e) {
    this.setData({
      sliderOffset: e.currentTarget.offsetLeft,
      activeIndex: e.currentTarget.id
    });
  },
  /*商品详情Tab 结束*/

  //手指触摸动作开始 记录起点X坐标 所有订单
  touchstart: function (e) {
    //开始触摸时 重置所有删除
    this.data.myOrders.forEach(function (v, i) {
      if (v.isTouchMove == undefined || v.isTouchMove)//只操作为true的
        v.isTouchMove = false;
    })
    try {
      this.setData({
        startX: e.changedTouches[0].clientX,
        startY: e.changedTouches[0].clientY,
        myOrders: this.data.myOrders
      })
    } catch (e) {
      return;
    }
  },
  //滑动事件处理
  touchmove: function (e, orderName) {
    var that = this,
      index = e.currentTarget.dataset.index,//当前索引
      startX = that.data.startX,//开始X坐标
      startY = that.data.startY,//开始Y坐标
      touchMoveX = e.changedTouches[0].clientX,//滑动变化坐标
      touchMoveY = e.changedTouches[0].clientY,//滑动变化坐标
      //获取滑动角度
      angle = that.angle({ X: startX, Y: startY }, { X: touchMoveX, Y: touchMoveY });
    that.data.myOrders.forEach(function (v, i) {
      v.isTouchMove = false
      //滑动超过30度角 return
      if (Math.abs(angle) > 30) return;
      if (i == index) {
        if (touchMoveX > startX) //右滑
          v.isTouchMove = false
        else //左滑
          v.isTouchMove = true
      }
    })
    //更新数据
    that.setData({
      myOrders: that.data.myOrders
    })
  },
  /**
   * 计算滑动角度
   * @param {Object} start 起点坐标
   * @param {Object} end 终点坐标
   */
  angle: function (start, end) {
    var _X = end.X - start.X,
      _Y = end.Y - start.Y
    //返回角度 /Math.atan()返回数字的反正切值
    return 360 * Math.atan(_Y / _X) / (2 * Math.PI);
  },
  //删除事件
  del: function (e) {
    var that = this;
    var index = e.currentTarget.dataset.index;
    var orderStatus = that.data.myOrders[index].orderStatus;
    if (orderStatus == '待再次购买' || orderStatus == '待付款') {
      wx.showModal({
        content: '确定要删除吗',
        confirmText: "确定",
        confirmColor: "#18BC9C",
        cancelText: "取消",
        success: function (res) {
          if (res.confirm) {
            var orderNumber = that.data.myOrders[index].orderNumber;
            that.data.myOrders.splice(index, 1);
            app.ajax.reqPost('/shoppingMall/orderInfoDelete', {
              "orderNumber": orderNumber
            }, function (res) {
              if (!res || res.error == true) {//失败直接返回        
                return;
              }
              if (res.error == false) {
                that.setData({
                  myOrders: that.data.myOrders
                });
              }
            });
          } else {
            return;
          }
        }
      });
    }
    else {
      wx.showModal({
        content: '该状态下订单不能被删除',
        confirmColor: "#18BC9C",
        showCancel: false,
        success: function (res) {
          if (res.confirm) {
            return;
          }
        }
      });
    }
  },
  goPay: function (e) {
    var index = e.currentTarget.id;
    var that = this;
    that.data.myOrders[index].detail.forEach(item => {
      item["choosedFlag"] = true;
      item["feeSum"] = 0;
      item.produceArr.forEach(itemBottom => {
        itemBottom["choosedFlag"] = true;
        itemBottom["feeSum"] = app.com.mul(parseFloat(itemBottom.price), itemBottom.itemCount);
        item.feeSum = app.com.add(item.feeSum, itemBottom.feeSum);
      })
    })
    var shoppingCartTemp = {
      "allItemCount": that.data.myOrders[index].allItemCount,
      "chooseItemCount": that.data.myOrders[index].allItemCount,
      "feeSum": that.data.myOrders[index].feeSum,
      "choosedFlag": true,
      "detail": that.data.myOrders[index].detail
    }
    app.globalData.shoppingCartTemp = shoppingCartTemp;
    wx.navigateTo({
      url: '../check/check'
    })
  },
  goComment: function (e) {
    var index = e.currentTarget.id;
    var orderInfo = this.data.myOrders[index];
    delete orderInfo.isTouchMove;
    wx.navigateTo({
      url: '../goComment/goComment?data=' + JSON.stringify(orderInfo)
    });
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    that.data.activeIndex = options.id;
    /*商品详情Tab 开始*/
    wx.getSystemInfo({
      success: function (res) {
        that.setData({
          sliderLeft: (res.windowWidth / that.data.tabs.length - sliderWidth) / 2,
          sliderOffset: res.windowWidth / that.data.tabs.length * that.data.activeIndex,
          activeIndex: that.data.activeIndex
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

  orderStatusFormat: function (tempArr) {
    tempArr.forEach(item => {
      var temp;
      if (item.orderStatus == 0) {
        temp = '待付款';
      }
      else if (item.orderStatus == 1) {
        temp = '待发货';
      }
      else if (item.orderStatus == 2) {
        temp = '待确认收货';
      }
      else if (item.orderStatus == 3) {
        temp = '待评价';
      }
      else if (item.orderStatus == 4) {
        temp = '待再次购买';
      }
      else if (item.orderStatus == 5) {
        temp = '待审核';
      }
      else if (item.orderStatus == 6) {
        temp = '审核不通过';
      }
      else if (item.orderStatus == 7) {
        temp = '商家退换，待买家发货';
      }
      else if (item.orderStatus == 8) {
        temp = '待商家收货';
      }
      else if (item.orderStatus == 9) {
        temp = '商家退款，待买家发货';
      }
      else if (item.orderStatus == 10) {
        temp = '待退款';
      }
      else if (item.orderStatus == 11) {
        temp = '待好万家介入';
      }
      else if (item.orderStatus == '待付款') {
        temp = 0;
      }
      else if (item.orderStatus == '待发货') {
        temp = 1;
      }
      else if (item.orderStatus == '待确认收货') {
        temp = 2;
      }
      else if (item.orderStatus == '待评价') {
        temp = 3;
      }
      else if (item.orderStatus == '待再次购买') {
        temp = 4;
      }
      else if (item.orderStatus == '待审核') {
        temp = 5;
      }
      else if (item.orderStatus == '审核不通过') {
        temp = 6;
      }
      else if (item.orderStatus == '商家退换，待买家发货') {
        temp = 7;
      }
      else if (item.orderStatus == '待商家收货') {
        temp = 8;
      }
      else if (item.orderStatus == '商家退款，待买家发货') {
        temp = 9;
      }
      else if (item.orderStatus == '待退款') {
        temp = 10;
      }
      else if (item.orderStatus == '待好万家介入') {
        temp = 11;
      }
      item.orderStatus = temp;
    });
    return tempArr;
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
        that.setData({
          myOrders: that.orderStatusFormat(res.myOrders)
        });
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