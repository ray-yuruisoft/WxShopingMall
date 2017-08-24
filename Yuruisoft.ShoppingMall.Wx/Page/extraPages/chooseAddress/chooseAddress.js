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
    app.globalData.userAddress.forEach((item, index) => {
      item.checked = checkboxItems[index].checked
    });
    this.saveAndBack();
  },
  saveAndBack: function () {
    wx.setStorage({
      key: 'userAddress',
      data: app.globalData.userAddress
    });
    wx.navigateBack({
    });
  },
  edit: function (e) {
    var id = e.currentTarget.id;
    console.log(id);
    if (id != undefined) {
      wx.navigateTo({
        url: '../chooseAddressEdit/chooseAddressEdit?id=' + id
      })
      return;
    }
    wx.navigateTo({
      url: '../chooseAddressEdit/chooseAddressEdit'
    })
  },
  onLoad: function (e) {
  },
  onShow: function (e) {
    var userAddress = app.globalData.userAddress;
    if (userAddress != undefined) {
      var temp = userAddress.map((item, index) => {
        return {
          address: item.city + item.address,
          name: item.name,
          phoneNumber: item.phoneNumber,
          value: index,
          checked: item.checked,
          'default': item.default
        }
      })
      this.setData({
        checkboxItems: temp
      })
    }
  }
});