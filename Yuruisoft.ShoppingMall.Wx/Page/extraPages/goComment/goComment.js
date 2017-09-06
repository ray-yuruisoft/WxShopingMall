// Page/extraPages/goComment/goComment.js
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {

  },
  commentNow: function (e) {
    var x = e.currentTarget.dataset.xindex;
    var y = e.currentTarget.dataset.yindex;
    var that = this;

    var imageFile = this.data.orderInfo.detail[x].produceArr[y].imageFile;
    imageFile.forEach((item, index) => {
      const uploadTask = wx.uploadFile({
        url: app.globalData.servsers + '/shoppingMall/uploadCommentImages',
        filePath: item.filePath,
        name: 'file',
        header: {
          'haowanFamily': 'www.haowanFamily.com',
          'content-type': 'multipart/form-data',
        },
        formData: {
          "produceInfoId": 3
        },
        success: function (res) {
          var data = res.data

        },
        fail: function () {
          that.data.orderInfo.detail[x].produceArr[y].imageFile[index].uploadProgress = 0;
          that.data.orderInfo.detail[x].produceArr[y].imageFile[index].uploadSuccess = false;
          that.setData({
            orderInfo: that.data.orderInfo
          })
        }
      });
      uploadTask.onProgressUpdate((res) => {//自带异步回调
        that.data.orderInfo.detail[x].produceArr[y].imageFile[index].uploadProgress = res.progress;
        that.setData({
          orderInfo: that.data.orderInfo
        });
      });
    });
  },

  commentInput: function (e) {
    var x = e.currentTarget.dataset.xindex;
    var y = e.currentTarget.dataset.yindex;
    var value = e.detail.value;
    this.data.orderInfo.detail[x].produceArr[y].commentText = value;
    this.setData({
      orderInfo: this.data.orderInfo
    })
  },
  checkboxChange: function (e) {
    var x = e.currentTarget.dataset.xindex;
    var y = e.currentTarget.dataset.yindex;

    var checkboxItems = this.data.orderInfo.detail[x].produceArr[y].checkboxItems, values = e.detail.value;
    for (var i = 0, lenI = checkboxItems.length; i < lenI; ++i) {
      checkboxItems[i].checked = false;

      for (var j = 0, lenJ = values.length; j < lenJ; ++j) {
        if (checkboxItems[i].value == values[j]) {
          checkboxItems[i].checked = true;
          break;
        }
      }
    }
    this.data.orderInfo.detail[x].produceArr[y].checkboxItems = checkboxItems;
    this.setData({
      orderInfo: this.data.orderInfo
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
    var x = e.currentTarget.dataset.xindex;
    var y = e.currentTarget.dataset.yindex;
    var that = this;
    if (that.data.orderInfo.detail[x].produceArr[y].imageFile.length > 2) { return; }
    wx.chooseImage({
      sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
      sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
      success: function (res) {
        // 返回选定照片的本地文件路径列表，tempFilePath可以作为img标签的src属性显示图片
        that.data.orderInfo.detail[x].produceArr[y].imageFile = that.data.orderInfo.detail[x].produceArr[y].imageFile.concat(
          res.tempFilePaths.map(item => {
            return {
              filePath: item,
              uploadding: false,
              uploadProgress: 0,
              uploadSuccess: true
            };
          })
        );
        that.setData({
          orderInfo: that.data.orderInfo
        });
      }
    })
  },
  previewImage: function (e) {
    var x = e.currentTarget.dataset.xindex;
    var y = e.currentTarget.dataset.yindex;
    var imageFiles = this.data.orderInfo.detail[x].produceArr[y].imageFile.map(item => { return item.filePath });
    wx.previewImage({
      current: e.currentTarget.id, // 当前显示图片的http链接
      urls: imageFiles// 需要预览的图片http链接列表
    })
  },
  deleteImage: function (e) {
    var x = e.currentTarget.dataset.xindex;
    var y = e.currentTarget.dataset.yindex;
    var z = e.currentTarget.dataset.index;
    var that = this;
    var orderInfo = this.data.orderInfo
    wx.showModal({
      content: '确定要删除当前图片吗？',
      confirmText: "确定",
      cancelText: "取消",
      confirmColor: "#18BC9C",
      success: function (res) {
        if (res.confirm) {
          orderInfo.detail[x].produceArr[y].imageFile = orderInfo.detail[x].produceArr[y].imageFile.filter((item, index) => {
            if (index != z)
              return {};
          });
          that.setData({
            orderInfo: orderInfo
          });
        } else {
          return;
        }
      }
    });
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
        itemBottom["checkboxItems"] = [{ name: '匿名发表', value: '0', checked: false }];
        itemBottom["commentText"] = "";
        itemBottom["imageFile"] = [];
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