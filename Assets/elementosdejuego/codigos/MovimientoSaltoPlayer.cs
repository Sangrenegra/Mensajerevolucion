using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovimientoSaltoPlayer : MonoBehaviour
{
    [Header("MovimientoHorizontal")]
    public float fuerzaHorizontal;
    float valorHorizontal;
    Animator animator;

    [Header("Vida Personaje")]
    public int vidaTotal;
    public int vidaActual;
    public TMP_Text textoVidaPlayer;

    [Header("Salto")]
    public float fuerzaSalto;
    public float alturaRayo;
    public LayerMask capaPiso;
    Rigidbody2D rigidbody2D;
    Vector2 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaTotal;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        posicionInicial = transform.position;

        if (fuerzaHorizontal <= 0) { fuerzaHorizontal = 1; }
        if (fuerzaSalto <= 0) { fuerzaSalto = 5f; }
        if (alturaRayo <= 0) { alturaRayo = 1.5f; }

        // Inicializar el texto con el valor de la vida actual
        textoVidaPlayer.text = "Vida:" + vidaActual.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // MoviminetoHorizontal
        valorHorizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Correr", Mathf.Abs(valorHorizontal));

        if (valorHorizontal != 0)
        {
            if (valorHorizontal > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (valorHorizontal < 0)
            {
                transform.rotation = Quaternion.Euler(0, -1, 0);
            }
            transform.position += Vector3.right * valorHorizontal * fuerzaHorizontal * Time.deltaTime;
        }

        // MovimientoVertical
        Debug.DrawRay(transform.position, Vector2.down * alturaRayo, Color.red, 0.01f);
        if (Physics2D.Raycast(transform.position, Vector2.down, alturaRayo, capaPiso))
        {
            Debug.Log("Tocando Piso");
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rigidbody2D.velocity = Vector2.up * fuerzaSalto;
                Debug.Log("Salta");
            }
        }

        // Lógica para reducir la vida cuando toca un enemigo
        // Debe ser llamado en otro lugar, cuando corresponda.
    }

    void Vida(int vidaRecibe)
    {
        vidaActual += vidaRecibe;
        textoVidaPlayer.text = "Vida:" + vidaActual.ToString();
        Debug.Log("Cambio de vida: " + vidaRecibe);

        if (vidaActual <= 0)
        {
            vidaActual = vidaTotal; // Reestablece la vida a la cantidad total
            transform.position = posicionInicial;
            textoVidaPlayer.text = "Vida:" + vidaActual.ToString();
        }
    }

    // Método para reducir la vida cuando toca un enemigo
    public void ReducirVidaEnemigo(int cantidadDanio)
    {
        vidaActual -= cantidadDanio;
        Vida(0); // Solo actualiza el texto de la vida, ya que el respawn se maneja en el método Vida()
    }
}
