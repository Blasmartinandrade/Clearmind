using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Clearmind.Models.Entidades;
using Clearmind.Models.Services;
using Clearmind.Database;



namespace Clearmind.Controllers;




[Route("api/usuarios")]
public class UsuarioController : Controller{
    private readonly DataContext _context;
    private readonly UsuarioServices _service;

    public UsuarioController(DataContext context, UsuarioServices service)
    {
        _context = context;
        _service = service;
    }



    [HttpPost("crear")] //api/usuarios/crear
    public IActionResult CrearUsuario([FromBody] Usuario usuario)
    {

        //Manejo de codigos de estado
        if (usuario != null)
        {
            return new ObjectResult(new { mensaje = "Producto creado con éxito"})
            {
                StatusCode = 201

            };
        }
        else
        {
            return new ObjectResult(new { mensaje = "No se pudo crear el producto" })
            {
                StatusCode = 400 
            };
        }
        
    }

    
    
}


/*
[Route("api/productos")]
public class ProductController : Controller
{
    [HttpPost("crear")] // Ruta: /api/productos/crear
    public IActionResult CreateProduct([FromBody] Product product)
    {
        // Lógica para crear un nuevo producto
    }

    [HttpDelete("eliminar")] // Ruta: /api/productos/eliminar
    public IActionResult DeleteProduct(int id)
    {
        // Lógica para eliminar un producto por ID
    }

    // Otras acciones aquí
}


Retorno de Json

var datos = new { clave = "valor", otro = 123 }; 
        return new JsonResult(datos);
    }

*/