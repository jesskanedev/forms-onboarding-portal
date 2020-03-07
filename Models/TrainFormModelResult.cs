using System;

namespace forms_onboarding_portal.Models
{
    /// <summary>
    /// Result object from TrainModel operation.
    /// </summary>
    public class TrainFormModelResult
    {
        /// <summary>
        /// The ID of the trained model.
        /// </summary>
        public Guid ModelId { get; set; } 
    }
}
