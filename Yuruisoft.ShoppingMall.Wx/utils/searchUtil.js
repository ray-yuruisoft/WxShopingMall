function searchKeyListGet(searchKeyArr, input, searchKeyDisplayNum) {//搜索栏关键字列表获取
  var TempArr = searchKeyArr.map(item => {
    return {
      id: item.id,
      listImageUrl: item.listImageUrl,
      listTitle: item.listTitle,
      evaluationCount: item.evaluationCount,
      evaluationPercent: item.evaluationPercent,
      price: item.price,
      unit: item.unit,
      sort: (item.listTitle.indexOf(input) == -1) ? 99999 : item.listTitle.indexOf(input)
    }
  })
  var returnTemp = TempArr.sort(function (value1, value2) {
    if (value1.sort > value2.sort) {
      return 1;
    } else if (value1.sort < value2.sort) {
      return -1;
    } else {
      return 0;
    }
  });

  if (searchKeyDisplayNum != '' && searchKeyDisplayNum != undefined)
  {
    returnTemp = returnTemp.slice(0, searchKeyDisplayNum)
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

module.exports = {
  searchKeyListGet: searchKeyListGet
}