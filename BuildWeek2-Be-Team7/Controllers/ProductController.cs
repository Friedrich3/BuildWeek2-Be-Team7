using BuildWeek2_Be_Team7.DTOs.Pharmacy;
using BuildWeek2_Be_Team7.Models.Pharmacy;
using BuildWeek2_Be_Team7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek2_Be_Team7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto addProductDto)
        {
            try
            {
                var result = await _productService.AddProductAsync(addProductDto);

                if (!result) 
                {
                    return BadRequest(new { messagge = "Ops qualcosa è andato storto" });
                }

                return Ok(new { message = "Prdotto aggiunto con successo" });
            }
            catch
            {
                return StatusCode(500, new { messagge = "Ops qualcosa è andato storto!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var Products = await _productService.GetAllProductsAsync();

                if (Products == null)
                {
                    return BadRequest(new { message = "Ops qualcosa è andato storto!" });
                }

                int count = Products.Count();
                string countstring = count == 1 ? $"{count} prodotto trovato" : $"{count} prodotti trovati";

                return Ok(new { message = countstring, Products });
            }
            catch
            {
                return StatusCode(500, new { messagge = "Ops qualcosa è andato storto!" });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var Product = await _productService.GetProductByIdAsync(id);

                if (Product == null)
                {
                    return BadRequest(new { message = "Ops qualcosa è andato storto!" });
                }

               return Ok(new { Product });
            }
            catch
            {
                return StatusCode(500, new { messagge = "Ops qualcosa è andato storto!" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangeProduct([FromQuery] Guid id, [FromBody] ChangeProductDto changeProductDto)
        {
            try
            {
                var result = await _productService.ChangeProductAsync(id, changeProductDto);

                if (!result)
                {
                    return BadRequest(new { message = "Ops qualcosa è andato storto!" });
                }

                return Ok(new { message = "Prodotto aggiornato con successo!" });
            }
            catch
            {
                return StatusCode(500, new { messagge = "Ops qualcosa è andato storto!" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);

                if (!result)
                {
                    return BadRequest(new { message = "Ops qualcosa è andato storto!" });
                }

                return Ok(new { message = "Prodotto eliminato con successo!" });
            }
            catch
            {
                return StatusCode(500, new { messagge = "Ops qualcosa è andato storto!" });
            }
        }
    }
}
