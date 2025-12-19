using UnityEngine;

public interface IMirrorSurface : IInteractable
{
    Vector3 Reflect(Vector3 inDirection, Vector3 normal);
}
