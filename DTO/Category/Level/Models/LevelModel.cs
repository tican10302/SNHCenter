using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Base;

namespace DTO.Category.Level.Models
{
    public class LevelModel : ModelBase
    {
        public string? Name { get; set; }
        public long Fee { get; set; }
        public string? Note { get; set; }
        public string? Program { get; set; }
        public Guid ProgramId { get; set; }
    }
}
