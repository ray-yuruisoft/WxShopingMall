function reqPost(url, data, cb) {//Post请求
  wx.request({
    // url: "http://www.yurusoft.net" + url,
    url: "http://localhost:4943" + url,
    data: data,
    method: 'POST',
    header: {
      'content-type': 'application/json',
      'haowanFamily': 'www.haowanFamily.com'
    },
    success: function (res) {
      return typeof cb == "function" && cb(res.data)
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


module.exports = {
  reqPost: reqPost,            //Post请求
  Login: Login,//登录函数
  regexEmail: regexEmail //正则验证Email
}