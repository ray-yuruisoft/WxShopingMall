Page({
  data: {
    checkboxItems: [
      { name: 'standard is dealt for u.', value: '0', checked: true },
      { name: 'standard is dealicient for u.', value: '1' }
    ]
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


    wx.navigateTo({
      url: '../chooseAddressEdit/chooseAddressEdit',
    })
  }
});