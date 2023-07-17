 
namespace Mercury.Shared.Models
{

    public class CommandButtonStr
    {
        public string? Size { get; set; }
        public string? Variant { get; set; }
        public string? ButtonStyle { get; set; }
        public string? Text { get; set; }
        public string? Icon { get; set; }
    }

    public class CommandGroupStr
    {
        public string? Title { get; set; }
        public string? AlignItems { get; set; } = "Center";
        public string? JustifyContent { get; set; } = "SpaceAround";
        public List<CommandButtonStr>? Buttons { get; set; }
    }
}