using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class BlacklistedToken
{
    public string TokenId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime BlacklistedStatusExpires { get; set; }
}
