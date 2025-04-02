using BuildWeek2_Be_Team7.DTOs.Pharmacy;
using BuildWeek2_Be_Team7.Models.Pharmacy;
using BuildWeek2_Be_Team7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek2_Be_Team7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Farmacista, Admin")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto addProductDto)
        {
            try
            {
                var result = await _productService.AddProductAsync(addProductDto);

                if (!result) 
                {
                    return BadRequest(new { messagge = "Ops qualcosa è andato storto" });
                }

                return Ok(new { message = "Prodotto aggiunto con successo" });
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
        public async Task<IActionResult> ChangeProduct([FromQuery] Guid id, [FromForm] ChangeProductDto changeProductDto)
        {           
            try
            { 
               //ModelState.Clear(); 
                var result = await _productService.ChangeProductAsync(id, changeProductDto);

                if (!result)
                {
                    return BadRequest(new { message = "Ops, qualcosa è andato storto!" });
                }

                return Ok(new { message = "Prodotto aggiornato con successo!" });
            }
            catch
            {
                return StatusCode(500, new { message = "Errore interno del server!" });
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

        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _productService.GetAllCategoriesAsync();

                if (categories == null)
                {
                    return BadRequest(new { message = "Ops qualcosa è andato storto!" });
                }

                return Ok(new { categories });
            }
            catch
            {
                return StatusCode(500, new { messagge = "Ops qualcosa è andato storto!" });
            }
        }

        [HttpGet("drawers")]
        public async Task<IActionResult> GetDrawers()
        {
            try
            {
                var result = await _productService.getDrawers();

                if (result == null)
                {
                    return BadRequest(new { message = "Ops qualcosa è andato storto!" });
                }

                return Ok(new { message = "Bravi TUTTI clapclap", data = result });
            }
            catch
            {
                return StatusCode(500, new { messagge = "Ops qualcosa è andato storto!" });
            }
        }
}
