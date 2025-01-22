using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using ApiNet6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ApiNet6.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]")]
    [Authorize] //JWT
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly DbapiContext _dbapiContext;

        public ProductController(DbapiContext dbapiContext)
        {
            _dbapiContext = dbapiContext;
        }

        [HttpGet]
        [Route("List")]
        public IActionResult List()
        { 
            List<Product> products = new List<Product>();
            try
            {
                products = _dbapiContext.Products.Include(oC => oC.oCategory).ToList();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok", Response = products });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, Response = products });
            }
        }

        [HttpGet]
        [Route("ListById/{idProduct:int}")]
        public IActionResult ListById(int idProduct)
        {
            Product product = new Product();
            try
            {
                product = _dbapiContext.Products.Find(idProduct);

                if (product == null)
                {
                    return BadRequest("Product not found");
                }

                product = _dbapiContext.Products.Where(x => x.IdProduct == idProduct).Include(oC => oC.oCategory).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok", Response = product });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, Response = product });
            }
        }

        [HttpPost]
        [Route("SaveProduct")]
        public IActionResult SaveProduct([FromBody] Product product)
        {
            try
            {
                Product productAdd = new Product
                {
                    IdCategory = product.IdCategory,
                    Description = product.Description,
                    Barcode = product.Barcode,
                    Brand = product.Brand,
                    Price = product.Price
                };

                _dbapiContext.Products.Add(productAdd);
                _dbapiContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok", Response = product });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message});
            }
        }

        [HttpPut]
        [Route("EditProduct")]
        public IActionResult Edit([FromBody] Product product)
        {
            try
            {
                Product productFound = _dbapiContext.Products.Find(product.IdProduct);

                if (productFound == null)
                {
                    return BadRequest("Product not found");
                }

                _dbapiContext.Products.Update(product);
                _dbapiContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteProduct/{idProduct:int}")]
        public IActionResult Delete(int idProduct)
        {
            try
            {
                Product productFound = _dbapiContext.Products.Find(idProduct);

                if (productFound == null)
                {
                    return BadRequest("Product not found");
                }

                _dbapiContext.Products.Remove(productFound);
                _dbapiContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
