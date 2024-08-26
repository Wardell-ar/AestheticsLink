using SurgeryProjectService.Dto;
using System.Threading.Tasks;

namespace SurgeryProjectService
{
    public interface ISurgeryProjectService
    {
        Task<List<SurgeryProjectDto>> GetSurgeryProjectDetailsAsync(string cusId);

        Task<List<ProjectDto>> GetProjectInfoAsync(ProjectInfoRequest dto);
    }
}