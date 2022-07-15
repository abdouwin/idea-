using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public FlatBallLauncher flatBallLauncher;

    public int TargetIndex;

    public Rigidbody rb;

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            flatBallLauncher.StopCalc = true;

            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;

            //flatBallLauncher.target = flatBallLauncher.TargetsList[TargetIndex];

            //TargetIndex++;

            Debug.Log("YES");
        }
    }
}
