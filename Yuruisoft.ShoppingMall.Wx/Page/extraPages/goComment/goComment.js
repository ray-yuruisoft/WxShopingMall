// Page/extraPages/goComment/goComment.js
var app = getApp();

Page({

  /**
   * 页面的初始数据
   */
  data: {
  },
  showToastWrong: function (title) {
    wx.showToast({
      title: title,
      image: '../../../style/images/replaceIcon/exclamation-sign.png'
    })
  },
  showToastSuccess: function (title) {
    wx.showToast({
      title: title,
      icon: 'success'
    })
  },
  commentNow: function (e) {
    var that = this;
    for (var i = 0; i < that.data.orderInfo.detail.length; i++) {
      for (var j = 0; j < that.data.orderInfo.detail[i].produceArr.length; j++) {
        if (that.data.orderInfo.detail[i].produceArr[j].commentText.length < 6 || that.data.orderInfo.detail[i].produceArr[j].commentText.length > 500) {
          that.showToastWrong("字数要在6-500之间");
          return;
        }
      }
    }
    var result = undefined;
    for (var x = 0; x < that.data.orderInfo.detail.length; x++) {
      for (var y = 0; y < that.data.orderInfo.detail[x].produceArr.length; y++) {
        result = that.commentServerUpdate(that, x, y);//这里只有失败才有返回值
      }
    }
    if (result == undefined) {
      app.ajax.reqPost('/shoppingMall/commentComplete', {//所有上传成功，准备更新数据库
        orderNumber: that.data.orderInfo.orderNumber
      }, function (res) {
        if (!res || res.error == true) {//失败直接返回     
          that.showToastWrong("错误代码101");
          return false;
        }
        wx.showModal({
          content: '评价成功',
          showCancel: false,
          confirmColor: '#18BC9C',
          success: function (res) {
            if (res.confirm) {
              wx.navigateBack({});
            }
          }
        });
      });
    }
  },
  commentServerUpdate: function (that, x, y) {
    var imageFile = that.data.orderInfo.detail[x].produceArr[y].imageFile;
    var produceInfo = that.data.orderInfo.detail[x].produceArr[y];
    var contentJson = {
      produceInfoId: produceInfo.id,
      commentStarCount: produceInfo.commentStarCount,
      isAnonymous: produceInfo.checkboxItems[0].checked,
      commentText: produceInfo.commentText,
      imageFile: imageFile.map(item => {
        return item.filePath
      })
    };
    var imageUpSuc = false;
    if (imageFile.length != 0)
      imageFile.forEach((item, index) => {
        const uploadTask = wx.uploadFile({
          url: app.globalData.servsers + '/shoppingMall/uploadCommentImages',
          filePath: item.filePath,
          name: 'file',
          header: {
            'haowanFamily': 'www.haowanFamily.com',
            'content-type': 'multipart/form-data'
          },
          formData: {
            index: index,
            contentJson: JSON.stringify(contentJson),
            orderNumber: that.data.orderInfo.orderNumber,
            merchantInfoId: that.data.orderInfo.detail[x].merchantId
          },
          success: function (res) {
            var data = JSON.parse(res.data);
            if ((!data.error) && imageUpSuc && data.updateDb) {//服务器存储错误

              app.ajax.reqPost('/shoppingMall/commentSubmit', {//所有上传成功，准备更新数据库
                contentJson: contentJson,
                orderNumber: that.data.orderInfo.orderNumber,
                merchantInfoId: that.data.orderInfo.detail[x].merchantId
              }, function (res) {
                if (!res || res.error == true) {//失败直接返回     
                  that.showToastWrong("错误代码102");
                  return false;
                }
              });
            }
          },
          fail: function () {
            uploadTask.abort();
            that.data.orderInfo.detail[x].produceArr[y].imageFile[index].uploadProgress = 0;
            that.data.orderInfo.detail[x].produceArr[y].imageFile[index].uploadSuccess = false;
            that.setData({
              orderInfo: that.data.orderInfo
            })
            that.showToastWrong("图片上传失败");
            return false;
          }
        });
        uploadTask.onProgressUpdate((res) => {//自带异步回调
          that.data.orderInfo.detail[x].produceArr[y].imageFile[index].uploadProgress = res.progress;
          that.setData({
            orderInfo: that.data.orderInfo
          });
          // 所有图片上传成功后，可以更新数据库 开始
          var successCount = 0;
          var imageFile = that.data.orderInfo.detail[x].produceArr[y].imageFile;
          for (var i = 0; i < imageFile.length; i++) {
            if (imageFile[i].uploadProgress == 100)
              successCount++;
          }
          if (successCount == imageFile.length) { imageUpSuc = true; }
          // 所有图片上传成功后，可以更新数据库 结束
        });
      });
    else {
      app.ajax.reqPost('/shoppingMall/commentSubmit', {
        contentJson: contentJson,
        orderNumber: that.data.orderInfo.orderNumber,
        merchantInfoId: that.data.orderInfo.detail[x].merchantId
      }, function (res) {
        if (!res || res.error == true) {//失败直接返回
          that.showToastWrong("错误代码103");
          return false;
        }
      });
    }
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
        if (that.data.orderInfo.detail[x].produceArr[y].imageFile.length > 3) {
          that.data.orderInfo.detail[x].produceArr[y].imageFile = that.data.orderInfo.detail[x].produceArr[y].imageFile.slice(0, 3)
        }
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