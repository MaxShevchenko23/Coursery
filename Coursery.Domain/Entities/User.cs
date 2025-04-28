namespace Coursery.Domain.Entities;

public class User : BaseEntity
{
    [EmailAddress]
    public string Email { get; set; }
    
    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; }
    
    public string? Photo { get; set; }
    
    [MaxLength(50)]
    public string? Profession { get; set; }
    
    public Role Role { get; set; }

    public IList<Enroll> EnrolledCourses { get; set; }
    
    [MinLength(8)]
    public string Password { get; set; }
    
    public bool Authenticate(string password)
    {
        if (HashPassword(password) != Password)
        {
            return false;
        }

        return true;

    }

    public bool UpdateProfile(string firstName, string lastName, string? photo = null)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            return false;
        }

        FirstName = firstName;
        LastName = lastName;
        Photo = photo;
        return true;
    }

    public bool ChangePassword(string oldPassword, string newPassword)
    {
        if (newPassword.Length < 8 || 
            !newPassword.Any(char.IsDigit) || 
            !newPassword.Any(char.IsUpper) || 
            !newPassword.Any(char.IsLower))
        {
            return false; 
        }

        Password = HashPassword(newPassword);
        
        Password = newPassword; 
        return true;
    }
    
    private string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public Role GetRole()
    {
        return Role;
    }

    public string GetEmail()
    {
        return Email;
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
    
}

public enum Role
{
    Student,
    Teacher,
    Admin
}