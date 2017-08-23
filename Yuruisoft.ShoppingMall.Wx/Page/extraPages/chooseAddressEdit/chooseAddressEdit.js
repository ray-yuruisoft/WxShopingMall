// chooseAddressEdit.js
var app = getApp();
var address;
var animation;
Page({
  data: {
    defaultAddress: false,
    phoneNumInputSuccess: undefined,
    nameInputSuccess: undefined,
    confirmButtonDisabled: true,

    animationData: {},
    animationAddressMenu: {},
    addressMenuIsShow: false,
    value: [0, 0, 0],
    provinces: [],
    citys: [],
    areas: [],
    province: '',
    city: '',
    area: ''
  },
  defaltAddress: function () {
    var temp = !this.data.defaultAddress;
    this.setData({
      defaultAddress: temp
    })
  },
  showToastWrong: function (title) {
    wx.showToast({
      title: title,
      image: '../../../style/images/replaceIcon/exclamation-sign.png'
    })
  },
  formSubmit: function (e) {
    var addressInfo = e.detail.value;
    var temp = this.data.areaInfo;
    while (temp.indexOf(',') != -1) {
      temp = temp.replace(',', '');
    }
    addressInfo["city"] = temp;
    console.log(addressInfo);
    if (app.globalData.userAddress) {
      app.globalData.userAddress.push(addressInfo);
    }
    else {
      app.globalData.userAddress = [];
      app.globalData.userAddress.push(addressInfo);
    }
    wx.setStorage({
      key: 'userAddress',
      data: app.globalData.userAddress,
    });
    wx.navigateBack({
    });
  },
  vPhoneNum: function (e) {
    var input = e.detail.value;
    var that = this;
    if (input == '') {//1、空
      that.setData({
        phoneNumInputSuccess: false
      })
      that.checkForm();
      return;
    }

    if (!app.com.regexPhoneNum(input)) {//2、格式
      that.setData({
        phoneNumInputSuccess: false
      })
      that.checkForm();
      that.showToastWrong('手机号码不正确');
      return;
    }

    that.setData({
      phoneNumber: input,
      phoneNumInputSuccess: true
    })
    that.checkForm();
  },
  vName: function (e) {
    var input = e.detail.value;
    var that = this;
    if (input == '') {//1、空
      that.setData({
        nameInputSuccess: false
      })
      that.checkForm();
      return;
    }

    that.setData({
      name: input,
      nameInputSuccess: true
    })
    that.checkForm();
  },
  vAddress: function (e) {
    var input = e.detail.value;
    var that = this;
    if (input == '') {//1、空
      that.setData({
        addressInputSuccess: false
      })
      that.checkForm();
      return;
    }

    that.setData({
      address: input,
      addressInputSuccess: true
    })
    that.checkForm();
  },
  checkForm: function (e) {
    if (!this.data.nameInputSuccess) {//1、姓名
      this.setData({
        confirmButtonDisabled: true
      })
      return false;
    }
    if (!this.data.phoneNumInputSuccess) {//2、手机号码
      this.setData({
        confirmButtonDisabled: true
      })
      return false;
    }
    if (this.data.areaInfo == undefined) {//3、城市
      this.setData({
        confirmButtonDisabled: true
      })
      return false;
    }
    if (!this.data.addressInputSuccess) {//4、详细地址
      this.setData({
        confirmButtonDisabled: true
      })
      return false;
    }
    this.setData({
      confirmButtonDisabled: false
    })
    return true;
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    app.ajax.reqPost('/shoppingMall/cityGet', {
    }, function (res) {
      if (!res) {//失败直接返回        
        return
      }
      address = res;
      // 初始化动画变量
      var animation = wx.createAnimation({
        duration: 500,
        transformOrigin: "50% 50%",
        timingFunction: 'ease',
      })
      that.animation = animation;
      // 默认联动显示北京
      var id = address.provinces[0].id    
      that.setData({
        provinces: address.provinces,
        citys: address.citys[id],
        areas: address.areas[address.citys[id][0].id],
      })
      
      // if (options.id != undefined) { 
      //   var index = options.id;
      //   var userAddressArr = app.globalData.userAddress;
      //   var userAddress = userAddressArr[index];
      //   var name = userAddress.name
      //   that.setData({
      //     name: name,
      //     phoneNumber: userAddress.phoneNumber,
      //     areaInfo: userAddress.city,
      //     address: userAddress.address
      //   })
      // }
    })
  },
  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

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

  },
  // 点击所在地区弹出选择框
  selectDistrict: function (e) {
    var that = this
    if (that.data.addressMenuIsShow) {
      return
    }
    that.startAddressAnimation(true)
  },
  // 执行动画
  startAddressAnimation: function (isShow) {
    var that = this
    if (isShow) {
      that.animation.translateY(0 + 'vh').step()
    } else {
      that.animation.translateY(40 + 'vh').step()
    }
    that.setData({
      animationAddressMenu: that.animation.export(),
      addressMenuIsShow: isShow,
    })
  },
  // 点击地区选择取消按钮
  cityCancel: function (e) {
    this.startAddressAnimation(false)
  },
  // 点击地区选择确定按钮
  citySure: function (e) {
    var that = this
    var city = that.data.city
    var value = that.data.value
    that.startAddressAnimation(false)
    // 将选择的城市信息显示到输入框
    var areaInfo = that.data.provinces[value[0]].name + ',' + that.data.citys[value[1]].name + ',' + that.data.areas[value[2]].name
    that.setData({
      areaInfo: areaInfo,
    })
    that.checkForm();
  },
  hideCitySelected: function (e) {
    this.startAddressAnimation(false)
  },
  // 处理省市县联动逻辑
  cityChange: function (e) {
    var value = e.detail.value
    var provinces = this.data.provinces
    var citys = this.data.citys
    var areas = this.data.areas
    var provinceNum = value[0]
    var cityNum = value[1]
    var countyNum = value[2]
    if (this.data.value[0] != provinceNum) {
      var id = provinces[provinceNum].id
      this.setData({
        value: [provinceNum, 0, 0],
        citys: address.citys[id],
        areas: address.areas[address.citys[id][0].id],
      })
    } else if (this.data.value[1] != cityNum) {
      var id = citys[cityNum].id
      this.setData({
        value: [provinceNum, cityNum, 0],
        areas: address.areas[citys[cityNum].id],
      })
    } else {
      this.setData({
        value: [provinceNum, cityNum, countyNum]
      })
    }
  }
});