// MerCollection.js
var app = getApp();
var sliderWidth = 96; // 需要设置slider的宽度，用于计算中间位置

Page({

  /**
   * 页面的初始数据
   */
  data: {
    chooseAllFlag: false,
    editState: false,
    tabs: ["商品", "搭配"],//商品收藏Tab
    activeIndex: 0,//商品详情Tab
    sliderOffset: 0,//商品详情Tab
    sliderLeft: 0,//商品详情Tab

    MerCollection: [],
    startX: 0, //开始坐标
    startY: 0
  },
  navigation: function (e) {
    var id = e.currentTarget.id;
    wx.navigateTo({
      url: '../produceDetails/produceDetails?id=' + id,
    })
  },
  cancleCollect: function () {
    if (this.data.MerCollection.length == 0) { return }

    var tempData = [];
    if (!this.data.chooseAllFlag) {
      this.data.MerCollection.forEach(item => {
        if (!item.choosedFlag) {
          tempData.push(item);
        }
      });
    }
    this.setData({
      MerCollection: tempData,
      chooseAllFlag: false
    })
    app.globalData.MerCollection = tempData;
    wx.setStorageSync('MerCollection', tempData);
  },

  chooseAll: function (e) {

    if (this.data.MerCollection.length == 0) { return }

    var temp = !this.data.chooseAllFlag;
    this.data.MerCollection.forEach(item => {
      item.choosedFlag = temp;
    })
    this.setData({
      MerCollection: this.data.MerCollection,
      chooseAllFlag: temp
    });
  },
  checkChoosedState: function () {
    var temp = true;
    this.data.MerCollection.forEach(item => {
      if (item.choosedFlag == false) {
        temp = false;
      }
    })
    this.setData({
      chooseAllFlag: temp
    })
  },
  produceChoose: function (e) {
    var index = e.currentTarget.dataset.index;
    this.data.MerCollection[index].choosedFlag = !this.data.MerCollection[index].choosedFlag;
    this.setData({
      MerCollection: this.data.MerCollection
    })
    this.checkChoosedState();
  },
  editChange: function (e) {
    this.data.MerCollection.forEach(item => {
      item["choosedFlag"] = false;
    })
    this.setData({
      chooseAllFlag: false,
      editState: e.detail.value,
      MerCollection: this.data.MerCollection
    })
  },
  /*商品详情Tab 开始*/
  tabClick: function (e) {
    this.setData({
      sliderOffset: e.currentTarget.offsetLeft,
      activeIndex: e.currentTarget.id
    });
  },
  /*商品详情Tab 结束*/

  //手指触摸动作开始 记录起点X坐标
  touchstart: function (e) {
    //开始触摸时 重置所有删除
    this.data.MerCollection.forEach(function (v, i) {
      if (v.isTouchMove)//只操作为true的
        v.isTouchMove = false;
    })
    this.setData({
      startX: e.changedTouches[0].clientX,
      startY: e.changedTouches[0].clientY,
      MerCollection: this.data.MerCollection
    })
  },
  //滑动事件处理
  touchmove: function (e) {
    var that = this,
      index = e.currentTarget.dataset.index,//当前索引
      startX = that.data.startX,//开始X坐标
      startY = that.data.startY,//开始Y坐标
      touchMoveX = e.changedTouches[0].clientX,//滑动变化坐标
      touchMoveY = e.changedTouches[0].clientY,//滑动变化坐标
      //获取滑动角度
      angle = that.angle({ X: startX, Y: startY }, { X: touchMoveX, Y: touchMoveY });
    that.data.MerCollection.forEach(function (v, i) {
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
      MerCollection: that.data.MerCollection
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
    this.data.MerCollection.splice(e.currentTarget.dataset.index, 1)
    this.setData({
      MerCollection: this.data.MerCollection
    })
    app.globalData.MerCollection = this.data.MerCollection;
    wx.setStorageSync('MerCollection', app.globalData.MerCollection);
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    var MerCollection = app.globalData.MerCollection;
    if (MerCollection != '' && MerCollection.length != 0) {

      this.setData({
        MerCollection: MerCollection
      })
    }

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