using GestaHogar.Models;
using GestaHogar.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaHogar.DTO
{
    public class UserProductDto
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public UFloat Amount { get; set; }
        public EUnit Unit { get; set; }
        public UFloat CurrentStock { get; set; }
        public UFloat DailyUse { get; set; }
        public UFloat NormalStock { get; set; }

        public UserProductDto(UserProduct userProduct, Product product)
        {
            if (userProduct.ProductId != product.Id)
            {
                throw new ArgumentException("Ids don't match");
            }

            ProductId = userProduct.ProductId;
            UserId = userProduct.UserId;
            CurrentStock = userProduct.CurrentStock;
            DailyUse = userProduct.DailyUse;
            NormalStock = userProduct.NormalStock;
            ProductName = product.Name;
            Category = product.Category;
            Amount = product.Amount;

        }

        public UserProduct GetUserProduct()
        {
            return new UserProduct
            {
                ProductId = this.ProductId,
                UserId = this.UserId,
                CurrentStock = this.CurrentStock,
                DailyUse = this.DailyUse,
                NormalStock = this.NormalStock
            };
        }
    }
}
