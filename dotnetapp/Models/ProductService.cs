using dotnetapp.Models;
using System.Linq;

namespace dotnetapp.Models
{
    public interface IProductService
    {
        public IQueryable<Product> GetProductList();
        public bool AddProduct(Product product);
        public bool DeleteProduct(int Id);
    }
    //Fill ur code
    public class ProductService : IProductService
    {
       private readonly ProductDBContext _dbContext;

        public ProductService(ProductDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Product> GetProductList()
        {
           return this._dbContext.Products;
          //return null;
        }
        public bool AddProduct(Product product)
        {
           var result = _dbContext.Products.Add(product);
           int res=_dbContext.SaveChanges();
           if(res > 0)
             {
                return true;
            }
            return false;
        }

        public bool DeleteProduct(int Id)
        {
            var filteredData = _dbContext.Products.Where(x => x.Id == Id).FirstOrDefault();
           var result = _dbContext.Remove(filteredData);
           _dbContext.SaveChanges();
            return result != null ? true : false;
           //return false;
        }
    }
}