namespace BingeWatching.API.ViewModels
{
    public class RecommendationRequest
    {
        public int Score { get; set; }
        public string ContentId { get; set; }
        public string UserId { get; set; }
    }
}
