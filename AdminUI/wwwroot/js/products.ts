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
})


function LoadCategory(e: Event)
{
    //@ts-ignore
    const selectElement: HTMLSelectElement = e.target;
    console.log(selectElement.value);

    //@ts-ignore
    selectElement.parentElement.submit();
    // @ts-ignore
    // const inputElement:HTMLInputElement = e.target;
    // const index = inputElement.getAttribute('index');
    //
    // document.querySelector(``);
}

function onPageLinkClick(e: any)
{
    // @ts-ignore
    const page = this.getAttribute('page');
    
    const currentPageInput = document.querySelector<HTMLInputElement>('#input-currentPage');
    currentPageInput!.value = page;
    const pagingForm = document.querySelector<HTMLFormElement>('#paging-form');
    pagingForm?.submit()    
}

