using ContoPizzaApi.Commands;
using ContoPizzaApi.Interfaces;
using MediatR;

namespace ContoPizzaApi.CommandHandlers;

public class DeletePizzaHandler : IRequestHandler<DeletePizzaCommand>
{
    private readonly IPizzaService _pizzaService;
    private readonly IBackupServiceBeforeDelete _backupServiceBeforeDelete;
    private readonly IBackupServiceBlob _backupServiceBlob;
    private readonly IBackupServiceFile _backupServiceFile;

    public DeletePizzaHandler(IPizzaService pizzaService,IBackupServiceBeforeDelete backupServiceBeforeDelete,IBackupServiceBlob backupServiceBlob,IBackupServiceFile backupServiceFile)
{
        _pizzaService = pizzaService;
        _backupServiceBeforeDelete = backupServiceBeforeDelete;
        _backupServiceBlob = backupServiceBlob; 
        _backupServiceFile = backupServiceFile;
}
    public async Task<Unit> Handle(DeletePizzaCommand request, CancellationToken cancellationToken)
    {
        var pizza = await  _pizzaService.GetAsync(request.Id);
        _backupServiceBeforeDelete.SaveBeforeDelete(pizza);
        _backupServiceBlob.SavePizzaToBlob(pizza);
        _backupServiceFile.SavePizzaToFile(pizza);
        await _pizzaService.RemoveAsync(request.Id);
        return Unit.Value;
    }
}
