using ApiWithAuhtenticationBearer.Models;
using FluentValidation;

namespace ApiWithAuhtenticationBearer.Validators
{
    public class ValidationRegisterUserDto : AbstractValidator<RegisterUserDto>
    {
        //tylko db context validuje
        //public ValidationRegisterUserDto(ApplicationDbContext dbContext)
        //{
        //    RuleFor(x => x.Email)
        //    .NotEmpty()
        //    .EmailAddress(); // w postaci adresu mail !

        //    RuleFor(x => x.Password).MinimumLength(6);

        //    RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

        //    RuleFor(x => x.Email)
        //    .Custom((value, context) =>
        //    {
        //        var emailExistYet = dbContext.Users.Any(u => u.Email == value);
        //        if (emailExistYet)
        //        {
        //            context.AddFailure("Email", "That email is taken!");
        //        }
        //    });
        //}
    }
}
