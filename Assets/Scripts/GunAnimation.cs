using UnityEngine;

class GunAnimation
{
    private const string ROTATING_PARAM = "IsRotating";

    private readonly Animator _animator;

    private readonly int _idIsRotating;

    public GunAnimation(Animator animator)
    {
        _animator = animator;

        _idIsRotating = Animator.StringToHash(ROTATING_PARAM);
    }

    public void StopRotating()
    {
        _animator.speed = 0;
    }

    public void Rotating()
    {
        _animator.speed = 1;
    }
}
