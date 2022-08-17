using Ecommerce.Email.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Email.Model
{
    [Table("email_logs")]
    public class EmailLog : BaseEntity
    {
        [Column("email")]
        public string? Email { get; set; }
        [Column("log")]
        public string? Log { get; set; }
        [Column("sent_date")]
        public DateTime? SentDate { get; set; }
    }
}
