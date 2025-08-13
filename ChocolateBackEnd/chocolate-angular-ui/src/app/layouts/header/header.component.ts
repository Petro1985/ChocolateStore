import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {TranslateService} from "@ngx-translate/core";
import {localstorageConstants} from "../../constants/localstorage-constants";
import {StorageService} from "../../services/storage-service/storage-service";
import {Observable, debounceTime, distinctUntilChanged, filter, fromEvent, map, switchMap} from "rxjs";
import {ICategory} from "../../services/contracts/category";
import {ModalComponent} from "../../_modal/modal.component";
import {ModalService} from "../../_modal";
import {SearchService} from "../../services/search-service";
import {ImageService} from "../../services/image-service";
import { ICategorySearchResult, IProductSearchResult, ISearchResultResponse } from '../../services/contracts/search-result';
import { CategoryService } from '../../services/category-service';
import { ProductService } from '../../services/product-service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements AfterViewInit {
  public category$: Observable<ICategory | null>;
  public searchResults$: Observable<ISearchResultResponse>;
  public productResults: IProductSearchResult[] = [];
  public categoryResults: ICategorySearchResult[] = [];
  
  @ViewChild('loginModal') loginModal?: ModalComponent;
  @ViewChild('signupModal') signupModal?: ModalComponent;
  @ViewChild('searchInput') searchInput!: ElementRef;

  constructor(private translateService: TranslateService,
              private modalService: ModalService,
              private searchService: SearchService,
              private imageService: ImageService,
              private categoryService: CategoryService,
              private productService: ProductService) {
    this.category$ = this.categoryService.getCurrentCategory();
    this.searchResults$ = new Observable<ISearchResultResponse>();
  }

  private initializeSearch(): void {
    const searchInput = this.searchInput.nativeElement;
    
    // Subscribe to all input events to handle clearing results
    fromEvent(searchInput, 'input').subscribe((event: any) => {
      const searchText = event.target.value;
      if (searchText.length < 3) {
        this.clearSearchResults();
      }
    });

    // Main search subscription with delay and minimum length
    this.searchResults$ = fromEvent(searchInput, 'input').pipe(
      map((event: any) => event.target.value),
      filter(text => text.length > 2), // Only search if more than 2 characters
      debounceTime(500), // Wait 1 second after last input
      distinctUntilChanged(), // Only search if text changed
      switchMap(searchTerm => this.searchService.globalSearch(searchTerm))
    );

    // Subscribe to show results or handle errors
    this.searchResults$.subscribe({
      next: (results) => {
        console.log('Search results:', results);
        this.productResults = results.products;
        this.categoryResults = results.categories;
      },
      error: (error) => {
        console.error('Search error:', error);
        this.productResults = [];
        this.categoryResults = [];
      }
    });
  }

  ngAfterViewInit(): void {
    this.modalService.add(this.loginModal);
    this.modalService.add(this.signupModal);
    this.initializeSearch();
  }

  onLanguageChange(event: any) {
    localStorage.setItem(localstorageConstants.language, event.target.value);
    this.translateService.use(event.target.value)
  }

  onLoginClick() {
    this.modalService.open('loginModal');
  }

  getImageUrl(photoId?: string): string {
    return photoId ? this.imageService.getImageUrl(photoId) : './assets/NoImage.png';
  }

  private clearSearchResults(): void {
    this.productResults = [];
    this.categoryResults = [];
  }

  onCategoryClick(category: ICategorySearchResult): void {
    this.categoryService.setCurrentCategory(category.id);
    this.clearSearchResults();
    this.searchInput.nativeElement.value = '';
  }

  async onProductClick(product: IProductSearchResult): Promise<void> {
    this.categoryService.setCurrentCategory(product.categoryId);
    this.productService.setCurrentProduct(product.id);
    this.clearSearchResults();
    this.searchInput.nativeElement.value = '';
    this.modalService.open('product-modal');    
  }
}
