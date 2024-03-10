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
    public class AuthorRegister
    {
        [Key]
        public Guid AuthorRegisterId { get; set; } = Guid.NewGuid();
        [Description("AccountantId")]
        public Guid AccountantId { get; set; }
        [Description("Họ và tên")]
        public string Name { get; set; }
        [Description("Email")]
        public string? Email { get; set; }
        [Description("Số điện thoại")]
        [StringLength(20, ErrorMessage = "Số điện thoại quá dài")]
        public string? PhoneNumber { get; set; }
        [Description("Ngày đăng ký")]
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        [Description("Loại hình đăng ký")]
        public int AccountantType { get; set; } = 1; // 1 - Cá nhân, 2 - Doanh nghiệp
        [Description("Hình thức thanh toán")]
        public int PaymentType { get; set; } = 1; // 1 - Viettel Money, 2 - Thẻ ngân hàng
        [Description("Tên ngân hàng")]
        public string BankName { get; set; }
        [Description("Số thẻ")]
        public string BankNumber { get; set; }
        [Description("Số tài khoản")]
        public string BankAccount { get; set; }

        public virtual Accountant Accountant { get; set; } = null;
    }
}
