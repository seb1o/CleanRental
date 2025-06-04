using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace CleanRental.model;

public partial class Film
{
    public int FilmId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? ReleaseYear { get; set; }

    public int LanguageId { get; set; }

    public int RentalDuration { get; set; }

    public decimal RentalRate { get; set; }

    public int? Length { get; set; }

    public decimal ReplacementCost { get; set; }

    public DateTime LastUpdate { get; set; }

    public List<string>? SpecialFeatures { get; set; }

    public NpgsqlTsVector Fulltext { get; set; } = null!;

    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    public virtual ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual Language Language { get; set; } = null!;
}
