using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Gets all products asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<IList<Product>> GetAllProductsAsync();

        /// <summary>
        /// Searches the products by name asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task<IList<Product>> SearchProductsByNameAsync(string name);

        /// <summary>
        /// Gets the product by product identifier asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task<Product> GetProductByProductIdAsync(Guid productId);

        /// <summary>
        /// Creates the product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Task<Product> CreateProductAsync(Product product);

        /// <summary>
        /// Updates the product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Task<Product> UpdateProductAsync(Product product);

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(Guid productId);

        /// <summary>
        /// Gets all product options asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IList<ProductOption>> GetAllProductOptionsAsync(Guid productId);

        /// <summary>
        /// Gets the product option asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="optionId">The option identifier.</param>
        /// <returns></returns>
        Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId);

        /// <summary>
        /// Creates the product option asynchronous.
        /// </summary>
        /// <param name="productOption">The product option.</param>
        /// <returns></returns>
        Task<ProductOption> CreateProductOptionAsync(ProductOption productOption);

        /// <summary>
        /// Updates the product option asynchronous.
        /// </summary>
        /// <param name="productOption">The product option.</param>
        /// <returns></returns>
        Task<ProductOption> UpdateProductOptionAsync(ProductOption productOption);

        /// <summary>
        /// Deletes the product option asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="optionId">The option identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteProductOptionAsync(Guid productId, Guid optionId);
    }
}
