using System;
using System.Collections.Generic;

namespace SyntaxBackEnd.Models;

public partial class Stat
{
    public ulong MyRowId { get; set; }

    public int UserId { get; set; }

    public int TimesPlayed { get; set; }

    public int Kills { get; set; }

    public int Deaths { get; set; }
}
