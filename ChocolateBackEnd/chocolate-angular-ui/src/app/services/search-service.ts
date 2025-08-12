import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ISearchResultResponse } from './contracts/search-result';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private readonly _serverUrl: string = environment.serverApiUrl;

  constructor(private http: HttpClient) { }

  public globalSearch(searchTerm: string): Observable<ISearchResultResponse> {
    const path = `${this._serverUrl}Search?SearchString=${encodeURIComponent(searchTerm)}`;
    return this.http.get<ISearchResultResponse>(path);
  }
}
