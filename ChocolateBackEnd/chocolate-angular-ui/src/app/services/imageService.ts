import {environment} from "../../environments/environment";

export class ImageService
{
  public getImageUrl(imageId: string): string
  {
    if (!imageId || imageId == '00000000-0000-0000-0000-000000000000')
    {
      return `./assets/NoImage.png`;
    }
    return `${environment.serverApiUrl}Photos/${imageId}`;
  }
}
