function reqPOST(url, data, cb) {//Get请求
  wx.request({
    //url: getApp().data.servsers + url,
    url: "http://localhost:4943/" + url,
    data: data,
    method: 'POST',
    header: {
      'content-type': 'application/json',
      'yuruisoft': 'www.yuruisoft.com'
    },
    success: function (res) {
      return typeof cb == "function" && cb(res.data)
    },
    fail: function () {
      return typeof cb == "function" && cb(false)
    }
  })
}

module.exports = {
  reqPOST: reqPOST            //Post请求
}