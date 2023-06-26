using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entities
{
    public class AssignmentVisits
    {
        //public int idVisitAssigned { get; set; }
        public int idTechnical { get; set; }
        public int idClient { get; set; }
        public string ubication { get; set; }
        public string reasonVisit { get; set; }
        public int statusVisit { get; set; }
        public string visitSchedule { get; set; }
        public int idSupervisor { get; set; }
        public string arrivalVisit { get; set; }
        public string visitFinished { get; set; }
        public string registerDate { get; set; }
        public string registerTime { get; set; }
    }
}
