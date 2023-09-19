using System.Runtime.InteropServices;

namespace Game.Data
{
    [Guid("7D307D79-1DF8-47A7-B433-B84C41EFD05F")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public class LogComponentData : EntityComponentDataExt, ILogComponentData
    {
        public override bool IsReadOnly => true;
    }
}