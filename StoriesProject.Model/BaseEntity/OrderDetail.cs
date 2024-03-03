using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu detail của đơn hàng
/// </summary>
public partial class OrderDetail
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Mã đơn hàng master")]
    public Guid OrderId { get; set; }

    [Description("Tên sản phẩm")]
    public string? ProductName { get; set; }

    [Description("Mô tả sản phẩm")]
    public string? ProductDescription { get; set; }

    [Description("Số lượng")]
    public int Quantity { get; set; }

    [Description("Giá tiền")]
    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;
}
