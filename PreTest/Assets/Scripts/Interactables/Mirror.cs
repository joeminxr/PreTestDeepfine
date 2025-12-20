using UnityEngine;
using Preassignment.Interactables;

public sealed class Mirror : MonoBehaviour, IMirrorSurface
{
    public Vector3 Reflect(Vector3 incomingDirection, Vector3 surfaceNormal)
    {
        return Vector3.zero; //tbd
    }

    public void OnInteractStart()
    {
    }

    public void OnInteractEnd()
    {
    }
}