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

})

function LoadCategory(e: Event)
{
    //@ts-ignore
    const selectElement: HTMLSelectElement = e.target;
    console.log(selectElement.value);

    //@ts-ignore
    selectElement.parentElement.submit();
}


