using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] private Vector3[] puntos;
    public Vector3[] Puntos => puntos;

    private bool juegoIniciado;

    public Vector3 PosicionActual { get; set; }

    private void Start()
    {
        PosicionActual = transform.position;
        juegoIniciado = true;
    }

    public Vector3 ObtenerPosicionMovimiento(int index){

        return PosicionActual + puntos[index];
    }

    private void OnDrawGizmos()
    {
        if (juegoIniciado == false && transform.hasChanged)
        {
            PosicionActual = transform.position;
        }

        if (puntos == null || puntos.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < puntos.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(puntos[i] + PosicionActual, 0.5f);
            if (i < puntos.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(puntos[i] + PosicionActual, puntos[i+1] + PosicionActual);
            }
        }
    }
}
