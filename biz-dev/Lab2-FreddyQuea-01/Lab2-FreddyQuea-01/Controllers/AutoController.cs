using Lab2_FreddyQuea_01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace Lab2_FreddyQuea_01.Controllers;

[ApiController]
[Route("fquea/autos")]
public class AutoController : ControllerBase
{
  private static readonly List<Auto<Persona>> AutosPersona = new();
  private static readonly List<Auto<Empresa>> AutosEmpresa = new();

  // <-- POST - Agregar auto (Persona) -->
  [HttpPost("persona")]
  public IActionResult AddAutoPersona([FromBody] Auto<Persona> auto)
  {
    auto.Id = AutosPersona.Count + 1;
    AutosPersona.Add(auto);
    return Ok(new { message = "Auto Agregado", auto });
  }

  // <-- POST - Agregar auto (Empresa) -->
  [HttpPost("empresa")]
  public IActionResult AddAutoEmpresa([FromBody] Auto<Empresa> auto)
  {
    auto.Id = AutosEmpresa.Count + 1;
    AutosEmpresa.Add(auto);
    return Ok(new { message = "Auto Agregado", auto });
  }

  // <-- GET - Obtener todos los autos (Persona y Empresa) -->
  [HttpGet("persona")]
  public IActionResult GetAllAutoPersona() => Ok(AutosPersona);

  [HttpGet("empresa")]
  public IActionResult GetAllAutoEmpresa() => Ok(AutosEmpresa);

  // <-- GET - Obtener un auto por ID -->
  [HttpGet("persona/{id}")]
  public IActionResult GetAutoPersona([FromRoute] int id)
  {
    var auto = AutosPersona.FirstOrDefault(a => a.Id == id);
    return auto == null ? NotFound() : Ok(auto);
  }

  [HttpGet("empresa/{id}")]
  public IActionResult GetAutoEmpresa([FromRoute] int id)
  {
    var auto = AutosEmpresa.FirstOrDefault(a => a.Id == id);
    return auto == null ? NotFound() : Ok(auto);
  }

  // <-- PUT - Actualizar un auto -->
  [HttpPut("persona/{id}")]
  public IActionResult UpdateAutoPersona([FromRoute] int id, [FromBody] Auto<Persona> auto)
  {
    var index = AutosPersona.FindIndex(a => a.Id == id);
    if (index == -1) return NotFound();

    auto.Id = id;
    AutosPersona[index] = auto;
    return Ok(new { message = "Auto Actualizado", auto });
  }

  [HttpPut("empresa/{id}")]
  public IActionResult UpdateAutoPersona([FromRoute] int id, [FromBody] Auto<Empresa> auto)
  {
    var index = AutosEmpresa.FindIndex(a => a.Id == id);
    if (index == -1) return NotFound();

    auto.Id = id;
    AutosEmpresa[index] = auto;
    return Ok(new { message = "Auto Actualizado", auto });
  }

  // <-- PATCH - Actualizacion parcial de un auto -->
  [HttpPatch("persona/{id}")]
  public IActionResult PatchAutoPersona([FromRoute] int id, [FromBody] Auto<Persona>? autoPatch)
  {
    if (autoPatch == null) return BadRequest(new { message = "Datos inválidos" });

    var auto = AutosPersona.FirstOrDefault(a => a.Id == id);
    if (auto == null) return NotFound(new { message = "Auto no encontrado" });

    if (!string.IsNullOrEmpty(autoPatch.Marca)) auto.Marca = autoPatch.Marca;
    if (!string.IsNullOrEmpty(autoPatch.Modelo)) auto.Modelo = autoPatch.Modelo;
    if (!string.IsNullOrEmpty(autoPatch.Placa)) auto.Placa = autoPatch.Placa;
    if (!string.IsNullOrEmpty(autoPatch.SedeTransito)) auto.SedeTransito = autoPatch.SedeTransito;
    if (autoPatch.Propietario != null) auto.Propietario = autoPatch.Propietario;

    return Ok(new { message = "Auto actualizado", auto });
  }

  [HttpPatch("empresa/{id}")]
  public IActionResult PatchAutoEmpresa([FromRoute] int id, [FromBody] Auto<Empresa>? autoPatch)
  {
    if (autoPatch == null) return BadRequest(new { message = "Datos inválidos" });

    var auto = AutosEmpresa.FirstOrDefault(a => a.Id == id);
    if (auto == null) return NotFound(new { message = "Auto no encontrado" });

    if (!string.IsNullOrEmpty(autoPatch.Marca)) auto.Marca = autoPatch.Marca;
    if (!string.IsNullOrEmpty(autoPatch.Modelo)) auto.Modelo = autoPatch.Modelo;
    if (!string.IsNullOrEmpty(autoPatch.Placa)) auto.Placa = autoPatch.Placa;
    if (!string.IsNullOrEmpty(autoPatch.SedeTransito)) auto.SedeTransito = autoPatch.SedeTransito;
    if (autoPatch.Propietario != null) auto.Propietario = autoPatch.Propietario;

    return Ok(new { message = "Auto actualizado", auto });
  }

  // <-- DELETE Eliminar un auto -->
  [HttpDelete("persona/{id}")]
  public IActionResult DeleteAutoPersona([FromRoute] int id)
  {
    var auto = AutosPersona.FirstOrDefault(a => a.Id == id);
    if (auto == null) return NotFound();
    AutosPersona.Remove(auto);
    return Ok(new { message = "Auto Eliminado", auto });
  }

  [HttpDelete("empresa/{id}")]
  public IActionResult DeleteAutoEmpresa([FromRoute] int id)
  {
    var auto = AutosEmpresa.FirstOrDefault(a => a.Id == id);
    if (auto == null) return NotFound();
    AutosEmpresa.Remove(auto);
    return Ok(new { message = "Auto Eliminado", auto });
  }
}