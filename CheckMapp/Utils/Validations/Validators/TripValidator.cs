using System;
using System.Linq;
using FluentValidation;
using CheckMapp.Model.Tables;
using CheckMapp.Utils.Validations.Validators.CustomValidators;
using CheckMapp.Resources;

namespace CheckMapp.Utils.Validations.Validators
{
    public class TripValidator : AbstractValidator<Trip>
    {
        public TripValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(AppResources.Error_EmptyName);
            RuleFor(x => x.MainPictureData).NotNull().WithMessage(AppResources.Error_EmptyPicture);
            RuleFor(x => x.DepartureLatitude).SetValidator(new ValidateTripCoordinates());
        }
    }
}
