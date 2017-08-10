// shoppingCart.js
var app = getApp();
var shoppingCartGet = function (arrALLDetails, shoppingCartLess) {

  var arrNoLevel = shoppingCartLess.detail.map(itemshoppingCart => {
    var temp = {};
    arrALLDetails.forEach(itemAll => {
      if (itemshoppingCart.id == itemAll.id) {
        temp = {
          listImageUrl: itemAll.listImageUrl,
          listTitle: itemAll.listTitle,
          price: itemAll.price
        }
      }
    })
    if (temp != {})
      return {//得到一维数据记录所有值
        id: itemshoppingCart.id,
        merchantId: itemshoppingCart.merchantId,
        merchantName: itemshoppingCart.merchantName,
        shoppingCartNum: itemshoppingCart.shoppingCartNum,
        listImageUrl: temp.listImageUrl,
        listTitle: temp.listTitle,
        price: temp.price
      }
  });

  var tempReturn = arrNoLevel.map(item => {//得到重复的商家数组
    return {
      merchantId: item.merchantId,
      merchantName: item.merchantName,
      produceArr: []
    }
  })

  var tempNoRepeat = (function () {//去除数组中重复对象
    var unique = {};
    tempReturn.forEach(function (a) { unique[JSON.stringify(a)] = 1 });
    tempReturn = Object.keys(unique).map(function (u) { return JSON.parse(u) });
    return tempReturn
  })(tempReturn);

  tempNoRepeat.forEach(itemTop => {//经过处理，得到二维数组
    arrNoLevel.forEach(itemNext => {
      if (itemTop.merchantId == itemNext.merchantId) {
        itemTop.produceArr.push({
          id: itemNext.id,
          shoppingCartNum: itemNext.shoppingCartNum,
          listImageUrl: itemNext.listImageUrl,
          listTitle: itemNext.listTitle,
          price: itemNext.price
        })
      }
    })
  })
  return tempNoRepeat;
}

Page({

  /**
   * 页面的初始数据
   */
  data: {
    chooseAllOn: false,
    shoppingCart: [],
    totalCount: 0,
    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n',

    icon_angleChooseNum: false,//商品数量栏隐藏图标
    chooseNum: 1,//商品选择的数量
  },
  chooseAll: function () {
    if (!this.data.chooseAllOn) {
      this.setData({
        chooseAllOn: true
      })
    }
    else {
      this.setData({
        chooseAllOn: false
      })
    }
  },
  navigation: function (e) {
    var id = e.currentTarget.id;
    wx.navigateTo({
      url: '../extraPages/produceDetails/produceDetails?id=' + id,
    })
  },

  /* 数量选择组件 开始*/
  plusChooseNum: function () {//商品选择加号
    if (this.data.chooseNum == 999)
      return;
    var count = ++this.data.chooseNum;
    this.setData({
      chooseNum: count
    })
  },
  minusChooseNum: function () {//商品选择减号
    if (this.data.chooseNum == 1)
      return;
    var count = --this.data.chooseNum;
    this.setData({
      chooseNum: count
    })
  },
  inputChooseNum: function (e) {
    if (e.detail.value == 0)
      return;
    var temp = parseInt(e.detail.value);
    this.setData({
      chooseNum: temp
    })
  },
  inputChooseNumBlur: function (e) {
    if (e.detail.value == 0)
      this.setData({
        chooseNum: 1
      })
  },
  nothingToDo: function (e) {
    console.log(e);
  },
  /*数量选择组件 结束*/


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
      var tempShoppingCart = app.globalData.shoppingCart;
      var arrTemp = app.globalData.searchKeyObject.searchKeyArray;
      var shoppingCart = shoppingCartGet(arrTemp, tempShoppingCart);
      this.setData({
        shoppingCart: shoppingCart,
        totalCount: tempShoppingCart.shoppingCartNum
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