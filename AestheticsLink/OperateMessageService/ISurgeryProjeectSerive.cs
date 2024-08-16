using SurgeryProjectService.Dto;
using System.Threading.Tasks;

namespace SurgeryProjectService
{
    public interface ISurgeryProjectService
    {
        Task<SurgeryProjectDto> GetSurgeryProjectDetailsAsync(string projId);
    }
}