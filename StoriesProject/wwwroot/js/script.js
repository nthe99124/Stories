
function toggleSidebar() {
    const hamBurger = document.querySelector(".toggle-btn");

    hamBurger.addEventListener("click", function () {
        document.querySelector("#sidebar").classList.toggle("expand");
    });
}

function registerMouseEvents(classShowComicInfo, classTab) {
    const showComicInfo = $('#' + classShowComicInfo);
    $('.' + classTab + ' tbody tr').each(function () {
        var row = $(this);
        if (row) {
            row.on('mouseenter', function (event) {
                const mouseX = event.clientX;
                const mouseY = event.clientY;

                showComicInfo.css({
                    left: mouseX + 'px',
                    top: mouseY + 'px',
                    display: 'block'
                });
            });
            row.on('mouseleave', function () {
                //showComicInfo.css('display', 'none');
            });
        }
    });

    if (showComicInfo) {
        showComicInfo.on('mouseleave', function () {
            showComicInfo.css('display', 'none');
        });
    }
};

window.autoIncrease = {
    startAutoIncrease: function () {
        let valueDisplay = document.querySelectorAll(".auto_increase");
        let duration = 2000;

        valueDisplay.forEach((item, index) => {
            let start = 0;
            let end = parseInt(item.getAttribute("data-val"));
            let increment = Math.ceil((end / duration) * 50);
            let stepCount = Math.ceil(end / increment);
            let currentStep = 0;
            let isLastItem = index === valueDisplay.length - 1;
            let timer = setInterval(() => {
                start += increment;
                currentStep++;
                if (currentStep >= stepCount) {
                    item.textContent = isLastItem
                        ? formatNumber(end)
                        : formatCurrencyVND(end);
                    clearInterval(timer);
                } else {
                    item.textContent = isLastItem
                        ? formatNumber(start)
                        : formatCurrencyVND(start);
                }
            }, 50);
        });
    }
};

function formatCurrencyVND(number) {
    return number.toLocaleString("vi-VN", { style: "currency", currency: "VND" });
}

function formatNumber(number) {
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") + " cuốn";
}