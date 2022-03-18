using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using ContoPizzaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContoPizzaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    private readonly PizzasService _pizzasService;
    private readonly IBackupService _backupService;

    public PizzasController(PizzasService pizzasService, IBackupService backupService)
    {
        _pizzasService = pizzasService;
        _backupService = backupService;
    }

    [HttpGet]
    public async Task<List<Pizza>> Get() => 
        await _pizzasService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Pizza>> Get(string id)
    {
        var pizza = await  _pizzasService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        return pizza;
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
        var pizza = await  _pizzasService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        updatedBook.Id = pizza.Id;

        await  _pizzasService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var pizza = await  _pizzasService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();

        }


        _backupService.SavePizzaToFile(pizza);

        
        //_backupService.SavePizzaToBlob(pizza);
        //_backupService.SavePizza(pizza);

        await  _pizzasService.RemoveAsync(id);

        return NoContent();
    }
}
