// produceDetails.js
var app = getApp();
var sliderWidth = 96; // 需要设置slider的宽度，用于计算中间位置

var currentshoppingCartNum;//当前商品的购物车数量
Page({

  /**
   * 页面的初始数据
   */
  data: {
    produceDetail: {},//商品详细对象
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
    shoppingCartItemCount: (app.globalData.shoppingCart == '') ? 0 : app.globalData.shoppingCart.allItemCount,//购物车总数


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
    wx.switchTab({
      url: '../../shoppingCart/shoppingCart',
    })
  },
  AddshoppingCart: function (e) {
    var that = this;
    var chooseNum = this.data.chooseNum;//商品选中的数量

    if (this.data.shoppingCartItemCount != 999) {

      // 在该商品没有被加入购物车的情况下，该商品的初始化
      var price = that.data.produceDetail.price;//先计算出价格
      var feeSum = app.com.mul(parseFloat(price), chooseNum);
      var produceObject = {
        id: that.data.produceDetail.id,
        itemCount: chooseNum,//商品总数
        listImageUrl: that.data.produceDetail.listImageUrl,
        listTitle: that.data.produceDetail.title,
        price: price,//商品单价
        feeSum: feeSum.toFixed(2),//商品总价
        choosedFlag: true//是否被选中
      }

      var testExist = 0;//有这个商品的情况和没有这个商品的情况测试！
      var testMerchanIdExist = 0;
      if (app.globalData.shoppingCart != '') {//分两种情况

        var tempShoppingCart = app.globalData.shoppingCart;

        tempShoppingCart.allItemCount += chooseNum;//第一层级先改变
        tempShoppingCart.chooseItemCount += chooseNum;
        tempShoppingCart.feeSum = app.com.add(parseFloat(tempShoppingCart.feeSum), parseFloat(feeSum)).toFixed(2);

        tempShoppingCart.detail.forEach(item => {
          if (item.merchantId == that.data.produceDetail.merchantId) {

            item.allItemCount += chooseNum;//第二层级再改变
            item.chooseItemCount += chooseNum;
            item.feeSum = app.com.add(parseFloat(item.feeSum), parseFloat(feeSum)).toFixed(2);
            testMerchanIdExist++;
            item.produceArr.forEach(itemBottom => {
              if (itemBottom.id == that.data.produceDetail.id) {//有这个商品的情况和没有这个商品的情况
                itemBottom.itemCount += chooseNum;//第三层级再改变
                itemBottom.feeSum = app.com.add(parseFloat(itemBottom.feeSum), parseFloat(feeSum)).toFixed(2);
                testExist++;
              }
            })
            if (testExist == 0) {//没有这种商品的情况,直接PUSH
              item.produceArr.push(produceObject);
            }
          }
        })
        if (testMerchanIdExist == 0) {//还有一种情况是 另一个商家 第一次进来   
          var produceArr = [];
          produceArr.push(produceObject);
          var detailObject = {
            merchantId: that.data.produceDetail.merchantId,//商户ID
            merchantName: that.data.produceDetail.merchantName,//商户名字

            allItemCount: chooseNum,//该商户下，所有商品总数
            chooseItemCount: chooseNum,//该商户下，所有被选中的商品数
            feeSum: feeSum.toFixed(2),//该商户下，所有被选中商品总价
            choosedFlag: true,//该商户下，所有是否被选中

            produceArr: produceArr
          }
          tempShoppingCart.detail.push(detailObject);
        }
        app.globalData.shoppingCart = tempShoppingCart;
      }
      else//初始化购物车数据结构
      {//对第一次加入的购物车来说，加入的所有数量都是一样的
        var initCount = chooseNum;

        var produceArr = [];
        produceArr.push(produceObject);

        app.globalData.shoppingCart = {
          //...省略会员ID和会员名，做会员时候，加上 TODO:
          allItemCount: initCount,//该购物车下，所有商品总数
          chooseItemCount: initCount,//该购物车下，所有被选中商品总数         
          feeSum: feeSum.toFixed(2),//该购物车下，所有被选中商品总价
          choosedFlag: true,//该购物车下，所有是否被选中

          detail: [{
            merchantId: that.data.produceDetail.merchantId,//商户ID
            merchantName: that.data.produceDetail.merchantName,//商户名字

            allItemCount: initCount,//该商户下，所有商品总数
            chooseItemCount: initCount,//该商户下，所有被选中的商品数
            feeSum: feeSum.toFixed(2),//该商户下，所有被选中商品总价
            choosedFlag: true,//该商户下，所有是否被选中

            produceArr: produceArr
          }]
        }
      }

      this.setData({//必须要单独设置一个购物车数量显示变量，初始化才不出错
        shoppingCartItemCount: app.globalData.shoppingCart.allItemCount
      })
      
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
    var currentPrId = options.id

    if (app.globalData.shoppingCart != '') {//购物车数据结构初始化
      this.setData({
        shoppingCart: app.globalData.shoppingCart,
        shoppingCartItemCount: app.globalData.shoppingCart.allItemCount
      })
    }

    var that = this;
    app.ajax.reqPOST('/shoppingMall/produceDetailGet', {//TODO:这里可以做大数据扩展
      "id": currentPrId,//TODO:用户信息,调整推荐策略
    }, function (res) {
      if (!res || res.error == true) {//失败直接返回        
        return
      }
      that.setData({
        produceDetail: res
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