﻿using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.Auth.Database.Entities;
public sealed class Scope : EntityWithUlidId
{
    public required string Name { get; set; }

    public ICollection<AuthClaim> Claims { get; set; } = [];
}