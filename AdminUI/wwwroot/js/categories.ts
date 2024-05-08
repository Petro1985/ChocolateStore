interface Category
{
    Id: string,
    Name: string,
    PhotoId: string,
    Photo: File,
    HasChanged: boolean,
}

const changeFlagInputElements: Map<number, Category> = new Map<number, Category>();

document.addEventListener('DOMContentLoaded', () =>
{
    const pageLinks  = document.querySelectorAll<HTMLSelectElement>('.page-item');
    pageLinks.forEach(x =>
    {
        x.addEventListener('click', onPageLinkClick);
    })

    const searchInput  = document.querySelector<HTMLInputElement>('#search-input');
    searchInput?.addEventListener('change', ApplySearch);

    const inputImages = document.querySelectorAll<HTMLInputElement>('.input-image');
    inputImages.forEach(x =>
    {
        x.addEventListener('change', OnInputImageChange)
    })
    
    searchInput?.focus();
})

function ApplySearch(e: any)
{
    const searchElement: HTMLInputElement = e.target;
    searchElement.form?.submit();
}

async function OnInputImageChange(e: any) {
    const inputElement: HTMLInputElement = e.target;
    const imageElement = inputElement.parentElement?.querySelector('img');
    
    const files = inputElement?.files;
    if (files && files.length > 0) {
        const file = files[0];
        const imageBase64 = _arrayBufferToBase64(await file.arrayBuffer());
        
        imageElement!.src = `data:image/png;base64, ${imageBase64}`;
    }
}

