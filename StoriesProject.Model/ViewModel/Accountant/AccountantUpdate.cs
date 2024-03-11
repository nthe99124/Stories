using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.ViewModel.Accountant
{
    public class AccountantUpdate
    {
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public GenderType Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Introduce { get; set; }
        public string? ImgAvatar { get; set; }
    }
}
