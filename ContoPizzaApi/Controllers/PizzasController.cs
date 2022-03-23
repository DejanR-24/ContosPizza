using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using ContoPizzaApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ContoPizzaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    private readonly IPizzaService _pizzaService;
    private readonly IBackupServiceBeforeDelete _backupServiceBeforeDelete;
    private readonly IBackupServiceOnCreate _backupServiceOnCreate;
    private readonly IBackupServiceBlob _backupServiceBlob;
    private readonly IBackupServiceFile _backupServiceFile;
    private readonly IMapper _mapper;

    public PizzasController(IPizzaService pizzaService, IBackupServiceBeforeDelete backupServiceBeforeDelete,IBackupServiceOnCreate backupServiceOnCreate,IBackupServiceBlob backupServiceBlob,IBackupServiceFile backupServiceFile, IMapper mapper)
    {
        _pizzaService = pizzaService;
        _backupServiceBeforeDelete = backupServiceBeforeDelete;
        _backupServiceOnCreate = backupServiceOnCreate;
        _backupServiceBlob = backupServiceBlob;
        _backupServiceFile = backupServiceFile;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<List<Pizza>> Get() => 
        await _pizzaService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Pizza>> Get(string id)
    {
        var pizza = await  _pizzaService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        return pizza;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Pizza newPizza)
    {
        await  _pizzaService.CreateAsync(newPizza);

        //Save with AutoMapper

        var metadata = _mapper.Map<Metadata>(newPizza);
        _backupServiceOnCreate.SaveOnCreate(metadata);


        return CreatedAtAction(nameof(Get), new { id = newPizza.Id }, newPizza);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Pizza updatedPizza)
    {
        var pizza = await  _pizzaService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        updatedPizza.Id = pizza.Id;

        await  _pizzaService.UpdateAsync(id, updatedPizza);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var pizza = await  _pizzaService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();

        }

        _backupServiceBeforeDelete.SaveBeforeDelete(pizza);
        _backupServiceBlob.SavePizzaToBlob(pizza);
        _backupServiceFile.SavePizzaToFile(pizza);

        
        
 

        await  _pizzaService.RemoveAsync(id);

        return NoContent();
    }
}
