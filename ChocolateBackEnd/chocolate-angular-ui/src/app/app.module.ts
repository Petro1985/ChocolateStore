import {NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {MainPageModule} from "./pages/main-page/main-page.module";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {NgOptimizedImage} from "@angular/common";
import {FooterComponent} from "./layouts/footer/footer.component";
import {HeaderComponent} from "./layouts/header/header.component";
import {NavigationComponent} from "./components/navigation/navigation.component";
import {TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {StorageService} from "./services/storage-service/storage-service";
import {FetchService} from "./services/fetch-service";
import {ImageService} from "./services/image-service";
import {SlickCarouselModule} from "ngx-slick-carousel";
import {ModalModule} from "./_modal";
import { LoginFormComponent } from './components/login-form/login-form.component';
import { SignupFormComponent } from './components/signup-form/signup-form.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
// import {IConfig, NgxMaskDirective, NgxMaskPipe, provideNgxMask} from "ngx-mask";
import { InputMaskModule } from '@ngneat/input-mask';

export function HttpLoaderFactory(http: HttpClient)
{
  return new TranslateHttpLoader(http);
}

// const maskConfigFunction: () => Partial<IConfig> = () => {
//   return {
//     validation: false,
//   };
// };


@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    HeaderComponent,
    FooterComponent,
    LoginFormComponent,
    SignupFormComponent,
  ],
  imports: [
    InputMaskModule.forRoot({ inputSelector: 'input', isAsync: true }),
    MainPageModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgOptimizedImage,
    ModalModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    SlickCarouselModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  providers: [FetchService, ImageService, HttpClient, StorageService],
  bootstrap: [AppComponent],
})
export class AppModule { }
