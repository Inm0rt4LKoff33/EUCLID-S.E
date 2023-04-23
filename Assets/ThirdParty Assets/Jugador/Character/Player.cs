using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.Text;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header("Control y animaciones")]
    private Animator anim;
	private CharacterController controller;


    [Header("Control de movimiento")]
	float inputX;
	float inputZ;
	bool isMovePressed;

    [SerializeField]
    float walkFactor = 5.0F;

    [SerializeField]
    float rotationFactor = 10.0F;

    [Header("Control de salto")]
	[SerializeField]
	float jumpForce = 5.0F;

	[SerializeField]
	float gravityMultiplier = 8.0F;

	[SerializeField]
	int maximunNumberOfJumps = 1;

    [SerializeField]
	float gravity;
    float velocityGravity;
    int numberOfJumps;
    bool isJump;

    [SerializeField]
    Slider sanityBar;
    float maxSanity = 100;

    // Duración de la invulnerabilidad
    [SerializeField]
    float invulnerabiliySecs;
    // Timer de la invulnerabilidad para volver a la normalidad
    float invulnerabiliyCountdown;

    void Awake () {
		controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
        gravity = Physics.gravity.y;
        sanityBar.value = maxSanity;
    }

    void Update (){

        if (controller.isGrounded)
        {
            anim.SetBool("Jump", false);
        }


		//Detectar movimientos
		handleInputs();
        handleGravity();
        handleMove();
        handleRotation();

        Invulnerability();
    }

    private void Invulnerability()
    {
        if (invulnerabiliyCountdown > 0)
        {
            invulnerabiliyCountdown -= Time.deltaTime;
        }
    }

    void handleInputs()
	{

		inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        isJump = Input.GetButtonDown("Jump");

        //DETECTAR CUANDO SE ESTA MOVIENDO
        isMovePressed = inputX != 0.0F || inputZ != 0.0F;

        animacionesMovimiento();
    }

    //SALTO
    void handleJump()
    {
        if (numberOfJumps > maximunNumberOfJumps)
        {
            return;
        }

        numberOfJumps++;
        velocityGravity = jumpForce / numberOfJumps;
        anim.SetBool("Jump", true);
    }

    void handleGravity()
    {
        bool isGrounded = IsGrounded();

        if (isGrounded)
        {
             if (velocityGravity < -1.0F)
            {
                velocityGravity = -1.0F;
            }

            if (isJump)
            {
                handleJump();
                StartCoroutine(WaitForGroundCorutine());
            }
        }
        else
        {

            if (isJump && maximunNumberOfJumps > 1)
            {
                handleJump();
            }
            velocityGravity += gravity * gravityMultiplier * Time.deltaTime;
        }


    }

    //MOVIMIENTO GENERAL
    void handleMove()
    {
 
        // Obtener la dirección hacia adelante del objeto
        Vector3 forward = transform.forward;

        // Modificar la dirección para que ignore la componente y
        forward.y = 0f;
        forward.Normalize();

        // Multiplicar la dirección hacia adelante por la entrada vertical
        Vector3 moveDirection = forward * inputZ;

        // Ajustar la velocidad según el factor de caminar/correr
        moveDirection *= walkFactor;

        // Ajustar la velocidad hacia abajo por la gravedad
        moveDirection.y = velocityGravity;

        // Mover al personaje utilizando el controlador de personajes
        controller.Move(moveDirection * Time.deltaTime);
    }

    //ROTACION DEL PERSONAJE
    void handleRotation()
    {

        float horizontalInput = Input.GetAxis("Horizontal");

        if (isMovePressed)
        {
            if (!isJump && controller.isGrounded) {

                if (horizontalInput != 0)
                {
                    transform.Rotate(Vector3.up, horizontalInput * rotationFactor * Time.deltaTime);
                }
            }

        }
    }

    //DETECTAR CUANDO SE ESTA EN EL SUELO
    bool IsGrounded()
    {
        return controller.isGrounded;
    }

    IEnumerator WaitForGroundCorutine()
    {

        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(() => IsGrounded());
        numberOfJumps = 0;

    }


    void animacionesMovimiento()
    {

        if (isMovePressed)
        {
            anim.SetInteger("AnimationPar", 1);
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alien"))
        {
            if (invulnerabiliyCountdown <= 0)
            {
                // Quitar sanidad en el slider
                sanityBar.value -= 20;

                invulnerabiliyCountdown = invulnerabiliySecs;
            }
        }
    }
}
