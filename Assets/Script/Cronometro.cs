using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cronometro : MonoBehaviour
{
    private float tiempoInicio; // Tiempo en el que se inició el cronómetro
    private bool cronometroActivo; // Indica si el cronómetro está activo

    // Método para iniciar el cronómetro
    public void IniciarCronometro()
    {
        tiempoInicio = Time.time; // Guardar el tiempo actual
        cronometroActivo = true; // Activar el cronómetro
    }

    // Método para detener el cronómetro
    public void DetenerCronometro()
    {
        cronometroActivo = false; // Desactivar el cronómetro
    }

    // Método para reiniciar el cronómetro
    public void ReiniciarCronometro()
    {
        tiempoInicio = Time.time; // Reiniciar el tiempo de inicio
    }

    private void Update()
    {
        if (cronometroActivo)
        {
            float tiempoTranscurrido = Time.time - tiempoInicio; // Calcular el tiempo transcurrido

            // Formatear el tiempo en minutos, segundos y milisegundos
            int minutos = (int)(tiempoTranscurrido / 60);
            int segundos = (int)(tiempoTranscurrido % 60);
            float milisegundos = (tiempoTranscurrido % 1) * 1000;

            // Imprimir el tiempo en la consola
            Debug.LogFormat("{0:00}:{1:00}:{2:000}", minutos, segundos, milisegundos);
        }
    }
}
