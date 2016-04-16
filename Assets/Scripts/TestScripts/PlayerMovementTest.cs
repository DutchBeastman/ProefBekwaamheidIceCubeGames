//Created By: Jeremy Bond
//Date: 07/04/2016

using UnityEngine;
using System.Collections;

public class PlayerMovementTest : MonoBehaviour
{
    private float startX;

    protected void Awake()
    {
        startX = transform.position.x;
    }

    protected void Update()
    {
        float y = transform.position.y;
        Debug.Log(-y % 1);
        if (-y % 1 < 0.45f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (MathUtils.difference(startX, transform.position.x) < 3 || transform.position.x > startX)
                {
                    transform.position += Vector3.left;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (MathUtils.difference(startX, transform.position.x) < 3 || transform.position.x < startX)
                {
                    transform.position += Vector3.right;
                }
            }
        }
    }
}
