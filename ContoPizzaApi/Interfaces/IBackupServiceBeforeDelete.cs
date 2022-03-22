using ContoPizzaApi.Models;
using System.Collections.Generic;

namespace ContoPizzaApi.Interfaces;

public interface IBackupServiceBeforeDelete
{
   void SaveBeforeDelete<T>(T instance);
    
}
