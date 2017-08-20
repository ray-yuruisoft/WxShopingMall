// registerPage.js
var app = getApp();
var session;
Page({
  data: {
    showTopTipsFail: false,

    showEmailvertify: false,
    countryCodes: ["+86", "+80", "+84", "+87"],
    countryCodeIndex: 0,
    isAgree: false,

    emailIsRight: undefined,

    phoneNumIsRight: undefined,
    phoneNumIsEmpty: undefined,

    passwordInputSuccess: undefined,
    passwordIsRight: undefined,
    passwordIsEmpty: undefined,
    passwordLengthRight: undefined,

    accountNoRepeat: undefined,
    accountIsRight: undefined,
    accountIsEmpty: undefined,
    accountLengthRight: undefined,
    accountInputSuccess: undefined,

    registerButtonDisabled: true
  },
  local: {
    account: undefined,
    password: undefined,
    phoneNum: undefined,
    email: undefined,
    vCode: undefined
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
  vCode: function (e) {
    var input = e.detail.value;
    var that = this;
    if (input == '') {//1、空
      that.setData({
        vCodeInputSuccess: false
      })
      that.checkForm();
      return;
    }
    app.ajax.reqPost('/shoppingMall/checkVCode', {
      vCode: input,
    }, function (res) {//2、是否正确
      if (!res || res.error) {
        that.setData({
          vCodeInputSuccess: false
        })
        that.validateCodeGet();
        that.checkForm();
        return;
      }
      that.setData({
        vCodeInputSuccess: true
      })
      that.local.vCode = input;
      that.checkForm();
    }, session)
  },
  vPhoneNum: function (e) {
    var input = e.detail.value;
    var that = this;
    if (input == '') {//1、空
      that.setData({
        phoneNumIsEmpty: true,
        phoneNumInputSuccess: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      phoneNumIsEmpty: false
    })

    if (!app.com.regexPhoneNum(input)) {//2、格式
      that.setData({
        phoneNumIsRight: false,
        phoneNumInputSuccess: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      phoneNumIsRight: true
    })

    app.ajax.reqPost('/shoppingMall/checkPhoneNumRepeat', {
      phoneNum: input,
    }, function (res) {//3、是否重复
      if (!res) {
        that.setData({
          phoneNumNoRepeat: false
        })
      }
      that.setData({
        phoneNumNoRepeat: !res.error,
        phoneNumInputSuccess: !res.error
      })
      that.local.phoneNum = input;
      that.checkForm();
    })

  },
  vPassword: function (e) {
    var input = e.detail.value;
    var that = this;
    if (input == '') {//1、空
      that.setData({
        passwordIsEmpty: true,
        passwordInputSuccess: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      passwordIsEmpty: false
    })

    if (input.length < 6 || input.length > 20) {//2、长度
      that.setData({
        passwordLengthRight: false,
        passwordInputSuccess: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      passwordLengthRight: true
    })

    if (!app.com.regexPassword(input)) {//3、格式
      that.setData({
        passwordInputSuccess: false,
        passwordIsRight: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      passwordIsRight: true,
      passwordInputSuccess: true
    })
    this.local.password = input;
    this.checkForm();
  },
  vAccount: function (e) {//1、空 2、长度 3、格式 4、是否重复
    var input = e.detail.value;
    var that = this;
    if (input == '') {//1、空
      that.setData({
        accountIsEmpty: true,
        accountInputSuccess: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      accountIsEmpty: false
    })

    if (input.length < 4 || input.length > 20) {//2、长度
      that.setData({
        accountLengthRight: false,
        accountInputSuccess: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      accountLengthRight: true
    })

    if (app.com.regexNumber(input)) {//3、纯数字
      that.setData({
        accountIsNumber: true,
        accountInputSuccess: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      accountIsNumber: false
    })

    if (!app.com.regexAccount(input)) {//4、格式
      that.setData({
        accountInputSuccess: false,
        accountIsRight: false
      })
      that.checkForm();
      return;
    }
    that.setData({
      accountIsRight: true
    })

    app.ajax.reqPost('/shoppingMall/checkAccountRepeat', {
      account: input,
    }, function (res) {//5、是否重复
      if (!res) {
        that.setData({
          accountNoRepeat: false
        })
      }
      that.setData({
        accountNoRepeat: !res.error,
        accountInputSuccess: !res.error
      })
      that.local.account = input;
      that.checkForm();
    })
  },
  vEmail: function (e) {//正则验证Email
    if (!app.com.regexEmail(e.detail.value)) {
      this.setData({
        emailIsRight: false
      })
    }
    else {
      this.setData({
        emailIsRight: true
      })
      this.local.email = e.detail.value;
    }
    this.checkForm();
  },
  emailvertify: function (e) {
    var switchOn = e.detail.value;
    if (switchOn) {
      this.setData({
        showEmailvertify: true
      })
    }
    else {
      this.setData({
        showEmailvertify: false
      })
    }
    this.checkForm();
  },
  bindAgreeChange: function (e) {
    this.setData({
      isAgree: !!e.detail.value.length
    });
    this.checkForm();
  },
  checkForm: function () {//提交前检查
    if (!this.data.isAgree) {//1、同意协议
      this.setData({
        registerButtonDisabled: true
      })
      return false;
    }
    if (!this.data.accountInputSuccess) {//2、用户名正确
      this.setData({
        registerButtonDisabled: true
      })
      return false;
    }
    if (!this.data.passwordInputSuccess) {//3、密码正确
      this.setData({
        registerButtonDisabled: true
      })
      return false;
    }
    if (!this.data.vCodeInputSuccess) {//4、验证码正确
      this.setData({
        registerButtonDisabled: true
      })
      return false;
    }
    if (!this.data.phoneNumInputSuccess) {//5、手机正确
      this.setData({
        registerButtonDisabled: true
      })
      return false;
    }
    if (this.data.showEmailvertify) {//6、邮箱正确

    }
    this.setData({
      registerButtonDisabled: false
    })
    return true;
  },
  showTopTipsFail: function () {
    var that = this;
    this.setData({
      showTopTipsFail: true
    });
    setTimeout(function () {
      that.setData({
        showTopTipsFail: false
      });
    }, 3000);
  },
  submit: function (e) {
    var account = this.local.account;
    var password = this.local.password;
    var phoneNum = this.local.phoneNum;
    var email = this.local.email;
    var vCode = this.local.vCode;
    var that = this;
    if (!(this.data.showEmailvertify && email)) {
      email = null;
    }
    if (!(this.checkForm() && account && password && phoneNum)) {
      this.showTopTipsFail();
      return;
    }
    app.ajax.reqPost('/shoppingMall/accountAdd', {
      account: account,
      password: password,
      phoneNum: phoneNum,
      email: email,
      vCode: vCode
    }, function (res) {//3、是否重复
      if (!res || res.error) {
        if (res.error == 'VCODEWRONG') {
          that.setData({
            vCodeInputSuccess: false
          })
          that.validateCodeGet();
          that.checkForm();
          return;
        }
        that.showTopTipsFail();
        return;
      }
      app.globalData.account = account;
      wx.setStorage({
        key: 'account',
        data: account,
      })
      wx.showModal({
        title: '注册成功！',
        content: '感谢注册 haowanFamily.com账户，更多便捷应用请登录好万家官方网站：www.haowanFamily.com',
        confirmColor: '#18BC9C',
        showCancel: false,
        confirmText: '点击跳转',
        success: function (res) {
          if (res.confirm) {
            wx.switchTab({
              url: '../../userInfo/userInfo',
            })
          }
        }
      });
    }, session);
  },
  onLoad: function () {
    this.validateCodeGet();
  }
});