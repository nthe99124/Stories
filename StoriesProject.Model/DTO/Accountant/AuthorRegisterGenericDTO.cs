using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesProject.Model.BaseEntity
{
    /// <summary>
    /// Bảng lưu thông tin đăng ký tác giả
    /// </summary>
    public class AuthorRegisterGenericDTO
    {
        public Guid AuthorRegisterId { get; set; } = Guid.NewGuid();
        public Guid AccountantId { get; set; }
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
