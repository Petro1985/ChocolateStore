"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
let deletePhotoId = '';
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
    const inputMainPhotos = document.querySelectorAll('input[type="file"][id^="main-photo-file-"]');
    inputMainPhotos.forEach(x => {
        x.addEventListener('change', OnMainPhotoInputChange);
    });
    const inputNewPhotos = document.querySelectorAll('input[type="file"][id^="new-photo-file-"]');
    inputNewPhotos.forEach(x => {
        x.addEventListener('change', (e) => OnNewPhotoInputChange(e).then());
    });
    const deletePhotoButtons = document.querySelectorAll('[id^="delete-photo-"]');
    deletePhotoButtons.forEach(x => {
        x.addEventListener('click', showAndSetupDeleteModal);
    });
    const confirmButton = document.querySelectorAll('#confirmDelete');
    confirmButton[0].addEventListener('click', (e) => deletePhoto(e).then());
    const cancelButton = document.querySelectorAll('#cancelDelete');
    cancelButton[0].addEventListener('click', hideDeleteModal);
    setMasks();
});
function OnNewPhotoInputChange(e) {
    return __awaiter(this, void 0, void 0, function* () {
        //@ts-ignore
        const inputElement = e.target;
        const productId = inputElement.getAttribute('data-product-id');
        const files = inputElement === null || inputElement === void 0 ? void 0 : inputElement.files;
        if (files && files.length > 0) {
            for (let i = 0; i < files.length; i++) {
                const file = files[i];
                const imageBase64 = _arrayBufferToBase64(yield file.arrayBuffer());
                const url = `${BaseUrl}/Product/${productId}/Photos`;
                const request = {
                    "ProductId": productId,
                    "PhotoBase64": imageBase64,
                };
                yield fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(request)
                })
                    .then(response => {
                    if (!response.ok)
                        throw new Error('Network response was not ok');
                    return response.json();
                })
                    .catch(error => {
                    console.error('Error:', error);
                });
            }
        }
        window.location.assign(window.location.href);
    });
}
function LoadCategory(e) {
    //@ts-ignore
    const selectElement = e.target;
    //@ts-ignore
    selectElement.parentElement.submit();
}
function deletePhoto(e) {
    return __awaiter(this, void 0, void 0, function* () {
        if (!deletePhotoId)
            return;
        const url = `${BaseUrl}/Photos/${deletePhotoId}`;
        yield fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
            if (!response.ok)
                throw new Error('Network response was not ok');
            const photoElement = document.getElementById(`photo-${deletePhotoId}`);
            photoElement === null || photoElement === void 0 ? void 0 : photoElement.remove();
        })
            .catch(error => {
            console.error('Error:', error);
        });
        hideDeleteModal();
    });
}
function setMasks() {
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
}
function showAndSetupDeleteModal(e) {
    var _a;
    //@ts-ignore
    const inputElement = e.currentTarget;
    deletePhotoId = (_a = inputElement.getAttribute('data-photo-id')) !== null && _a !== void 0 ? _a : '';
    const modalElement = document.querySelector('#deleteModal');
    if (!modalElement)
        return;
    modalElement.classList.add('d-flex');
}
function hideDeleteModal() {
    const modalElement = document.querySelector('#deleteModal');
    modalElement === null || modalElement === void 0 ? void 0 : modalElement.classList.remove('d-flex');
}
//# sourceMappingURL=products-page.js.map