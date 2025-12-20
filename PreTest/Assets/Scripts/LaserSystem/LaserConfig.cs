namespace Preassignment.LaserSystem
{
    /// <summary>
    /// Immutable configuration data for the Laser system.
    /// 
    /// Constructed explicitly to demonstrate clear ownership and readiness
    /// for external data injection (e.g. JSON, DI, server-driven configs).
    /// 
    /// For this assignment, values are hard-coded at creation time.
    /// </summary>
    public sealed class LaserConfig
    {
        public int MaxReflections { get; }
        public float MaxLaserDistance { get; }
        public float SurfaceOffset { get; }
        public string[] CollisionLayerNames { get; }

        public LaserConfig(
            int maxReflections,
            float maxLaserDistance,
            float surfaceOffset,
            string[] collisionLayerNames)
        {
            MaxReflections = maxReflections;
            MaxLaserDistance = maxLaserDistance;
            SurfaceOffset = surfaceOffset;
            CollisionLayerNames = collisionLayerNames;
        }
    }
}