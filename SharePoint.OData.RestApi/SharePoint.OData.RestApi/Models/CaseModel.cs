using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.OData.RestApi
{
    public class CaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Authority { get; set; }
        public string CaseNumber { get; set; }
        public string Origin { get; set; }
        public string Category { get; set; }
        public string CategoryOther { get; set; }
        public string Description { get; set; }
        public string ContradictionId { get; set; }
        public string DecisionId { get; set; }
        public string PartyId { get; set; }
        public string OwnerId { get; set; }
        public string DocumentStatus { get; set; }
    }
}
