function reqPost(url, data, cb, sessionId) {//Post请求
  wx.request({
    // url: "http://www.yurusoft.net" + url,
    url: "http://localhost:4943" + url,
    data: data,
    method: 'POST',
    header: {
      'haowanFamily': 'www.haowanFamily.com',
      'content-type': 'application/x-www-form-urlencoded',
      'Cookie': sessionId,
    },
    success: function (res) {
      var temp = res.data;
      if (res.header['Set-Cookie'])
        temp['session'] = res.header['Set-Cookie'].split(';')[0];
      return typeof cb == "function" && cb(temp)
    },
    fail: function () {
      return typeof cb == "function" && cb(false)
    }
  })
}

function Login(code, encryptedData, iv, rawData, signature) {

  reqPost('/shoppingMall/userLogin', {
    "code": code,
    "encryptedData": encryptedData,
    "iv": iv,
    "raw": rawData,
    "signature": signature
  }, function (res) {
    if (!res) {
      console.log("失败！")
      return
    }

    wx.setStorage({
      key: 'thirdSessionKey',
      data: res.thirdSessionKey,
      success: function (res) {
      },
      fail: function (res) {
        // fail
      },
      complete: function (res) {
        // complete
      }
    })
  });

}


function regexEmail(value) {
  return /[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?/.test(value)
}

function regexAccount(value) {
  return /^(?!_)(?!.*?_$)[a-zA-Z0-9_\u4e00-\u9fa5]+$/.test(value)
}

function regexPassword(value) {
  return /((?=.*[0-9])(?=.*[A-z]))|((?=.*[A-z])(?=.*[^A-z0-9]))|((?=.*[0-9])(?=.*[^A-z0-9]))^.{6,20}$/.test(value)
}

function regexPhoneNum(value) {
  return /^1[3|4|5|7|8][0-9]{9}$/.test(value)
}

function regexNumber(value) {
  return /^\d+$/.test(value)
}

module.exports = {
  reqPost: reqPost,            //Post请求
  Login: Login,//登录函数
  regexEmail: regexEmail, //正则验证Email
  regexAccount: regexAccount,//正则验证用户名
  regexPassword: regexPassword,//正则验证密码
  regexPhoneNum: regexPhoneNum,//正则验证手机号码
  regexNumber: regexNumber,//正则验证纯数字
}