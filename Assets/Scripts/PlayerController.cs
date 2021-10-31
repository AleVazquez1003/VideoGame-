using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//VARIABLES MOVIMIENTO DEL PERSONAJE
	public float jumpForce = 6f;
	Rigidbody2D rigidBody;
	Animator animator;
	public float runningSpeed = 2f;

	const string STATE_ALIVE = "isAlive";
	const string STATE_ON_THE_GROUND = "isOnTheGround";

	public LayerMask groundMask; //detecta los objetos con quienes se puede colisionar

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

    // Start is called before the first frame update
    void Start()
    {
		animator.SetBool(STATE_ALIVE, true);
		animator.SetBool(STATE_ON_THE_GROUND, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Jump();	
		}

		animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

		Debug.DrawRay(this.transform.position, Vector2.down * 1.4f, Color.red);
    }

	void FixedUpdate()
    {
		if(rigidBody.velocity.x < runningSpeed)
        {
			rigidBody.velocity = new Vector2(runningSpeed, //x
											 rigidBody.velocity.y //y
											 );
        }
    }

	void Jump()
	{
		if(IsTouchingTheGround()) //Esto condiciona que solo se puede saltar cuando se està tocando el suelo
		{ 
			rigidBody.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
		}
	}

	//Nos indica si el personaje està o no tocando el suelo
	bool IsTouchingTheGround()
	{
		if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, groundMask))
		{
			//TODO: programar logica de contacto con el suelo 
			animator.enabled = true;
			return true;
		}
        else 
		{
			//TODO: programar logica de no contacto 
			animator.enabled = false;
			return false;
		}
	}
}
