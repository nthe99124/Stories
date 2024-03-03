using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng này đẻ ra tránh việc không có serve để tray cho mt product => sử dụng thay thế ILogger
/// </summary>
public partial class LogEntry
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Loại log")]
    public string LogLevel { get; set; }

    [Description("API log")]
    public string RequestApi { get; set; }

    [Description("Nội dung log")]
    public string Message { get; set; }

    [Description("Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
