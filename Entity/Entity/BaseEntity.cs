using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public abstract class BaseEntity
    {
        [Key]
        public string GUID { get; set; }
        public string FACTORY { get; set; }
        public string CREATOR { get; set; }
        public DateTime? CREATE_TIME { get; set; }
        public string MODIFIER { get; set; }
        public DateTime? MODIFY_TIME { get; set; }
        public string FLAG { get; set; }
        public string DELETE_FLAG { get; set; }
    }
}
