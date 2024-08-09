using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TingleVendas.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace TingleVendas.Controllers
{

    public class LoginController : Controller
    {
        private readonly TingleVendasContext _context;
        //private readonly ILogger<LoginController> _logger;

       
        public LoginController(TingleVendasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Validate(Usuario usuario)
        {
            try
            {

            
            var _usuario = _context.Usuario.FirstOrDefaultAsync(u => u.Login == usuario.Login).Result;

            //ViewData["ListaMenu"] = new SelectList(_context.GrupoMenu, "IdGrupo", "Idmenu");
            //ViewData["ListaMenu"] = new SelectList(_context.Grupo, "Id", "Descricao");

            
            if (_usuario != null)
            {
                if (_usuario.Senha == CalculateMD5Hash(usuario.Senha))
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, _usuario.Login),
                        new Claim("FullName", _usuario.Nome),
                        new Claim(ClaimTypes.Role, "Administrator"),
                        new Claim("LastChanged", DateTime.Now.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        //AllowRefresh = <bool>,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        //IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        //IssuedUtc = <DateTimeOffset>,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    
                     HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),

                        authProperties);


                    //_signInManager.SignInAsync(user, isPersistent: false);

                    //_logger.LogInformation("User logged in.");
                    //HttpContext.Session.SetObjectAsJson("sessionUsuario", JsonConvert.SerializeObject(_usuario));
                    //HttpContext.Session.SetString("usuario", "movies");
                    //ViewData["usuario"] = _usuario;
                    //s.SetString("sessionUsuario", JsonConvert.SerializeObject(_usuario));
                    //Setar uma session com um objeto
                    HttpContext.Session.SetString("sessionUsuario", JsonConvert.SerializeObject(_usuario));
                    HttpContext.Session.SetString("usuario", _usuario.Nome + " " + _usuario.Sobrenome);
                    //return RedirectToAction("Index", "Movies");

                    return Json(new { status = true, message = "Login sucesso!" });
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Json(new { status = false, message = "Senha inválida!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Login inválido!" });
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult VerificaAcesso(string tela)
        {
            var usuario = HttpContext.Session.GetObjectFromJson<Usuario>("sessionUsuario");
            
            return Json(new { status = false, message = "Login inválido!" });
        }

        public ActionResult LogOff()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            RedirectToPage("/Login/Index");
            return Json(new { status = true, message = "Você saiu do sistema!" });
        }

        public string CalculateMD5Hash(string input)
        {
            // Calcular o Hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // Converter byte array para string hexadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }



    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            try
            {
                var value = session.GetString(key);

                return (value == null ? default(T) : JsonConvert.DeserializeObject<T>(value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }

}
