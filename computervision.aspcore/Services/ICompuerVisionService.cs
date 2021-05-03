using computervision.aspcore.Services.Dto;
using System.IO;
using System.Threading.Tasks;

namespace computervision.aspcore.Services
{
    public interface ICompuerVisionService
    {
        Task<ImageAnalysisViewModel> AnalyzeImageUrl(string imageUrl);
    }
}
