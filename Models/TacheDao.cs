using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    [Table("TASK")]
    public class TacheDao 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("T_TITLE")]
        public String Title { get; set; }

        [Column("T_DESCRIPTION")]
        public string Description { get; set; }

        [Column("T_CATEGORY")]
        public TaskCategory Category { get; set; }

        [Column("T_STATUS")]
        public TaskStatues Statues { get; set; }

        [Column("T_START_DATE")]
        public DateTime StartDate{ get; set; }

        [Column("T_END_DATE")]
        public DateTime EndDate { get; set; }


       
    }
   public enum TaskCategory : int
    {
               
        Work = 0,       

        Meeting = 1,
        
        Activity = 2

    }
    public enum TaskStatues
    {
        ToDo,
        InProgress,
        Complete
    }
}
