using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPruebaTecnica.Modelo;
using System.Net;

namespace ApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly GodoyCordobaContext _context;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(GodoyCordobaContext context, ILogger<UsuariosController> logger)
        {
            _context = context;
            _logger = logger;   
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuarios()
        {
            try
            {
                var ListaUsuarios = await _context.Usuarios.ToListAsync();
                if (ListaUsuarios == null)
                {
                    return NotFound("No hay usuarios registrados, por favor registre usuarios");
                }       
                return Ok(ListaUsuarios);
            }
            catch(Exception error)
            {
                _logger.LogError(error, "Error al obtener lista de usuarios");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud, por favor vuelve a intentarlo");
            }
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ListarUsuarioBYId(long id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound($"El usuario con cédula {id} no existe en el sistema, por favor valide");
                }

                return Ok(usuario);
            }
            catch(Exception error)
            {
                // Log para tener en cuenta los registros y posibles excepciones
                _logger.LogError(error, "Error al obtener el usuario con ID {Id}", id);

                // Return a 500 Internal Server Error with a generic error message
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud, por favor vuelve a intentarlo");
            }
           
        }

        //crear usuario en la tabla Usuarios
        [HttpPost]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            try
            {
                if (UsuarioExists(usuario.Cedula))
                {
                    return Conflict($"El usuario con la cédula {usuario.Cedula} ya existe, si desea cambiar algún dato por favor actualice el usuario");
                }

                usuario.Puntaje = usuario.CalcularPuntaje();  
                usuario.FechaAcceso = DateTime.Now; 

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok("Usuario creado con éxito");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al ecrear un usuario en la tabla de la base de datos.");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud, por favor vuelve a intentarlo");
            }
        }

        //Editar un usuario existente en la tabla Usuarios
        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuario usuarioNuevo)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(usuarioNuevo.Cedula);       

                if(usuarioExistente == null)
                {
                    return NotFound($"El usuario con cédula {usuarioNuevo.Cedula} no existe en el sistema, por favor valide");       
                }

                usuarioNuevo.Puntaje = usuarioNuevo.CalcularPuntaje();       
                usuarioNuevo.FechaAcceso = usuarioExistente.FechaAcceso;  // respetar la fecha de acceso que tenía el usuario  
                
                _context.Usuarios.Update(usuarioNuevo);  

                await _context.SaveChangesAsync();

                return Ok("Usuario creado con éxito");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al ecrear un usuario en la tabla de la base de datos.");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud, por favor vuelve a intentarlo");
            }
        }

        // borrar usuario en la tabla Usuarios por Id = Cedula
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarUsuario(long id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound("El usuario que se desea eliminar no existe en la base de datos");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(Exception error)
            {
                _logger.LogError(error, "Error al eliminar un usuario en la tabla de la base de datos.");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud, por favor vuelve a intentarlo"); 
            }
           
        }

        private bool UsuarioExists(long id)
        {
            try
            {
                var ExistenciaUsuario = _context.Usuarios.Any(e => e.Cedula == id);
                return ExistenciaUsuario;
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al comprobar la existencia de un usuario en la tabla de la base de datos.");
                return false;
            }
         
        }


    }
}
