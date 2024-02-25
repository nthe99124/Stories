using System.ComponentModel.DataAnnotations;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

public partial class CoinLog
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int CoinTransacted { get; set; }

    [Required]
    public decimal MoneyConvert { get; set; }

    [Required]
    public CoinLogType Type { get; set; }

    [Required]
    public bool IsApproved { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual Accountant? Accountant { get; set; }
}
