﻿using System.ComponentModel.DataAnnotations;

namespace StoriesProject.API.Model.BaseEntity;

public partial class TopicStory
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TopicId { get; set; }

    public Guid StoryId { get; set; }

    public virtual Story Story { get; set; } = null!;

    public virtual Topic Topic { get; set; } = null!;
}