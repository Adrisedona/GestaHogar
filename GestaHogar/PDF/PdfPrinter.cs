using ArrayToPdf;
using GestaHogar.DTO;
using GestaHogar.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaHogar.PDF
{
    public static class PdfPrinter
    {
        public static async Task PrintPdf(string filePath, IEnumerable<UserProductDto> userProducts)
        {
            CreateParentFolder(filePath);

            UFloat dif;
            byte[] pdf = userProducts.ToPdf(schema => schema
                .PageOrientation(PdfOrientations.Portrait)
                .PageMarginLeft(15)
                .PageMarginRight(15)
                .AddColumn("Nombre del producto", p => p.ProductName)
                .AddColumn("Cantidad actual", p => p.CurrentStock)
                .AddColumn("Cantidad normal", p => p.NormalStock)
                .AddColumn("Cantida a comprar", p => (dif = p.NormalStock - p.CurrentStock) > 0 ? dif : 0)
                .AddColumn("Unidad", p => p.Unit.ToString())
            );

            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            await stream.WriteAsync(pdf);
        }

        private static void CreateParentFolder(string filePath)
        {
            string parentPath;
            if (!Directory.Exists(parentPath = new FileInfo(filePath).DirectoryName!))
            {
                CreateParentFolder(parentPath);
                Directory.CreateDirectory(parentPath);
            }
        }
    }
}
