/*cont�m classes e interfaces para manipula��o de cole��es n�o-gen�ricas, como ArrayList, Hashtable, etc. � usada para trabalhar com listas, filas, e outros tipos de cole��es que n�o possuem um tipo espec�fico de dado (n�o gen�ricas).  */
using System.Collections;
/*  vers�o gen�rica de System.Collections e permite a cria��o de cole��es fortemente tipadas. Ela inclui classes como List<T>, Dictionary<TKey, TValue>, Queue<T>, etc. Usar cole��es gen�ricas � mais seguro e eficiente porque elas s�o tipadas, evitando problemas de convers�o de tipos.    */
using System.Collections.Generic;
/*biblioteca principal do Unity e cont�m tudo que � espec�fico do desenvolvimento de jogos, como f�sica, renderiza��o, manipula��o de objetos e componentes do Unity (por exemplo, GameObject, Transform, Rigidbody2D). Ela permite controlar o comportamento de objetos na cena, acessar componentes, entre outros.    */
using UnityEngine;

/*define uma nova classe chamada Movimento que herda de MonoBehaviour. Como herda de MonoBehaviour, ela pode ser anexada a um GameObject, permitindo que o Unity execute e gerencie os m�todos especiais dessa classe, como Update() e FixedUpdate().   */
public class MovimentoPersonagem : MonoBehaviour
{
    private float horizontalInput;  // armazena os valores de entrada do teclado
    private Rigidbody2D rb; // refer�ncia ao objeto RigidBody do personagem


    [SerializeField] private int velocidadeDoPersonagem = 5; // vari�vel para alterar velocidade do personagem
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask chaoLayer;

    // vari�vel verdadeira quando o personagem estiver no ch�o e falsa quando o personagem n�o estiver mais encostando no ch�o
    private bool estaNoChao;

    // constante de for�a de pulo, movida para uma vari�vel para facilitar ajustes
    private float forcaPulo = 600f;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private int runningHash = Animator.StringToHash("running");
    private int jumpinghash = Animator.StringToHash("jumping");

    private void Awake()
    {
        // comando que procura no personagem o componente do tipo RigidBody2D, ao encontrar anexa a vari�vel
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update � chamado uma vez por frame
    void Update() // leituras cont�nuas de entrada devem estar no m�todo Update para evitar perdas e duplica��es 
    {
        horizontalInput = Input.GetAxis("Horizontal");  // captura a esquerda e direita do teclado

        // Verificar se o personagem est� no ch�o
        estaNoChao = Physics2D.OverlapCircle(peDoPersonagem.position, 0.2f, chaoLayer);

        /*Encontrei problemas na hora de pular ent�o fiz o console retornar para verificar se o ch�o estava sendo reconhecido*/

        // Mostrar no console se o personagem est� no ch�o
        Debug.Log("Est� no ch�o: " + estaNoChao);

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao) // S� permite o pulo se a tecla for pressionada e o personagem estiver no ch�o
        {
            // Mostrar no console que o personagem est� tentando pular
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

        private void FixedUpdate() // l�gicas cont�nuas envolvendo a f�sica devem estar no m�todo FixedUpdate
        {
            // Ajuste: modificado o movimento horizontal para manter a velocidade y atual do personagem e aplicamos apenas o movimento horizontal
            rb.velocity = new Vector2(horizontalInput * velocidadeDoPersonagem, rb.velocity.y); // velocidade do personagem
        }
    }