using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
public class RefreshToken
{
    [Key]
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string UserId { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiryDate;

    public IdentityUser User { get; set; }  // Foreign key relation to the user
}