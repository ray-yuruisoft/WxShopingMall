// userInfo.js
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    userInfo: {},
    bannerListArr: [{
      id: 1,
      iconName: "icon-weixin",
      icontitle: "微信在线",
      badgeNum: 9
    }, {
      id: 2,
      iconName: "icon-mobile",
      icontitle: "拨打电话",
      badgeNum: 1
    },
    {
      id: 3,
      iconName: "icon-envelope",
      icontitle: "发送邮件",
      badgeNum: 9
    }, {
      id: 4,
      iconName: "icon-ellipsis",
      icontitle: "其他服务",
      badgeNum: 0
    }],

    listTypeOne: {
      listArr: [{
        id: 1,
        title: "我的订单",
        checkName: "查看全部订单",
        listArr: [{
          id: 1,
          iconName: "icon-credit-card-alt",
          icontitle: "待付款",
          badgeNum: 10
        }, {
          id: 2,
          iconName: "icon-truck",
          icontitle: "待收货",
          badgeNum: 1
        },
        {
          id: 3,
          iconName: "icon-comments-o",
          icontitle: "待评价",
          badgeNum: 9
        }, {
          id: 4,
          iconName: "icon-wrench",
          icontitle: "退换/售后",
          badgeNum: 0
        }]
      }]
    },
    listTypeTwo: {
      title: "我的关注",
      listArr: [{
        id: 1,
        title: "商品收藏",
        count: 0
      }, {
        id: 2,
        title: "店铺收藏",
        count: 1
      },
      {
        id: 3,
        title: "搭配收藏",
        count: 0
      }, {
        id: 4,
        title: "我的足迹",
        count: 10
      }]

    },
    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n',
  },

  listTypeTwoItemTap: function (e) { },
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