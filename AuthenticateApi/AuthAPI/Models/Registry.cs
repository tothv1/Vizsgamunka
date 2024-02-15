using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class Registry
{
    public string TempUserid { get; set; } = null!;

    public string TempUsername { get; set; } = null!;

    public string TempFullname { get; set; } = null!;

    public string TempEmail { get; set; } = null!;

    public string TempHash { get; set; } = null!;

    public DateTime TempRegdate { get; set; }

    public int TempRoleid { get; set; }

    public string TempConfirmationKey { get; set; } = null!;
}
