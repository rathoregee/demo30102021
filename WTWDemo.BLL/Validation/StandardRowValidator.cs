using FluentValidation;
using Demo.BLL.Models;

namespace Demo.BLL.Validation
{
    public class StandardRowValidator : AbstractValidator<StandardPersonData>
    {
        const string ValidZip = @"^\d{5}(?:[-\s]\d{4})?$";
        public StandardRowValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is not valid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is not valid");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is not valid");
            RuleFor(x => x.Zip).NotEmpty().WithMessage("Zip is not valid");
            RuleFor(x => x.Zip).Matches(ValidZip).WithMessage("Zip is not valid");
        }       
    }
}