using FrontToBackFlowers.Models;

namespace FrontToBackFlowers.ViewModels
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; } = new List<SliderImage>();
        public Slider? Slider { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
