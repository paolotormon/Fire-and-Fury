using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth = 3.0f;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration = 1;
    [SerializeField] private int numberOfFlashes = 3;
    private SpriteRenderer spriteRend;

    private bool invulnerable;

    [Header("Death")]
    [SerializeField] private int levelToLoad = 5;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void LoadLevel()
    {
        if (levelToLoad == 0)
        {
            ScoreManager.Instance.ResetScore();
        }
        SceneManager.LoadScene(levelToLoad);
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                dead = true;

                if (GetComponent<PlayerMovement>() != null)
                {
                    //Player
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    StartCoroutine(PlayerDeath());
                }
                else if (GetComponentInParent<EnemyPatrol>())
                {
                    //Enemy
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                    GetComponent<MeleeEnemy>().enabled = false;
                    Destroy(this.gameObject, 1.0f);
                }
            }
        }
    }

    public IEnumerator PlayerDeath()
    {
        //Destroy(this.gameObject, 1.5f);
        yield return new WaitForSeconds(1.5f);
        LoadLevel();
    }

    public void AddHealth(float _value)
    {
        if (!dead)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        }
    }
    //For testing purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        //invulnerablity duration
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes / 2);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes / 2);
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
}
