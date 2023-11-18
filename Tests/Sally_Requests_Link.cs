using Xunit;
using Tailwind.Auth.Data;
using Tailwind.Auth.Data.Models;
using Microsoft.EntityFrameworkCore;
using Tailwind.Auth.Services;
namespace Tailwind.Mail.Tests;

public class Sally_Wants_a_Link:TestBase
{
  [Fact]
  public void A_Link_Is_Created()
  {
    var user = new User();
    user.PrepareMagicLink();
    //assert the link is there
    Assert.NotNull(user.MagicLink);
  }
  [Fact]
  public void The_Link_Is_Not_Expired()
  {
    var user = new User();
    user.PrepareMagicLink();
    Assert.False(user.IsMagicLinkExpired());
  }
}