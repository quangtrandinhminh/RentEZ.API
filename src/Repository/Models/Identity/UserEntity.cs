﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Repository.Models.Base;
using Utility.Helpers;

namespace Repository.Models.Identity;

public class UserEntity : IdentityUser<int>
{
    public UserEntity()
    {
        CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
    }

    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Avatar { get; set; }
    public DateOnly? BirthDate { get; set; }

    public virtual ICollection<UserRoleEntity> UserRoles { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    public virtual Shop? ManagedShop { get; set; }

    // Base Property
    public int? CreatedBy { get; set; }
    public int? LastUpdatedBy { get; set; }
    public int? DeletedBy { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastUpdatedTime { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }

    // Identity Property
    public DateTimeOffset? Verified { get; set; }
    public string? OTP { get; set; }
    public bool IsActive => EmailConfirmed;
    public override bool EmailConfirmed => Verified.HasValue;

    [NotMapped]
    public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
    [NotMapped]
    public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
}

public class RoleEntity : IdentityRole<int>
{
    public virtual ICollection<UserRoleEntity> UserRoles { get; set; }

    [NotMapped]
    public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

    [NotMapped]
    public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
}

public class UserRoleEntity : IdentityUserRole<int>
{
    public virtual UserEntity User { get; set; }
    public virtual RoleEntity Role { get; set; }
}

public class RefreshToken : BaseEntity
{
    [ForeignKey(nameof(UserId))]
    public UserEntity User { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTimeOffset Expires { get; set; }
    public bool IsExpired => CoreHelper.SystemTimeNow >= Expires;
    public bool IsActive => !IsExpired;
}

