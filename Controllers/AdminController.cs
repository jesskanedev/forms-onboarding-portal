using System.Threading.Tasks;
using forms_onboarding_portal.Configuration;
using forms_onboarding_portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.FormRecognizer;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;

namespace forms_onboarding_portal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IFormRecognizerClient _formClient;

        public AdminController()
        {
            _formClient = new FormRecognizerClient(new ApiKeyServiceClientCredentials(FormRecogniserSettings.ApiKey)) { Endpoint = FormRecogniserSettings.Endpoint };
        }

        [HttpPost("TrainModel")]
        public async Task<TrainFormModelResult> TrainModel()
        {
            TrainResult result = await _formClient.TrainCustomModelAsync(new TrainRequest(FormRecogniserSettings.TrainingDataUrl));
            return new TrainFormModelResult { ModelId = result.ModelId };
        }
    }
}
