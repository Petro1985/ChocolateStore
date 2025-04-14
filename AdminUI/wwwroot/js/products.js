"use strict";
document.addEventListener('DOMContentLoaded', () => {
    const productAccordion = document.querySelectorAll('#collapse-button');
    productAccordion.forEach(x => {
        x.addEventListener('click', (e) => {
            x.classList.toggle('toggled');
            console.log('Toggled', x);
        });
    });
    const categorySelector = document.querySelector('#category-selector');
    categorySelector === null || categorySelector === void 0 ? void 0 : categorySelector.addEventListener('change', LoadCategory);
    const pageLinks = document.querySelectorAll('.page-item');
    pageLinks.forEach(x => {
        x.addEventListener('click', onPageLinkClick);
    });
    const inputImages = document.querySelectorAll('.input-image');
    inputImages.forEach(x => {
        x.addEventListener('change', OnInputImageChange);
    });
});
function LoadCategory(e) {
    //@ts-ignore
    const selectElement = e.target;
    console.log(selectElement.value);
    //@ts-ignore
    selectElement.parentElement.submit();
}
//# sourceMappingURL=products.js.map