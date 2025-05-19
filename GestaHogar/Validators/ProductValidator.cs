using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GestaHogar.Models;
using GestaHogar.Util;

namespace GestaHogar.Validators
{

    public class ProductValidator : AbstractValidator<Product>
    {
        //TODO: crear lista de palabas censuradas
        private static readonly string[] CENSORED_WORDS = { };

        public ProductValidator()
        {
            RuleFor(p => p.Unit).IsInEnum();
            RuleFor(p => p.Name).Must(n => !CENSORED_WORDS.Any(w => n.Equals(w, StringComparison.CurrentCultureIgnoreCase)));
            RuleFor(p => p.Category).Must(n => !CENSORED_WORDS.Any(w => n.Equals(w, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
