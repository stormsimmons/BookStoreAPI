using BookStore.Domain.Enums;
using System;

namespace BookStore.Domain.CommandCriteria
{
    public class CommandCriteria
    {
        private CommandType? _commandType = null;
        public CommandType CommandType
        {
            get
            {
                if (_commandType == null)
                {
                    throw new NotImplementedException();
                }
                return (CommandType)_commandType;
            }
            protected set => _commandType = value;
        }
    }
}
