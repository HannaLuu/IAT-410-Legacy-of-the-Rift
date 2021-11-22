using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private int numSegments = 24;
    private float maxZ = 8f;
    public float xPosRange = 1f;
    private float yPosRange = 0.7f;
    //private float radius = 1f;
    //private Vector2 midPoint;
    private Color colour = new Color(85, 255, 198);

    private float currXPos, currYPos;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        //for(int i = 1; i < numSegments-1; ++i)
        //{
        //    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

        //    lineRenderer.SetPosition(i, new Vector3(Random.Range(-xPosRange, xPosRange), Random.Range(-yPosRange, yPosRange), z));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        colour.a -= 1f * Time.deltaTime;

        lineRenderer.SetColors(colour, colour);

        //for (int i = 1; i < numSegments - 1; ++i)
        //{
        //    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

        //    currXPos = lineRenderer.GetPosition(i).x;
        //    currYPos = lineRenderer.GetPosition(i).y;
        //    lineRenderer.SetPosition(i, new Vector3(Random.Range(-(currXPos - xPosRange), (currXPos + xPosRange)), Random.Range(-(currYPos - yPosRange), (currYPos + yPosRange)), z));
        //}

        if (colour.a <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
