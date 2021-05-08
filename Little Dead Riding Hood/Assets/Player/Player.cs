using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator anim;
	private bool facingLeft = false;
	private bool animLock = false;
	/*public float slideSpeed;*/
	public float animMultiplier = 1f;
	public float runSpeed;
	#region Bow
	private Transform firepointLeft;
	private Transform firepointRight;
	public GameObject arrowPrefab;
	public float baseArrowSpeed = 30f;
	private float arrowSpeed;
	#endregion
	public enum State
	{
		Idle_Left, Idle_Right,
		Turn_Left, Turn_Right,
		Jump_Left, Jump_Right,
		Duck_Left, Duck_Right,
		/*Slide_Left, Slide_Right,*/

		Axe_Left, Axe_Right,
		Sword_Left, Sword_Right,
		Bow_Left, Bow_Right,

		Hit_Left, Hit_Right,
		Run_Left, Run_Right
	}
	public static State state;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		firepointLeft = GameObject.Find("FirepointLeft").GetComponent<Transform>();
		firepointRight = GameObject.Find("FirepointRight").GetComponent<Transform>();

		state = State.Idle_Right;
	}
	private void Update()
	{
		#region Player Input
		if (!facingLeft && Input.GetKeyDown(KeyCode.LeftArrow))
		{
			facingLeft = true;
			if (!animLock)
			{
				state = State.Turn_Right;
			}
		}
		else if (facingLeft && Input.GetKeyDown(KeyCode.RightArrow))
		{
			facingLeft = false;
			if (!animLock)
			{
				state = State.Turn_Left;
			}
		}
		if (!animLock)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				//Jump
				animLock = true;
				if (facingLeft)
				{
					state = State.Jump_Left;
				}
				else if (!facingLeft)
				{
					state = State.Jump_Right;
				}
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				//Duck
				animLock = true;
				if (facingLeft)
				{
					state = State.Duck_Left;
				}
				else if (!facingLeft)
				{
					state = State.Duck_Right;
				}
			}
			/*if (Input.GetKeyDown(KeyCode.Space))
			{
				animLock = true;
				if (facingLeft)
				{
					state = State.Slide_Left;
				}
				else if (!facingLeft)
				{
					state = State.Slide_Right;
				}
			}*/
			if (Input.GetKeyDown(KeyCode.W))
			{
				//Special ability eg: blanks all enemies, slow time, ignore colour codes
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				//Axe
				animLock = true;
				if (facingLeft)
				{
					state = State.Axe_Left;
				}
				else if (!facingLeft)
				{
					state = State.Axe_Right;
				}
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				//Sword
				animLock = true;
				if (facingLeft)
				{
					state = State.Sword_Left;
				}
				else if (!facingLeft)
				{
					state = State.Sword_Right;
				}
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				//Bow
				animLock = true;
				if (facingLeft)
				{
					state = State.Bow_Left;
				}
				else if (!facingLeft)
				{
					state = State.Bow_Right;
				}
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				//Advance left
				state = State.Run_Left;
				facingLeft = true;
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				//Advance right
				state = State.Run_Right;
				facingLeft = false;
			}
		}
		#endregion
		anim.SetFloat("AnimSpeed", animMultiplier);
		arrowSpeed = baseArrowSpeed * animMultiplier;
		anim.Play(state.ToString());
	}
	private void FixedUpdate()
	{
		switch (state)
		{
			case State.Idle_Left:
				rb.velocity = Vector2.zero;
				break;
			case State.Idle_Right:
				rb.velocity = Vector2.zero;
				break;
			case State.Turn_Left:
				rb.velocity = Vector2.zero;
				break;
			case State.Turn_Right:
				rb.velocity = Vector2.zero;
				break;
			/*case State.Slide_Left:
				rb.velocity = Vector2.left * slideSpeed;
				break;
			case State.Slide_Right:
				rb.velocity = Vector2.right * slideSpeed;
				break;*/
			case State.Run_Left:
				rb.velocity = Vector2.left * runSpeed;
				break;
			case State.Run_Right:
				rb.velocity = Vector2.right * runSpeed;
				break;
		}
	}
	public void AnimResetLeft()
	{
		if (facingLeft)
		{
			state = State.Idle_Left;
		}
		else
		{
			state = State.Turn_Left;
		}
		animLock = false;
	}
	public void AnimResetRight()
	{
		if (!facingLeft)
		{
			state = State.Idle_Right;
		}
		else
		{
			state = State.Turn_Right;
		}
		animLock = false;
	}
	public void Shoot()
	{
		if (state == State.Bow_Left)
		{
			Instantiate(arrowPrefab, firepointLeft.position, firepointLeft.rotation).GetComponent<PlayerProjectile>().projectileSpeed = arrowSpeed;
		}
		else if (state == State.Bow_Right)
		{
			Instantiate(arrowPrefab, firepointRight.position, firepointRight.rotation).GetComponent<PlayerProjectile>().projectileSpeed = arrowSpeed;
		}
	}
}
