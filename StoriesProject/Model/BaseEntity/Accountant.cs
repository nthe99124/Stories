using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

public partial class Accountant
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    [Required]
    public string UserName { get; set; }

    [StringLength(50)]
    [Required]
    public string Password { get; set; }

    [Required]
    public string Language { get; set; } = "vi-VN";

    public bool IsLocked { get; set; }

    public int Coin { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual CoinLog IdNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RoleAccountant> RoleAccountantAccounts { get; set; } = new List<RoleAccountant>();

    public virtual ICollection<RoleAccountant> RoleAccountantCreatedByNavigations { get; set; } = new List<RoleAccountant>();

    public virtual ICollection<Story> StoryCreatedByNavigations { get; set; } = new List<Story>();

    public virtual ICollection<Story> StoryModifiedByNavigations { get; set; } = new List<Story>();
}
