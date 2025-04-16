"use strict";
const changeFlagInputElements = new Map();
document.addEventListener('DOMContentLoaded', () => {
    const pageLinks = document.querySelectorAll('.page-item');
    pageLinks.forEach(x => {
        x.addEventListener('click', onPageLinkClick);
    });
    const searchInput = document.querySelector('#search-input');
    searchInput === null || searchInput === void 0 ? void 0 : searchInput.addEventListener('change', ApplySearch);
    const inputImages = document.querySelectorAll('.input-image');
    inputImages.forEach(x => {
        x.addEventListener('change', OnMainPhotoInputChange);
    });
    searchInput === null || searchInput === void 0 ? void 0 : searchInput.focus();
});
function ApplySearch(e) {
    var _a;
    const searchElement = e.target;
    (_a = searchElement.form) === null || _a === void 0 ? void 0 : _a.submit();
}
//# sourceMappingURL=categories.js.map