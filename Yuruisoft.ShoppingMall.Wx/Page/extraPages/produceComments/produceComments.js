// Page/extraPages/produceComments/produceComments.js
var app = getApp();
var sliderWidth = 96; // 需要设置slider的宽度，用于计算中间位置
var produceId;
/**  
 * post 请求加载评价列表数据   
 * "page" ：页数  
 * "pageSize" ：每页数量  
 */
// 当前页数
var pageIndexGood = 1, pageIndexNormal = 1, pageIndexBad = 1, pageIndexImg = 1, pageIndexAll = 1;
var pageSize = 3;
var dataGet = (that, varName, pageIndex, kindOfComments) => {
  var comments = that.data[varName] == undefined ? [] : that.data[varName];
  app.ajax.reqPost('/shoppingMall/commentsGet', {
    produceId: produceId,
    pageIndex: pageIndex,
    pageSize: pageSize,
    kindOfComments: kindOfComments
  }, function (res) {
    if (!res || res.error) {//失败直接返回        
      return
    }
    comments = comments.concat(res);//连接下一页
    that.setData({
      [varName]: comments
    });
  });
}
var loadCommentData = function (that, kindOfComments) {
  // 获取上一页数据 
  if (kindOfComments == 0) {
    dataGet(that, "allComments", pageIndexAll, 0);
    pageIndexAll++;// 页数加一  
  } else if (kindOfComments == 1) {
    dataGet(that, "goodComments", pageIndexGood, 1);
    pageIndexGood++;// 页数加一  
  } else if (kindOfComments == 2) {
    dataGet(that, "normalComments", pageIndexNormal, 2);
    pageIndexNormal++;// 页数加一  
  } else if (kindOfComments == 3) {
    dataGet(that, "badComments", pageIndexBad, 3);
    pageIndexBad++;// 页数加一  
  } else if (kindOfComments == 4) {
    dataGet(that, "imgComments", pageIndexImg, 4);
    pageIndexImg++;// 页数加一  
  }
  wx.hideNavigationBarLoading() //完成停止加载
  wx.stopPullDownRefresh() //停止下拉刷新
}
Page({
  /**
   * 页面的初始数据
   */
  data: {
    activeIndex: 0,//商品详情Tab
    sliderOffset: 0,//商品详情Tab
    sliderLeft: 0,//商品详情Tab
  },
  /**
     * 页面相关事件处理函数--监听用户下拉动作
     */
  onPullDownRefresh: function () {
    wx.showNavigationBarLoading() //在标题栏中显示加载
    var that = this;
    // 刷新的准备工作，想将页数设置为一，然后清空文章列表信息，定位在距顶部为0的地方  
    if (that.data.activeIndex == 0) {
      pageIndexAll = 1;
      that.setData({ allComments: [] });
    } else if (that.data.activeIndex == 1) {
      pageIndexGood = 1;
      that.setData({ goodComments: [] });
    } else if (that.data.activeIndex == 2) {
      pageIndexNormal = 1;
      that.setData({ normalComments: [] });
    } else if (that.data.activeIndex == 3) {
      pageIndexBad = 1;
      that.setData({ badComments: [] });
    } else if (that.data.activeIndex == 4) {
      pageIndexImg = 1;
      that.setData({ imgComments: [] });
    }
    // 加载数据 
    loadCommentData(that, that.data.activeIndex);
  },
  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
    var that = this;
    loadCommentData(that, that.data.activeIndex);
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
    produceId = options.proId
    var tabs = [{ title: "全部", content: (options.evaluationCount == "null") ? 0 : options.evaluationCount }, { title: "好评", content: (options.goodCommentCount == "null") ? 0 : options.goodCommentCount }, { title: "中评", content: (options.normalCommentCount == "null") ? 0 : options.normalCommentCount }, { title: "差评", content: (options.badCommentCount == "null") ? 0 : options.badCommentCount }, { title: "晒单", content: (options.commentWithImgCount == "null") ? 0 : options.commentWithImgCount }]
    that.setData({
      tabs: tabs//商品收藏Tab
    });
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
    loadCommentData(that, 0);
    loadCommentData(that, 1);
    loadCommentData(that, 2);
    loadCommentData(that, 3);
    loadCommentData(that, 4);
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
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})