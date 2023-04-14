using System;
using System.Collections.Generic;

namespace MySite.Models;

public partial class Song
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Style> Styles { get; set; } = new List<Style>();


}
