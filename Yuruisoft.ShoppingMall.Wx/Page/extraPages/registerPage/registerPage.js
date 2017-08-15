// registerPage.js
var app = getApp();

Page({
  data: {
    showTopTips: false,
    showEmailvertify: false,
    countryCodes: ["+86", "+80", "+84", "+87"],
    countryCodeIndex: 0,
    isAgree: false,

    phoneNumIsRight: true,
    emailIsRight: true,
    accountIsRight: true,
    passwordIsRight: true
  },
  local: {},

  vPhoneNum: function (e) { },
  vPassword: function (e) { },
  vAccount: function (e) {
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
  },
  emailvertify: function (e) {
    var switchOn = e.detail.value;
    if (switchOn) {
      this.setData({
        showEmailvertify: true
      })
    }
    else{
      this.setData({
        showEmailvertify: false
      })
    }
  },
  bindAgreeChange: function (e) {
    this.setData({
      isAgree: !!e.detail.value.length
    });
  }
});