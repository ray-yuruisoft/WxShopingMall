// userInfo.js
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {  
    userInfo: {},
    listTypeOne: {
      listArr: [{
        id:1,
        title: "我的订单",
        checkName: "查看全部订单",
        listArr: [{
          id: 1,
          iconName: "icon-credit-card-alt",
          icontitle: "待付款"
        },{
          id:2,
          iconName:"icon-truck",
          icontitle: "待收货"
        },
        {
          id:3,
          iconName:"icon-comments-o",
          icontitle:"待评价"
        },{
          id:4,
          iconName:"icon-undo",
          icontitle:"退换/售后"
        }]
      }]
    },

    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n',
  },


  listTypeOneTap: function (e) {

    console.log(e);

  },
  listTypeOneItemTap: function (e) {

    console.log(e);

  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var userInfo = app.globalData.userInfo;
    this.setData({
      userInfo: userInfo
    })
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