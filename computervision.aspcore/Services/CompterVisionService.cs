using computervision.aspcore.Services.Dto;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace computervision.aspcore.Services
{
    public class CompterVisionService : ICompuerVisionService
    {
        // Add your Computer Vision subscription key and endpoint
        private string subscriptionKey = "PASTE_YOUR_COMPUTER_VISION_SUBSCRIPTION_KEY_HERE";
        private string endpoint = "PASTE_YOUR_COMPUTER_VISION_ENDPOINT_HERE";
        /*
 * AUTHENTICATE
 * Creates a Computer Vision client used by each example.
 */
        public ComputerVisionClient Authenticate()
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
              { Endpoint = endpoint };
            return client;
        }

        public async Task<ImageAnalysisViewModel> AnalyzeImageUrl(string imageUrl)
        {
            try
            {
                // Creating a list that defines the features to be extracted from the image. 
                ComputerVisionClient client = Authenticate();
                List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
                {
                    VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                    VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                    VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                    VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                    VisualFeatureTypes.Objects
                };
                ImageAnalysis results;
                using (Stream imageStream = File.OpenRead(imageUrl))
                {
                    results = await client.AnalyzeImageInStreamAsync(imageStream, visualFeatures: features);
                    //imageStream.Close();
                }
                    
                ImageAnalysisViewModel imageAnalysis = new ImageAnalysisViewModel();
                imageAnalysis.imageAnalysisResult = results;
                return imageAnalysis;
            }
            catch (System.Exception ex)
            {
                // add your log capture code
                throw;
            }
           
        }
      
    }
}
