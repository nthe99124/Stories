function initializeCarousel() {
    var currentSlide = 0;
    var totalSlides = $('.slide-item').length;

    function nextSlide() {
        currentSlide++;
        if (currentSlide >= totalSlides) {
            currentSlide = 0;
        }
        $('.slide-item').removeClass('layui-this');
        $('.slide-item').eq(currentSlide).addClass('layui-this');
        $('.dot-config').removeClass('layui-this');
        $('.dot-config').eq(currentSlide).addClass('layui-this');
    }

    $('.layui-carousel-arrow[lay-type="sub"]').click(function () {
        currentSlide--;
        if (currentSlide < 0) {
            currentSlide = totalSlides - 1;
        }
        $('.slide-item').removeClass('layui-this');
        $('.slide-item').eq(currentSlide).addClass('layui-this');
        $('.dot-config').removeClass('layui-this');
        $('.dot-config').eq(currentSlide).addClass('layui-this');
    });

    $('.layui-carousel-arrow[lay-type="add"]').click(function () {
        nextSlide();
    });

    setInterval(nextSlide, 3000); // Change slide every 5 seconds
}

function initializeSlide() {
    var defaultItems = 6;
    var moveItems = 1;
    // Kích thước của mỗi mục
    var itemWidth = $('.cs-item').outerWidth(true);
    var totalItems = $('.cs-item').length;
    // Số lượng mục còn lại
    var remainingItems = totalItems - defaultItems;
    // Thiết lập chiều rộng cho .slide-wrapper
    $('.slide-wrapper').css('width', itemWidth * totalItems + 'px');

    // Xử lý khi nhấn nút prev
    $('.prev').click(function () {
        var currentPosition = parseInt($('.slide-wrapper').css('left'));
        if (currentPosition < 0) {
            $('.slide-wrapper').animate({ left: '+=' + (itemWidth * moveItems) + 'px' }, 'slow');
        }
    });

    // Xử lý khi nhấn nút next
    $('.next').click(function () {
        var currentPosition = parseInt($('.slide-wrapper').css('left'));
        if (Math.abs(currentPosition) < (itemWidth * remainingItems)) {
            $('.slide-wrapper').animate({ left: '-=' + (itemWidth * moveItems) + 'px' }, 'slow');
        }
    });
}

window.handleAvatarHover = () => {
    $('.j-header-avatar').mouseenter(function () {
        $('.j-dialog-avatar').css('display', 'block'); // Hiển thị phần tử khi hover vào .j-header-avatar
    });

    $('.j-header-avatar').mouseleave(function () {
        $('.j-dialog-avatar').css('display', 'none'); // Ẩn phần tử khi di chuột ra khỏi .j-header-avatar
    });
}