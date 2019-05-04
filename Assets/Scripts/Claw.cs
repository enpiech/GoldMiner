using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour {

    [SerializeField]
    private Transform _origin;
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private Gun gun;
    [SerializeField]
    private ScoreManager scoreManager;

    private Vector2 _target;
    private int jewelValue = 100;
    private GameObject childObject;
    private LineRenderer _lineRenderer;

    private bool _hitJewel;
    private bool _isRestracting;
    private bool _isShooting;
    private int _mass = 0;

    // Use this for initialization
    void Awake () {
        Init();
	}

    void Init()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        float step = (_speed - _mass) * Time.deltaTime;

        //TODO: Push claw using addForce, then make a curve linerenderer follow it

        transform.position = Vector2.MoveTowards (transform.position, _target, step);

        _lineRenderer.SetPosition(0, _origin.position);
        _lineRenderer.SetPosition(1, transform.position);
        _lineRenderer.widthMultiplier = 0.1f;

        if (transform.position == _origin.position && _isRestracting)
        {
            gun.CollectedObject();

            if (_hitJewel)
            {
                scoreManager.AddPoint(jewelValue);
                _hitJewel = false;

                _mass = 0;
                _hitJewel = false;
                Destroy(childObject);
            }
               
            gameObject.SetActive(false);
        }
	}

    public void SetTarget(Vector2 target)
    {
        _target = target;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _isRestracting = true;
        _target = _origin.position;
         
        if (other.gameObject.CompareTag("Loot"))
        {
            _hitJewel = true;
            LootBehavior loot = other.transform.GetComponent<LootBehavior>();
            _mass = loot.Weight;
            jewelValue = loot.Value;

            childObject = other.gameObject;
            other.transform.SetParent(this.transform);
        }
    }

    public void SpawnClaw()
    {
        this.gameObject.SetActive(true);
    }

    public void DestroyClaw()
    {
        this.gameObject.SetActive(false);
    }

}
