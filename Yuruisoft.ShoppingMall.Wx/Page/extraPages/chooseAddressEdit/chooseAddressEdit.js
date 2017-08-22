// chooseAddressEdit.js
Page({
  data: {
    showTopTips: false,

    province: ["四川", "广东", "广西"],
    provinceIndex: 0,

    accounts: ["微信号", "QQ", "Email"],
    accountIndex: 0,

  },
  bindProvinceChange: function (e) {
    console.log('picker country 发生选择改变，携带值为', e.detail.value);

    this.setData({
      ProvinceIndex: e.detail.value
    })
  }
  
});