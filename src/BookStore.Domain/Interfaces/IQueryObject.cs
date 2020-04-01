using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Interfaces
{
    public interface IQueryObject<TQuery, TQueryResponse, TResponse>
    {
        TQuery SearchDescriptor { get;}
        TResponse FormatSearchResponse(TQueryResponse queryResponse);
    }
}
