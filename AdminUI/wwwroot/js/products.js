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
    // const categoryRoots = document.querySelectorAll<HTMLTableRowElement>('tr[id^="category-@j"]');
    //
    // categoryRoots.forEach((x, ind) =>
    // {
    //     const fileInputs = document.querySelectorAll<HTMLInputElement>('input[type="file"]');
    //     const changeFlagInputs = document.querySelectorAll<HTMLInputElement>('[id^=photo-changed-]');
    // });
    //
    // const fileInputs = document.querySelectorAll<HTMLInputElement>('input[type="file"]');
    // fileInputs?.forEach(x =>
    // {
    //     x.addEventListener('change', onFileLoaded);
    // })
    //
    // const changeFlagInputs = document.querySelectorAll<HTMLInputElement>('[id^=photo-changed-]');
    // changeFlagInputs.forEach(x =>
    // {
    //     const index = Number(x.getAttribute('index'));
    //     changeFlagInputElements.set(index, );
    // });
});
function LoadCategory(e) {
    //@ts-ignore
    const selectElement = e.target;
    console.log(selectElement.value);
    //@ts-ignore
    selectElement.parentElement.submit();
    // @ts-ignore
    // const inputElement:HTMLInputElement = e.target;
    // const index = inputElement.getAttribute('index');
    //
    // document.querySelector(``);
}
function onPageLinkClick(e) {
    // @ts-ignore
    const page = this.getAttribute('page');
    const currentPageInput = document.querySelector('#input-currentPage');
    currentPageInput.value = page;
    const pagingForm = document.querySelector('#paging-form');
    pagingForm === null || pagingForm === void 0 ? void 0 : pagingForm.submit();
}
//# sourceMappingURL=products.js.map