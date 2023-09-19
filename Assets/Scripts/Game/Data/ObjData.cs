namespace Game.Data
{
    public readonly struct ObjData
    {
        public readonly uint id;
        public readonly float distance;
        public readonly bool inVision;

        public ObjData(uint id, float distance, bool inVision)
        {
            this.id = id;
            this.distance = distance;
            this.inVision = inVision;
        }
    }
}