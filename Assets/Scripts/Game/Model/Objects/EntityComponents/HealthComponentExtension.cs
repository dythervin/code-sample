namespace Game
{
    public static class HealthComponentExtension
    {
        public static float GetValuePrc(this IHealthComponent healthComponent)
        {
            return healthComponent.Value / healthComponent.Max;
        }
    }
}