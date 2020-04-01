using BookStore.Domain.Enums;
using System;

namespace BookStore.Domain.QueryCriteria
{
    public abstract class QueryCriteria
    {
        private QueryType? _queryType = null;
        public QueryType QueryType
        {
            get
            {
                if (_queryType == null)
                {
                    throw new NotImplementedException();
                }
                return (QueryType)_queryType;
            }
            protected set => _queryType = value;
        }
    }
}
