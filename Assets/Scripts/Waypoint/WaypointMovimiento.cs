using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DireccionMovimiento
{
    Horizontal,
    Vertical
}
public class WaypointMovimiento : MonoBehaviour
{
    [SerializeField] protected float velocidad;

    public Vector3 PuntoPorMoverse => miWaypoint.ObtenerPosicionMovimiento(puntoActualIndex);

    protected Waypoint miWaypoint;
    protected Animator miAnimator;
    protected int puntoActualIndex;
    protected Vector3 ultimaPosicion;


    void Start()
    {
        puntoActualIndex = 0;
        miAnimator = GetComponent<Animator>();
        miWaypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        MoverPersonaje();
        RotarPersonaje();
        RotarVertical();
        if (ComprobarPuntoActualAlcanzado())
        {
            ActualizarIndexMovimiento();
        }
    }

    private void MoverPersonaje()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuntoPorMoverse,
        velocidad * Time.deltaTime);
    }

    private bool ComprobarPuntoActualAlcanzado()
    {
        float distanciaHaciaPuntoActual = (transform.position - PuntoPorMoverse).magnitude;
        if (distanciaHaciaPuntoActual < 0.1f)
        {
            ultimaPosicion = transform.position;
            return true;
        }
        return false;
    }

    private void ActualizarIndexMovimiento()
    {
        if (puntoActualIndex == miWaypoint.Puntos.Length - 1)
        {
            puntoActualIndex = 0;
        }
        else
        {
            if (puntoActualIndex < miWaypoint.Puntos.Length - 1)
            {
                puntoActualIndex ++;
            }
        }
    }

    protected virtual void RotarPersonaje(){
        
    }

    protected virtual void RotarVertical(){

    }
}
