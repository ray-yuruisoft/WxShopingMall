// produceDetails.js
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    produceDetails: {},
    indicator_dots: true,//是否显示面板指示点
    indicator_color: 'rgba(0, 0, 0, .3)',
    indicator_active_color: 'red',
    autoplay: true,//是否自动切换
    interval: 3000,//自动切换时间间隔	
    duration: 1000,//滑动动画时长	
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    console.log(options)
    var that = this;
    app.ajax.reqPOST('/shoppingMall/produceDetailGet', {//TODO:这里可以做大数据扩展
      "id": options.id,//TODO:用户信息,调整推荐策略
    }, function (res) {
      if (!res || res.error == true) {//失败直接返回        
        return
      }

      console.log(res)
      var produceDetails = res.bannerImages.map((item, index) => {
        return {
          id: index,
          bannerImageUrl: res.bannerImageDic + item
        }
      })
      that.setData({
        title: res.title,
        price: res.price.toFixed(2),
        unit: res.unit,
        produceDetails: produceDetails
      })
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