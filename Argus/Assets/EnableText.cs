using UnityEngine;
using System.Collections;

public class EnableText : MonoBehaviour {
    Transform Canvas;
    //float counter = 0;
    bool FirstEnter = true;
    public float Lifetime = 3;
    bool isAlive = true;
    private bool moved = false;

    void Awake()
    {
        Canvas = transform.FindChild("Canvas").transform;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("Player") && FirstEnter)
        {
            FirstEnter = false;
            //Vector3 pos = Canvas.position
            Canvas.Translate(new Vector3(0, 0, -5));
        }
            
    }

    void Update()
    {
        if(FirstEnter == false){
            Lifetime -= Time.deltaTime;
            if (Lifetime < 0)
            {
                isAlive = false;
            }
            if (isAlive == false && moved == false)
            {
                Canvas.parent.gameObject.SetActive(false);
                //Canvas.Translate(new Vector3(0, 0, 5));
                //moved = true;
            }
        }
        
    }
}
/*

        Canvas = transform.FindChild("Canvas").transform;
        Vector3 pos = Canvas.position;
        Lifetime -= Time.deltaTime;
        if (up)
        {
            Canvas.Translate(0, Time.deltaTime * 0.1f, 0);
            counter += Time.deltaTime * 0.1f;
            if (counter > 0.09f)
            {
                up = false;
            }
        }
        else
        {
            Canvas.Translate(0, -Time.deltaTime * 0.1f, 0);
            counter -= Time.deltaTime * 0.1f;
            if (counter < -0.09f)
            {
                up = true;
            }
        } 
 
*/
