using System.Security.Cryptography.X509Certificates;

namespace SibSIU.Identity.Infrastructure;

public static class ReadConfigurationExtensions
{
    public static string GetConnectionStringByName(this WebApplicationBuilder builder, string name)
    {
        return builder.Configuration.GetConnectionString(name)
            ?? throw new InvalidOperationException($"Connection string '{name}' not found.");
    }

    public static string GetSymmetricSecurityKey(this WebApplicationBuilder builder, string certificateName)
    {
        string? encryptionKey = builder.Configuration[$"SymmetricSecurityKey:{certificateName}"];
        return encryptionKey ?? "DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=";
    }

    public static X509Certificate2? GetCertificate(this WebApplicationBuilder builder, string certificateName)
    {
        X509Certificate2? certificate = null;
        string? thumbprint = builder.Configuration[$"SymmetricSecurityKey:{certificateName}:Thumbprint"];
        if (!string.IsNullOrWhiteSpace(thumbprint))
        {
            X509Store certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = certStore.Certificates.Find(
                X509FindType.FindByThumbprint, thumbprint, false);
            certStore.Close();

            certificate = certCollection.FirstOrDefault();
        }

        return certificate;
    }
}
