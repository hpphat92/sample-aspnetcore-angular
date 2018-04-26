using System;

namespace NewApp.Entities
{
    public interface IBaseEntity
    {
        string Id { get; set; }
        DateTime CreatedTime { get; set; }
        DateTime UpdatedTime { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            CreatedTime = DateTime.UtcNow;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
