using BuildWeek2_Be_Team7.Data;
using BuildWeek2_Be_Team7.DTOs.Pharmacy;
using Microsoft.EntityFrameworkCore;
using BuildWeek2_Be_Team7.Models.Pharmacy;

namespace BuildWeek2_Be_Team7.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddProductAsync(AddProductDto addProductDto)
        {
            try
            {

                string webPath = null;
                if (addProductDto.Image != null)
                {
                    var fileName = addProductDto.Image.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "assets", "images", fileName);
                    await using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await addProductDto.Image.CopyToAsync(stream);
                    }
                    webPath = Path.Combine("assets", "images", fileName);
                }

                var existingCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Name == addProductDto.CompanyName);

                if (existingCompany == null)
                {
                    existingCompany = new Company()
                    {
                        Address = addProductDto.Address,
                        Name = addProductDto.CompanyName,
                        Tel = addProductDto.Tel,
                    };

                    _context.Companies.Add(existingCompany);
                }

                var newProduct = new Product()
                {
                    Name = addProductDto.Name,
                    Image = webPath,
                    Price = addProductDto.Price,
                    isMed = addProductDto.isMed,
                    IdCategory = addProductDto.CategoryId,
                    IdDrawer = addProductDto.DrawerId,
                    IdCompany = existingCompany.Id
                };

                _context.Add(newProduct);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                {
                    _logger.LogError(ex, ex.Message);
                    return false;
                }
            }
        }

        public async Task<List<ProductResponseDto>?> GetAllProductsAsync()
        {
            try
            {
                var ProductsList = await _context.Products.Include(p => p.Company).Include(p => p.Drawer).Include(p => p.Category).ToListAsync();

                var Products = new List<ProductResponseDto>();

                foreach (var product in ProductsList)
                {
                    var newProduct = new ProductResponseDto()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        isMed = product.isMed,
                        Image = product.Image,
                        Drawer = new DrawerDto()
                        {
                            Name = product.Drawer.Name,
                            Position = product.Drawer.Position
                        },
                        Company = new CompanyDto()
                        {
                            Name = product.Company.Name,
                            Address = product.Company.Address,
                            Tel = product.Company.Tel
                        },
                        CategoryName = product.Category.Name
                    };
                    Products.Add(newProduct);
                }

                return Products;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
        {
            try
            {
                var existingProduct = await _context.Products.Include(p => p.Company).Include(p => p.Drawer).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

                if (existingProduct == null)
                {
                    return null;
                }

                var Product = new ProductResponseDto()
                {
                    Id = existingProduct.Id,
                    Name = existingProduct.Name,
                    Price = existingProduct.Price,
                    isMed = existingProduct.isMed,
                    Image = existingProduct.Image,
                    Drawer = new DrawerDto()
                    {
                        Name = existingProduct.Drawer.Name,
                        Position = existingProduct.Drawer.Position
                    },
                    Company = new CompanyDto()
                    {
                        Name = existingProduct.Company.Name,
                        Address = existingProduct.Company.Address,
                        Tel = existingProduct.Company.Tel
                    },
                    CategoryName = existingProduct.Category.Name
                };

                return Product;
            }
            catch
            {
                return null;
            }
        }

        
        public async Task<bool> ChangeProductAsync(Guid id, ChangeProductDto changeProductDto)
        {
            try
            {
                var existingProduct = await _context.Products.Include(p => p.Company).Include(p => p.Drawer).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

                if (existingProduct == null)
                {
                    return false;
                }

                string webPath = existingProduct.Image;
                if (changeProductDto.Image != null)
                {
                    if (!string.IsNullOrEmpty(existingProduct.Image))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), existingProduct.Image.TrimStart('/'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }

                    var fileName = changeProductDto.Image.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "assets", "images", fileName);

                    await using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await changeProductDto.Image.CopyToAsync(stream);
                    }
                    webPath = "/assets/images/" + fileName;
                }

                existingProduct.Image = webPath;
                existingProduct.Name = changeProductDto.Name;
                existingProduct.Price = changeProductDto.Price;
                existingProduct.isMed = changeProductDto.isMed;
                existingProduct.IdDrawer = changeProductDto.DrawerId;

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            try
            {
                var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (existingProduct == null)
                {
                    return false;
                }

                _context.Products.Remove(existingProduct);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
