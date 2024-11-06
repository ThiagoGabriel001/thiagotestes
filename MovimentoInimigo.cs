/*contém classes e interfaces para manipulação de coleções não-genéricas, como ArrayList, Hashtable, etc. É usada para trabalhar com listas, filas, e outros tipos de coleções que não possuem um tipo específico de dado (não genéricas).  */
using System.Collections;
/*versão genérica de System.Collections e permite a criação de coleções fortemente tipadas. Ela inclui classes como List<T>, Dictionary<TKey, TValue>, Queue<T>, etc. Usar coleções genéricas é mais seguro e eficiente porque elas são tipadas, evitando problemas de conversão de tipos.*/
using System.Collections.Generic;
/*biblioteca principal do Unity e contém tudo que é específico do desenvolvimento de jogos, como física, renderização, manipulação de objetos e componentes do Unity (por exemplo, GameObject, Transform, Rigidbody2D). Ela permite controlar o comportamento de objetos na cena, acessar componentes, entre outros.*/
using UnityEngine;

public class MovimentoInimigo : MonoBehaviour
{
    private Transform posicaoDoJogador;
    [SerializeField] private int velocidadeDoInimigo = 4;
    [SerializeField] private float distanciaPara;
    public Animator animator;
    public bool ladodireito = true; // true = personagem está virado para a direita
    [SerializeField] private float forcaDoPulo = 10f;
    [SerializeField] private Transform detectorDeObstaculo;
    [SerializeField] private LayerMask layerObstaculo;

    private Rigidbody2D rb;

    void Start()
    {
        posicaoDoJogador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, posicaoDoJogador.position) > distanciaPara)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(posicaoDoJogador.position.x, transform.position.y),
                velocidadeDoInimigo * Time.deltaTime
            );
        }

        if ((transform.position.x - posicaoDoJogador.position.x) < 0 && ladodireito)
        {
            Virar();
        }

        if ((transform.position.x - posicaoDoJogador.position.x) > 0 && !ladodireito)
        {
            Virar();
        }

        DetectarObstaculoEAplciarPulo();
    }

    private void DetectarObstaculoEAplciarPulo()
    {
        // Define a direção do Raycast com base na direção do inimigo
        Vector2 direcaoRaycast = ladodireito ? Vector2.right : Vector2.left;

        // Lança o Raycast na direção correta
        RaycastHit2D hitInfo = Physics2D.Raycast(detectorDeObstaculo.position, direcaoRaycast, 1.0f, layerObstaculo);

        if (hitInfo.collider != null)
        {
            rb.AddForce(Vector2.up * forcaDoPulo, ForceMode2D.Impulse);
        }
    }

    void Virar()
    {
        ladodireito = !ladodireito;

        Vector3 novoScale = transform.localScale;
        novoScale.x *= -1;
        transform.localScale = novoScale;
    }
}