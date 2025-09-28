using UnityEngine;

public class Villano : MonoBehaviour
{
    [Header("Movimiento")]
    public Transform jugador;           // Asignar jugador en inspector
    public float velocidad = 2f;
    public float distanciaAtaque = 1.5f;
    public float tiempoEntreAtaques = 2f;
    private float tiempoUltimoAtaque;

    [Header("Vida")]
    public float vida = 100f;

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia > distanciaAtaque)
        {
            Vector3 direccion = (jugador.position - transform.position).normalized;
            transform.position += direccion * velocidad * Time.deltaTime;
        }
        else
        {
            if (Time.time - tiempoUltimoAtaque > tiempoEntreAtaques)
            {
                Atacar();
                tiempoUltimoAtaque = Time.time;
            }
        }
    }

    private void Atacar()
    {
        Debug.Log("El villano ataca al jugador!");
        // Aquí puedes restar vida al jugador si tiene script de vida
        // Ejemplo: jugador.GetComponent<PlayerAttackController>().RecibirDanio(1f);
    }

    public void RecibirDanio(float cantidad)
    {
        vida -= cantidad;
        Debug.Log($"Villano recibió {cantidad} de daño. Vida restante: {vida}");

        if (vida <= 0f) Morir();
    }

    private void Morir()
    {
        Debug.Log("El villano ha sido derrotado!");
        Destroy(gameObject);
    }
}
