using WebApplication4.DTOs_Card;
using WebApplication4.Models;

namespace WebApplication4.Mappings
{
    public static class CardAddRequestExtensions
    {
        public static Card ToCard(this CardAddRequest cardAddRequest, string userId)
        {
            return new Card
            {
                UserId = userId,
                Title = cardAddRequest.Title,
                Subtitle = cardAddRequest.Subtitle,
                Description = cardAddRequest.Description,
                Phone = cardAddRequest.Phone,
                Email = cardAddRequest.Email,
                Web = cardAddRequest.Web,
                Image = cardAddRequest.Image,
                Address = cardAddRequest.Address
            };
        }
    }
}
