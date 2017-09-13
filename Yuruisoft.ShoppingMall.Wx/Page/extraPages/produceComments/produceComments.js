// Page/extraPages/produceComments/produceComments.js
var app = getApp();
var sliderWidth = 96; // 需要设置slider的宽度，用于计算中间位置
Page({

  /**
   * 页面的初始数据
   */
  data: {
    activeIndex: 0,//商品详情Tab
    sliderOffset: 0,//商品详情Tab
    sliderLeft: 0,//商品详情Tab
  },
  previewImage: function (e) {
    var index = e.currentTarget.id;
    var objsIndex = e.currentTarget.dataset.objsindex;
    wx.previewImage({
      current: this.data.evaluationObjs.commentDetail[objsIndex].evaluationImages[index], // 当前显示图片的http链接
      urls: this.data.evaluationObjs.commentDetail[objsIndex].evaluationImages// 需要预览的图片http链接列表
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
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    /*商品详情Tab 开始*/
    wx.getSystemInfo({
      success: function (res) {
        that.setData({
          sliderLeft: (res.windowWidth / 5 - sliderWidth) / 2,
          sliderOffset: res.windowWidth / 5 * that.data.activeIndex,
          activeIndex: that.data.activeIndex
        });
      }
    });
    /*商品详情Tab 结束*/
    app.ajax.reqPost('/shoppingMall/commentsGet', {
      produceId: options.proId
    }, function (res) {
      if (!res || res.error) {//失败直接返回        
        return
      }
      var tabs = [{ title: "全部", content: res.goodCommentCount }, { title: "好评", content: res.badCommentCount }, { title: "中评", content: res.normalCommentCount }, { title: "差评", content: res.badCommentCount }, { title: "晒单", content: res.imageShowCount }]
      that.setData({
        evaluationObjs: res,
        tabs: tabs//商品收藏Tab
      });    
    });
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