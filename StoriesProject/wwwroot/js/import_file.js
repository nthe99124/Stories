window.blazorFunctions = {
    setupImageHandling: function () {
        const fileInput = document.getElementById('fileInput');
        const imageContainer = document.getElementById('imageContainer');
        const imageModal = document.getElementById('imageModal');
        const modalImage = document.getElementById('modalImage');


        fileInput.addEventListener('change', function () {
            const files = this.files;
            console.log("files: ", files)
            for (let i = 0; i < files.length; i++) {
                const file = files[i];
                console.log("file: ", file)
                if (file.type.startsWith('image/')) {
                    const reader = new FileReader();
                    reader.onload = function (event) {
                        const imageUrl = event.target.result;
                        const imageItem = document.createElement('div');
                        imageItem.classList.add('imageItem');
                        // Lưu trữ thông tin cần thiết của File
                        imageItem.dataset.fileName = file.name;
                        imageItem.dataset.fileType = file.type;
                        imageItem.dataset.fileSize = file.size;
                        // console.log("file-x: ", imageItem.dataset.file);
                        const image = new Image();
                        image.src = imageUrl;
                        image.onload = function () {
                            const ratio = Math.min(135 / this.width, 180 / this.height);
                            this.width *= ratio;
                            this.height *= ratio;
                        };
                        const overlay = document.createElement('div');
                        overlay.classList.add('overlay');
                        const detailButton = document.createElement('button');
                        detailButton.textContent = 'Detail';
                        detailButton.addEventListener('click', function () {
                            modalImage.src = imageUrl;
                            imageModal.style.display = "block";
                        });
                        const deleteButton = document.createElement('button');
                        deleteButton.textContent = 'Delete';
                        overlay.appendChild(detailButton);
                        overlay.appendChild(deleteButton);
                        imageItem.appendChild(image);
                        imageItem.appendChild(overlay);
                        imageContainer.appendChild(imageItem);
                    };
                    reader.readAsDataURL(file);
                }
            }
        });

        imageContainer.addEventListener('click', function (event) {
            if (event.target.tagName === 'BUTTON' && event.target.textContent === 'Delete') {
                const imageItem = event.target.closest('.imageItem');
                imageContainer.removeChild(imageItem);
            }
        });

        // Đóng modal khi người dùng nhấp vào nút đóng hoặc bất kỳ đâu ngoài modal
        const closeBtn = document.querySelector(".close");
        closeBtn.addEventListener("click", function () {
            style.display = "none";
        });

        window.addEventListener("click", function (event) {
            if (event.target == imageModal) {
                imageModal.style.display = "none";
            }
        });
    }
};
function upload() {
    var images_raw = document.getElementById('imageContainer').getElementsByClassName('imageItem');
    console.log("images_raw: ", images_raw);
    var imageFiles = [];
    for (var i = 0; i < images_raw.length; i++) {
        // Lấy thông tin cần thiết của File từ thuộc tính dataset của imageItem
        var fileName = images_raw[i].dataset.fileName;
        var fileType = images_raw[i].dataset.fileType;
        var fileSize = images_raw[i].dataset.fileSize;
        // Tạo một đối tượng File mới từ thông tin đã lưu trữ
        var fileData = new File([], fileName, { type: fileType, lastModified: Date.now(), size: fileSize });
        imageFiles.push(fileData);
    }
    console.log("imageFiles: ", imageFiles);
    return imageFiles;
}
function addFiles() {
    // Trigger click event on the hidden file input
    document.getElementById('fileInput').click();
}