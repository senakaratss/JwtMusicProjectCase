using FluentValidation;
using JwtMusicProjectCase.Business.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.ValidationRules
{
    public class RegisterValidator:AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Şifre boş bırakılamaz.")
               .MinimumLength(6)
               .WithMessage("Şifre en az 6 karakter olmalıdır.")
               .Matches("[0-9]")
               .WithMessage("Şifre en az bir rakam içermelidir.")
               .Matches("[a-z]")
               .WithMessage("Şifre en az bir küçük harf içermelidir.")
               .Matches("[A-Z]")
               .WithMessage("Şifre en az bir büyük harf içermelidir.")
               .Matches("[^a-zA-Z0-9]")
               .WithMessage("Şifre en az bir özel karakter içermelidir.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre tekrar alanı boş bırakılamaz.")
                .Equal(x => x.Password)
                .WithMessage("Şifreler eşleşmiyor.");
        }
    }
}
