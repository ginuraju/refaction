using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Data.Infrastructure.Helpers;
using Data.Infrastructure.Logging;
using Data.Models;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(string connectionString)
        {
            _context = new DataContext(connectionString);
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return await TimingHelper.ExecuteWithTimingAsync(async () => await _context.Product.ToListAsync(),
                elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsFailed(elapsedTime, ex));
        }

        public async Task<IList<Product>> SearchProductsByNameAsync(string name)
        {
            return
                await
                    TimingHelper.ExecuteWithTimingAsync(
                        async () => await _context.Product.Where(p => p.Name.StartsWith(name)).ToListAsync(),
                        elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                        (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsFailed(elapsedTime, ex));
        }

        public async Task<Product> GetProductByProductIdAsync(Guid productId)
        {
            return
                await
                    TimingHelper.ExecuteWithTimingAsync(
                        async () =>
                            await _context.Product.SingleOrDefaultAsync(p => p.Id == productId && p.IsNew == false),
                        elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                        (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsFailed(elapsedTime, ex));
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await TimingHelper.ExecuteWithTimingAsync(async () =>
            {
                var newProduct = _context.Product.Add(product);
                await _context.SaveChangesAsync();
                return newProduct;
            },
                elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime));
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await TimingHelper.ExecuteWithTimingAsync(async () =>
            {
                var existingProduct = await _context.Product.FindAsync(product.Id);
                if (existingProduct == null || existingProduct.IsNew)
                {
                    return null;
                }
                _context.Product.AddOrUpdate(existingProduct);
                await _context.SaveChangesAsync();
                return product;
            },
                elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime));
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            return await TimingHelper.ExecuteWithTimingAsync(async () =>
            {
                var product = await _context.Product.FindAsync(productId);
                if (product == null)
                {
                    return false;
                }
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            },
                elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime));
        }

        public async Task<IList<ProductOption>> GetAllProductOptionsAsync(Guid productId)
        {
            return
                await
                    TimingHelper.ExecuteWithTimingAsync(
                        async () => await _context.ProductOption.Where(p => p.Id == productId).ToListAsync(),
                        elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                        (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsFailed(elapsedTime, ex));
        }

        public async Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId)
        {
            return
                await
                    TimingHelper.ExecuteWithTimingAsync(
                        async () =>
                            await
                                _context.ProductOption.SingleOrDefaultAsync(
                                    p => p.ProductId == productId && p.Id == optionId && p.IsNew == false),
                        elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                        (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsFailed(elapsedTime, ex));
        }

        public async Task<ProductOption> CreateProductOptionAsync(ProductOption productOption)
        {
            return await TimingHelper.ExecuteWithTimingAsync(async () =>
            {
                var newProductOption = _context.ProductOption.Add(productOption);
                await _context.SaveChangesAsync();
                return newProductOption;
            },
                elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime));
        }

        public async Task<ProductOption> UpdateProductOptionAsync(ProductOption productOption)
        {
            return await TimingHelper.ExecuteWithTimingAsync(async () =>
            {
                var existingProductOption = await _context.ProductOption.FindAsync(productOption.Id);
                if (existingProductOption == null || existingProductOption.IsNew)
                {
                    return null;
                }
                _context.ProductOption.AddOrUpdate(existingProductOption);
                await _context.SaveChangesAsync();
                return productOption;
            },
                elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime));
        }

        public async Task<bool> DeleteProductOptionAsync(Guid productId, Guid optionId)
        {
            return await TimingHelper.ExecuteWithTimingAsync(async () =>
            {
                var productOption = await _context.ProductOption.FindAsync(productId);
                if (productOption == null || productOption.ProductId != productId)
                {
                    return false;
                }
                _context.ProductOption.Remove(productOption);
                await _context.SaveChangesAsync();
                return true;
            },
                elapsedTime => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime),
                (elapsedTime, ex) => DatabaseEventSource.Log.RetrieveProductsCompleted(elapsedTime));
        }
    }
}