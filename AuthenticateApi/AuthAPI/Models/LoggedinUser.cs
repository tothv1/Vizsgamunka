using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class LoggedInUser
{
    public string Userid { get; set; } = null!;

    public string Token { get; set; } = null!;
}
