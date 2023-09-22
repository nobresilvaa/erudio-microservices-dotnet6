using GeekShopping.IdentityServer.Utils;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(Repository));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var products = await _repository.FindById(id);
            if (products == null) return NotFound();
            return Ok(products);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductVO>> Creat([FromBody] ProductVO vo)
        {
            if (vo == null) return BadRequest();
            var products = await _repository.Creat(vo);
            return Ok(products);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO vo)
        {
            if (vo == null) return BadRequest();
            var products = await _repository.Update(vo);
            return Ok(products);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]

        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}
