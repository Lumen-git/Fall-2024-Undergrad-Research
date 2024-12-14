using UnityEngine;

public class SurfaceDetection : MonoBehaviour
{
    private GameObject targetMesh = null;
    private Transform targetMeshTransform = null;
    private Plane infinitePlane;
    private bool isSurfaceDetected = false;
    public Vector3[] vertices;
    [SerializeField] Material redMaterial;
    [SerializeField] Material greenMaterial;
    [SerializeField] Material blueMaterial;
    public Vector2[] meshVertices;


    void Update()
    {
        // Continuously search for the target surface
        if (!isSurfaceDetected)
        {
            GameObject surfaceObject = GameObject.FindWithTag("Surface");
            if (surfaceObject != null)
            {
                targetMesh = surfaceObject;
                targetMeshTransform = targetMesh.transform;
                isSurfaceDetected = true;
            }
            return;
        }

        // Check if the target transform is still valid
        if (targetMeshTransform == null || targetMeshTransform.gameObject == null)
        {
            isSurfaceDetected = false;
            return; 
        }

        infinitePlane = new Plane(targetMeshTransform.up, targetMeshTransform.position);

        //Get vertices of mesh
        MeshFilter meshFilter = targetMesh.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        vertices = mesh.vertices;

        //Transform vertices to world space
        for (int i = 0; i < vertices.Length; i++){
            vertices[i] = targetMeshTransform.TransformPoint(vertices[i]);
            Debug.DrawRay(vertices[i], Vector3.left, Color.red, 0.1f);
        }

        //project vertices onto infinitePlane
        for (int i = 0; i < vertices.Length; i++){
            vertices[i] = Vector3.ProjectOnPlane(vertices[i], infinitePlane.normal);
            Debug.DrawRay(vertices[i], infinitePlane.normal, Color.blue, 0.1f);
        }

        Vector2[] projectedVertices2D = new Vector2[vertices.Length - 1];
        for (int i = 0; i < vertices.Length - 1; i++){
            // Convert each vertex to 2D coordinates
            projectedVertices2D[i] = ConvertTo2D(vertices[i], infinitePlane);
        }


        Vector3 closestPointOnPlane = GetClosestPointOnPlane(transform.position, infinitePlane);
        Vector3 closestPointProjected = Vector3.ProjectOnPlane(closestPointOnPlane, infinitePlane.normal);
        Debug.DrawRay(closestPointProjected, infinitePlane.normal * 0.2f, Color.red);

        Vector2 closestPoint2D = ConvertTo2D(closestPointProjected, infinitePlane);

        bool isClosestPointInside = isPointInPolygon(closestPoint2D, projectedVertices2D);

        float pointsDistance = Vector3.Distance(closestPointOnPlane, transform.position);

        if (isClosestPointInside && pointsDistance < 0.05f){
            transform.GetComponent<Renderer>().material = greenMaterial;
        } else if (isClosestPointInside){
            transform.GetComponent<Renderer>().material = blueMaterial;
        } else {
            transform.GetComponent<Renderer>().material = redMaterial;
        }



    }

    Vector3 GetClosestPointOnPlane(Vector3 point, Plane infPlane){
        // Find the signed distance from the point to the infinite plane
        float distanceToPlane = infPlane.GetDistanceToPoint(point);

        // Project the point onto the plane by subtracting the normal times the distance
        Vector3 closestPoint = point - infPlane.normal * distanceToPlane;

        return closestPoint;
    }

    bool isPointInPolygon(Vector2 point, Vector2[] polygonVertices){
    int intersectionCount = 0;
    int vertexCount = polygonVertices.Length;

    // Loop through all edges of the polygon
    for (int i = 0; i < vertexCount; i++){
        Vector2 v1 = polygonVertices[i];
        Vector2 v2 = polygonVertices[(i + 1) % vertexCount];  // Wrap around to the first vertex

        // Check if the ray crosses the edge
        if ((v1.y > point.y) != (v2.y > point.y) &&
            point.x < (v2.x - v1.x) * (point.y - v1.y) / (v2.y - v1.y) + v1.x)
        {
            intersectionCount++;
        }
    }

    // If the number of intersections is odd, the point is inside the polygon
    return (intersectionCount % 2 == 1);
    }

    Vector2 ConvertTo2D(Vector3 point, Plane plane)
    {
        // Define local axes for the plane
        Vector3 planeRight, planeForward;

        // If the plane is vertical (close to the y-axis), use x and z for the local 2D axes
        if (Mathf.Abs(plane.normal.y) > 0.9f)
        {
            planeRight = Vector3.right;
            planeForward = Vector3.forward;
        }
        else
        {
            // For non-vertical planes, calculate the local axes using cross products
            planeRight = Vector3.Cross(plane.normal, Vector3.up).normalized;
            planeForward = Vector3.Cross(plane.normal, planeRight).normalized;
        }

        // Project the 3D point onto the plane's local 2D coordinate system
        return new Vector2(Vector3.Dot(point, planeRight), Vector3.Dot(point, planeForward));
    }

}
