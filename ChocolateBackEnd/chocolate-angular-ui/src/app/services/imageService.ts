import {environment} from "../../environments/environment";

export class ImageService
{
  public getImageUrl(imageId: string): string
  {
    return `${environment.serverApiUrl}Photos/${imageId}`;
  }
}
