using ContoPizzaApi.Models;
using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;



namespace ContoPizzaApi.Controllers;
[ApiController]
[Route("api/[controller]")]

public class BeverageController : Controller
{
    private readonly IBeverageService _beverageService;
    private readonly IBackupServiceBeforeDelete _backupServiceBeforeDelete;
    private readonly IBackupServiceOnCreate _backupServiceOnCreate;
    private readonly IMapper _mapper;


    public BeverageController(IBeverageService beverageService, IBackupServiceBeforeDelete backupServiceBeforeDelete, IMapper mapper,IBackupServiceOnCreate backupServiceOnCreate)
    {
        _beverageService = beverageService;
        _backupServiceOnCreate = backupServiceOnCreate;
        _backupServiceBeforeDelete = backupServiceBeforeDelete;
        _mapper = mapper;

    }




    [HttpGet]
    public async Task<List<Beverage>> Get() =>
        await _beverageService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Beverage>> Get(string id)
    {
        var beverage = await _beverageService.GetAsync(id);

        if (beverage is null)
        {
            return NotFound();
        }

        return beverage;
    }



    [HttpPost]
    public async Task<IActionResult> Post(Beverage newBeverage)
    {
        await _beverageService.CreateAsync(newBeverage);


        //Save with AutoMapper

        var metadata = _mapper.Map<Metadata>(newBeverage);
        _backupServiceOnCreate.SaveOnCreate(metadata);

        return CreatedAtAction(nameof(Get), new { id = newBeverage.Id }, newBeverage);
    }



    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Beverage updatedBook)
    {
        var beverage = await _beverageService.GetAsync(id);

        if (beverage is null)
        {
            return NotFound();
        }

        updatedBook.Id = beverage.Id;

        await _beverageService.UpdateAsync(id, updatedBook);

        return NoContent();
    }




    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var beverage = await _beverageService.GetAsync(id);

        if (beverage is null)
        {
            return NotFound();

        }

        _backupServiceBeforeDelete.SaveBeforeDelete(beverage);

        await _beverageService.RemoveAsync(id);

        return NoContent();
    }
}
