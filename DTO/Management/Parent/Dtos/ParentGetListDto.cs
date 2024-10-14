using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Base;

namespace DTO.Management.Parent.Dtos
{
    public class ParentGetListDto : GetListPagingRequest
    {
        public Guid? ProvinceId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? WardId { get; set; }

    }
}
