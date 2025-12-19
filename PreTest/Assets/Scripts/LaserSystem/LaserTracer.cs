using System.Collections.Generic;
using UnityEngine;

namespace LaserSystem
{
    public sealed class LaserTracer
    {
        private readonly LaserConfig _config;
        private readonly LayerMask _collisionMask;

        public LaserTracer(LaserConfig config, LayerMask collisionMask)
        {
            _config = config;
            _collisionMask = collisionMask;
        }

        public TraceResult Trace(Vector3 origin, Vector3 direction)
        {
            return null; //tbd
        }
    }

    /// <summary>
    /// Store all points needed to render the laser.
    /// Store all hits for interaction.
    /// Be renderer-agnostic.
    /// </summary>
    public sealed class TraceResult
    {
        public IReadOnlyList<Vector3> Points { get; }
        public IReadOnlyList<RaycastHit> Hits { get; }

        public TraceResult(List<Vector3> points, List<RaycastHit> hits)
        {
            Points = points;
            Hits = hits;
        }
    }
}