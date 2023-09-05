using FluentValidation;

namespace CarAPI.Models.Validators
{
    public class CarValidators : AbstractValidator<CarDTO>
    {
        public CarValidators() 
        {
            RuleFor(x => x.Name).NotEqual("string").NotEqual(""); 
            RuleFor(x => x.Brand).NotEqual("string").NotEqual("");
        }  
    }
}