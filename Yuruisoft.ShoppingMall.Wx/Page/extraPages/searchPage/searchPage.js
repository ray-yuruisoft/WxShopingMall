// searchPage.js
var app = getApp();

var pageNum = 1; // 当前页数  
var searchTitle = ""; // 搜索关键字  
var searchList = [];//保存了分页搜索中，所有搜索列表
var searchListTemp = [];//保存了分页搜索中，未被显示的分页内容
/**  
 * post 请求加载文章列表数据   
 * "page" ：页数  
 * "pageSize" ：每页数量  
 */
var loadSearchData = function (that) {
  // 获取上一页数据  
  var allMsg = that.data.searchKeyList;

  var concatArr = searchListTemp.slice(0, app.globalData.searchListPageSize);
  allMsg = allMsg.concat(concatArr);//连接下一页
  searchListTemp = searchListTemp.slice(concatArr.length, searchListTemp.length);//更新

  that.setData({
    searchKeyList: allMsg
  })

  wx.hideNavigationBarLoading() //完成停止加载
  wx.stopPullDownRefresh() //停止下拉刷新

  // 页数加一  
  pageNum++;
}
Page({

  /**
   * 页面的初始数据
   */
  data: {
    searchKeyList: [], // 搜索结果列表  
    searchLogList: [], // 存储搜索历史记录信息 
    keyWordList: [], //关键字提示栏列表
    inputShowed: false, // 搜索输入框是否显示  
    inputVal: "", // 搜索的内容  
    searchLogShowed: false // 是否显示搜索历史记录  
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    if (options.searchData == '' || options.searchData == undefined) {
      searchList = [];
      return;
    }
    else {
      this.setData({
        inputVal: options.searchData,
        inputShowed: true
      })
    }
    searchList = app.search.searchKeyListGet(app.globalData.searchKeyObject.searchKeyArray, options.searchData)
    searchListTemp = searchList;

    loadSearchData(that);
    var searchLogTemp = wx.getStorageSync('searchLog')
    if (searchLogTemp == '') {
      searchLogTemp = [];
    }
    searchLogTemp.push(options.searchData);

    wx.setStorage({
      key: 'searchLog',
      data: searchLogTemp,
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
    wx.showNavigationBarLoading() //在标题栏中显示加载

    var that = this;
    // 刷新的准备工作，想将页数设置为一，然后清空文章列表信息，定位在距顶部为0的地方  
    pageNum = 1;
    that.setData({
      searchKeyList: []
    });
    // 加载数据  
    loadSearchData(that);
  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
    var that = this;
    loadSearchData(that);
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  },


  // 显示搜索输入框和搜索历史记录  
  showInput: function () {
    var that = this;
    if ("" != wx.getStorageSync('searchLog')) {
      that.setData({
        inputShowed: true,
        searchLogShowed: true,
        searchLogList: wx.getStorageSync('searchLog')
      });
    } else {
      that.setData({
        inputShowed: true,
        searchLogShowed: true
      });
    }
  },

  // 显示搜索历史记录  
  searchLogShowed: function () {
    var that = this;
    if ("" != wx.getStorageSync('searchLog')) {
      that.setData({
        searchLogShowed: true,
        searchLogList: wx.getStorageSync('searchLog')
      });
    } else {
      that.setData({
        searchLogShowed: true
      });
    }
  },

  // 点击 搜索 按钮后 隐藏搜索记录，并加载数据  
  searchData: function () {
    var that = this;
    if (this.data.inputVal == '')
      return;
    searchList = app.search.searchKeyListGet(app.globalData.searchKeyObject.searchKeyArray, this.data.inputVal);
    searchListTemp = searchList;

    that.setData({
      searchKeyList: [],
      searchLogShowed: false
    });
    pageNum = 1;

    loadSearchData(that);
    // 搜索后将搜索记录缓存到本地  
    if ("" != searchTitle) {
      var searchLogData = that.data.searchLogList;

      var returnFlag = false;
      searchLogData.forEach(function (currentValue, index, arr) {//防止重复写入
        if (currentValue == searchTitle) {
          returnFlag = true;
          return;
        }
      }, this)
      if (returnFlag)
        return;

      searchLogData.push(searchTitle);
      wx.setStorageSync('searchLog', searchLogData);
    }
  },

  // 点击叉叉icon 清除输入内容，同时清空关键字，并加载数据  
  clearInput: function () {
    var that = this;
    that.setData({
      msgList: [],
      inputVal: ""
    });
    searchTitle = "";
    pageNum = 1;
    loadSearchData(that);
  },

  // 输入内容时 把当前内容赋值给 查询的关键字，并显示搜索记录  
  inputTyping: function (e) {
    var that = this;
    // 如果不做这个if判断，会导致 searchLogList 的数据类型由 list 变为 字符串  
    if (e.detail.value == '')
      return;
    var allKeyWord = allKeyWordGet();//提示关键字列表获取
    var keyList = keyListGet(allKeyWord, e.detail.value, app.globalData.searchKeyListNum);
    this.setData({
      keyWordList: keyList
    })

    if ("" != wx.getStorageSync('searchLog')) {
      that.setData({
        inputVal: e.detail.value,
        searchLogList: wx.getStorageSync('searchLog')
      });
    } else {
      that.setData({
        inputVal: e.detail.value,
        searchLogShowed: true
      });
    }
    searchTitle = e.detail.value;
  },
  keyWordTap: function (e) {//关键字点击
    this.setData({
      inputVal: e.currentTarget.dataset.skey,
      keyWordList:[]
    })
  },
  clearKeyWord:function(){//失去焦点时，清空提示信息
    this.setData({
      keyWordList: []
    })
  },
  // 通过搜索记录查询数据  
  searchDataByLog: function (e) {
    // 从view中获取值，在view标签中定义data-name(name自定义，比如view中是data-log="123" ; 那么e.target.dataset.log=123)  
    searchTitle = e.target.dataset.log;
    var that = this;
    that.setData({
      searchKeyList: [],
      searchLogShowed: false,
      inputVal: searchTitle
    });
    pageNum = 1;
    loadSearchData(that);
  },
  // 清除搜索记录  
  clearSearchLog: function () {
    var that = this;
    wx.removeStorageSync("searchLog");
    that.setData({
      searchLogShowed: false,
      searchLogList: []
    });
  },
})

var allKeyWordGet = function () {//从缓存数据中提取所有关键字

  var allKeyWordArr = app.globalData.searchKeyObject.searchKeyArray;
  var allRepeat = []
  allKeyWordArr.forEach(function (currentValue, index, arr) {
    currentValue.listKeys.forEach(function (cValue) {
      allRepeat.push(cValue);
    })
  }, this)

  return outRepeat(allRepeat);
}
var outRepeat = function (a) {//去掉数组中的重复项
  var hash = [], arr = [];
  for (var i = 0; i < a.length; i++) {
    hash[a[i]] != null;
    if (!hash[a[i]]) {
      arr.push(a[i]);
      hash[a[i]] = true;
    }
  }
  return arr;
}
var keyListGet = function (allKeyWord, input, num) {
  var tempArr = allKeyWord.map(item => {
    return {
      keyWord: item,
      sort: (item.indexOf(input) == -1) ? 99999 : item.indexOf(input)
    }
  })
  var returnTemp = tempArr.sort(function (value1, value2) {
    if (value1.sort > value2.sort) {
      return 1;
    } else if (value1.sort < value2.sort) {
      return -1;
    } else {
      return 0;
    }
  });
  if (num != '' && num != undefined) {
    returnTemp = returnTemp.slice(0, num)
  }
  for (var i = 0; i < returnTemp.length; i++) {//排除包含不匹配的情况
    if (returnTemp[i].sort == 99999) {
      if (i == 0) {
        return []
      }
      return returnTemp.slice(0, i)
    }
  }
  return returnTemp
}
