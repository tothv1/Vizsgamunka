﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthAPI.Models;

public partial class LoggedInUser
{
    public int LoggedIsUsersId { get; set; }

    public string Userid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime? SessionExpires { get; set; }

    [JsonIgnore]
    public virtual RegisteredUser User { get; set; } = null!;
}
