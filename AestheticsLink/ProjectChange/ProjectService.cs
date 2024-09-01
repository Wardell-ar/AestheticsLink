using ProjectChange.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace ProjectChange
{
    public class ProjectService : IProjectService
    {
        public bool AddProject(NewProject project)
        {
            try
            {
                //增加手术项目
                PROJECT newProject = TransProjectDto(project);
                DbContext.db.Insertable(newProject).ExecuteCommand();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private PROJECT TransProjectDto(NewProject project)
        {
            var newProject = new PROJECT();
            var foundDate = DateTime.Now;
            // 获取数据库中已存在的最大 CUS_ID
            string lastProjectId = GetMaxProjectId();
            // 生成新的 CUS_ID
            int newId;
            if (lastProjectId == "")
            {
                newId = 1;
            }
            else
            {
                newId = int.Parse(lastProjectId) + 1;
            }
            newProject.PROJ_ID = newId.ToString();//自己设置
            newProject.FOUND_DATE = foundDate;
            newProject.NAME = project.NAME;
            newProject.PRICE = project.PRICE;

            return newProject;

        }
        private string GetMaxProjectId()
        {
            string sql = "SELECT MAX(TO_NUMBER(PROJ_ID)) FROM PROJECT";

            // 执行 SQL 查询
            var result = DbContext.db.Ado.GetString(sql);

            return result;
        }
        public bool ChangePrice(ChangePriceDto price)
        {
            try
            {
                DbContext.db.Ado.ExecuteCommand(
                    "UPDATE PROJECT SET PRICE =:newPrice WHERE PROJ_ID =:projID",
                    new
                    {
                        newPrice = price.price,
                        projID = price.proj_id,
                    });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveProject(RemoveProjectDto remove)
        {
            try
            {
                foreach (string id in remove.ids)
                    DbContext.db.Ado.ExecuteCommand(
                        "DELETE FROM PROJECT WHERE PROJ_ID =:projID",
                         new
                         {
                             cusID = id,
                         });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
