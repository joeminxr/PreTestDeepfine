using System.Linq;
using UnityEngine;

namespace Preassignment.LaserSystem
{
    /// <summary>
    /// Laser visuals. Updates line renderer accordingly to TraceResult data
    /// </summary>
    public sealed class LaserRenderer
    {
        private LineRenderer _lineRenderer;

        public LaserRenderer(LineRenderer lineRenderer)
        {
            _lineRenderer = lineRenderer;
            _lineRenderer.useWorldSpace = true;
        }

        /// <summary>
        /// Updates the laser and its reflection points
        /// </summary>
        /// <param name="traceResult"></param>
        public void Render(TraceResult traceResult)
        {
            // Laser is always on in this assignment so TraceResult is expected to always contain a valid path
            var points = traceResult.Points;

            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }
    }
}
