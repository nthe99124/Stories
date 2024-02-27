using System.ComponentModel.DataAnnotations;
using static StoriesProject.API.Model.Enum.DataType;

namespace StoriesProject.API.Model.BaseEntity;

public partial class Order
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    public OrderPaymentMethod PaymentMethod { get; set; }

    public decimal Discounts { get; set; }

    public decimal Taxes { get; set; }

    public int CoinTransacted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual Accountant? CreatedByNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
