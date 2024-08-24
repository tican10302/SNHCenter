﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Menu
    {
        public Menu()
        {
            Sort = 0;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = new();
        public required string Name { get; set; }
        public required string ControllerName { get; set; }
        public required string Controller { get; set; }
        public required string Action { get; set; }
        public GroupPermission GroupPermission { get; set; }
        public int Sort { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsApprove { get; set; }
        public bool IsStatistic { get; set; }
        public bool IsActived { get; set; }
        public bool IsShowMenu { get; set; }
    }
}
