using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Management.Parent.Dtos
{
    public class ParentGetListDto
    {
        public Guid? ProvinceId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? WardId { get; set; }

    }
}
