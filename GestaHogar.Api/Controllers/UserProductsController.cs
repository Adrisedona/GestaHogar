using GestaHogar.Api.Data;
using GestaHogar.DTO;
using GestaHogar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaHogar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductsController(AppDbContext context, UserManager<User> userManager)
        : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly UserManager<User> _userManager = userManager;

        // Fix for the CS1061 error in the GetUserProducts method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProductDto>>> GetUserProducts()
        {
            var userProducts = await _context
                .UserProducts.Where(up => up.UserId == GetUserId())
                .ToListAsync();

            var userProductDtos = new List<UserProductDto>();

            foreach (var up in userProducts)
            {
                var product = await _context
                    .Products.Where(p => p.Id == up.ProductId)
                    .FirstOrDefaultAsync();

                if (product != null)
                {
                    userProductDtos.Add(new UserProductDto(up, product));
                }
            }

            return userProductDtos;
        }

        // GET: api/UserProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProductDto>> GetUserProduct(int id)
        {
            var userProduct = await _context.UserProducts.FindAsync(id, GetUserId());

            if (userProduct == null)
            {
                return NotFound();
            }

            var product = await _context
                .Products.Where(p => p.Id == userProduct.ProductId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return new UserProductDto(userProduct, product);
        }

        // PUT: api/UserProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProduct(int id, UserProduct userProduct)
        {
            if (id != userProduct.ProductId)
            {
                return BadRequest();
            }

            userProduct.UserId ??= GetUserId();

            _context.Entry(userProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProduct>> PostUserProduct(UserProduct userProduct)
        {
            userProduct.UserId ??= GetUserId();
            _context.UserProducts.Add(userProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserProductExists((int)userProduct.ProductId!))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest("Hubo un error con la solicitud");
                }
            }

            return CreatedAtAction(
                "GetUserProduct",
                new { id = userProduct.ProductId },
                userProduct
            );
        }

        // DELETE: api/UserProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProduct(int id)
        {
            var userProduct = await _context.UserProducts.FindAsync(id, GetUserId());
            if (userProduct == null)
            {
                return NotFound();
            }

            _context.UserProducts.Remove(userProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("updateall")]
        public async Task<IActionResult> UpdateAllUserProducts()
        {
            var userId = GetUserId();
            _context
                .UserProducts.Where(up => up.UserId == userId)
                .ToList()
                .ForEach(up => up.CurrentStock = up.NormalStock);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("updateproduct/{id}")]
        public async Task<IActionResult> UpdateUserProduct(int id)
        {
            var userId = GetUserId();
            var userProduct = await _context.UserProducts.FindAsync(id, userId);

            if (userProduct == null)
            {
                return NotFound();
            }

            userProduct.CurrentStock = userProduct.NormalStock;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProductExists(int id)
        {
            return _context.UserProducts.Any(e => e.ProductId == id && e.UserId == GetUserId());
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User)!;
        }
    }
}
