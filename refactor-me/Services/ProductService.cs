using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories;

namespace refactor_me.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<IList<Product>> SearchProductsByNameAsync(string name)
        {
            return await _productRepository.SearchProductsByNameAsync(name);
        }

        public async Task<Product> GetProductByProductIdAsync(Guid productId)
        {
            return await _productRepository.GetProductByProductIdAsync(productId);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            return await _productRepository.DeleteProductAsync(productId);
        }

        public async Task<IList<ProductOption>> GetAllProductOptionsAsync(Guid productId)
        {
            return await _productRepository.GetAllProductOptionsAsync(productId);
        }

        public async Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId)
        {
            return await _productRepository.GetProductOptionAsync(productId, optionId);
        }

        public async Task<ProductOption> CreateProductOptionAsync(ProductOption productOption)
        {
            return await _productRepository.CreateProductOptionAsync(productOption);
        }

        public async Task<ProductOption> UpdateProductOptionAsync(ProductOption productOption)
        {
            return await _productRepository.UpdateProductOptionAsync(productOption);
        }

        public async Task<bool> DeleteProductOptionAsync(Guid productId, Guid optionId)
        {
            return await _productRepository.DeleteProductOptionAsync(productId, optionId);
        }
    }
}