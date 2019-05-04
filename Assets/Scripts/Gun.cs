using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{ 
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    public Claw _claw;

    private Transform _transform;
    private GunAnimation _animation;

    private bool _isShooting;

    void Awake()
    {
        init();
    }

    void init()
    {
        this._transform = transform;
        this._animation = new GunAnimation(_animator);
        _isShooting = false;
    }

    void Update()
    {
        handleInput();
    }

    void handleInput()
    {
        if (Input.GetButton("Fire1"))
        {
            launchClaw();
        }
        else if (Input.GetButton("Fire1"))
        {
            retractClaw();   
        }
    }

    void launchClaw()
    {
        if (_isShooting) return;

        _isShooting = true;

        _animation.StopRotating();

        Vector2 down = transform.TransformDirection(Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, down);

        if (hit.collider != null)
        {
            _claw.SpawnClaw();
            _claw.SetTarget(hit.point);
        }
    }

    void retractClaw()
    {
        if (!_isShooting) return;

        _claw.SetTarget(transform.position);
    }

    public void CollectedObject()
    {
        _isShooting = false;
        _animation.Rotating();
    }
}