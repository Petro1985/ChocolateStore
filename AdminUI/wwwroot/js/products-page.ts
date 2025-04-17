declare var Inputmask: any;

let deletePhotoId: string = '';

interface Category
{
    Id: string,
    Name: string,
    PhotoId: string,
    Photo: File,
    HasChanged: boolean,
}

document.addEventListener('DOMContentLoaded', () =>
{
    const productAccordion  = document.querySelectorAll('#collapse-button');
    
    productAccordion.forEach(x =>
    {
        x.addEventListener('click', (e) =>
        {
            x.classList.toggle('toggled');
            console.log('Toggled', x);
        })
    });

    const categorySelector  = document.querySelector<HTMLSelectElement>('#category-selector');
    categorySelector?.addEventListener('change', LoadCategory);

    const pageLinks  = document.querySelectorAll<HTMLSelectElement>('.page-item');
    
    pageLinks.forEach(x =>
    {
        x.addEventListener('click', onPageLinkClick);
    })

    const inputMainPhotos = document.querySelectorAll<HTMLInputElement>('input[type="file"][id^="main-photo-file-"]');
    inputMainPhotos.forEach(x =>
    {
        x.addEventListener('change', OnMainPhotoInputChange)
    })

    const inputNewPhotos = document.querySelectorAll<HTMLInputElement>('input[type="file"][id^="new-photo-file-"]');
    inputNewPhotos.forEach(x =>
    {
        x.addEventListener('change', (e) => OnNewPhotoInputChange(e).then())
    })

    const deletePhotoButtons = document.querySelectorAll<HTMLElement>('[id^="delete-photo-"]');
    deletePhotoButtons.forEach(x =>
    {
        x.addEventListener('click', showAndSetupDeleteModal);
    })

    const confirmButton = document.querySelectorAll<HTMLElement>('#confirmDelete');
    confirmButton[0].addEventListener('click', (e) => deletePhoto(e).then());

    const cancelButton = document.querySelectorAll<HTMLElement>('#cancelDelete');
    cancelButton[0].addEventListener('click', hideDeleteModal);

    setMasks();
})

async function OnNewPhotoInputChange(e: Event) {
    //@ts-ignore
    const inputElement: HTMLInputElement = e.target;
    const productId = inputElement.getAttribute('data-product-id');
        
    const files = inputElement?.files;
    if (files && files.length > 0) {
        
        for (let i = 0; i < files.length; i++) {
            const file = files[i];
            const imageBase64 = _arrayBufferToBase64(await file.arrayBuffer());

            const url = `${BaseUrl}/Product/${productId}/Photos`
            const request = {
                "ProductId": productId,
                "PhotoBase64": imageBase64,
            }

            await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(request)
            })
            .then(response => {
                if (!response.ok) throw new Error('Network response was not ok');
                return response.json();
            })
            .catch(error => {
                console.error('Error:', error);
            });
        }
    }
    window.location.assign(window.location.href);
}

function LoadCategory(e: Event)
{
    //@ts-ignore
    const selectElement: HTMLSelectElement = e.target;

    //@ts-ignore
    selectElement.parentElement.submit();
}

async function deletePhoto(e: Event) {
    if (!deletePhotoId) return;
    const url = `${BaseUrl}/Photos/${deletePhotoId}`;   
    
    await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) throw new Error('Network response was not ok');
        
        const photoElement = document.getElementById(`photo-${deletePhotoId}`);
        photoElement?.remove();
    })
    .catch(error => {
        console.error('Error:', error);
    });
        
    hideDeleteModal();   
}

function setMasks () {
    const elements = document.querySelectorAll('[id^="weight-input-"]');
    elements.forEach(element => {
        Inputmask({"regex": "^[0-9]*$"}).mask(element);
        console.log(element);
    });

    const widthElements = document.querySelectorAll('[id^="width-input-"]');
    widthElements.forEach(element => {
        Inputmask({"regex": "^[0-9]*(\\,[0-9]{0,2})?$"}).mask(element);
        console.log(element);
    });

    const heightElements = document.querySelectorAll('[id^="height-input-"]');
    heightElements.forEach(element => {
        Inputmask({"regex": "^[0-9]*(\\,[0-9]{0,2})?$"}).mask(element);
        console.log(element);
    });

    const priceElements = document.querySelectorAll('[id="UpdateProduct_Price"]');
    priceElements.forEach(element => {
        Inputmask({"regex": "^[0-9]*(\\,[0-9]{0,2})?$"}).mask(element);
        console.log(element);
    });
}

function showAndSetupDeleteModal(e: Event) {
    //@ts-ignore
    const inputElement: HTMLElement = e.currentTarget;
    deletePhotoId = inputElement.getAttribute('data-photo-id') ?? '';
        
    const modalElement = document.querySelector<HTMLElement>('#deleteModal');
    if (!modalElement) return;
    modalElement.classList.add('d-flex')
}

function hideDeleteModal() {
    const modalElement = document.querySelector<HTMLElement>('#deleteModal');
    modalElement?.classList.remove('d-flex');
}
