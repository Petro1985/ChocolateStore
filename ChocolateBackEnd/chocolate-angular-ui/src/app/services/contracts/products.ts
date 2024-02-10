export interface IProduct {
  id: string,
  name: string,
  description: string,
  priceRub: number,
  timeToMakeInHours: number,
  mainPhotoId: string,
  categoryId: string,
  categoryName: string,
  photos: string[],
}
