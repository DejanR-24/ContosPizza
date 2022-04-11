using ContoPizzaApi.Models;
using MediatR;

namespace ContoPizzaApi.Commands
{
    public class DeletePizzaCommand : IRequest
    {
        public string Id { get; }

        public DeletePizzaCommand(string id)
        {
            Id = id;

        }
    }
}
