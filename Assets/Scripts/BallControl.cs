using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            _rigidbody.velocity = GetNormalizedVelocity(_rigidbody.velocity.x, (_rigidbody.velocity.y / 2) + (collision.rigidbody.velocity.y / 3));
        }

        if (collision.gameObject.CompareTag("SideWall"))
        {
            GameManager.Shared.AddScore(collision.gameObject.name);
        }
    }

    public void Init(float idleTime)
    {
        Invoke(nameof(GoBall), idleTime);
    }

    private void GoBall()
    {
        _rigidbody.velocity = GetNormalizedVelocity(Random.Range(0, 2) == 0 ? -1 : 1, Random.Range(-1f, 1f));
    }

    private Vector2 GetNormalizedVelocity(float x, float y) => new Vector2(x, y).normalized * _speed;
}