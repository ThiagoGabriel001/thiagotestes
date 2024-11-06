/*cont�m classes e interfaces para manipula��o de cole��es n�o-gen�ricas, como ArrayList, Hashtable, etc. � usada para trabalhar com listas, filas, e outros tipos de cole��es que n�o possuem um tipo espec�fico de dado (n�o gen�ricas).  */
using System.Collections;
/*vers�o gen�rica de System.Collections e permite a cria��o de cole��es fortemente tipadas. Ela inclui classes como List<T>, Dictionary<TKey, TValue>, Queue<T>, etc. Usar cole��es gen�ricas � mais seguro e eficiente porque elas s�o tipadas, evitando problemas de convers�o de tipos.*/
using System.Collections.Generic;
/*biblioteca principal do Unity e cont�m tudo que � espec�fico do desenvolvimento de jogos, como f�sica, renderiza��o, manipula��o de objetos e componentes do Unity (por exemplo, GameObject, Transform, Rigidbody2D). Ela permite controlar o comportamento de objetos na cena, acessar componentes, entre outros.*/
using UnityEngine;

public class MovimentoInimigo : MonoBehaviour
{
    private Transform posicaoDoJogador;
    [SerializeField] private int velocidadeDoInimigo = 4;
    [SerializeField] private float distanciaPara;
    public Animator animator;
    public bool ladodireito = true; // true = personagem est� virado para a direita
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
        // Define a dire��o do Raycast com base na dire��o do inimigo
        Vector2 direcaoRaycast = ladodireito ? Vector2.right : Vector2.left;

        // Lan�a o Raycast na dire��o correta
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