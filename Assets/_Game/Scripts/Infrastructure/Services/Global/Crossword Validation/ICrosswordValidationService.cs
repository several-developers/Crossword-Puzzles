using System;
using Core.Enums;

namespace Core.Infrastructure.Services.Global
{
    public interface ICrosswordValidationService
    {
        event Action<ValidateResult> OnValidationFinished;
        void Validate();
    }
}