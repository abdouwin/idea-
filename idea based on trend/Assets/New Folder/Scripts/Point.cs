using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int _pointball;
  public Game _scriptGame;

   

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Ball")){
  _scriptGame.point +=_pointball ;
  this.gameObject.SetActive(false);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
