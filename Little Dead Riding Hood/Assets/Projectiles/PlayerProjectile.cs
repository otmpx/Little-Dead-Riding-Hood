using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	private Rigidbody2D rb;
	private Vector2 startPos;
	public float expiryDist;
	public float projectileSpeed;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * projectileSpeed;
		startPos = transform.position;
	}
	private void Update()
	{
		if(Vector2.Distance(startPos, transform.position) > expiryDist)
		{
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D collision)
	{

	}
}
