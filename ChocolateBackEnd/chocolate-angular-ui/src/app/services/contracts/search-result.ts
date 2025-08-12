export interface ISearchResultResponse {
    products: IProductSearchResult[];
    categories: ICategorySearchResult[];
}

export interface ICategorySearchResult {
    id: string;
    name: string;
    mainPhotoId?: string;
}

export interface IProductSearchResult {
    id: string;
    name: string;
    categoryId: string;
    mainPhotoId?: string;
}
