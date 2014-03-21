using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour {
	private static PoolManager instance;
	public static PoolManager getInstance() { return instance; }


	/******************
	 * SERIALIZEFIELD *
	 ******************/
	[SerializeField]
	public int countBoids;

	[SerializeField]
	private GameObject boidsObject;
	public GameObject BoidsObject
	{
		get { return boidsObject; }
		set { boidsObject = value; }
	}
	/**********
	 * PUBLIC *
	 **********/
	private static readonly Vector3 SPAWN_LOCATION = new Vector3(0, 0, 0);
	private static readonly Quaternion SPAWN_ROTATION = new Quaternion(0, 0, 0, 0);

	/***********
	 * PRIVATE *
	 ***********/
	private static List<GameObject> genePool;
	

	// Use this for initialization
	void Start () {
		Debug.Log("[BOIDS SCRIPT] Start");
		genePool = new List<GameObject>();
		for (int i = 0; i < countBoids; ++i) {
			Vector3 loc = new Vector3(i*3, 0, 0);
			genePool.Add((GameObject)Instantiate(boidsObject, loc, boidsObject.transform.rotation));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}