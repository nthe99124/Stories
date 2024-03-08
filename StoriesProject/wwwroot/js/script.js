
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