using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    public delegate void CollidedWithTerrain();
    public event CollidedWithTerrain OnCollidedWithTerrain = delegate {};
    private Rigidbody2D rigidbody2D;
    private Animation blinkingAnimation;
    public float knockBackStrength = 3;
    public float hitStunDurationInSeconds = 2;
    private bool inHitStun = false;
    private float hitStunTimer = 0;
    [SerializeField] private AudioSource terrainHitSound;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        blinkingAnimation = GetComponent<Animation>();
    }

    void Update()
    {
        var sharedGameState = GameManager.Singleton.sharedGameState;
        if (sharedGameState != null)
        {
            sharedGameState.spaceshipPosition.Value = new Vector2(transform.localPosition.x, transform.localPosition.y);
            sharedGameState.spaceshipRotation.Value = transform.eulerAngles.z;
        }

        if (inHitStun)
        {
            hitStunTimer += Time.fixedDeltaTime;
            if (hitStunTimer >= hitStunDurationInSeconds)
            {
                hitStunTimer = 0;
                inHitStun = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "OverworldTerrain")
        {
            if (inHitStun)
            {
                return;
            }
            var collisionPoint = collision.GetContact(0).point;
            var knockBackDirection = (rigidbody2D.position - collisionPoint).normalized;

            rigidbody2D.AddForce(knockBackDirection * knockBackStrength, ForceMode2D.Impulse);
            blinkingAnimation.Play();
            inHitStun = true;

            terrainHitSound.Play();

            OnCollidedWithTerrain();
        }
    }
}
