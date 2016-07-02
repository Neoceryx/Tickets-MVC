$(function () {

    $('.nav > li').on('click', function () {
        var $this = $(this);
        $this.addClass('active');
        $this.siblings().removeClass('active');
    });
})