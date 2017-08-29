$('#companyTitle').click(function () {
    if ($("#firstP").hasClass("firstP")) {
        $("#firstP").removeClass("firstP");
        $('#folder').collapse('show');
        $("#myCarousel").carousel('pause');
    } else {
        $("#firstP").addClass("firstP");
        $('#folder').collapse('hide');
        $("#myCarousel").carousel('cycle');
    }
})
$('.itemClick').click(function (e) {
    window.location.href = e.currentTarget.dataset.url;
})


/**
 * Created by Administrator on 2017/7/11.
 */
'use strict';
$(function () {
    // 获取手指在轮播图元素上的一个滑动方向（左右）

    // 获取界面上轮播图容器
    var $carousels = $('.carousel');
    var startX, endX;
    // 在滑动的一定范围内，才切换图片
    var offset = 50;
    // 注册滑动事件
    $carousels.on('touchstart', function (e) {
        // 手指触摸开始时记录一下手指所在的坐标x
        startX = e.originalEvent.touches[0].clientX;
        startY = e.originalEvent.touches[0].clientY;
    });
    $carousels.on('touchmove', function (e) {
        // 目的是：记录手指离开屏幕一瞬间的位置 ，用move事件重复赋值
        endX = e.originalEvent.touches[0].clientX;
        endY = e.originalEvent.touches[0].clientY;
    });
    $carousels.on('touchend', function (e) {

        //结束触摸一瞬间记录手指最后所在坐标x的位置 endX
        //比较endX与startX的大小，并获取每次运动的距离，当距离大于一定值时认为是有方向的变化
        var distance = Math.abs(startX - endX);
        var angle = function (start, end) {
            var diff_x = end.x - start.x,
                diff_y = end.y - start.y;
            //返回角度,不是弧度
            return 360 * Math.atan(diff_y / diff_x) / (2 * Math.PI);
        }
        //获取滑动角度
        angle = angle({ x: startX, y: startY }, { x: endX, y: endY });
        if (Math.abs(angle) > 30) return;
        if (distance > offset) {
            //说明有方向的变化
            //根据获得的方向 判断是上一张还是下一张出现
            $(this).carousel(startX > endX ? 'next' : 'prev');
        }
    })

});