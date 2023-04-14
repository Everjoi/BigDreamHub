using System;
using System.Collections.Generic;

namespace MySite.Models;

public partial class Author
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; } = new List<Song>();
}
