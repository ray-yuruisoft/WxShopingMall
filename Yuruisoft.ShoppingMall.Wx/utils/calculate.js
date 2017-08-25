function add(a, b) {
  var c, d, e;
  try {
    c = a.toString().split(".")[1].length;
  } catch (f) {
    c = 0;
  }
  try {
    d = b.toString().split(".")[1].length;
  } catch (f) {
    d = 0;
  }
  return e = Math.pow(10, Math.max(c, d)), (mul(a, e) + mul(b, e)) / e;
}

function sub(a, b) {
  var c, d, e;
  try {
    c = a.toString().split(".")[1].length;
  } catch (f) {
    c = 0;
  }
  try {
    d = b.toString().split(".")[1].length;
  } catch (f) {
    d = 0;
  }
  return e = Math.pow(10, Math.max(c, d)), (mul(a, e) - mul(b, e)) / e;
}

function mul(a, b) {
  var c = 0,
    d = a.toString(),
    e = b.toString();
  try {
    c += d.split(".")[1].length;
  } catch (f) { }
  try {
    c += e.split(".")[1].length;
  } catch (f) { }
  return Number(d.replace(".", "")) * Number(e.replace(".", "")) / Math.pow(10, c);
}

function div(a, b) {
  var c, d, e = 0,
    f = 0;
  try {
    e = a.toString().split(".")[1].length;
  } catch (g) { }
  try {
    f = b.toString().split(".")[1].length;
  } catch (g) { }
  return c = Number(a.toString().replace(".", "")), d = Number(b.toString().replace(".", "")), mul(c / d, Math.pow(10, f - e));
}


function checkAllFee(shoppingCart) {//结算购物车内所有选中项目
  var oneLevelFeeSum = 0;
  var oneLevelChooseItemCount = 0;
  var twoLevelFeeSum;
  var twoLevelChooseItemCount;
  var twoLevelChoosedFlag;
  var oneLevelChoosedFlag = true;
  if (shoppingCart.detail.length == 0) {
    return {
      allItemCount: 0,
      chooseItemCount: 0,
      feeSum: "0.00",
      choosedFlag: false,
      detail: []
    }
  }
  shoppingCart.detail.forEach(item => {
    twoLevelFeeSum = 0;//初始化
    twoLevelChooseItemCount = 0;
    twoLevelChoosedFlag = true;//默认二层被选中

    item.produceArr.forEach(itemBottom => {
      if (itemBottom.choosedFlag) {
        itemBottom.feeSum = mul(parseInt(itemBottom.itemCount), parseFloat(itemBottom.price)).toFixed(2);
        twoLevelChooseItemCount += itemBottom.itemCount;
        twoLevelFeeSum = add(twoLevelFeeSum, itemBottom.feeSum).toFixed(2);
      }
      if (!itemBottom.choosedFlag) {//三层只要有没有选中的，二层都不能选中
        twoLevelChoosedFlag = false;
      }
    })
    item.chooseItemCount = twoLevelChooseItemCount;//赋值保存
    item.feeSum = twoLevelFeeSum;
    item.choosedFlag = twoLevelChoosedFlag;

    if (!item.choosedFlag) {//同理 二层只要有没有选中的，一层都不能选中
      oneLevelChoosedFlag = false;
    }
    oneLevelFeeSum = add(oneLevelFeeSum, twoLevelFeeSum).toFixed(2);
    oneLevelChooseItemCount += twoLevelChooseItemCount;

  })
  shoppingCart.chooseItemCount = oneLevelChooseItemCount;
  shoppingCart.feeSum = oneLevelFeeSum;
  shoppingCart.choosedFlag = oneLevelChoosedFlag;
  return shoppingCart;
}

module.exports = {
  add: add,
  sub: sub,
  mul: mul,
  div: div,
  checkAllFee: checkAllFee
}