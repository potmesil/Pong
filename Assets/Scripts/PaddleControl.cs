using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleControl : MonoBehaviour
{
    [SerializeField] private float _boundY;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var position = transform.position;
        if (position.y > _boundY)
        {
            position.y = _boundY;
        }
        else if (position.y < -_boundY)
        {
            position.y = -_boundY;
        }
        transform.position = position;
    }

    private void OnMove(InputValue input)
    {
        _rigidbody.velocity = input.Get<Vector2>() * 10;
    }
}