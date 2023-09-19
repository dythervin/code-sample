namespace Game
{
    public static class EntityExtensions
    {
        private static IEntityColliderMap _colliderMap;

        public static void Init(IEntityColliderMap colliderMap)
        {
            _colliderMap = colliderMap;
        }

        public static bool HasCollider(this IEntityExt entityExt, int colliderId)
        {
            return _colliderMap.TryGetValue(colliderId, out IEntityExt entity) && entity == entityExt;
        }
    }
}