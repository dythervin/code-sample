using Dythervin.Game.Framework.Data;

namespace Game.Data
{
    public interface IEntityRODataExt : IEntityROData
    {
        IEntityROVars VarResolver { get; }
    }

    public interface IEntityDataExt : IEntityData, IEntityRODataExt
    {
        float Radius { get; }

        new IEntityVars VarResolver { get; }
    }
}