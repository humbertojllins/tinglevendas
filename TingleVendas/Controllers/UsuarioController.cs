using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TingleVendas.Models;

namespace TingleVendas.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly TingleVendasContext _context;

        public UsuarioController(TingleVendasContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Seta os dados de sessao do usuario para ser utilizada na pagina _layout
        /// </summary>
        private void setaDadosSessao()
        {
            var usuario = HttpContext.Session.GetObjectFromJson<Usuario>("sessionUsuario");

            if (usuario != null)
            {
                //ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["email"] = usuario.Email;
                ////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = "Cadastrar Usuário";
                //this.ControllerContext.RouteData.Values["action"].ToString();

                int totalMenus = _context.Menu.Count();
                //Esconde os menus
                for (int i = 1; i <= totalMenus; i++)
                {
                    ViewData[i.ToString()] = "none";
                }

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewData["ListaMenu"] = new SelectList(_context.GrupoMenu.Where(gm => gm.IdGrupo == usuario.IdGrupo), "Idmenu", "Idmenu");
                    //Mostra os menus para o perfil do usuário
                    foreach (var item in ((SelectList)ViewData["ListaMenu"]))
                    {
                        ViewData[item.Value] = "Normal";
                    }
                }
            }
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            try
            {
                setaDadosSessao();
                var tingleVendasContext = _context.Usuario.Include(u => u.IdGrupoNavigation).ToListAsync();
                return View(await tingleVendasContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.IdGrupoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            ViewData["IdGrupo"] = new SelectList(_context.Grupo, "Id", "Descricao", usuario.IdGrupo);
            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            setaDadosSessao();
            ViewData["IdGrupo"] = new SelectList(_context.Grupo, "Id", "Descricao");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Email,Telefone,Login,Senha,Foto,IdGrupo,Cpf")] Usuario usuario)
        {
            setaDadosSessao();
            if (ModelState.IsValid)
            {
                Usuario novo = usuario;
                novo.Senha = CalculateMD5Hash(usuario.Senha);
                _context.Add(novo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupo, "Id", "Descricao", usuario.IdGrupo);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupo, "Id", "Descricao", usuario.IdGrupo);
            //Usuario novo = usuario;
            //usuario.Senha = CalculateMD5Hash(usuario.Senha);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Email,Telefone,Login,Senha,Foto,IdGrupo,Cpf")] Usuario usuario)
        {
            setaDadosSessao();
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Usuario novo = usuario;
                    //novo.Senha = CalculateMD5Hash(usuario.Senha);
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupo, "Id", "Descricao", usuario.IdGrupo);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> EditSenha(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupo, "Id", "Descricao", usuario.IdGrupo);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSenha(int id, [Bind("Id,Nome,Sobrenome,Email,Telefone,Login,Senha,Foto,IdGrupo,Cpf")] Usuario usuario)
        {
            setaDadosSessao();
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Usuario novo = usuario;
                    novo.Senha = CalculateMD5Hash(usuario.Senha);
                    _context.Update(usuario);
                        await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupo, "Id", "Descricao", usuario.IdGrupo);
            return View(usuario);
        }


        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.IdGrupoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            setaDadosSessao();
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
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
}
