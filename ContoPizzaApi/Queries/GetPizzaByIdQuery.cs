using ContoPizzaApi.Models;
using MediatR;

namespace ContoPizzaApi.Queries
{
    public class GetPizzaByIdQuery : IRequest<Pizza>
    {
        public string Id { get;  }
        public GetPizzaByIdQuery(string id)
        {
            Id = id;
        }
    }
}
