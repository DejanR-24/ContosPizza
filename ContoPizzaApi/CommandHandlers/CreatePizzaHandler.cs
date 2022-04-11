using ContoPizzaApi.Commands;
using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using MediatR;

namespace ContoPizzaApi.CommandHandlers
{
    public class CreatePizzaHandler : IRequestHandler<CreatePizzaCommand, Pizza>
    {
        private readonly IBackupServiceOnCreate _backupServiceOnCreate;
        private readonly IPizzaService _pizzaService;

        public CreatePizzaHandler(IPizzaService pizzaService,IBackupServiceOnCreate backupServiceOnCreate)
        {
                _pizzaService = pizzaService;
                _backupServiceOnCreate = backupServiceOnCreate;
        }
        public async Task<Pizza> Handle(CreatePizzaCommand request, CancellationToken cancellationToken)
        {   
            _backupServiceOnCreate.SaveOnCreate(request.NewPizza);
             return await _pizzaService.CreateAsync(request.NewPizza);
         
        }
    }
}
