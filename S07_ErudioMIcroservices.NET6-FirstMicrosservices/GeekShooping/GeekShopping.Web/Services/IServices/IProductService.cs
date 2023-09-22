using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService 
    {
        public Task<IEnumerable<ProductModel>> FindAllProducts();
        public Task<ProductModel> FindProductById(long Id);
        public Task<ProductModel> CreateProduct(ProductModel model);
        public Task<ProductModel> UpdateProduct(ProductModel model);
        public Task<bool> DeleteProductById(long Id);
    }
}
