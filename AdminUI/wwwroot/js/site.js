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
function _arrayBufferToBase64(buffer) {
    let binary = '';
    const bytes = new Uint8Array(buffer);
    const len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}
function OnInputImageChange(e) {
    return __awaiter(this, void 0, void 0, function* () {
        var _a;
        const inputElement = e.target;
        const imageElement = (_a = inputElement.parentElement) === null || _a === void 0 ? void 0 : _a.querySelector('img');
        const files = inputElement === null || inputElement === void 0 ? void 0 : inputElement.files;
        if (files && files.length > 0) {
            const file = files[0];
            const imageBase64 = _arrayBufferToBase64(yield file.arrayBuffer());
            imageElement.src = `data:image/png;base64, ${imageBase64}`;
        }
    });
}
//# sourceMappingURL=site.js.map