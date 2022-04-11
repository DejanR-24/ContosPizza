using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using ContoPizzaApi.Queries;
using ContoPizzaApi.Commands;

namespace ContoPizzaApi.Controllers;

[EnableCors("MyPolicy")]
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
    private readonly IMediator _mediator;


    
    public PizzasController(IPizzaService pizzaService, IBackupServiceBeforeDelete backupServiceBeforeDelete,IBackupServiceOnCreate backupServiceOnCreate,IBackupServiceBlob backupServiceBlob,IBackupServiceFile backupServiceFile, IMapper mapper,
        IMediator mediator)
    {
        _pizzaService = pizzaService;
        
        _backupServiceBeforeDelete = backupServiceBeforeDelete;
        _backupServiceOnCreate = backupServiceOnCreate;
        _backupServiceBlob = backupServiceBlob;
        _backupServiceFile = backupServiceFile;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPizzas()
    {
        var query = new GetAllPizzasQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetPizza(string id)
    {
        var query = new GetPizzaByIdQuery(id);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound();
    }



    [HttpPost]
    public async Task<IActionResult> CreatePizza(Pizza newPizza)
    {
        var command = new CreatePizzaCommand(newPizza);
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : NotFound();

    }




    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id,Pizza updatedPizza)
    {

        var command = new UpdatePizzaCommand(id,updatedPizza);
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeletePizzaCommand(id);
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : NotFound();
        
    }
}
