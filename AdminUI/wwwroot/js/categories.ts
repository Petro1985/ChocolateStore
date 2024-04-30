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


function onFileLoaded(e: Event)
{
    // @ts-ignore
    // const inputElement:HTMLInputElement = e.target;
    // const index = inputElement.getAttribute('index');
    //
    // document.querySelector(``);

}