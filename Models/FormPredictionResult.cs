using System;
using System.Collections.Generic;

namespace forms_onboarding_portal.Models
{
    /// <summary>
    /// Result object from Analyze model operation.
    /// </summary>
    public class FormPredictionResult
    {
        /// <summary>
        /// The ID of the result.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The predictions.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Predictions { get; set; }
    }
}
