using Core.Models.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Dtos;

namespace Core.Intefaces
{
    public interface IAssignmentVisitsService
    {
        Task<Response<string>> AddVisit(AssignmentVisits avisit);
        Task<Response<List<AssignmentVisits>>> GetAllVisitsByFilter(string filter);
        Task<Response<string>> RemoveVisit(int idAssignamentVisit, int statusVisit);
        Task<Response<List<AssignmentVisitsDto>>> GetAllVisits();
        Task<Response<string>> UpdateVisit(AssignmentVisitsDto avisit);
    }
}
