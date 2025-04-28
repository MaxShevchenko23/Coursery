using System.ComponentModel.DataAnnotations.Schema;

namespace Coursery.Domain;

public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public BaseEntity()
    {
        CreatedAt = DateTime.Now.ToUniversalTime();
    }
    
    public int GetId()
    {
        return Id;
    }

    public DateTime SetCreatedAt()
    {
        var lastDate = CreatedAt;
        CreatedAt = DateTime.Now;
        return lastDate;
    } 
}