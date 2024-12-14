using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class RoomScan : MonoBehaviour
{
    [SerializeField] int multi = 1;

    public Dictionary<int, Vector2> vertex = new Dictionary<int, Vector2>{};
    List<Vector2> points = new List<Vector2>{};

    int pointMarker = 0;
    public Dictionary<int, int[]> lineDefs = new Dictionary<int, int[]>{};
    int lineDefSidefront = 0;
    public float tolerance = 1f;
    [SerializeField] GameObject textBox;
    private string output = "namespace = \"zdoom\";\n";


    public void ConvertButton(){

        GameObject[] meshes = GameObject.FindGameObjectsWithTag("Surface");
        
        for (int i = 0; i < meshes.Length; i++){
            GameObject meshObject = meshes[i];
            MeshFilter meshFilter = meshObject.GetComponent<MeshFilter>();
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices3 = mesh.vertices;

            Vector2[] flatPoints = get2Dpoints(meshObject, vertices3);

            float[] vertexPair = new float[2]{Mathf.Infinity, Mathf.Infinity};

            if (points.Count > 0){
                for (int k = 0; k < points.Count; k++){
                    if (Mathf.Abs(Vector2.Distance(flatPoints[0], points[k])) <= tolerance && vertexPair[0] == Mathf.Infinity){
                        int matchingKey = vertex.FirstOrDefault(x => x.Value.Equals(points[k])).Key;
                        vertexPair[0] = matchingKey;
                    }
                    if (Mathf.Abs(Vector2.Distance(flatPoints[1], points[k])) <= tolerance && vertexPair[0] == Mathf.Infinity){
                        int matchingKey = vertex.FirstOrDefault(x => x.Value.Equals(points[k])).Key;
                        vertexPair[1] = matchingKey;
                    }
                    if (vertexPair[0] != Mathf.Infinity && vertexPair[1] != Mathf.Infinity){
                        break;
                    }
                    }
                }

                if (vertexPair[0] == Mathf.Infinity){
                    vertex[pointMarker] = flatPoints[0];
                    vertexPair[0] = pointMarker;
                    points.Add(flatPoints[0]);
                    pointMarker++;
                }

                if (vertexPair[1] == Mathf.Infinity){
                    vertex[pointMarker] = flatPoints[1];
                    vertexPair[1] = pointMarker;
                    points.Add(flatPoints[1]);
                    pointMarker++;
                }

                int[] vertexPairInt = new int[]{(int)vertexPair[0], (int)vertexPair[1]};
                lineDefs[lineDefSidefront] = vertexPairInt;
                lineDefSidefront++;

            
        }

        for (int i = 0; i < pointMarker; i++){
            output = output + "vertex // " + i.ToString() + "\n{\nx = " + vertex[i].x.ToString() + ";\ny = " + vertex[i].y.ToString() + ";\n}\n\n";
        }

        for (int i = 0; i < lineDefSidefront; i++){
            output = output + "linedef // " + i.ToString() + "\n{\nv1 = " + lineDefs[i][0].ToString() + ";\nv2 = " + lineDefs[i][1].ToString() + ";\nsidefront = " + i.ToString() + ";\nblocking = true;\n}\n\n";
        }

        for (int i = 0; i < lineDefSidefront; i++){
            output = output + "sidedef // " + i.ToString() + "\n{\nsector = 0;\ntexturemiddle = \"STARTAN2\";\n}\n\n";
        }

        output = output + "sector // 0\n{\nheightfloor = 0;\nheightceiling = 128;\ntexturefloor = \"FLOOR0_1\";\ntextureceiling = \"CEIL1_1\";\nlightlevel = 192;\n}\n\n";

        textBox.GetComponent<TMP_InputField>().text = output;
        textBox.SetActive(true);
        GUIUtility.systemCopyBuffer = output;

    }

    private Vector2[] get2Dpoints(GameObject mesh, Vector3[] verts){       
        Vector2[] convert = new Vector2[verts.Length];
        Vector2[] result = new Vector2[2];

        for (int i = 0; i < verts.Length; i++){
            Vector3 worldPoint = mesh.transform.TransformPoint(verts[i]);
            convert[i] = new Vector2((int)(worldPoint.x * multi), (int)(worldPoint.z * multi));
        }

        Vector2 pointA = Vector2.zero;
        Vector2 pointB = Vector2.zero;
        float maxDistance = 0f;

        for (int i = 0; i < convert.Length; i++)
        {
            for (int j = i + 1; j < convert.Length; j++)
            {
                float distance = Vector2.Distance(convert[i], convert[j]);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    pointA = convert[i];
                    pointB = convert[j];
                }
            }
        }

        result[0] = pointA;
        result[1] = pointB;

        return result;
    }

}
