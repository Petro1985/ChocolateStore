
function onPageLinkClick(e: any)
{
    // @ts-ignore
    const page = this.getAttribute('page');
    const currentPageInput = document.querySelector<HTMLInputElement>('#input-currentPage');
    currentPageInput!.value = page;
    const pagingForm = document.querySelector<HTMLFormElement>('#paging-form');
    pagingForm?.submit();
    console.log('submitted', pagingForm, currentPageInput!.value)
}
