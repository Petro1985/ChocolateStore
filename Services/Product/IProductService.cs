﻿using Models;
using Models.Category;
using Models.Product;

namespace Services.Product;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllProducts();
    Task<IEnumerable<ProductDTO>> GetProductsByCategory(Guid categoryId);
    Task<IEnumerable<CategoryDTO>> GetAllCategories();
    Task<ProductDTO> GetProduct(Guid productId);
    Task<ProductDTO> GetProductWithPhotoIds(Guid productId);
    Task<CategoryDTO> GetCategory(Guid categoryId);
    Task<Guid> AddNewProduct(ProductCreateRequest product);
    Task UpdateProduct(ProductDTO product);
    Task UpdateCategory(CategoryDTO category);
    Task SetMainPhoto(Guid productId, Guid photoId);
    Task<Guid> AddNewCategory(CategoryDTO category);
}