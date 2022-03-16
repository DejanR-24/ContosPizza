using ContoPizzaApi.Models;
using ContoPizzaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContoPizzaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    private readonly PizzasService _pizzasService;

    public PizzasController(PizzasService pizzasService) =>
        _pizzasService = pizzasService;

    [HttpGet]
    public async Task<List<Pizza>> Get() =>
        await _pizzasService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Pizza>> Get(string id)
    {
        var book = await  _pizzasService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Pizza newPizza)
    {
        await  _pizzasService.CreateAsync(newPizza);

        return CreatedAtAction(nameof(Get), new { id = newPizza.Id }, newPizza);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Pizza updatedBook)
    {
        var book = await  _pizzasService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await  _pizzasService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await  _pizzasService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await  _pizzasService.RemoveAsync(id);

        return NoContent();
    }
}
