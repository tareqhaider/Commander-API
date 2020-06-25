using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommander
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);

        void CreateCommand(Command cmd);

        bool SaveChanges();

        void UpdateCommand(Command cmd);

        void DeleteCommand(Command cmd);
    }
}