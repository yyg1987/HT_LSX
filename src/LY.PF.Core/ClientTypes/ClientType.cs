using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.PF.ClientTypes
{
   public partial class ClientType : Entity<int>, IHasCreationTime
    {
        public string ClientTypeName { get; set; }
        public string Remark { get; set; }
        public bool IsValid { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
