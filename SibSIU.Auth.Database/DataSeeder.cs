using Microsoft.EntityFrameworkCore;

using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Authenticate.Password;
using SibSIU.Core.Names;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database;
internal static class DataSeeder
{
    internal static void Seed(this ModelBuilder builder)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        Role admin = new()
        {
            Id = Ulid.NewUlid(now),
            Name = RoleNames.BaseAdministrator,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        Role baseUser = new()
        {
            Id = Ulid.NewUlid(now),
            Name = RoleNames.BaseUser,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        builder.Entity<Role>().HasData(admin, baseUser);

        Scope emailScope = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Scopes.Email,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        Scope addressScope = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Scopes.Address,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        Scope offlineAccessScope = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Scopes.OfflineAccess,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        Scope openIdScope = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Scopes.OpenId,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        Scope phoneScope = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Scopes.Phone,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        Scope profileScope = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Scopes.Profile,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        Scope rolesScope = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Scopes.Roles,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        builder.Entity<Scope>().HasData(emailScope, addressScope, offlineAccessScope, openIdScope, phoneScope, profileScope, rolesScope);

        AuthClaimType subClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Subject,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType accessTokenHashClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.AccessTokenHash,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType activeClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Active,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType addressClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Address,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType audClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Audience,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType authenticationContextReferenceClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.AuthenticationContextReference,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType authenticationMethodReferenceClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.AuthenticationMethodReference,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType authenticationTimeClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.AuthenticationTime,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType authenticationServerClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.AuthorizationServer,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType authorizedPartyClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.AuthorizedParty,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType birthDayClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Birthdate,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType clientIdClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.ClientId,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType codeHashClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.CodeHash,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType countryClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Country,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType emailClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Email,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType emailVerifiedClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.EmailVerified,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType expireAtClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.ExpiresAt,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };


        AuthClaimType familyNameClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.FamilyName,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType formattedClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Formatted,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType genderClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Gender,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType givenNameClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.GivenName,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType issuerAtClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.IssuedAt,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType issuerClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Issuer,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType jwtidClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.JwtId,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType keyIdClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.KeyId,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType localeClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Locale,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType localityClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Locality,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType middleNameClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.MiddleName,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType nameClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Name,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType nicknameClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Nickname,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType nonceClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Nonce,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType notBeforeClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.NotBefore,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType phoneClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.PhoneNumber,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType phoneVerifiedClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.PhoneNumberVerified,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType pictureClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Picture,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType postalCodeClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.PostalCode,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType preferredUserNameClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.PreferredUsername,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType profileClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Profile,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType regionClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Region,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType rfpClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.RequestForgeryProtection,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType roleClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Role,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType scopeClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Scope,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType streetClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.StreetAddress,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType targetUrlClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.TargetLinkUri,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType tokenTypeClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.TokenType,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType tokenUsageClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.TokenUsage,
            IncludeInAccessToken = true,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType updateAtClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.UpdatedAt,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType usernameClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Username,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType websiteClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Website,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        AuthClaimType zoneInfoClaimType = new()
        {
            Id = Ulid.NewUlid(now),
            Name = OpenIddict.Abstractions.OpenIddictConstants.Claims.Zoneinfo,
            IncludeInAccessToken = false,
            IncludeInIdentityToken = true,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now
        };

        builder.Entity<AuthClaimType>().HasData(
            subClaimType,
            accessTokenHashClaimType,
            activeClaimType,
            addressClaimType,
            audClaimType,
            authenticationContextReferenceClaimType,
            authenticationMethodReferenceClaimType,
            authenticationTimeClaimType,
            authenticationServerClaimType,
            authorizedPartyClaimType,
            birthDayClaimType,
            clientIdClaimType,
            codeHashClaimType,
            countryClaimType,
            emailClaimType,
            emailVerifiedClaimType,
            expireAtClaimType,
            familyNameClaimType,
            formattedClaimType,
            genderClaimType,
            givenNameClaimType,
            issuerAtClaimType,
            issuerClaimType,
            jwtidClaimType,
            keyIdClaimType,
            localeClaimType,
            localityClaimType,
            middleNameClaimType,
            nameClaimType,
            nicknameClaimType,
            nonceClaimType,
            notBeforeClaimType,
            phoneClaimType,
            phoneVerifiedClaimType,
            pictureClaimType,
            postalCodeClaimType,
            preferredUserNameClaimType,
            profileClaimType,
            regionClaimType,
            rfpClaimType,
            roleClaimType,
            scopeClaimType,
            streetClaimType,
            targetUrlClaimType,
            tokenTypeClaimType,
            tokenUsageClaimType,
            updateAtClaimType,
            usernameClaimType,
            websiteClaimType,
            zoneInfoClaimType);

        AuthClaimTypeScopes emailInEmail = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = emailClaimType.Id,
            ScopeId = emailScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes emailVerifiedInEmail = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = emailVerifiedClaimType.Id,
            ScopeId = emailScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes addressInAddress = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = addressClaimType.Id,
            ScopeId = addressScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes countryInAddress = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = countryClaimType.Id,
            ScopeId = addressScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes regionInAddress = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = regionClaimType.Id,
            ScopeId = addressScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes postalCodeInAddress = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = postalCodeClaimType.Id,
            ScopeId = addressScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes localityInAddress = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = localityClaimType.Id,
            ScopeId = addressScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes streetInAddress = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = streetClaimType.Id,
            ScopeId = addressScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes phoneInPhone = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = phoneClaimType.Id,
            ScopeId = phoneScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes phoneVerifiedInPhone = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = phoneVerifiedClaimType.Id,
            ScopeId = phoneScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes profileInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = profileClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes nameInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = nameClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes familyNameInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = familyNameClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes givenNameInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = givenNameClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes middleNameInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = middleNameClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes nicknameInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = nicknameClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes preferredInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = preferredUserNameClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes pictureInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = pictureClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes websiteInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = websiteClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes genderInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = genderClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes birthdayInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = birthDayClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes zoneInfoInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = zoneInfoClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes localeInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = localeClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes updateAtInProfile = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = updateAtClaimType.Id,
            ScopeId = profileScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        AuthClaimTypeScopes subjectInOpenId = new()
        {
            Id = Ulid.NewUlid(now),
            ClaimTypeId = subClaimType.Id,
            ScopeId = openIdScope.Id,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true
        };

        builder.Entity<AuthClaimTypeScopes>().HasData(
            emailInEmail,
            emailVerifiedInEmail,
            addressInAddress,
            countryInAddress,
            regionInAddress,
            postalCodeInAddress,
            streetInAddress,
            localityInAddress,
            phoneInPhone,
            phoneVerifiedInPhone,
            profileInProfile,
            nameInProfile,
            familyNameInProfile,
            givenNameInProfile,
            middleNameInProfile,
            nicknameInProfile,
            preferredInProfile,
            pictureInProfile,
            websiteInProfile,
            genderInProfile,
            birthdayInProfile,
            zoneInfoInProfile,
            localeInProfile,
            updateAtInProfile,
            subjectInOpenId);

        Gender male = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            Name = "Мужской",
        };
        Gender female = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            Name = "Женский",
        };

        builder.Entity<Gender>().HasData(male, female);

        var result = HashCalculator.Hash("adminDDT_1");
        User userAdmin = new()
        {
            Id = Ulid.NewUlid(),
            Email = "admin@sibsiu.ru",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "Admin",
            Patronymic = "Admin",
            UserName = "Admin",
            BirthOfDate = new DateTimeOffset(2001, 1, 18, 0, 0, 0, TimeSpan.Zero),
            GenderId = male.Id,
            PhoneNumber = "+7-(900)-00-00-0000",
            Password = result.Password,
            PasswordSalt = result.Salt,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            IsConfirmedUser = true,
            IsTemporaryPassword = false,
        };

        builder.Entity<User>().HasData(userAdmin);

        UserRole userAdminWithAdminRole = new()
        {
            RoleId = admin.Id,
            UserId = userAdmin.Id,
        };

        builder.Entity<UserRole>().HasData(userAdminWithAdminRole);

        Organization organization = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            ShortName = "СибГИУ",
            FullName = "Федеральное государственное бюджетное образовательное учреждение высшего образования «Сибирский государственный индустриальный университет»",
            KPP = "421701001",
            OGRN = "1024201470908",
            TIN = "4216003509"
        };

        builder.Entity<Organization>().HasData(organization);

        Unit sibSIU = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            DeanCode = null,
            Parent = null,
            ShortName = "СибГИУ",
            FullName = "Федеральное государственное бюджетное образовательное учреждение высшего образования «Сибирский государственный индустриальный университет»",
        };

        builder.Entity<Unit>().HasData(sibSIU);
    }
}
