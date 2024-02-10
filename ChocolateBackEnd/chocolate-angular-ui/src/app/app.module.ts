import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NavigationComponent} from './components/navigation/navigation.component';
import {HeaderComponent} from './layouts/header/header.component';
import {FooterComponent} from './layouts/footer/footer.component';
import {NgOptimizedImage} from "@angular/common";
import {FetchService} from "./services/fetch-service";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ImageService} from "./services/imageService";
import {CategoriesListModule} from "./pages/categories-list/categories-list.module";
import {ProductsListModule} from "./pages/products-list/products-list.module";
import {TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {StorageService} from "./services/storage-service/storage-service";

export function HttpLoaderFactory(http: HttpClient)
{
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    HeaderComponent,
    FooterComponent,
  ],
  imports: [
    CategoriesListModule,
    ProductsListModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgOptimizedImage,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [FetchService, ImageService, HttpClient, StorageService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
