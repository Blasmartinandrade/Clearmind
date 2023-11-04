using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Clearmind.Models.Entidades;
using Clearmind.Models.Services;




namespace Clearmind.Controllers;




[Route("api/usuarios")]
public class UsuarioController : Controller{
    private readonly UsuarioServices _service;

    public UsuarioController(UsuarioServices service)
    {
        _service = service;
    }




    
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarUsuario([FromBody]Usuario user)
    {
        try
        {
            await _service.EliminarUsuario(user.Id);
            return NoContent();
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, ex.Message);
        }
    }
}



    

/*
Retorno de Json

var datos = new { clave = "valor", otro = 123 }; 
        return new JsonResult(datos);
    }

*/