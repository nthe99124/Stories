using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu lịch sử giao dịch coin
/// </summary>
public partial class CoinLog
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Số coin đã được chuyển đổi")]
    public int CoinTransacted { get; set; }

    [Description("Số tiền đã được chuyển đổi ra coin")]
    public decimal MoneyConvert { get; set; }

    [Description("Loại giao dịch")]
    public CoinLogType Type { get; set; }

    [Description("Cờ đánh dấu đã được thực hiện chưa")]
    public bool IsApproved { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    [Description("Người tạo")]
    public Guid? CreatedBy { get; set; }

    public virtual Accountant? Accountant { get; set; }
}
