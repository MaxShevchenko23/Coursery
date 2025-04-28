using System.ComponentModel.DataAnnotations;
using System.Linq;

public class PasswordComplexityAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = value as string;
        if (password == null)
        {
            return new ValidationResult("Password is required.");
        }

        if (password.Length < 8 || 
            !password.Any(char.IsDigit) || 
            !password.Any(char.IsUpper) || 
            !password.Any(char.IsLower))
        {
            return new ValidationResult("Password must be at least 8 characters long and contain at least one digit, one uppercase letter, and one lowercase letter.");
        }

        return ValidationResult.Success;
    }
}