using Microsoft.EntityFrameworkCore;

namespace CoinOrderApi.Data.Models
{
    [Index(nameof(Id))]
    [Index(nameof(DeletedDate))]
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}
