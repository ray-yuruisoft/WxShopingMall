// userInfo.js
var app = getApp();
var passwordLockTime = 6;
var session;
Page({

  /**
   * 页面的初始数据
   */
  data: {
    vcodeImg: "",
    eysNoOpen: true,
    account: undefined,
    showModalStatus: false,//自定义模态框
    userInfo: {},
    bannerListArr: [{
      id: 1,
      iconName: "icon-weixin",
      icontitle: "微信在线",
      badgeNum: 0
    }, {
      id: 2,
      iconName: "icon-mobile",
      icontitle: "直通电话",
      badgeNum: 0
    },
    {
      id: 3,
      iconName: "icon-envelope",
      icontitle: "留言反馈",
      badgeNum: 0
    }, {
      id: 4,
      iconName: "icon-ellipsis",
      icontitle: "其他服务",
      badgeNum: 0
    }],

    listTypeOne: {
      listArr: [{
        functionName: "myOrders",
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
        functionName: 'MerCollection',
        title: "商品收藏",
        count: 0
      }, {
        functionName: '',
        title: "店铺收藏",
        count: 1
      },
      {
        functionName: '',
        title: "搭配收藏",
        count: 0
      }, {
        functionName: '',
        title: "我的足迹",
        count: 10
      }]

    },
    hanSpace: '\r\n\r\n\r\n\r\n',//空格输出
    space: '\r\n',
  },

  myOrders: function () {
    wx.navigateTo({
      url: '../extraPages/myOrders/myOrders'
    })
  },
  accountConfig: function () {
    wx.navigateTo({
      url: '../extraPages/accountConfig/accountConfig'
    })
  },
  validateCodeGet: function () {
    var that = this;
    app.ajax.reqPost('/shoppingMall/validateCodeGet', {
    }, function (res) {
      if (!res || res.error == true) {//失败直接返回        
        return
      }
      session = res.session;
      that.setData({
        vcodeImg: res.base64Image
      })
    });
  },
  validateCodeCheck: function (e) {
    var input = e.detail.value;
    this.setData({
      inputVcode: input
    })
  },
  inputClear: function () {
    this.setData({
      inputAccount: ''
    });
  },
  inputEyeOpen: function () {
    var temp = !this.data.eysNoOpen;
    this.setData({
      eysNoOpen: temp
    });
  },
  showToastWrong: function (title) {
    wx.showToast({
      title: title,
      image: '../../style/images/replaceIcon/exclamation-sign.png'
    })
  },
  showFindPassModel: function () {
    this.util('close');
    wx.showModal({
      title: '账号或密码不正确',
      confirmText: "找回密码",
      confirmColor: '#18BC9C',
      cancelText: "确定",
      success: function (res) {
        console.log(res);
        if (res.confirm) {

        } else {

        }
      }
    });
  },
  showFindPassWordModel: function () {
    wx.showModal({
      title: '账号或密码多次不正确，请尝试找回密码',
      confirmText: "找回密码",
      confirmColor: '#18BC9C',
      cancelText: "确定",
      success: function (res) {
        console.log(res);
        if (res.confirm) {

        } else {

        }
      }
    });
  },
  formSubmit: function (e) {
    var that = this;
    var name = e.detail.value.name;
    var password = e.detail.value.password;
    var isEmail = app.com.regexEmail(name);
    var isPhoneNum = app.com.regexPhoneNum(name);
    var thirdSessionKey = wx.getStorageSync('thirdSessionKey');
    var vCode = e.detail.value.vCode;

    app.ajax.reqPost('/shoppingMall/login', {
      name: name,
      password: password,
      isEmail: isEmail,
      isPhoneNum: isPhoneNum,
      thirdSessionKey: thirdSessionKey,
      vCode: vCode
    }, function (res) {
      if (!res) {//失败直接返回        
        return
      }
      if (res.error) {
        if (res.failCount < 3) {
          if (res.error == 'NAMEWRONG') {
            that.showToastWrong('账号或密码错误');
            return;
          }
          if (res.error == 'PASSWORDWRONG') {
            if (passwordLockTime == 0) {//密码输入错误6次
              that.showFindPassModel();
              return;
            }
            if (passwordLockTime > 3) {
              that.showToastWrong('账号或密码错误');
              passwordLockTime--;
              return;
            }
            that.showToastWrong('密码错误，您还可以尝试' + passwordLockTime + '次');
            passwordLockTime--;
            return;
          }
        }
        if (res.failCount >= 3) {
          that.validateCodeGet();
          that.setData({
            vCodeInputShow: true
          })

          if (res.error == 'NAMEWRONG') {
            that.showToastWrong('账号或密码错误');
            return;
          }
          if (res.error == 'PASSWORDWRONG') {
            if (passwordLockTime == 0) {//密码输入错误6次
              that.showFindPassModel();
              return;
            }
            if (passwordLockTime > 3) {
              that.showToastWrong('账号或密码错误');
              passwordLockTime--;
              return;
            }
            that.showToastWrong('密码错误，您还可以尝试' + passwordLockTime + '次');
            passwordLockTime--;
            return;
          }
          if (res.error == 'VCODEWRONG') {
            that.showToastWrong('验证码错误');
            return;
          }
        }
        that.showToastWrong('网络或其他错误');
        return;
      }
      that.setData({
        inputAccount: '',
        inputPassword: ''
      });
      app.globalData.account = res.account;
      that.setData({
        account: res.account
      })

      that.util('close');
      wx.setStorage({
        key: 'account',
        data: res.account,
      })

      wx.setStorage({
        key: 'sessionData',
        data: res.password
      })
      app.globalData.password = res.password
      wx.setStorage({
        key: 'phoneNumber',
        data: res.phoneNumber
      })
      app.globalData.phoneNumber = res.phoneNumber
      if (res.email != '') {
        wx.setStorage({
          key: 'email',
          data: res.email
        })
        app.globalData.email = res.email
      }

    }, session);
  },
  inputAccount: function (e) {
    var input = e.detail.value;
    this.setData({
      inputAccount: input
    })
  },
  inputPassword: function (e) {
    var password = e.detail.value;
    this.setData({
      inputPassword: password
    })
  },
  passwordForgot: function (e) { },
  register: function (e) {
    this.setData({
      showModalStatus: false
    })
    wx.navigateTo({
      url: '../extraPages/registerPage/registerPage',
    })
  },
  powerDrawer: function (e) {

    if (this.data.account) {
      return;
    }
    if (passwordLockTime == 0) {
      this.showFindPassWordModel();
      return;
    }
    var currentStatu = e.currentTarget.dataset.statu;
    this.util(currentStatu)
  },
  util: function (currentStatu) {
    /* 动画部分 */
    // 第1步：创建动画实例   
    var animation = wx.createAnimation({
      duration: 200,  //动画时长  
      timingFunction: "linear", //线性  
      delay: 0  //0则不延迟  
    });

    // 第2步：这个动画实例赋给当前的动画实例  
    this.animation = animation;

    // 第3步：执行第一组动画  
    animation.opacity(0).rotateX(-100).step();

    // 第4步：导出动画对象赋给数据对象储存  
    this.setData({
      animationData: animation.export()
    })

    // 第5步：设置定时器到指定时候后，执行第二组动画  
    setTimeout(function () {
      // 执行第二组动画  
      animation.opacity(1).rotateX(0).step();
      // 给数据对象储存的第一组动画，更替为执行完第二组动画的动画对象  
      this.setData({
        animationData: animation
      })

      //关闭  
      if (currentStatu == "close") {
        this.setData(
          {
            showModalStatus: false
          }
        );
      }
    }.bind(this), 200)
    // 显示  
    if (currentStatu == "open") {
      this.validateCodeGet();
      this.setData(
        {
          showModalStatus: true
        }
      );
    }
  },
  /**
   * 
   */

  MerCollection: function (e) {
    wx.navigateTo({
      url: '../extraPages/MerCollection/MerCollection',
    })
  },

  listTypeOneTap: function (e) {

    console.log(e);

  },
  listTypeOneItemTap: function (e) {

    console.log(e);

  },
  bannerItemTap: function (e) {

    if (e.currentTarget.id == 2) {
      wx.makePhoneCall({
        phoneNumber: '15308202328' //仅为示例
      })
    }



  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var userInfo = app.globalData.userInfo;
    this.setData({
      userInfo: userInfo
    })

    var that = this;
    app.ajax.reqPost('/shoppingMall/recommentListsGet', {//TODO:这里可以做大数据扩展
      "userInfo": "",//TODO:用户信息,调整推荐策略
      "takeNum": app.globalData.recommentListsNum,
      "prefers": ""  //TODO:用户喜好,调整推荐策略
    }, function (res) {
      if (!res || res.error == true) {//失败直接返回        
        return
      }
      var temp = res.map(item => {//格式化数字
        return {
          id: item.id,
          listImageUrl: item.listImageUrl,
          listTitle: item.listTitle,
          evaluationCount: item.evaluationCount,
          evaluationPercent: item.evaluationPercent.toFixed(2),
          price: item.price.toFixed(2),
          unit: item.unit
        }
      })
      that.setData({
        recommentLists: temp
      })

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
  onShow: function (e) {
    var account = app.globalData.account
    if (account == undefined) {
      this.setData({
        account: ''
      })
    } else {
      this.setData({
        account: account
      })
    }
    var MerCollection = app.globalData.MerCollection;
    if (MerCollection != '' && MerCollection.length != 0) {

      var listTypeTwo = this.data.listTypeTwo;
      listTypeTwo.listArr[0].count = MerCollection.length;
      this.setData({
        listTypeTwo: listTypeTwo
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