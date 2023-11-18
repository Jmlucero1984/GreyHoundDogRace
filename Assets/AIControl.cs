using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIControl : MonoBehaviour {
	[Header("Variables de Animación")]
	public int numeroDePartida;
	public Animator animator;
	public float resistenciaViento=0;
	public string variableMovimiento;
	public string variableMultiplier;
	public GameObject liebre;
	public GameObject front;
	public GameObject right;
	public GameObject left;
	public int currentWP = 0;
	public float inheritedSpeed=16.0f;
	public float baseSpeed=16.0f;

	public NavMeshAgent agent;
	Vector3 posAnterior;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent>();
		posAnterior = transform.position; 
	}
	private void Update()
	{	agent.speed=baseSpeed-resistenciaViento;
		Vector3 posActual = transform.position;
		float vel = (posActual - posAnterior).magnitude / Time.deltaTime;
		posAnterior = transform.position;
		if(front==null){
			resistenciaViento=1;
		} else {
			resistenciaViento=0;
		}
		//float agentVel = GetComponent<Rigidbody>().velocity.magnitude;
		var velocidadActual=agent.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
		var randomVelocity = UnityEngine.Random.Range(-0.1f, 0.1f);
        velocidadActual += randomVelocity;
        if (velocidadActual < baseSpeed-1.5f) velocidadActual = baseSpeed-1.5f;
        if (velocidadActual > baseSpeed+0.5f) velocidadActual = baseSpeed+0.5f;
		agent.GetComponent<UnityEngine.AI.NavMeshAgent>().speed=velocidadActual;



		//Debug.Log("La velocidad del galgo es: " + vel);
		animator.SetFloat(variableMovimiento, vel * 4);
	 
		animator.SetFloat(variableMultiplier, 2 - ((20 - vel) / 20));
		

	   var liebrePos = liebre.transform.position;
		
		var hacialiebreDir = liebrePos - transform.position;
		 
	}
 
}
