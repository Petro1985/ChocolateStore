import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { HeaderComponent } from './layouts/header/header.component';
import { FooterComponent } from './layouts/footer/footer.component';
import { NgOptimizedImage } from "@angular/common";
import { CategoriesComponent } from './components/categories/categories.component';
import { CategoryCardComponent } from './components/category-card/category-card.component';
import { FetchService } from "./services/fetch-service";
import {HttpClientModule} from "@angular/common/http";
import {ImageService} from "./services/imageService";

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        NavigationComponent,
        HeaderComponent,
        FooterComponent,
        CategoriesComponent,
        CategoryCardComponent,
        CategoryCardComponent,
    ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgOptimizedImage
  ],
  providers: [FetchService, ImageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
