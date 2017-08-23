var app = getApp();
Page({
  data: {
    checkboxItems: []
  },

  checkboxChange: function (e) {
    var checkboxItems = this.data.checkboxItems, values = e.detail.value;
    checkboxItems.forEach(item => {
      item.checked = false;
      if (item.value == values) {
        item.checked = true;
      }
    })
    this.setData({
      checkboxItems: checkboxItems
    });
  },
  edit: function (e) {
    var id = e.currentTarget.id;
    console.log(id);
    if (id != undefined) {
      wx.navigateTo({
        url: '../chooseAddressEdit/chooseAddressEdit?id=' + id
      })
    }
    wx.navigateTo({
      url: '../chooseAddressEdit/chooseAddressEdit'
    })
  },
  onLoad: function (e) {
  },
  onShow: function (e) {
    var userAddress = app.globalData.userAddress;
    if (userAddress) {
      var temp = userAddress.map((item, index) => {
        if (index == 0)
          return {
            address: item.city + item.address,
            name: item.name,
            phoneNumber: item.phoneNumber,
            value: index,
            checked: true
          }
        return {
          address: item.city + item.address,
          name: item.name,
          phoneNumber: item.phoneNumber,
          value: index
        }
      })
      this.setData({
        checkboxItems: temp
      })
    }
  }
});