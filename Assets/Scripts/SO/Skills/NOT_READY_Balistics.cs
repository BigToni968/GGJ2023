using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balistics : MonoBehaviour
{
    [SerializeField]
    private float power = 5f;
    [SerializeField]
    private GameObject bulletObj;
    [SerializeField]
    private float dragForce = 2f;
    [SerializeField]
    private float gravityScale = 1f;

    private LineRenderer lr;

    private Vector2 dragStartPos;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetButton("Fire1"))
        {
            Vector2 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (dragStartPos - dragEndPos) * power;

            Vector2[] trajectory = Plot((Vector2)dragStartPos, _velocity, 500);

            lr.positionCount = trajectory.Length;

            Vector3[] postions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                postions[i] = (Vector3)trajectory[i];
            }
            lr.SetPositions(postions);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Vector2 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (dragStartPos - dragEndPos) * power;

            IMove spawned = Instantiate(bulletObj, dragStartPos, new Quaternion()).GetComponent<IMove>();
            //spawned.Move(_velocity);
        }
    }

    public Vector2[] Plot( Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestamp = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * gravityScale * timestamp * timestamp;

        float drag = 1f - timestamp / dragForce;
        Vector2 moveStep = velocity * timestamp;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }
}
