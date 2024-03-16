window.dashboard = {
    startAutoIncrease: function () {
        let valueDisplay = document.querySelectorAll(".auto_increase");
        let duration = 2000; // Thời gian để hiển thị giá trị từ 0 đến giá trị cuối cùng (2 giây)
        valueDisplay.forEach((item, index) => {
            let start = 0;
            console.log("item:", item);
            let end = parseInt(item.getAttribute("data-val"));
            // console.log(end)
            let label = item.getAttribute("data-label");
            console.log(label)
            let increment = Math.ceil((end / duration) * 50); // Bước tăng số làm tròn
            let stepCount = Math.ceil(end / increment); // Số lần tăng cần thiết để đạt giá trị cuối cùng
            let currentStep = 0; // Số lần tăng hiện tại
            let timer = setInterval(() => {
                start += increment;
                currentStep++;
                if (currentStep >= stepCount) {
                    item.textContent = formatNumber(end, label);
                    clearInterval(timer);
                } else {
                    item.textContent = formatNumber(start, label)
                }
            }, 50); // Thời gian tạo ra hiệu ứng (mỗi 50ms)
        });
    },
    chart_bar: function () {
        new Chart(document.getElementById("bar-chart"), {
            type: "bar",
            data: {
                labels: [
                    "Tác giả 1",
                    "Tác giả 2",
                    "Tác giả 3",
                    "Tác giả 4",
                    "Tác giả 5",
                    "Tác giả 6",
                    "Tác giả 7",
                    "Tác giả 8",
                    "Tác giả 9",
                    "Tác giả 10",
                ],
                datasets: [
                    {
                        label: "Triệu đồng (VNĐ)",
                        backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                        borderColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                        data: [20, 10.5, 2, 78, 43, 23, 12, 45, 67, 89],
                    },
                ],
            },
            options: {
                plugins: {
                    legend: {
                        display: false,
                    },
                    title: {
                        display: true,
                        text: "Top 10 tác giả có doanh thu cao nhất tháng",
                    },
                },
                scales: {
                    y: {

                        title: {
                            display: true,
                            text: "Doanh thu (Triệu đồng)", // Tên trục Y
                        },
                        ticks: {
                            callback: function (value) {
                                return value.toLocaleString();
                            },
                        },
                    }

                },
                responsive: true,
            },
        });
    },
    chart_doughnut: function () {
        new Chart(document.getElementById("doughnut-chart"), {
            type: "doughnut",
            data: {
                labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
                datasets: [
                    {
                        label: "Population (millions)",
                        backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                        data: [2478, 5267, 734, 784, 433],
                        hover: {
                            // Cấu hình chú thích hiển thị khi di chuột qua
                            backgroundColor: function (context) {
                                // Lấy ra màu nền của phần tử được di chuột qua
                                const backgroundColor = context.dataset.backgroundColor[context.dataIndex];
                                return backgroundColor; // Giữ nguyên màu nền
                            },
                            borderColor: "white", // Thêm viền trắng cho chú thích
                            borderWidth: 2, // Độ dày viền
                            hoverRadius: 10, // Kích thước bán kính của chú thích

                            // Hiển thị label và giá trị tương ứng
                            display: true,
                            bodyColor: "white", // Màu nền của chú thích
                            bodyFontColor: "black", // Màu chữ của chú thích
                            bodyFontSize: 14, // Kích thước chữ của chú thích
                            bodyFontStyle: "bold", // Kiểu chữ của chú thích (đậm)
                            bodySpacing: 10, // Khoảng cách giữa các dòng trong chú thích
                            ไตลDistance: 0, // Loại bỏ khoảng cách giữa label và giá trị

                            // Hiển thị label và giá trị theo định dạng phần trăm
                            formatter: function (value, context) {
                                const label = context.dataset.label + ": ";
                                const data = value + " (" + (value / context.dataset.data.reduce((a, b) => a + b, 0) * 100).toFixed(1) + "%)";
                                return label + data;
                            },
                        },
                    },
                ],
            },
            options: {
                plugins: {
                    legend: {
                        display: true, // Hiển thị chú thích (legend)
                    },
                    title: {
                        display: true,
                        text: "Predicted world population (millions) in 2050",
                    },
                },
                responsive: true,
            },
        });
    },

    chart_line: function () {
        var ctx = document.getElementById("line-chart").getContext("2d");
        var myChart = new Chart(ctx, {
            type: "line",
            data: {
                labels: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
                datasets: [
                    {
                        label: "Doanh thu (triệu đồng)",
                        data: [12, 19, 3, 5, 2, 3, 12, 19, 3, 5, 2, 3],
                        backgroundColor: "rgba(7, 113, 7,0.2)",
                        borderColor: "rgba(7, 113, 7,1)",
                        borderWidth: 1,
                    },
                ],
            },
            options: {
                plugins: {
                    legend: {
                        display: false,
                    },
                    title: {
                        display: true,
                        text: "Doanh thu hàng tháng",
                    },
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: "Doanh thu (Triệu đồng)", // Tên trục Y
                        },
                        ticks: {
                            callback: function (value) {
                                return value.toLocaleString();
                            },
                        },
                    },
                },
            },
        });
    }

}
function formatNumber(number, label) {
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") + " " + label;
}