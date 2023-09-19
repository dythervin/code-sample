using Dythervin.Common;
using Dythervin.Game.Framework.Data;

namespace Game.Data
{
    public interface IGameComponentDataExt : IGameComponentData
    {
    }

    public abstract class GameComponentDataExt : ModelData, IGameComponentDataExt
    {
        protected override TypeID<IModelData> GroupId => Features.GetDataGroupId<IGameComponentData>();
    }
}