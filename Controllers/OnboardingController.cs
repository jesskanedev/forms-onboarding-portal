using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using forms_onboarding_portal.Configuration;
using forms_onboarding_portal.Models;
using forms_onboarding_portal.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.FormRecognizer;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;

namespace forms_onboarding_portal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnboardingController : ControllerBase
    {
        private readonly IFormRecognizerClient _formClient;

        public OnboardingController()
        {
            _formClient = new FormRecognizerClient(new ApiKeyServiceClientCredentials(FormRecogniserSettings.ApiKey))
            {
                Endpoint = FormRecogniserSettings.Endpoint
            };
        }

        [HttpPost("ProcessForms")]
        public async Task<IEnumerable<FormPredictionResult>> ProcessForms([FromForm]ProcessFormsRequest request)
        {
            var results = new List<FormPredictionResult>();

            var stagedFiles = await SaveFilesToStagingDirectory(request.RunId, request.Files);
            var runFileNames = stagedFiles.ToList();
            
            if (runFileNames.ToList().Count > 0)
            {
                foreach (var filePath in runFileNames)
                {
                    // Open saved file and send to form recognition API. For some reason, this doesn't work without doing the save first.
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        AnalyzeResult result = await _formClient.AnalyzeWithCustomModelAsync(
                            FormRecogniserSettings.TaxCodeFormModelId, fileStream, contentType: "application/pdf");

                        if (result.Errors.Count == 0 && result.Pages.Count > 0)
                        {
                            var extractedPage = result.Pages[0];

                            var predictions = new List<KeyValuePair<string, string>>();
                            foreach (var predictionResult in extractedPage.KeyValuePairs)
                            {
                                if (predictionResult.Key.Count > 0 && predictionResult.Value.Count > 0)
                                {
                                    predictions.Add(new KeyValuePair<string, string>(predictionResult.Key[0].Text, predictionResult.Value[0].Text));
                                }
                            }

                            results.Add(new FormPredictionResult { Id = Guid.NewGuid(), Predictions = predictions });
                        }
                    }
                }
            }

            return results;
        }

        private async Task<IEnumerable<string>> SaveFilesToStagingDirectory(Guid runId, IEnumerable<IFormFile> file)
        {
            List<string> runFileNames = new List<string>();
            foreach (var formFile in file)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath =
                        $"D:\\Dev\\Oceania\\StagingFiles\\{runId}\\{Guid.NewGuid()}.pdf";

                    // Save file to disk
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        runFileNames.Add(filePath);
                    }
                }
            }

            return runFileNames;
        }
    }
}
