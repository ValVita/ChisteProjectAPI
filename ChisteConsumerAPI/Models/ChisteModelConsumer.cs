namespace ChisteConsumerAPI.Models
{
    public class ChisteModelConsumer
    {
        public int Id { get; set; }
        public string ExternalId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
