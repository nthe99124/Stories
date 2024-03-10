
function toggleSidebar() {
    const hamBurger = document.querySelector(".toggle-btn");

    hamBurger.addEventListener("click", function () {
        document.querySelector("#sidebar").classList.toggle("expand");
    });
}

window.registerMouseEvents = () => {
    const showComicInfo = document.getElementById('showComicInfo');

    document.querySelectorAll('tr').forEach(row => {
        row.addEventListener('mouseenter', (event) => {
            const mouseX = event.clientX;
            const mouseY = event.clientY;

            showComicInfo.style.left = `${mouseX}px`;
            showComicInfo.style.top = `${mouseY}px`;
            showComicInfo.style.display = 'block';
        });

        row.addEventListener('mouseleave', () => {
            showComicInfo.style.display = 'none';
        });
    });

    showComicInfo.addEventListener('mouseleave', () => {
        showComicInfo.style.display = 'none';
    });
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