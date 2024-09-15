using Mangas.Domain.Entities;
using Mangas.Services.Features.mangas;
using Microsoft.AspNetCore.Mvc;

namespace Mangas.Controllers.V1;

[ApiController]
[Route("api/controller")]

public class MangasController : ControllerBase
{
    private readonly MangaService _mangaService;

    public MangasController(MangaService mangaService)
    {
        this._mangaService = mangaService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_mangaService.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var manga = _mangaService.GetById(id);

        if(manga == null)
        return NotFound();

        return Ok(manga);
    }

    [HttpPost]
    public IActionResult Add([FromBody]Manga manga)
    {
        return CreatedAtAction(nameof(GetById),new {id = manga.Id},manga);
    }

    [HttpPut("{id}")]
    public IActionResult Update (int id, Manga manga)
    {
        if(id != manga.Id)
        
            return BadRequest();

            _mangaService.Update(manga);
            return NoContent();    
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _mangaService.Delete(id);
        return NoContent();
    }
}
