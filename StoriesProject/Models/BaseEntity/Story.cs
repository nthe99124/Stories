using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

public partial class Story
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Code { get; set; }

    public decimal Price { get; set; }

    public string? Content { get; set; }

    public string? ImageLink { get; set; }

    public string? VideoLink { get; set; }

    public string? Description { get; set; }

    public TypeOfStory TypeOfStory { get; set; }

    public string? ModifiedDescription { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Accountant? CreatedByNavigation { get; set; }

    public virtual Accountant? ModifiedByNavigation { get; set; }

    public virtual ICollection<TopicStory> TopicStories { get; set; } = new List<TopicStory>();
}
