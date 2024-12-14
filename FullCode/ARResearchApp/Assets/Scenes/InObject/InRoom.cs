using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InRoom : MonoBehaviour
{

    [SerializeField] Material redMaterial;
    [SerializeField] Material greenMaterial;
    [SerializeField] GameObject ball;
    private bool isDetected = false;
    public List<Plane> infinitePlanes = new List<Plane>();
    public GameObject[] meshes;
    public int passedTargets = 0;


    void Update()
    {

        infinitePlanes = new List<Plane>();

        meshes = GameObject.FindGameObjectsWithTag("Surface");

        foreach (GameObject mesh in meshes){
            infinitePlanes.Add(new Plane(mesh.transform.up, mesh.transform.position));
            Debug.DrawRay(mesh.transform.position, mesh.transform.up, Color.red);
        }


        for (int i = 0; i < infinitePlanes.Count; i++){

            Vector3 closestPointOnPlane = GetClosestPointOnPlane(ball.transform.position, infinitePlanes[i]);
            float distance = infinitePlanes[i].GetDistanceToPoint(ball.transform.position);

            if (distance >= 0){
                passedTargets++;
            }

        }

        if (passedTargets == infinitePlanes.Count){
            ball.transform.GetComponent<Renderer>().material = greenMaterial;
        } else {
            ball.transform.GetComponent<Renderer>().material = redMaterial;
        }

        passedTargets = 0;
        Debug.Log(passedTargets);


    }


    Vector3 GetClosestPointOnPlane(Vector3 point, Plane infPlane){
        // Find the signed distance from the point to the infinite plane
        float distanceToPlane = infPlane.GetDistanceToPoint(point);

        // Project the point onto the plane by subtracting the normal times the distance
        Vector3 closestPoint = point - infPlane.normal * distanceToPlane;

        return closestPoint;
    }

}
