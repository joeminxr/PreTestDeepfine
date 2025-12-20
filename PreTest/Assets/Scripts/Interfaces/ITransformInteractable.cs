using UnityEngine;

namespace Preassignment.Interactables
{
    public interface ITransformInteractable
    {
        Transform Transform { get; }
        void BeginTransform();
        void UpdateTransform(Vector3 position, Quaternion rotation);
        void EndTransform();
    }
}
