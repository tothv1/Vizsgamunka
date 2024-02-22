using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthAPI.Models;

public partial class TempRole
{
    public int TempRoleId { get; set; }

    public string RoleName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Registry> Registries { get; set; } = new List<Registry>();
}
