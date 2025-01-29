using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPruebaTecnica.Models;
using System.Net;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly GodoyCordobaContext _context;

        public UsuariosController(GodoyCordobaContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet("ListarUsuarios")]
        public async Task<ActionResult> ListarUsuarios()
        {
            try
            {
                var ListaUsuarios = await _context.Usuarios.ToListAsync();
                if (ListaUsuarios == null)
                {
                    var ErrorResponse = new ApiResponse
                    {
                        Status = false,
                        Message = "No hay usuarios registrados, por favor registre usuarios",
                        Data = null,
                        Metadata = new Dictionary<string, object>()
                        {
                            { "Error" , "Tabla Vacia en la base de datos" }
                        }
                    };
                    return Ok(ErrorResponse);
                }


                var SuccessResponse = new ApiResponse
                {
                    Status = true,
                    Message = "Usuarios listados con éxito",
                    Data = ListaUsuarios,
                    Metadata = null
                };

                return Ok(SuccessResponse);

            }
            catch(Exception error)
            {

                var ErrorResponse = new ApiResponse
                {
                    Status = false,
                    Message = "Ocurrió un error interno al procesar la solicitud de listar usuarios, por favor vuelve a intentarlo",
                    Data = null,
                    Metadata = new Dictionary<string, object>()
                    {
                        { "Error" , error.Message }
                    }
                };
                return StatusCode(500, ErrorResponse);

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
                    var ErrorResponse = new ApiResponse
                    {
                        Status = false,
                        Message = $"El usuario con cédula {id} no existe en el sistema, por favor valide",
                        Data = null,
                        Metadata = new Dictionary<string, object>()
                        {
                            { "Error" , "El usuario no existe en la base de datos" }
                        }
                    };
                    return Ok(ErrorResponse);
                }

                var SuccessResponse = new ApiResponse
                {
                    Status = true,
                    Message = $"El usuario con cédula {id} no existe en el sistema, por favor valide",
                    Data = usuario,
                    Metadata = new Dictionary<string, object>()
                        {
                            { "Error" , "El usuario no existe en la base de datos" }
                        }
                };

                return Ok(SuccessResponse);
            }
            catch(Exception error)
            {
                var ErrorResponse = new ApiResponse
                {
                    Status = false,
                    Message = "Ocurrió un error interno al procesar la solicitud de listar usuario, por favor vuelve a intentarlo",
                    Data = null,
                    Metadata = new Dictionary<string, object>()
                    {
                        { "Error" , error.Message }
                    }
                };
                return StatusCode(500, ErrorResponse);
            }
           
        }

        //crear usuario en la tabla Usuarios
        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            try
            {
                if (UsuarioExists(usuario.Cedula))
                {
                    var ErrorResponse = new ApiResponse
                    {
                        Status = false,
                        Message = $"El usuario con la cédula {usuario.Cedula} ya existe, si desea cambiar algún dato por favor actualice el usuario",
                        Data = null,
                        Metadata = null
                    };      
                    return Ok(ErrorResponse);
                }

                usuario.Password = PasswordHasher.HashPassword(usuario.Password); // encriptar la contraseña    
                usuario.Puntaje = usuario.CalcularPuntaje();  
                usuario.FechaAcceso = DateTime.Now; 

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                var SuccessResponse = new ApiResponse
                {
                    Status = true,
                    Message = "Usuario creado con éxito",
                    Data = null,
                    Metadata = null
                };      

                return Ok("Usuario creado con éxito");
            }
            catch (Exception error)
            {
                var ErrorResponse = new ApiResponse
                {
                    Status = false,
                    Message = "Ocurrió un error interno al procesar la solicitud de crear usuario, por favor vuelve a intentarlo",
                    Data = null,
                    Metadata = new Dictionary<string, object>()
                    {
                        { "Error" , error.Message }
                    }
                };
                return StatusCode(500, ErrorResponse); ;
            }
        }

        //Editar un usuario existente en la tabla Usuarios
        [HttpPost("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            try
            {
            
                if(!UsuarioExists(usuario.Cedula))
                {
                    var ErrorResponse = new ApiResponse
                    {
                        Status = false,
                        Message = $"El usuario con cédula {usuario.Cedula} no existe en el sistema, por favor valide",
                        Data = null,
                        Metadata = new Dictionary<string, object>()
                        {
                            { "Error" , "El usuario no existe en la base de datos" }
                        }
                    };      
                    return Ok(ErrorResponse);       
                }

                usuario.Puntaje = usuario.CalcularPuntaje();       
                usuario.FechaAcceso = DateTime.Now;  // respetar la fecha de acceso que tenía el usuario  
                
                _context.Usuarios.Update(usuario);  

                await _context.SaveChangesAsync();

                var SuccessResponse = new ApiResponse
                {
                    Status = true,
                    Message = "Usuario actualizado con éxito",
                    Data = null,
                    Metadata = null
                };      

                return Ok(SuccessResponse);

            }
            catch (Exception error)
            {
               var ErrorResponse = new ApiResponse
               {
                   Status = false,
                    Message = "Ocurrió un error interno al procesar la solicitud de editar usuario, por favor vuelve a intentarlo",
                    Data = null,
                    Metadata = new Dictionary<string, object>()
                    {
                        { "Error" , error.Message }
                    }
                };
                return StatusCode(500, ErrorResponse);      
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
                    var ErrorResponse = new ApiResponse
                    {
                        Status = false,
                        Message = $"El usuario con cédula {id} no existe en la base de datos, por favor valide",
                        Data = null,
                        Metadata = new Dictionary<string, object>()
                        {
                            { "Error" , "El usuario no existe en la base de datos" }
                        }
                    };          
                    return Ok(ErrorResponse);
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                var SucessResponse = new ApiResponse
                {
                    Status = true,
                    Message = $"El usuario con cédula {id} fue eliminado con éxito",
                    Data = null,
                    Metadata = null
                };  
                return Ok(SucessResponse);
            }
            catch(Exception error)
            {
                var ErrorResponse = new ApiResponse
                {
                    Status = false,
                    Message = "Ocurrió un error interno al procesar la solicitud de borrar usuario, por favor vuelve a intentarlo",
                    Data = null,
                    Metadata = new Dictionary<string, object>()
                    {
                        { "Error" , error.Message }
                    }
                };  

                return StatusCode(500, ErrorResponse);
            }
           
        }

        //validar existencia de un User
        private bool UsuarioExists(long id)
        {
            try
            {
                var ExistenciaUsuario = _context.Usuarios.Any(e => e.Cedula == id);
                return ExistenciaUsuario;
            }
            catch (Exception error)
            {
                //_logger.LogError(error, "Error al comprobar la existencia de un usuario en la tabla de la base de datos.");
                return false;
            }
         
        }

        //servicio para loguear un usuario  
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email.ToLower() == login.email.ToLower().Trim());

                if(usuario == null)
                {

                    var ErrorResponse = new ApiResponse
                    {
                        Status = false,
                        Message = "El correo es incorrecto, por favor vuelva a intentar",
                        Data = null,
                        Metadata = new Dictionary<string, object>()
                        {
                            { "Error" , "El correo no existe en la base de datos" }
                        }
                    };

                    return Ok(ErrorResponse);    
                }

                if(!PasswordHasher.VerifyPassword(login.password, usuario.Password))
                {
                    var ErrorResponse = new ApiResponse
                    {
                        Status = false,
                        Message = "La contraseña es incorrecta",
                        Data = null,
                        Metadata = new Dictionary<string, object>()
                        {
                            { "Error" , "La contraseña con el HASH" }
                        }
                    };
                    return Ok(ErrorResponse);
                }       

                usuario.FechaAcceso = DateTime.Now; //actualizar la fecha de acceso 
                _context.Usuarios.Update(usuario);

                var SuccessResponse = new ApiResponse
                {
                    Status = true, 
                    Message = "Usuario logueado con éxito", 
                    Data = null,
                    Metadata = null
                };

                return Ok(SuccessResponse);

            }
            catch(Exception error)
            {
                var ErrorResponse = new ApiResponse
                {
                    Status = false,
                    Message = "Ocurrió un error interno al procesar la solicitud de logueo, vuelva a intentar",
                    Data = null,    
                    Metadata = new Dictionary<string, object>()
                    {
                        { "Error" , error.Message }
                    }
                };

                return StatusCode(500, ErrorResponse);
            }

        }

    }
}
