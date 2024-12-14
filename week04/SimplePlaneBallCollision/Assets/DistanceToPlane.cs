using UnityEngine;

public class ClosestPointOnMeshBounds : MonoBehaviour
{
    // Reference to the mesh object, can be any object with a mesh
    [SerializeField] private Transform targetMeshTransform;

    private Plane infinitePlane;
    private Vector3 meshSize;

    void Start()
    {
        // Get the mesh bounds size automatically
        meshSize = GetMeshBounds(targetMeshTransform);

        // Define the infinite plane using the normal and a point on the plane (meshTransform's position)
        infinitePlane = new Plane(targetMeshTransform.up, targetMeshTransform.position);
    }

    void Update()
    {

        // Find the closest point on the infinite plane from the object's position
        Vector3 closestPointOnPlane = GetClosestPointOnPlane(transform.position, infinitePlane);


        // Optional: visualize the closest point with a small sphere
        Debug.DrawLine(transform.position, closestPointOnPlane, Color.red);
        Debug.DrawRay(closestPointOnPlane, Vector3.up * 0.2f, Color.green);
    }

    Vector3 GetClosestPointOnPlane(Vector3 point, Plane infPlane)
    {
        // Find the signed distance from the point to the infinite plane
        float distanceToPlane = infPlane.GetDistanceToPoint(point);

        // Project the point onto the plane by subtracting the normal times the distance
        Vector3 closestPoint = point - infPlane.normal * distanceToPlane;

        return closestPoint;
    }

}
