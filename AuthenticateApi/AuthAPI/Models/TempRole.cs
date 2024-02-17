﻿using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class TempRole
{
    public int TempRoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Registry> Registries { get; set; } = new List<Registry>();
}
