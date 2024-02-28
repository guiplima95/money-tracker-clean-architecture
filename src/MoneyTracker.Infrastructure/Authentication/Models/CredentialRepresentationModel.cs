namespace MoneyTracker.Infrastructure.Authentication.Models;

public class CredentialRepresentationModel
{
    public string Value { get; set; }

    public bool Temporary { get; set; }

    public string Type { get; set; }
}
