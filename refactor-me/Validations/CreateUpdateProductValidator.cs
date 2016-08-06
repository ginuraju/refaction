using Data.Models;
using FluentValidation;

namespace refactor_me.Validations
{
    public class CreateUpdateProductValidator : AbstractValidator<Product>
    {
        public CreateUpdateProductValidator()
        {
            //Validation rules
        }
    }
}