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
	private int _countBoids;
	public int CountBoids {
		get {
			return _countBoids;
		}
		set {
			_countBoids = value;
		}
	}

    [SerializeField]
    private float _initVelocityFactor;
    public float InitVelocityFactor
    {
        get { return _initVelocityFactor; }
        set { _initVelocityFactor = value; }
    }

	[SerializeField]
	private GameObject _boidsObject;
	public GameObject BoidsObject
	{
		get { return _boidsObject; }
		set { _boidsObject = value; }
	}

	[SerializeField]
	private List<GameObject> _obstacleList;
	public List<GameObject> ObstacleList
	{
        get { return _obstacleList; }
        set { _obstacleList = value; }
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

		//Pool of Boid
		genePool = new List<GameObject>();
		for (int i = 0; i < _countBoids; ++i) {
			Vector3 loc = new Vector3(i*3, 0, 0);
			genePool.Add((GameObject)Instantiate(BoidsObject, loc, BoidsObject.transform.rotation));
			genePool[i].rigidbody.velocity = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100)).normalized * InitVelocityFactor;
            Debug.Log(genePool[i].rigidbody.velocity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}