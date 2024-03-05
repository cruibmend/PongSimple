using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour {

  //Velocidad
  [SerializeField] private float velocidad = 30.0f;

  //Contadores de goles
  public int golesIzquierda = 0;
  public int golesDerecha = 0;

  //Gol doble
  public bool doble = false;

  //Toques
  public int toques = 1;

  //Cajas de texto de los contadores
  public TMPro.TextMeshProUGUI ContadorIzquierdo, ContadorDerecho, Ronda;

  //Se ejecuta al arrancar
  void Start () {
    float inicio = Random.Range(0, 2);
    //Velocidad inicial hacia una dirección aleatoria
    if(inicio == 0){
      GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
    }else{
      GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
    }
  }

  void FixedUpdate(){
    float random = Random.Range(1, 6);
    switch (random)
    {
      case 1:
        transform.GetComponent<Renderer>().material.color = Color.red;
      break;
      case 2:
        transform.GetComponent<Renderer>().material.color = Color.cyan;
      break;
      case 3:
        transform.GetComponent<Renderer>().material.color = Color.green;
      break;
      case 4:
        transform.GetComponent<Renderer>().material.color = Color.magenta;
      break;
      case 5:
        transform.GetComponent<Renderer>().material.color = Color.yellow;
      break;
      default:
        transform.GetComponent<Renderer>().material.color = Color.white;
      break;
    }
  }
  
//Se ejecuta al colisionar
void OnCollisionEnter2D(Collision2D micolision){

  //transform.position es la posición de la bola
  //micolision contiene toda la información de la colisión
  //Si la bola colisiona con la raqueta:
  //micolision.gameObject es la raqueta
  //micolision.transform.position es la posición de la raqueta

  //Vector2 pos = new Vector2 (Random.Range (-10, 20), Random.Range (-5, 5));

  //Instantiate (GetComponent<Rigidbody2D>(), pos, transform.rotation);

  //Si choca con la raqueta izquierda
  if (micolision.gameObject.name == "RaquetaIzq"){

    //Valor de x
    int x = 1;

    //Aumento la variable de toques
    toques++;

    //Valor de y
    int y = direccionY(transform.position, micolision.transform.position);

    //Vector de dirección
    Vector2 direccion = new Vector2(x, y);

    //Aplico velocidad y modifico por número de toques
    GetComponent<Rigidbody2D>().velocity = direccion * (velocidad + (velocidad * (toques / 4)));

  }

  //Si choca con la raqueta derecha
  if (micolision.gameObject.name == "RaquetaDer"){

    //Valor de x
    int x = -1;

    //Aumento la variable de toques
    toques++;

    //Valor de y
    int y = direccionY(transform.position, micolision.transform.position);

    //Vector de dirección
    Vector2 direccion = new Vector2(x, y);

    //Aplico velocidad
    GetComponent<Rigidbody2D>().velocity = direccion * (velocidad + (velocidad * (toques / 4)));

  }
}

//Método para calcular la direccion de Y (deevuelve un número entero int)
int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta){

  if (posicionBola.y > posicionRaqueta.y){
    return 1; //Si choca por la parte superior de la raqueta, sale hacia arriba
  }
  else if (posicionBola.y < posicionRaqueta.y){
    return -1; //Si choca por la parte inferior de la raqueta, sale hacia abajo
  }
  else{
    return 0; //Si choca por la parte central de la raqueta, sale en horizontal
  }
}

//Reinicio la posición de la bola
public void reiniciarBola(string direccion){

  //Buscamos el GameObject de las raquetas
  var RaquetaIzq = GameObject.Find("RaquetaIzq");
  var RaquetaDer = GameObject.Find("RaquetaDer");

  //Posición 0 de la bola
  transform.position = Vector2.zero;

  //Reinicio los toques
  toques = 0;

  //Velocidad inicial de la bola
  velocidad = 30;
  
  if (direccion == "Derecha"){
    //Incremento goles al de la derecha
    if(doble){
      golesDerecha += 2;
      doble = false;
    }else{
      golesDerecha++;
    }
    //Lo escribo en el marcador
    ContadorDerecho.text = golesDerecha.ToString();
  }
  else if (direccion == "Izquierda"){
    //Incremento goles al de la izquierda
    if(doble){
      golesIzquierda += 2;
      doble = false;
    }else{
      golesIzquierda++;
    }
    //Lo escribo en el marcador
    ContadorIzquierdo.text = golesIzquierda.ToString();
  }

  //MODOS DE JUEGO
  //1. Normal
  //2. Bola rápida
  //3. Palas pequeñas
  //4. Bola Grande
  //5. SlowMotion (Tengo que ver como ajustar la velocidad de las palas)

  float random = Random.Range(1, 6);
  switch (random)
  {
    case 1:
      Ronda.text = "Ronda Normal";
      //Tamaño normal de las raquetas
      Vector3 size = new Vector3(15, 1, 1);
      RaquetaIzq.transform.localScale = size;
      RaquetaDer.transform.localScale = size;

      //Tamaño normal de la bola
      size = new Vector3(2, 2, 1);
      transform.localScale = size;
    break;
    case 2:
      Ronda.text = "Bola Rápida";
      velocidad = 80;
      //Tamaño normal de las raquetas
      size = new Vector3(15, 1, 1);
      RaquetaIzq.transform.localScale = size;
      RaquetaDer.transform.localScale = size;
      //Tamaño normal de la bola
      size = new Vector3(2, 2, 1);
      transform.localScale = size;
    break;
    case 3:
      Ronda.text = "Palas Pequeñas";
      size = new Vector3(3, 1, 1);
      //Tamaño normal de las raquetas
      RaquetaIzq.transform.localScale = size;
      RaquetaDer.transform.localScale = size;
      //Tamaño normal de la bola
      size = new Vector3(2, 2, 1);
      transform.localScale = size;
    break;
    case 4:
      Ronda.text = "Bola Grande";
      size = new Vector3(15, 1, 1);
      //Tamaño normal de las raquetas
      RaquetaIzq.transform.localScale = size;
      RaquetaDer.transform.localScale = size;
      //Tamaño normal de la bola
      size = new Vector3(10, 10, 1);
      transform.localScale = size;
    break;
    case 5:
      Ronda.text = "Gol Doble";
      size = new Vector3(15, 1, 1);
      //Tamaño normal de las raquetas
      RaquetaIzq.transform.localScale = size;
      RaquetaDer.transform.localScale = size;
      //Tamaño normal de la bola
      size = new Vector3(2, 2, 1);
      transform.localScale = size;
      doble = true;
    break;/*
    case 5:
      Ronda.text = "Slow Motion";
      velocidad = 10;
      size = new Vector3(15, 1, 1);
      transform.localScale = size;
      //Tamaño normal de las raquetas
      RaquetaIzq.transform.localScale = size;
      RaquetaDer.transform.localScale = size;
      //Tamaño normal de la bola
      size = new Vector3(2, 2, 1);
      transform.localScale = size;
    break;*/
  }

  //Velocidad y dirección
  if (direccion == "Derecha"){
    //Reinicio la bola
    GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
    //Vector2.right es lo mismo que new Vector2(1,0)
  }
  else if (direccion == "Izquierda"){
    //Reinicio la bola
    GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
    //Vector2.left es lo mismo que new Vector2(-1,0)
  }
}
}
