using Dythervin.Common;
using Dythervin.Game.Framework.Data;

namespace Game.Data
{
    public abstract class EntityComponentDataExt : ModelData, IEntityComponentDataExt
    {
        protected override TypeID<IModelData> GroupId => Features.GetDataGroupId<IEntityComponentData>();
    }
}