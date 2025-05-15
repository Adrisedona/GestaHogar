using GestaHogar.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaHogar.Models
{
    [PrimaryKey("UserId", "ProductId")]
    public class UserProduct
    {
        public required string UserId { get; set; }
        public int ProductId { get; set; }
        public UDouble NormalStock { get; set; }

        public UDouble CurrentStock { get; set; }
        public UDouble DailyUse { get; set; }

    }
}
