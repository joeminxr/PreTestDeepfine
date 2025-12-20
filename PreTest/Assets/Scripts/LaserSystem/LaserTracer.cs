using System.Collections.Generic;
using UnityEngine;
using Preassignment.Interactables;

namespace Preassignment.LaserSystem
{
    /// <summary>
    /// Raycast logic and checks to trace the path of the laser visual prior to updating the renderer
    /// </summary>
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
            var points = new List<Vector3> { origin };
            var hits = new List<RaycastHit>();

            Vector3 currentOrigin = origin;
            Vector3 currentDirection = direction.normalized;

            for (int i = 0; i < _config.MaxReflections; i++)
            {
                if (!Physics.Raycast(
                        currentOrigin,
                        currentDirection,
                        out RaycastHit hit,
                        _config.MaxLaserDistance,
                        _collisionMask))
                {
                    points.Add(
                        currentOrigin +
                        currentDirection * _config.MaxLaserDistance);
                    Debug.LogWarning("Didn't hit anything on config layers- are obstacles set to the correct layer?");
                    break;
                }

                hits.Add(hit);
                points.Add(hit.point);

                // for now- let's stop the loop when laser it hits the first obstacle
                // tbd: continue loop when hitting a mirror surface

                break;
            }

            return new TraceResult(points, hits);
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