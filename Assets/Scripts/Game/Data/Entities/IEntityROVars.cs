using System.Collections.Generic;
using Dythervin.ExpressionParser;

namespace Game.Data
{
    public interface IEntityROVars : IVarResolver<double>
    {
    }

    public interface IEntityVars : IEntityROVars, IDictionary<string, double>
    {
    }
}