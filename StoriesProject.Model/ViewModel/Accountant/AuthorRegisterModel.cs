
namespace StoriesProject.Model.ViewModel.Accountant
{
    public class AuthorRegisterModel
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public int AccountantType { get; set; } = 1; // 1 - Cá nhân, 2 - Doanh nghiệp
        public int PaymentType { get; set; } = 1; // 1 - Viettel Money, 2 - Thẻ ngân hàng
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string BankAccount { get; set; }
    }
}
