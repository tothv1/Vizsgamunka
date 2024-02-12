using System;
using System.Collections.Generic;

namespace SyntaxBackEnd.Models;

public partial class Userstat
{
    public ulong MyRowId { get; set; }

    public int UserId { get; set; }

    public int Kills { get; set; }

    public int Deaths { get; set; }

    public int Timesplayed { get; set; }

    public virtual User User { get; set; } = null!;
}
