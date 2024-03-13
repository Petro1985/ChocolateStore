import {Injectable} from "@angular/core";
import {environment} from "../../environments/environment";
import {BehaviorSubject, catchError, Observable, switchMap, tap, throwError} from "rxjs";
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {IUserLogin} from "./contracts/user-login";
import {UserClaim} from "./contracts/user-claim";

export interface IUser
{
  email: string,
  phone: string,
}

@Injectable({
  providedIn: 'root'
})
export class UserService
{
  private _serverUrl: string = environment.serverApiUrl;
  private _userClaims: BehaviorSubject<UserClaim[]> = new BehaviorSubject<UserClaim[]>([]);

  constructor(private client: HttpClient) {
  }

  public GetCurrentUserClaims(): BehaviorSubject<UserClaim[]>
  {
    return this._userClaims;
  }

  // public initUser()
  // {
  //   this.fet.GetUserInfo()
  //     .subscribe(
  //       {
  //         next: value => {
  //           this._user?.next(value);
  //         },
  //         error: err =>
  //         {
  //           localStorage.removeItem('token');
  //         }
  //       });
  // }

  public Login(email: string, password: string, remember: boolean) : Observable<UserClaim[]>
  {
    const url = this._serverUrl + 'Users/Login';
    const userLogin: IUserLogin = {
      userName: email,
      password: password,
      remember: remember,
    }

    const content = JSON.stringify(userLogin);

    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json",
      "Access-Control-Allow-Credentials": "true",
    });

    return this.client.post<string>(url, content, {headers: headers})
      .pipe(
        switchMap(x => {
          const url = this._serverUrl + 'Users/Info';
          const headers = new HttpHeaders({
            "Access-Control-Allow-Credentials": "true",
          });

          return this.client.get<UserClaim[]>(url, {headers: headers});
        }),
        tap({
          next: x => {
          this._userClaims.next(x);
          return x;
        }, error: _ => this._userClaims.next([])}),
        catchError((err) => {
          return throwError(() => err.error);
        })
      );
  }

  public isSignIn(): boolean
  {
    return this._userClaims.value.length > 0;
  }
}
