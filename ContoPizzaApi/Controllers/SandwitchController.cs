using ContoPizzaApi.Models;
using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ContoSandwitchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SandwitchController : ControllerBase
{
    private readonly SandwitchService _sandwitchService;
    private readonly IBackupServiceBeforeDelete _backupServiceBeforeDelete;
        private readonly IBackupServiceOnCreate _backupServiceOnCreate;
    private readonly IMapper _mapper;


    public SandwitchController(SandwitchService sandwitchService, IBackupServiceBeforeDelete backupServiceBeforeDelete,IBackupServiceOnCreate backupServiceOnCreate, IMapper mapper)
    {
        _sandwitchService = sandwitchService;
        _backupServiceBeforeDelete = backupServiceBeforeDelete;
        _backupServiceOnCreate = backupServiceOnCreate;
        _mapper = mapper;

    }

    [HttpGet]
    public async Task<List<Sandwitch>> Get() =>
        await _sandwitchService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Sandwitch>> Get(string id)
    {
        var sandwitch = await _sandwitchService.GetAsync(id);

        if (sandwitch is null)
        {
            return NotFound();
        }

        return sandwitch;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Sandwitch newSandwitch)
    {
        await _sandwitchService.CreateAsync(newSandwitch);


        //Save with AutoMapper

        var metadata = _mapper.Map<Metadata>(newSandwitch);
        _backupServiceOnCreate.SaveOnCreate(metadata);

        return CreatedAtAction(nameof(Get), new { id = newSandwitch.Id }, newSandwitch);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Sandwitch updatedBook)
    {
        var sandwitch = await _sandwitchService.GetAsync(id);

        if (sandwitch is null)
        {
            return NotFound();
        }

        updatedBook.Id = sandwitch.Id;

        await _sandwitchService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var sandwitch = await _sandwitchService.GetAsync(id);

        if (sandwitch is null)
        {
            return NotFound();

        }

        _backupServiceBeforeDelete.SaveBeforeDelete(sandwitch);






        await _sandwitchService.RemoveAsync(id);

        return NoContent();
    }
}
