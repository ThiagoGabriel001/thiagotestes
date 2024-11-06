/*contém classes e interfaces para manipulação de coleções não-genéricas, como ArrayList, Hashtable, etc. É usada para trabalhar com listas, filas, e outros tipos de coleções que não possuem um tipo específico de dado (não genéricas).  */
using System.Collections;
/*  versão genérica de System.Collections e permite a criação de coleções fortemente tipadas. Ela inclui classes como List<T>, Dictionary<TKey, TValue>, Queue<T>, etc. Usar coleções genéricas é mais seguro e eficiente porque elas são tipadas, evitando problemas de conversão de tipos.    */
using System.Collections.Generic;
/*biblioteca principal do Unity e contém tudo que é específico do desenvolvimento de jogos, como física, renderização, manipulação de objetos e componentes do Unity (por exemplo, GameObject, Transform, Rigidbody2D). Ela permite controlar o comportamento de objetos na cena, acessar componentes, entre outros.    */
using UnityEngine;

/*define uma nova classe chamada Movimento que herda de MonoBehaviour. Como herda de MonoBehaviour, ela pode ser anexada a um GameObject, permitindo que o Unity execute e gerencie os métodos especiais dessa classe, como Update() e FixedUpdate().   */
public class MovimentoPersonagem : MonoBehaviour
{
    private float horizontalInput;  // armazena os valores de entrada do teclado
    private Rigidbody2D rb; // referência ao objeto RigidBody do personagem


    [SerializeField] private int velocidadeDoPersonagem = 5; // variável para alterar velocidade do personagem
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask chaoLayer;

    // variável verdadeira quando o personagem estiver no chão e falsa quando o personagem não estiver mais encostando no chão
    private bool estaNoChao;

    // constante de força de pulo, movida para uma variável para facilitar ajustes
    private float forcaPulo = 600f;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private int runningHash = Animator.StringToHash("running");
    private int jumpinghash = Animator.StringToHash("jumping");

    private void Awake()
    {
        // comando que procura no personagem o componente do tipo RigidBody2D, ao encontrar anexa a variável
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update é chamado uma vez por frame
    void Update() // leituras contínuas de entrada devem estar no método Update para evitar perdas e duplicações 
    {
        horizontalInput = Input.GetAxis("Horizontal");  // captura a esquerda e direita do teclado

        // Verificar se o personagem está no chão
        estaNoChao = Physics2D.OverlapCircle(peDoPersonagem.position, 0.2f, chaoLayer);

        /*Encontrei problemas na hora de pular então fiz o console retornar para verificar se o chão estava sendo reconhecido*/

        // Mostrar no console se o personagem está no chão
        Debug.Log("Está no chão: " + estaNoChao);

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao) // Só permite o pulo se a tecla for pressionada e o personagem estiver no chão
        {
            // Mostrar no console que o personagem está tentando pular
            Debug.Log("Pulo acionado");

            rb.AddForce(Vector2.up * forcaPulo);
        }

        animator.SetBool(runningHash, horizontalInput != 0);
        animator.SetBool(jumpinghash, !estaNoChao);

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        } else if (horizontalInput < 0) {
            spriteRenderer.flipX = true;
        }
    }

        private void FixedUpdate() // lógicas contínuas envolvendo a física devem estar no método FixedUpdate
        {
            // Ajuste: modificado o movimento horizontal para manter a velocidade y atual do personagem e aplicamos apenas o movimento horizontal
            rb.velocity = new Vector2(horizontalInput * velocidadeDoPersonagem, rb.velocity.y); // velocidade do personagem
        }
    }