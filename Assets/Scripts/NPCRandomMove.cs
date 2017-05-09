using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCRandomMove : MonoBehaviour {

	public Vector3 m_Position;
	private NavMeshAgent m_NavMesh;

	// Use this for initialization
	void Start () {
		m_NavMesh = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!m_NavMesh.hasPath) {
			m_Position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
		}
		m_NavMesh.SetDestination(m_Position);
	}
}
