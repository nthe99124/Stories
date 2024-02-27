using System.ComponentModel.DataAnnotations;

namespace StoriesProject.API.Model.BaseEntity;

/// <summary>
/// Bảng này đẻ ra tránh việc không có serve để tray cho mt product => sử dụng thay thế ILogger
/// </summary>
public partial class LogEntry
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string LogLevel { get; set; }
    public string RequestApi { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
