using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TingleVendas.Models;

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
	//private readonly IUserRepository _userRepository;
    private readonly TingleVendasContext _context;

    public CustomCookieAuthenticationEvents(TingleVendasContext context)
	{
        // Get the database from registered DI services.
        _context = context;
    }

	public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
	{
		var userPrincipal = context.Principal;

		// Look for the LastChanged claim.
		var lastChanged = (from c in userPrincipal.Claims
						   where c.Type == "LastChanged"
						   select c.Value).FirstOrDefault();

        //var _usuario = _context.Usuario.FirstOrDefaultAsync(u => u.Login == usuario.Login).Result;

        if (string.IsNullOrEmpty(lastChanged))
            //||
			//!_userRepository.ValidateLastChanged(lastChanged))
		{
			context.RejectPrincipal();

			await context.HttpContext.SignOutAsync(
				CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}