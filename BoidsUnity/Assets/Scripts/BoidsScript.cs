using UnityEngine;
using System.Collections;

public class BoidsScript : MonoBehaviour
{
    [SerializeField]
    private float _timeBetweenMoves;
    public float TimeBetweenMoves
    {
        get { return _timeBetweenMoves; }
        set { _timeBetweenMoves = value; }
    }

    [SerializeField]
    private float _timeBetweenOverlapShere;
    public float TimeBetweenOverlapShere
    {
        get { return _timeBetweenOverlapShere; }
        set { _timeBetweenOverlapShere = value; }
    }

    [SerializeField]
    private float _visibilityRadius;
    public float VisibilityRadius
    {
        get { return _visibilityRadius; }
        set { _visibilityRadius = value; }
    }

    [SerializeField]
    private float _cohesionFactor;
    public float CohesionFactor
    {
        get { return _cohesionFactor; }
        set { _cohesionFactor = value; }
    }

    [SerializeField]
    private float _distanceMin;
    public float DistanceMin
    {
        get { return _distanceMin; }
        set { _distanceMin = value; }
    }

    [SerializeField]
    private float _aligmentFactor;
    public float AligmentFactor
    {
        get { return _aligmentFactor; }
        set { _aligmentFactor = value; }
    }

    private Transform _transform;
    public Rigidbody Rigidbd;
    public Collider Collid;

    private float _lastOverlayTime = 0.0f;
    private float _lastMoveTime = 0.0f;
                     
    private Collider[] _proximColliders;

    // Use this for initialization
	void Start ()
	{
	    _transform = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(Time.time - _lastOverlayTime >= TimeBetweenOverlapShere)
        {
            _lastOverlayTime = Time.time;
            _proximColliders = Physics.OverlapSphere(_transform.position, VisibilityRadius);
        }

        if (Time.time - _lastMoveTime >= TimeBetweenMoves)
        {
            _lastMoveTime = Time.time;
            MoveBoids();
        }
	}

    private void MoveBoids()
    {
        //Debug.Log("[MoveBoids] Runs");

        var curBoidPos = _transform.position;

        // Init Barycentre
        Vector3 cohesionVector = Vector3.zero;

        // Init Separation
        Vector3 separationVector = Vector3.zero;

        // Init Alignment
        Vector3 alignmentVector = Vector3.zero;

        if (_proximColliders == null)
            return;
        foreach (var col in _proximColliders)
        {
            var proximPos = col.transform.position;
            if (col != Collid)
            {
                // COHESION
                cohesionVector += proximPos;

                // SEPARATION
                if (Vector3.Distance(proximPos, curBoidPos) < DistanceMin)
                    separationVector -= (proximPos - curBoidPos);
           
                // ALIGNMENT
                alignmentVector += Rigidbd.velocity;
            }
        }
        cohesionVector /= (_proximColliders.Length - 1);
        cohesionVector = (cohesionVector - curBoidPos)/CohesionFactor;

        alignmentVector /= (_proximColliders.Length - 1);
        alignmentVector = (alignmentVector - Rigidbd.velocity)/ AligmentFactor;

        Vector3 vectorVelocity = Rigidbd.velocity + cohesionVector + separationVector + alignmentVector;
        _transform.rotation = Quaternion.LookRotation(vectorVelocity);
        Rigidbd.velocity = transform.forward* (2 * vectorVelocity.magnitude);
    }
}
