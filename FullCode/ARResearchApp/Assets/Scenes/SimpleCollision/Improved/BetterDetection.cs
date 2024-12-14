using UnityEngine;

public class BetterDetection : MonoBehaviour
{

    private bool surfacesDetected = false;
    private GameObject[] meshObjects;
    private Plane[] planes;
    private Mesh[] meshes;
    private bool collide = false;
    [SerializeField] Material greenMaterial;
    
    void Start(){

        //Get all mesh objects currently in scene
        meshObjects = GameObject.FindGameObjectsWithTag("Surface");
        planes = new Plane[meshObjects.Length];
        meshes = new Mesh[meshObjects.Length];

        //For each mesh object, create it's plane and get its mesh    
        for(int i = 0; i < meshObjects.Length; i++){
            planes[i] = new Plane(meshObjects[i].transform.up, meshObjects[i].transform.position);
            meshes[i] = meshObjects[i].GetComponent<MeshFilter>().mesh;
        }

        //Make sure something was actually detected before testing in update
        if (meshObjects.Length > 0){
            surfacesDetected = true;
        }

    }


    void Update(){

        if (surfacesDetected){

            //For each mesh in the scene, get point on the plane closest to the ball and check it's distance to the ball
            for (int i = 0; i < meshes.Length; i++){

                //Convert balls 3D position to point projected on plane
                Vector3 closestPointOnPlane = GetClosestPointOnPlane(transform.position, planes[i]);
                Vector3 closestPointProjected = Vector3.ProjectOnPlane(closestPointOnPlane, planes[i].normal);
                Vector2 closestPoint2D = ConvertTo2D(closestPointProjected, planes[i]);

                //Get distance to plane
                float pointsDistance = planes[i].GetDistanceToPoint(transform.position);

                //If the distance is less than 0.05, we consider the point to be close enough to collide. But we also have to check the mesh bounds
                if (pointsDistance < 0.05f){
                    Vector3[] vertices = meshes[i].vertices;
                    Vector2[] projectedVertices2D = new Vector2[vertices.Length - 1];

                    //Transform vertices to plane
                    for (int j = 0; j < vertices.Length; j++){
                        vertices[j] = meshObjects[i].transform.TransformPoint(vertices[j]);
                        vertices[j] = Vector3.ProjectOnPlane(vertices[j], planes[i].normal);
                    }

                    for (int j = 0; j < vertices.Length - 1; j++){
                        projectedVertices2D[j] = ConvertTo2D(vertices[j], planes[i]);
                    }

                    //Since we know the point is close to the plane, if it's in the mesh its a collision!
                    collide = isPointInPolygon(closestPoint2D, projectedVertices2D);

                }
            }

            if (collide){
                transform.GetComponent<Renderer>().material = greenMaterial;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }

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
