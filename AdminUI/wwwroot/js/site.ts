function _arrayBufferToBase64( buffer: ArrayBuffer ) {
    let binary = '';
    const bytes = new Uint8Array( buffer );
    const len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
        binary += String.fromCharCode( bytes[ i ] );
    }
    
    return window.btoa( binary );
}

async function OnMainPhotoInputChange(e: any) {
    const inputElement: HTMLInputElement = e.target;
    const imageElement = inputElement.parentElement?.parentElement?.querySelector('img');

    const files = inputElement?.files;
    if (files && files.length > 0) {
        const file = files[0];
        const imageBase64 = _arrayBufferToBase64(await file.arrayBuffer());

        imageElement!.src = `data:image/png;base64, ${imageBase64}`;
    }
}
