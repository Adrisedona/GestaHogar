using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaHogar.Api.Data;
using GestaHogar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace GestaHogar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserProductsController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/UserProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProduct>>> GetUserProducts()
        {
            return await _context.UserProducts.Where(up => up.UserId == GetUserId()).ToListAsync();
        }

        // GET: api/UserProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProduct>> GetUserProduct(int id)
        {
            var userProduct = await _context.UserProducts.FindAsync(id, GetUserId());

            if (userProduct == null)
            {
                return NotFound();
            }

            return userProduct;
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

            _context.Entry(userProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProductExists(id, GetUserId()))
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
            _context.UserProducts.Add(userProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserProductExists(userProduct.ProductId, GetUserId()))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserProduct", new { id = userProduct.ProductId }, userProduct);
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

        private bool UserProductExists(int id, string userId)
        {
            return _context.UserProducts.Any(e => e.ProductId == id && e.UserId == GetUserId());
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User)!;
        }
    }
}
