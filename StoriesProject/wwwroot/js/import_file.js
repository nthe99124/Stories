window.blazorFunctions = {
    setupImageHandling: function () {
        const fileInput = document.getElementById('fileInput');
        const imageContainer = document.getElementById('imageContainer');
        const imageModal = document.getElementById('imageModal');
        const modalImage = document.getElementById('modalImage');

        fileInput.addEventListener('change', function () {
            const files = this.files;
            for (let i = 0; i < files.length; i++) {
                const file = files[i];
                if (file.type.startsWith('image/')) {
                    const reader = new FileReader();
                    reader.onload = function (event) {
                        const imageUrl = event.target.result;
                        const imageItem = document.createElement('div');
                        imageItem.classList.add('imageItem');
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
    var imagesUrl = [];
    for (var i = 0; i < images_raw.length; i++) {
        var image = images_raw[i].getElementsByTagName('img')[0];
        imagesUrl.push(image.src);
    }
    console.log("imagesUrl: ", imagesUrl);
    return imagesUrl;

    // for (var i = 0; i < images.length; i++)
    // {
    //   var image = images[i];
    //   var formData = new FormData();
    //   formData.append('image', image.src);
    //   var xhr = new XMLHttpRequest();
    //   xhr.open('POST', 'upload.php', true);
    //   xhr.onload = function ()
    //   {
    //     if (xhr.status === 200)
    //     {
    //       console.log('Image uploaded successfully!');
    //     }
    //   };
    //   xhr.send(formData);
    // }
}
function addFiles() {
    // Trigger click event on the hidden file input
    document.getElementById('fileInput').click();
}