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
        public UFloat NormalStock { get; set; }

        public UFloat CurrentStock { get; set; }
        public UFloat DailyUse { get; set; }

    }
}
