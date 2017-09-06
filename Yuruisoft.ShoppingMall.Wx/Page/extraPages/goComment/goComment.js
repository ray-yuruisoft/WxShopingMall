// Page/extraPages/goComment/goComment.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    checkboxItems: [
      { name: '匿名发表', value: '0', checked: true }
    ],

    files: []//上传组件
    
  },

  checkboxChange: function (e) {
    console.log('checkbox发生change事件，携带value值为：', e.detail.value);

    var checkboxItems = this.data.checkboxItems, values = e.detail.value;
    for (var i = 0, lenI = checkboxItems.length; i < lenI; ++i) {
      checkboxItems[i].checked = false;

      for (var j = 0, lenJ = values.length; j < lenJ; ++j) {
        if (checkboxItems[i].value == values[j]) {
          checkboxItems[i].checked = true;
          break;
        }
      }
    }

    this.setData({
      checkboxItems: checkboxItems
    });
  },

  angleTapChoose: function (e) {
    var x = e.currentTarget.dataset.xindex;
    var y = e.currentTarget.dataset.yindex;
    var orderInfo = this.data.orderInfo;
    orderInfo.detail[x].produceArr[y].icon_angleChoose = !orderInfo.detail[x].produceArr[y].icon_angleChoose;
    this.setData({
      orderInfo: orderInfo
    });
  },
  commentStar: function (e) {
    var id = e.currentTarget.id;
    var x = e.currentTarget.dataset.xindex;
    var y = e.currentTarget.dataset.yindex;
    var orderInfo = this.data.orderInfo; 
    orderInfo.detail[x].produceArr[y].commentStarCount = id;
    this.setData({
      orderInfo: orderInfo
    })
  },
  // 上传组件 开始
  chooseImage: function (e) {
    var that = this;
    wx.chooseImage({
      sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
      sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
      success: function (res) {
        // 返回选定照片的本地文件路径列表，tempFilePath可以作为img标签的src属性显示图片
        that.setData({
          files: that.data.files.concat(res.tempFilePaths)
        });
      }
    })
  },
  previewImage: function (e) {
    wx.previewImage({
      current: e.currentTarget.id, // 当前显示图片的http链接
      urls: this.data.files // 需要预览的图片http链接列表
    })
  },
  // 上传组件 结束
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var orderInfo = JSON.parse(options.data);
    orderInfo.detail.forEach(item => {
      item.produceArr.forEach(itemBottom => {
        itemBottom["icon_angleChoose"] = false;
        itemBottom["commentStarCount"] = 5;
      });
    });
    this.setData({
      orderInfo: orderInfo
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