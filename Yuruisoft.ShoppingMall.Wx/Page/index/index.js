var app = getApp();
var flagSetOut;

Page({
  data: {
    list: [
      {
        icon: '',
        imageUrl: '../../style/images/replaceIcon/groceries.png',
        id: 'agProducts',
        name: '特色农产品',
        open: false,
        pages: [{ displayName: '小香菜', url: 'produceDetails' }, { displayName: '大头菜', url: 'produceDetails' }],
      },
      {
        icon: '',
        imageUrl: '../../style/images/replaceIcon/fresh-vendor.png',
        id: 'freshProducts',
        name: '特色生鲜',
        open: false,
        pages: [{ displayName: '生态羊肉', url: 'produceDetails' }, { displayName: '生态鸡肉', url: 'produceDetails' }]
      },
      {
        icon: '',
        imageUrl: '../../style/images/replaceIcon/ellipsis.png',
        id: 'findMore',
        name: '更多',
        open: false,
        pages: [{ displayName: '智能提示设置', url: 'produceDetails' }]
      }
    ],

    inputShowed: false,//搜索
    inputVal: "",

    icon_angleProduceSort: true,//商品总类栏隐藏图标
    icon_angleEverydayAdvice:false,//每日推荐隐藏图标
  },
  angleTapProduceSort: function () {
    if (this.data.icon_angleProduceSort == false) {
      this.setData({
        icon_angleProduceSort: true
      })
    }
    else {
      this.setData({
        icon_angleProduceSort: false
      })
    }
  },
  angleTapEverydayAdvice: function () {
    if (this.data.icon_angleEverydayAdvice == false) {
      this.setData({
        icon_angleEverydayAdvice: true
      })
    }
    else {
      this.setData({
        icon_angleEverydayAdvice: false
      })
    }
  },

  showInput: function () {
    this.setData({
      inputShowed: true
    });
  },
  hideInput: function () {
    this.setData({
      inputVal: "",
      inputShowed: false,
      articleList: [],
      searchKeyList: []
    });
  },
  clearInput: function () {
    this.setData({
      inputVal: "",
      searchKeyList: []
    });
  },
  keyListTap: function (e) {//搜索内容点击,函数主要清空作用
    console.log(e)
    this.setData({
      searchKeyList: []
    })
  },
  inputTyping: function (e) {//输入时异步本地查询
    var that = this;
    var searchKeyArr = app.globalData.searchKeyObject.searchKeyArray;

    if (flagSetOut != undefined) {
      clearTimeout(flagSetOut);
    }
    flagSetOut = setTimeout(function () {
      if (e.detail.value == '') {
        return
      }
      var searchKeyList = app.search.searchKeyListGet(searchKeyArr, e.detail.value, app.globalData.searchKeyDisplayNum);
      that.setData({
        searchKeyList: searchKeyList
      })
    }, 500);
    this.setData({
      inputVal: e.detail.value
    });
  },
  searching: function (e) {//搜索栏回车键
    var par = "searchData=" + e.detail.value;
    wx.navigateTo({
      url: '../extraPages/searchPage/searchPage?' + par,
    })
  },
  kindToggle: function (e) {//辅助选项栏
    var id = e.currentTarget.id, list = this.data.list;
    for (var i = 0, len = list.length; i < len; ++i) {
      if (list[i].id == id) {
        list[i].open = !list[i].open
      } else {
        list[i].open = false
      }
    }
    this.setData({
      list: list
    });
  },
  onLoad: function (options) {
    // 页面初始化 options为页面跳转所带来的参数  
    var that = this;
    app.ajax.reqPost('/shoppingMall/recommentListsGet', {//TODO:这里可以做大数据扩展
      "userInfo": "",//TODO:用户信息,调整推荐策略
      "takeNum": app.globalData.recommentListsNum,
      "prefers": ""  //TODO:用户喜好,调整推荐策略
    }, function (res) {
      if (!res || res.error == true) {//失败直接返回        
        return
      }
      var temp = res.map(item => {//格式化数字
        return {
          id: item.id,
          listImageUrl: item.listImageUrl,
          listTitle: item.listTitle,
          evaluationCount: item.evaluationCount,
          evaluationPercent: item.evaluationPercent.toFixed(2),
          price: item.price.toFixed(2),
          unit: item.unit
        }
      })
      that.setData({
        recommentLists: temp
      })

    });

  }
});
