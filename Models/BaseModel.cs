using System;

namespace NewApp.Models.DTOs
{
    public class BaseModel
    {
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            CreatedTime = DateTime.UtcNow;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}