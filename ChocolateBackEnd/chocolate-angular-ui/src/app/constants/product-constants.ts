import {IProduct} from "../services/contracts/products";

export const EmptyProduct: IProduct = {
  id: '',
  name: '',
  description: '',
  price: 0,
  timeToMakeInHours: 0,
  mainPhotoId: '',
  categoryId: '',
  categoryName: '',
  photos: []
}
