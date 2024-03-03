export interface IProduct {
  id: string,
  name: string,
  description: string,
  price: number,
  timeToMakeInHours: number,
  mainPhotoId: string,
  categoryId: string,
  categoryName: string,
  composition?: string,
  weight?: number,
  width?: number,
  height?: number,

  photos: string[],
}
