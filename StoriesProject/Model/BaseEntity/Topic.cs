using System;
using System.Collections.Generic;

namespace StoriesProject.Model.BaseEntity;

public partial class Topic
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual ICollection<TopicStory> TopicStories { get; set; } = new List<TopicStory>();
}
