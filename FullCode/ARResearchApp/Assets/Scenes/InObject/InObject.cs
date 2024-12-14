using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InObject : MonoBehaviour
{

    [SerializeField] GameObject ARCube;
    [SerializeField] Button spawnButton;
    [SerializeField] Material redMaterial;
    [SerializeField] Material greenMaterial;
    [SerializeField] GameObject ball;
    private bool isObjectDetected = false;
    public List<GameObject> targetMesh = new List<GameObject>();
    public List<Plane> infinitePlanes = new List<Plane>();
    public List<Transform> targetMeshTransforms = new List<Transform>();
    public int passedTargets = 0;


    void Update()
    {

        if (!isObjectDetected){
            GameObject surfaceObject = GameObject.FindWithTag("ARObject");
                
                foreach (Transform child in surfaceObject.transform){
                    targetMesh.Add(child.gameObject);
                    targetMeshTransforms.Add(child);
                    infinitePlanes.Add(new Plane(child.forward, child.position));
                }
                isObjectDetected = true;
            return;
        }

        for (int i = 0; i < infinitePlanes.Count; i++){

            Vector3 closestPointOnPlane = GetClosestPointOnPlane(ball.transform.position, infinitePlanes[i]);
            Debug.DrawRay(closestPointOnPlane, infinitePlanes[i].normal, Color.yellow);

            if (infinitePlanes[i].GetDistanceToPoint(ball.transform.position) >= 0){
                passedTargets++;
            }

        }

        if (passedTargets == infinitePlanes.Count){
            ball.transform.GetComponent<Renderer>().material = greenMaterial;
        } else {
            ball.transform.GetComponent<Renderer>().material = redMaterial;
        }

        passedTargets = 0;


    }

    public void spawnARCube(){
        Instantiate(ARCube, ball.transform.position, Quaternion.identity);
        spawnButton.interactable = false;
    }


    Vector3 GetClosestPointOnPlane(Vector3 point, Plane infPlane){
        // Find the signed distance from the point to the infinite plane
        float distanceToPlane = infPlane.GetDistanceToPoint(point);

        // Project the point onto the plane by subtracting the normal times the distance
        Vector3 closestPoint = point - infPlane.normal * distanceToPlane;

        return closestPoint;
    }

}
