using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Tailwind.Auth.Data.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{

    [Key]
    [Column("id")]
    public int ID { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("email")]
    [Required]
    public string? Email { get; set; }


    //this is the OAuth stuff which is set as a column here to make
    //life a little easier. We *could* have separate tables and all,
    //but simplicity is the name of the game here. Add whatever columns
    //you need to store the OAuth data for each provider.
    [Column("magic_link")]
    public string? MagicLink { get; set; }
    [Column("microsoft")]
    public string? Microsoft { get; set; }
    [Column("google")]
    public string? Google { get; set; }
    [Column("github")]
    public string? Github { get; set; }
    [Column("last_login")]
    public DateTimeOffset? LastLogin { get; set; }
    [Column("magic_link_expires_at")]
    public DateTimeOffset MagicLinkExpiresAt { get; set; } = new DateTime();
    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = new DateTime();

    public void PrepareMagicLink(){
      this.MagicLink = Guid.NewGuid().ToString();
      this.MagicLinkExpiresAt = DateTimeOffset.UtcNow.AddHours(1);
    }
    public bool IsMagicLinkExpired(){
      return this.MagicLinkExpiresAt < DateTimeOffset.UtcNow;
    }
}