namespace Preassignment.Interactables
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public sealed class Mirror : MonoBehaviour, ILaserInteractable, ITransformInteractable
    {
        public Vector3 Reflect(Vector3 incomingDirection, Vector3 surfaceNormal)
        {
            return Vector3.Reflect(incomingDirection, surfaceNormal);
        }

        public void OnLaserEnter()
        {
            Debug.Log("This mirror has been hit with a laser");
        }

        public void OnLaserExit() { }

        public Transform Transform => transform;

        public void BeginTransform()
        {
        }

        public void UpdateTransform(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }

        public void EndTransform()
        {
        }
    }
}