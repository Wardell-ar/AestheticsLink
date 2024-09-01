using RebookOperateService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebookOperateService
{
    public interface IRebookService
    {
        bool PostponeOperate(DelayedOperateDto delayedOperate);
    }
}
