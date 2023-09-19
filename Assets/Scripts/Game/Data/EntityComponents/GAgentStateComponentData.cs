using System.Runtime.InteropServices;

namespace Game.Data
{
    [Guid("661F92CE-D2C7-4227-85A5-CEC606D3CA3C")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public class GAgentStateComponentData : EntityComponentDataExt
    {
        public override bool IsReadOnly => true;
    }
}