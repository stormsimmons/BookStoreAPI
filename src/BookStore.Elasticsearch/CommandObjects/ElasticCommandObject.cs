using BookStore.Domain.CommandCriteria;
using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using BookStore.Elasticsearch.Interfaces;
using System;

namespace BookStore.Elasticsearch.CommandObjects
{
    public abstract class ElasticCommandObject<T, TCriteria> : IElasticCommand
        where T : class, IEntity
        where TCriteria : CommandCriteria
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

        public abstract T GetCommandValue(TCriteria commandCriteria);
    }
}
