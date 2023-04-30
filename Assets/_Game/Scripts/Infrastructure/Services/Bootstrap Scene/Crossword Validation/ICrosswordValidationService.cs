using System;
using Core.Enums;

namespace Core.Infrastructure.Services.BootstrapScene
{
    public interface ICrosswordValidationService
    {
        event Action<ValidateResult> OnValidationFinished;
        void Validate();
    }
}