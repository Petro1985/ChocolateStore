import {environment} from "../../environments/environment";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
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

  public getThumbnailImageUrl(imageId: string): string
  {
    if (!imageId || imageId == '00000000-0000-0000-0000-000000000000')
    {
      return `./assets/NoImage.png`;
    }
    return `${environment.serverApiUrl}Photos/Thumbnail/${imageId}`;
  }
}
