using Data.Models;
using FluentValidation;

namespace refactor_me.Validations
{
    public class CreateUpdateProductOptionValidator : AbstractValidator<ProductOption>
    {
        public CreateUpdateProductOptionValidator()
        {
            //Validation rules
        }
    }
}