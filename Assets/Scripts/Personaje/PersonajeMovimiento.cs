using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    //Creamos una variable privada de tipo float, a la cual llamamos "velocidad" y le ponemos el atributo SerializeField para poder asignarla en el inspector.
    [SerializeField] private float velocidad;

    //Creamos una propiedad de tipo bool llamada "enMovimiento" la cual nos devuelve true si el personaje se esta moviendo.
    public bool enMovimiento => direccionMovimiento.magnitude > 0f; 
    //Creamos una propiedad de tipo Vector2 la cual se llama "DireccionMovimiento" y esta nos devuelve el valor de la variable direccionMovimiento.
    public Vector2 DireccionMovimiento => direccionMovimiento;
    
    //Creamos una variable privada de tipo RigidBody2D la cual llamamos "miRigidBody2D"
    private Rigidbody2D miRigidBody2D;
    //Creamos una variable privada de tipo Vector2 la cual llamamos "direccionMovimiento"
    private Vector2 direccionMovimiento;
    private Vector2 input;

    private void Awake() {
        
        miRigidBody2D = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(x:Input.GetAxisRaw("Horizontal"), y:Input.GetAxisRaw("Vertical"));

        // X
        if(input.x > 0.1f){

            direccionMovimiento.x = 1f;
        }else if(input.x < 0f){

            direccionMovimiento.x = -1f;

        }else{

            direccionMovimiento.x = 0f;

        }

        // Y

        if(input.y > 0.1f){

            direccionMovimiento.y = 1f;
        }else if(input.y < 0f){

            direccionMovimiento.y = -1f;

        }else{

            direccionMovimiento.y = 0f;

        }
        
    }

    private void FixedUpdate() {
        
        miRigidBody2D.MovePosition(miRigidBody2D.position + direccionMovimiento * velocidad * Time.deltaTime);

    }

}
