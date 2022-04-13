using AutoMapper;
using ContoPizzaApi.Commands;
using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using MediatR;

namespace ContoPizzaApi.CommandHandlers
{
    public class CreatePizzaHandler : IRequestHandler<CreatePizzaCommand, Pizza>
    {
        private readonly IMapper _mapper;
        private readonly IBackupServiceOnCreate _backupServiceOnCreate;
        private readonly IPizzaService _pizzaService;

        public CreatePizzaHandler(IPizzaService pizzaService,IBackupServiceOnCreate backupServiceOnCreate, IMapper mapper)
        {
                _pizzaService = pizzaService;
                _backupServiceOnCreate = backupServiceOnCreate;
                _mapper = mapper;
        }
        public async Task<Pizza> Handle(CreatePizzaCommand request, CancellationToken cancellationToken)
        {
            var metadata = _mapper.Map<Metadata>(request.NewPizza);
            _backupServiceOnCreate.SaveOnCreate(metadata);
             return await _pizzaService.CreateAsync(request.NewPizza);
         
        }
    }
}
