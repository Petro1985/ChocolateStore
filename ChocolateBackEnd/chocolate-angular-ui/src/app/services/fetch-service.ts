import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ICategory} from "./contracts/category";
import {Injectable} from "@angular/core";

@Injectable()
export class FetchService
{

  private _serverUrl: string = environment.serverApiUrl;

  constructor(private client: HttpClient)
  {
  }

  public GetCategories(): Observable<ICategory[]>
  {
    const path = this._serverUrl + 'categories';
    return this.client.get<ICategory[]>(path);
  }
}
