"use strict";
function onPageLinkClick(e) {
    // @ts-ignore
    const page = this.getAttribute('page');
    const currentPageInput = document.querySelector('#input-currentPage');
    currentPageInput.value = page;
    const pagingForm = document.querySelector('#paging-form');
    pagingForm === null || pagingForm === void 0 ? void 0 : pagingForm.submit();
    console.log('submitted', pagingForm, currentPageInput.value);
}
//# sourceMappingURL=paging.js.map