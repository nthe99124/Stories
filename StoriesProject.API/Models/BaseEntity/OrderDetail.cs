using System.ComponentModel.DataAnnotations;

namespace StoriesProject.API.Model.BaseEntity;

public partial class OrderDetail
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;
}
