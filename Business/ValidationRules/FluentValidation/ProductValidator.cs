using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(P => P.ProductName).NotEmpty();
            RuleFor(P => P.ProductName).MinimumLength(2);
            RuleFor(P => P.UnitPrice).NotEmpty();
            RuleFor(P => P.UnitPrice).GreaterThan(0);
          //  RuleFor(P => P.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //RuleFor(P => P.ProductName).Must(StartWhitA).WithMessage("Product name must start with A"); // custom validation
        }
        private bool StartWhitA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
