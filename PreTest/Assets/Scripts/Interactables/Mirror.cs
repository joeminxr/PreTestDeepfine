namespace Preassignment.Interactables
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public sealed class Mirror : MonoBehaviour, IInteractable
    {
        public Vector3 Reflect(Vector3 incomingDirection, Vector3 surfaceNormal)
        {
            return Vector3.Reflect(incomingDirection, surfaceNormal);
        }

        public void OnInteractStart()
        {
        }

        public void OnInteractEnd()
        {
        }
    }
}