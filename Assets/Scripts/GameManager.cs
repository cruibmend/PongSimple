using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject palaIA;
    public GameObject bola;

    public float velocidad = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        palaIA = GameObject.Find("/Raquetas/RaquetaDer");
        bola = GameObject.Find("Bola");
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical");
        
        if(bola.transform.position.y < palaIA.transform.position.y){
            palaIA.transform.Translate(Vector2.left);
            //palaIA.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocidad);
        }else if(bola.transform.position.y > palaIA.transform.position.y){
            palaIA.transform.Translate(Vector2.right);
            //palaIA.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -(velocidad));
        }else{
            palaIA.transform.Translate(Vector2.zero);
           //palaIA.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
