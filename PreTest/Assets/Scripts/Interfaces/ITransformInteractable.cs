using UnityEngine;

namespace Preassignment.Interactables
{
    /// <summary>
    /// Interface for runtime manipulation of object transforms (e.g., position and rotation) independent of specific input methods
    /// </summary>
    public interface ITransformInteractable
    {
        bool IsBeingTransformed { get; }
        Transform Transform { get; }
        void BeginTransform();
        void UpdateTransform(Vector3 position, Quaternion rotation);
        void EndTransform();
    }
}
