// shoppingCart.js
var app = getApp();
var shoppingCartSave = function (shoppingCart) {
  app.globalData.shoppingCart = shoppingCart;
  wx.setStorageSync('shoppingCart', shoppingCart);
};
Page({

  /**
   * 页面的初始数据
   */
  data: {
    chooseAllOn: false,
    shoppingCart: {},
    totalCount: 0,
    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n',

    icon_angleChooseNum: false,//商品数量栏隐藏图标

  },
  chooseAll: function () {
    var chooseStatue = !this.data.chooseAllOn;
    var shoppingCart = this.data.shoppingCart;
    shoppingCart.choosedFlag = chooseStatue;
    shoppingCart.detail.forEach(item => {
      item.choosedFlag = chooseStatue;
      item.produceArr.forEach(itemBottom => {
        itemBottom.choosedFlag = chooseStatue;
      })
    });
    this.setData({
      chooseAllOn: chooseStatue,
      shoppingCart: shoppingCart
    })
    app.globalData.shoppingCart = shoppingCart;

  },
  navigation: function (e) {
    var id = e.currentTarget.id;
    wx.navigateTo({
      url: '../extraPages/produceDetails/produceDetails?id=' + id,
    })
  },

  /* 数量选择组件 开始*/
  doChooseNum: function (id, merchantId, price, operNum) {
    this.doProduceChoose(id, merchantId);//强制为选中状态，并且选中数目要加上

    //分三层处理
    var shoppingCart = this.data.shoppingCart;
    //第一层处理
    shoppingCart.allItemCount += operNum;
    shoppingCart.chooseItemCount += operNum;
    var temp = app.com.mul(operNum, parseFloat(price));
    shoppingCart.feeSum = app.com.add(parseFloat(shoppingCart.feeSum), temp).toFixed(2);

    shoppingCart.detail.forEach(item => {
      if (item.merchantId == merchantId) {
        //第二层处理
        item.allItemCount += operNum;
        item.chooseItemCount += operNum;
        item.feeSum = app.com.add(parseFloat(item.feeSum), temp).toFixed(2);

        item.produceArr.forEach(itemBottom => {
          if (itemBottom.id == id) {
            //第三层处理
            itemBottom.itemCount += operNum;
          }
        })
      }
    })

    this.setData({
      shoppingCart: shoppingCart
    })
    shoppingCartSave(shoppingCart);
  },

  plusChooseNum: function (e) {//商品选择加号
    if (e.currentTarget.dataset.itemcount == 999)
      return;
    var id = e.currentTarget.id;
    var merchantId = e.currentTarget.dataset.merchantid;
    var price = e.currentTarget.dataset.price;
    this.doChooseNum(id, merchantId, price, 1);
  },
  minusChooseNum: function (e) {//商品选择减号
    if (e.currentTarget.dataset.itemcount == 1)
      return;
    var id = e.currentTarget.id;
    var merchantId = e.currentTarget.dataset.merchantid;
    var price = e.currentTarget.dataset.price;
    this.doChooseNum(id, merchantId, price, -1);
  },
  inputChooseNum: function (e) {//TODO未设置
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
  merchantChoose: function (e) {

    var merchantId = e.currentTarget.id;
    var chooseItemState;
    var shoppingCart = this.data.shoppingCart;
    shoppingCart.detail.forEach(item => {
      if (item.merchantId == merchantId) {
        item.choosedFlag = !item.choosedFlag;
        chooseItemState = item.choosedFlag;
        item.produceArr.forEach(itemBottom => {
          itemBottom.choosedFlag = item.choosedFlag;
        })
      }
    })

    this.doChooseAll(chooseItemState);
    shoppingCart.choosedFlag = this.data.chooseAllOn;
    this.setData({
      shoppingCart: shoppingCart
    })
    shoppingCartSave(shoppingCart);
  },
  produceChoose: function (e) {
    var merchantId = e.currentTarget.dataset.merchantid;
    var id = e.currentTarget.id;
    var shoppingCart = this.data.shoppingCart;
    var chooseItemState;
    shoppingCart.detail.forEach(item => {
      if (item.merchantId == merchantId) {
        item.produceArr.forEach(itemBottom => {
          if (itemBottom.id == id) {
            itemBottom.choosedFlag = !itemBottom.choosedFlag;
            chooseItemState = itemBottom.choosedFlag;
          }
        })
        var tempFlag = true;
        if (item.choosedFlag)//商家栏选中状态，一种情况取消
        {
          if (chooseItemState == false) {
            item.choosedFlag = false;
          }
        }
        else {//商家栏未选中状态，一种情况选中
          for (var i = 0; i < item.produceArr.length; i++) {
            if (item.produceArr[i].choosedFlag == false) {
              tempFlag = false;
              break;
            }
          }
          if (tempFlag) {
            item.choosedFlag = true;
          }
        }
      }
    })
    this.doChooseAll(chooseItemState);
    shoppingCart.choosedFlag = this.data.chooseAllOn;
    this.setData({
      shoppingCart: shoppingCart
    })
    shoppingCartSave(shoppingCart);
  },

  doProduceChoose: function (id, merchantId) {
    var shoppingCart = this.data.shoppingCart;
    var chooseItemState;
    shoppingCart.detail.forEach(item => {
      if (item.merchantId == merchantId) {
        item.produceArr.forEach(itemBottom => {
          if (itemBottom.id == id) {
            itemBottom.choosedFlag = true;
            chooseItemState = true;
          }
        })
        var tempFlag = true;

        //商家栏未选中状态，一种情况选中
        for (var i = 0; i < item.produceArr.length; i++) {
          if (item.produceArr[i].choosedFlag == false) {
            tempFlag = false;
            break;
          }
        }
        if (tempFlag) {
          item.choosedFlag = true;
        }

      }
    })
    this.doChooseAll(chooseItemState);
    shoppingCart.choosedFlag = this.data.chooseAllOn;
    this.setData({
      shoppingCart: shoppingCart
    })
    shoppingCartSave(shoppingCart);
  },

  doChooseAll: function (itemState) {
    var chooseAllOn = this.data.chooseAllOn;
    var shoppingCart = this.data.shoppingCart;
    var testFlag = true;

    if (chooseAllOn)//全选被选中状态，一种情况取消
    {
      if (itemState == false) {
        this.setData({
          chooseAllOn: false
        })
      }
    }
    else {//全选未被选中状态，一种情况选中
      outer: for (var i = 0; i < shoppingCart.detail.length; i++) {
        for (var j = 0; j < shoppingCart.detail[i].produceArr.length; j++) {
          if (shoppingCart.detail[i].produceArr[j].choosedFlag == false) {
            testFlag = false;
            break outer;
          }
        }
      }
      if (testFlag) {
        this.setData({
          chooseAllOn: true
        })
      }
    }
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
        shoppingCart: app.globalData.shoppingCart,
        chooseAllOn: app.globalData.shoppingCart.choosedFlag
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