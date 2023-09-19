using System.Runtime.InteropServices;

namespace Game.Data
{
    [Guid("D4987FE9-966A-44DA-B8E5-50180C1D9183")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public class PlayerMovementComponentData : EntityComponentDataExt
    {
        public override bool IsReadOnly => true;
    }
}