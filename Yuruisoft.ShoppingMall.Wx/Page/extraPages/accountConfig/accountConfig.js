// accountConfig.js
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    emailShow: '',
    listArr: []
  },
  logOut: function (e) {

    wx.showModal({
      title: '确定要退出登录？',
      content: '退出登录后，将无法共享好万家账户的信息',
      confirmText: '保持登录',
      confirmColor: '#18BC9C',
      cancelText: '退出登录',
      success: function (res) {
        if (res.confirm) {
          return;
        } else if (res.cancel) {
          app.globalData.account = undefined;
          app.globalData.password = undefined;
          app.globalData.email = undefined;
          app.globalData.phoneNumber = undefined;

          wx.removeStorageSync('account');
          wx.removeStorageSync('sessionData');
          wx.removeStorageSync('email');
          wx.removeStorageSync('phoneNumber');

          wx.switchTab({
            url: '../../userInfo/userInfo',
          })
        }
      }
    })
    console.log(e);
  },
  modifyPhoneNum: function (e) {
    wx.navigateTo({
      url: '../modifyPhoneNum/modifyPhoneNum',
    })
  },
  userAddressConfig: function (e) {
    wx.navigateTo({
      url: '../chooseAddress/chooseAddress',
    })
   },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var userInfo = app.globalData.userInfo;
    var account = app.globalData.account;
    var email = app.globalData.email;
    var phoneNumber = app.globalData.phoneNumber;

    if (email) {
      var emailLenth = email.split('@')[0].length;
      var showLength = Math.round(emailLenth * 0.4);
      var showAfer = '';
      for (var i = 0; i < (emailLenth - showLength); i++) {
        showAfer += '*';
      }
      email = email.slice(0, showLength) + showAfer + '@' + email.split('@')[1];
    }

    if (phoneNumber) {
      var phoneNum = phoneNumber.toString();
      var before = phoneNum.slice(0, 3);
      var afer = phoneNum.slice(8, 11);
      phoneNum = before + '*****' + afer;
    }

    var temp = [
      {
        tapFunction: 'modifyPhoneNum',
        title: '修改绑定手机号码',
        content: phoneNum
      },
      {
        tapFunction: '',
        title: '修改登录密码',
        content: ''
      },
      {
        tapFunction: '',
        title: '支付密码管理',
        content: ''
      },
      {
        tapFunction: 'userAddressConfig',
        title: '收货地址管理',
        content: ''
      },
      {
        tapFunction: '',
        title: '账户申述',
        content: ''
      },
      {
        tapFunction: '',
        title: '好万家会员',
        content: ''
      },
      {
        tapFunction: '',
        title: '实名认证',
        content: ''
      },
      {
        tapFunction: 'logOut',
        title: '退出登录',
        content: ''
      }
    ]



    this.setData({
      userInfo: userInfo,
      account: account,
      emailShow: email,
      listArr: temp
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