﻿namespace ApiProject.Domain.Abstractions;
public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }  = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
