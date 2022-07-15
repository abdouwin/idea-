using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{

public GameObject _ball1;
public GameObject _ball2;
public int _time;
public GameObject _p2turn;
public GameObject _p1turn;

IEnumerator P2turntime(){

    yield return new WaitForSecondsRealtime(1);
    Turn2();

}

public void Turn2(){
    _p1turn.SetActive(false);
    _p2turn.SetActive(true);
}
    private void OnTriggerEnter(Collider other) {
        
if(other.gameObject.CompareTag("Ball")){

other.gameObject.SetActive(false);
_ball1.SetActive(false);
_ball2.SetActive(true);
if(_time == 1){

StartCoroutine("P2turntime");
}

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
