using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCommander : ICommander
    {
        private readonly CommanderContext _context;

        public SqlCommander(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Remove(cmd);

            _context.SaveChanges();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            //No Need
        }
    }
}