namespace FrontToBackFlowers.Areas.AdminPanel.Models
{
    public class SlideImageUpdateModel
    {
        public string ImageUrl { get; set; } = string.Empty;
        public IFormFile Image { get; set; }

    }
}
