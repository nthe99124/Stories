using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

public partial class OrderDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;
}
