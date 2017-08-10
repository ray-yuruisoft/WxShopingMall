// produceDetails.js
var app = getApp();
var sliderWidth = 96; // 需要设置slider的宽度，用于计算中间位置
var currentPrId;//当前页面商品ID
var currentshoppingCartNum;//当前商品的购物车数量
Page({

  /**
   * 页面的初始数据
   */
  data: {
    tabs: ["商品介绍", "规格参数", "售后保障"],//商品详情Tab
    activeIndex: 0,//商品详情Tab
    sliderOffset: 0,//商品详情Tab
    sliderLeft: 0,//商品详情Tab
    templateObject: {//规格参数表格
      listData: [
        { "th": "主体" },
        { "tdHead": "贮存条件", "tdBody": "深冷、冷冻 -18°C" },
        { "tdHead": "保质期", "tdBody": "7天" },
        { "tdHead": "净含量", "tdBody": "1.5kg" },
        { "th": "其他" },
        { "tdHead": "商品编号", "tdBody": "10401600506" }
      ]
    },
    merchantName: '',
    shoppingCartNum: (app.globalData.shoppingCart == '') ? 0 : app.globalData.shoppingCart.shoppingCartNum,//购物车总数
    produceDetails: {},
    produceTabInstruction: [],
    indicator_dots: true,//是否显示面板指示点
    indicator_color: 'rgba(0, 0, 0, .3)',
    indicator_active_color: 'red',
    autoplay: true,//是否自动切换
    interval: 3000,//自动切换时间间隔	
    duration: 1000,//滑动动画时长	

    icon_angleChooseNum: false,//商品数量栏隐藏图标
    chooseNum: 1,//商品选择的数量

    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n'
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
  angleTapChooseNum: function () {
    if (this.data.icon_angleChooseNum == false) {
      this.setData({
        icon_angleChooseNum: true
      })
    }
    else {
      this.setData({
        icon_angleChooseNum: false
      })
    }
  },
  /*数量选择组件 结束*/
  /*商品详情Tab 开始*/
  tabClick: function (e) {
    this.setData({
      sliderOffset: e.currentTarget.offsetLeft,
      activeIndex: e.currentTarget.id
    });
  },
  /*商品详情Tab 结束*/

  shoppingCartTap: function (e) {
    console.log(e);
    wx.switchTab({
      url: '../../shoppingCart/shoppingCart',
    })
  },
  AddshoppingCart: function (e) {
    var that = this;
    var chooseNum = this.data.chooseNum;
    if (this.data.shoppingCartNum != 999) {
      var temp = this.data.shoppingCartNum + chooseNum;
      this.setData({
        shoppingCartNum: temp
      })

      var testExist = 0;
      if (app.globalData.shoppingCart != '') {
        app.globalData.shoppingCart.shoppingCartNum = temp;
        app.globalData.shoppingCart.detail.forEach(item => {
          if (item.id == currentPrId) {
            item.shoppingCartNum = item.shoppingCartNum + chooseNum;
            testExist++;
          }
        })
        if (testExist == 0) {
          app.globalData.shoppingCart.detail.push(
            {
              id: currentPrId,
              merchantId: that.data.merchantId,
              merchantName: that.data.merchantName,
              shoppingCartNum: chooseNum
            }
          )
        }
      }
      else//初始化购物车数据结构
      {
        app.globalData.shoppingCart = {
          shoppingCartNum: temp,
          detail: [{
            id: currentPrId,
            merchantId: that.data.merchantId,
            merchantName: that.data.merchantName,
            shoppingCartNum: temp
          }]
        }
      }

      wx.setStorage({//异步缓存
        key: 'shoppingCart',
        data: app.globalData.shoppingCart
      })
    }
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    currentPrId = options.id

    if (app.globalData.shoppingCart != '') {
      this.setData({
        shoppingCartNum: app.globalData.shoppingCart.shoppingCartNum
      })
      app.globalData.shoppingCart.detail.forEach(item => {
        if (item.id == currentPrId) {
          currentshoppingCartNum = item.shoppingCartNum;
        }
      })
    }


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
      var produceTabInstruction = res.detailTabInstructionImageUrl.map((item, index) => {
        return {
          id: index,
          InstructionUrl: res.bannerImageDic + item
        }
      })

      that.setData({
        title: res.title,
        merchantId: res.merchantId,
        merchantName: res.merchantName,
        price: res.price.toFixed(2),
        unit: res.unit,
        produceDetails: produceDetails,
        produceTabInstruction: produceTabInstruction
      })
    });
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