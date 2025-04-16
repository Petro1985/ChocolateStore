declare var Inputmask: any;

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

    const inputImages = document.querySelectorAll<HTMLInputElement>('.input-image');
    inputImages.forEach(x =>
    {
        x.addEventListener('change', OnMainPhotoInputChange)
    })
})

function LoadCategory(e: Event)
{
    //@ts-ignore
    const selectElement: HTMLSelectElement = e.target;

    //@ts-ignore
    selectElement.parentElement.submit();
}


document.addEventListener("DOMContentLoaded", function () {
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
});
