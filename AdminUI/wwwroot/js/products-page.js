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
        x.addEventListener('change', OnMainPhotoInputChange);
    });
});
function LoadCategory(e) {
    //@ts-ignore
    const selectElement = e.target;
    //@ts-ignore
    selectElement.parentElement.submit();
}
document.addEventListener("DOMContentLoaded", function () {
    const elements = document.querySelectorAll('[id^="weight-input-"]');
    elements.forEach(element => {
        Inputmask({ "regex": "^[0-9]*$" }).mask(element);
        console.log(element);
    });
    const widthElements = document.querySelectorAll('[id^="width-input-"]');
    widthElements.forEach(element => {
        Inputmask({ "regex": "^[0-9]*(\\,[0-9]{0,2})?$" }).mask(element);
        console.log(element);
    });
    const heightElements = document.querySelectorAll('[id^="height-input-"]');
    heightElements.forEach(element => {
        Inputmask({ "regex": "^[0-9]*(\\,[0-9]{0,2})?$" }).mask(element);
        console.log(element);
    });
    const priceElements = document.querySelectorAll('[id="UpdateProduct_Price"]');
    priceElements.forEach(element => {
        Inputmask({ "regex": "^[0-9]*(\\,[0-9]{0,2})?$" }).mask(element);
        console.log(element);
    });
});
//# sourceMappingURL=products-page.js.map