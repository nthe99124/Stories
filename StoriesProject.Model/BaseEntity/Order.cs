using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu thông tin đơn hàng
/// </summary>
public partial class Order
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Tổng số tiền")]
    public decimal TotalAmount { get; set; }

    [Description("Trạng thái đơn hàng")]
    public OrderStatus Status { get; set; }

    [Description("Loại giao dịch")]
    public OrderPaymentMethod PaymentMethod { get; set; }

    [Description("Giảm giá")]
    public decimal Discounts { get; set; }

    [Description("Thuế")]
    public decimal Taxes { get; set; }

    [Description("Số coin được giao dịch")]
    public int CoinTransacted { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    [Description("Người tạo")]
    public Guid? CreatedBy { get; set; }

    public virtual Accountant? CreatedByNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
