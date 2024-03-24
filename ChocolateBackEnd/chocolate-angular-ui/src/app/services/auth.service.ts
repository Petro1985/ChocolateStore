import {Injectable} from '@angular/core';
import {Router,} from '@angular/router';
import {OidcSecurityService} from "angular-auth-oidc-client";

export interface IUser {
  email: string;
  email_verified: boolean;
  sub: string;
  avatarUrl?: string;
}

const defaultPath = '/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _user: IUser | null = null;
  private _isAuthenticated: boolean = false;

  get loggedIn(): boolean {
    return this._isAuthenticated;
  }

  private _lastAuthenticatedPath: string = defaultPath;

  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  constructor(private router: Router, private oidcSecurityService: OidcSecurityService) {
    this.oidcSecurityService.getUserData()
      .subscribe({
        next: v => {
          this._user = v;
          console.log('Пользователь получен -> ', v)
        }
      });

    this.oidcSecurityService.isAuthenticated$
      .subscribe({
        next: v => {
          this._isAuthenticated = v.isAuthenticated;
          console.log('Пользователь залогинен? -> ', v.isAuthenticated)
        }
      });
  }

  async logIn() {
    try {
      if (!this._isAuthenticated)
      {
        this.oidcSecurityService.authorize();
      }

      return {
        isOk: true,
        data: this._user
      };
    } catch {
      return {
        isOk: false,
        message: "Authentication failed"
      };
    }
  }

  getUser() {
    return this._user;
  }

  async logOut() {
    await this.oidcSecurityService.logoff()
      .subscribe({
        next: value => console.log(value)
      });
    // await this.router.navigate(['/login-form']);
  }

  async checkAuth() {
    await this.oidcSecurityService.checkAuth().subscribe({
    next: ({isAuthenticated, userData, accessToken}) => {
      console.log('Авторизация успешна пользователь ->', userData);
      this._user = userData;
    },
      error: err =>
      {
        console.error('ошибка -> ', err);
      }
    }
    );
  }
}

