// shoppingCart.js
var app = getApp();
var shoppingCartSave = function (shoppingCart) {
  app.globalData.shoppingCart = shoppingCart;
  wx.setStorageSync('shoppingCart', shoppingCart);
};
var currentForInput = {};
Page({
  /**
   * 页面的初始数据
   */
  data: {
    shoppingCart: {},
    totalCount: 0,
    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n',
    icon_angleChooseNum: false//商品数量栏隐藏图标
  },

  //手指触摸动作开始 记录起点X坐标
  touchstart: function (e) {
    //开始触摸时 重置所有删除
    this.data.shoppingCart.detail.forEach(item => {
      item.produceArr.forEach(function (v, i) {
        v['isTouchMove'] = false;
      });
    })

    this.setData({
      startX: e.changedTouches[0].clientX,
      startY: e.changedTouches[0].clientY,
      shoppingCart: this.data.shoppingCart
    })
  },
  //滑动事件处理
  touchmove: function (e) {
    var that = this,
      index = e.currentTarget.dataset.index,//当前索引
      merchantIndex = e.currentTarget.dataset.merchantindex,//商户号数组索引
      startX = that.data.startX,//开始X坐标
      startY = that.data.startY,//开始Y坐标
      touchMoveX = e.changedTouches[0].clientX,//滑动变化坐标
      touchMoveY = e.changedTouches[0].clientY,//滑动变化坐标
      //获取滑动角度
      angle = that.angle({ X: startX, Y: startY }, { X: touchMoveX, Y: touchMoveY });

    that.data.shoppingCart.detail[merchantIndex].produceArr.forEach(function (v, i) {
      v.isTouchMove = false
      //滑动超过30度角 return
      if (Math.abs(angle) > 30) return;
      if (i == index) {
        if (touchMoveX > startX) //右滑
          v.isTouchMove = false
        else //左滑
          v.isTouchMove = true
      }
    });

    //更新数据
    that.setData({
      shoppingCart: that.data.shoppingCart
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
    var shoppingCart = this.data.shoppingCart;
    var index = e.currentTarget.dataset.index;
    var merchantIndex = e.currentTarget.dataset.merchantindex;
    if (shoppingCart.detail[merchantIndex].produceArr.length == 1)
      shoppingCart.detail.splice(merchantIndex, 1);
    else
      shoppingCart.detail[merchantIndex].produceArr.splice(index, 1);



    shoppingCart = app.com.checkAllFee(shoppingCart);
    this.setData({
      shoppingCart: shoppingCart
    })
    app.globalData.shoppingCart = shoppingCart;
    shoppingCartSave(shoppingCart);

  },
  check: function () {
    var temp = this.data.shoppingCart.chooseItemCount;
    if (temp != undefined && temp > 0)
      wx.navigateTo({
        url: '../extraPages/check/check',
      })
  },
  chooseAll: function () {
    var chooseStatue = !this.data.shoppingCart.choosedFlag;
    var shoppingCart = this.data.shoppingCart;
    shoppingCart.detail.forEach(item => {//只需三层中的所有置为选中即可
      item.produceArr.forEach(itemBottom => {
        itemBottom.choosedFlag = chooseStatue;
      })
    });
    shoppingCart = app.com.checkAllFee(shoppingCart);
    this.setData({
      shoppingCart: shoppingCart
    })
    app.globalData.shoppingCart = shoppingCart;
    shoppingCartSave(shoppingCart);
  },
  navigation: function (e) {
    var id = e.currentTarget.id;
    wx.navigateTo({
      url: '../extraPages/produceDetails/produceDetails?id=' + id,
    })
  },
  /* 数量选择组件 开始*/
  doChooseNum: function (id, merchantId, price, operNum) {//商品加减处理函数
    var shoppingCart = this.data.shoppingCart;
    shoppingCart.detail.forEach(item => {
      if (item.merchantId == merchantId) {
        item.produceArr.forEach(itemBottom => {
          if (itemBottom.id == id) {
            itemBottom.choosedFlag = true;
            itemBottom.itemCount += operNum;
          }
        })
      }
    })
    shoppingCart = app.com.checkAllFee(shoppingCart);//强制选择会引发没有check项目，先check一下
    this.setData({
      shoppingCart: shoppingCart
    })
    shoppingCartSave(shoppingCart);
  },
  plusChooseNum: function (e) {//商品选择加号
    if (e.currentTarget.dataset.itemcount >= 999)
      return;
    var id = e.currentTarget.id;
    var merchantId = e.currentTarget.dataset.merchantid;
    var price = e.currentTarget.dataset.price;
    this.doChooseNum(id, merchantId, price, 1);
  },
  minusChooseNum: function (e) {//商品选择减号
    if (e.currentTarget.dataset.itemcount <= 1)
      return;
    var id = e.currentTarget.id;
    var merchantId = e.currentTarget.dataset.merchantid;
    var price = e.currentTarget.dataset.price;
    this.doChooseNum(id, merchantId, price, -1);
  },
  inputChooseNum: function (e) {//保存原有值
    currentForInput = {
      id: e.currentTarget.id,
      merchantId: e.currentTarget.merchantid,
      value: e.detail.value
    }
  },
  inputChooseNumBlur: function (e) {//input失去焦点触发
    var input = (e.detail.value <= 0 || e.detail.value == '') ? 1 : parseInt(e.detail.value);
    var originValue = currentForInput.value;
    var id = e.currentTarget.id;
    var merchantId = e.currentTarget.dataset.merchantid;
    var price = e.currentTarget.dataset.price;
    this.doChooseNum(id, merchantId, price, input - originValue);
  },
  nothingToDo: function (e) {//避免点击向导航传递
  },
  /*数量选择组件 结束*/

  merchantChoose: function (e) {//商户选中入口
    var merchantId = e.currentTarget.id;
    var chooseItemState;
    var shoppingCart = this.data.shoppingCart;
    shoppingCart.detail.forEach(item => {
      if (item.merchantId == merchantId) {
        item.produceArr.forEach(itemBottom => {
          itemBottom.choosedFlag = !item.choosedFlag;
        })
      }
    })
    shoppingCart = app.com.checkAllFee(shoppingCart);//结算所有费用和选中项
    this.setData({
      shoppingCart: shoppingCart
    })
    shoppingCartSave(shoppingCart);
  },
  produceChoose: function (e) {//商品选中入口
    var merchantId = e.currentTarget.dataset.merchantid;
    var id = e.currentTarget.id;
    var shoppingCart = this.data.shoppingCart;
    shoppingCart.detail.forEach(item => {
      if (item.merchantId == merchantId) {
        item.produceArr.forEach(itemBottom => {
          if (itemBottom.id == id) {
            itemBottom.choosedFlag = !itemBottom.choosedFlag;
          }
        })
      }
    })
    shoppingCart = app.com.checkAllFee(shoppingCart);//结算所有费用和选中项
    this.setData({
      shoppingCart: shoppingCart
    })
    shoppingCartSave(shoppingCart);
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
  onShow: function () {
    if (app.globalData.shoppingCart != '') {
      this.setData({
        shoppingCart: app.globalData.shoppingCart
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