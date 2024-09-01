using ProjectChange.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChange
{
    public interface IProjectService
    {
        bool AddProject(NewProject project);
        bool RemoveProject(RemoveProjectDto remove);
        bool ChangePrice(ChangePriceDto price);
    }
}
