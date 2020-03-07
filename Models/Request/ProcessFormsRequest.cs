using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace forms_onboarding_portal.Models.Request
{
    /// <summary>
    /// Request object for Process Forms.
    /// </summary>
    public class ProcessFormsRequest
    {
        /// <summary>
        /// The ID of this run.
        /// </summary>
        public Guid RunId { get; set; }

        /// <summary>
        /// The set of files to process.
        /// </summary>
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
