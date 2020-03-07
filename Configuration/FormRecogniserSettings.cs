using System;

namespace forms_onboarding_portal.Configuration
{
    public static class FormRecogniserSettings
    {
        public const string TrainingDataUrl = "";
        public const string Endpoint = "";
        public const string ApiKey = "";

        //TODO: update when necessary. This will refresh every time we train the model.
        public static Guid TaxCodeFormModelId = Guid.Empty;
    }
}
