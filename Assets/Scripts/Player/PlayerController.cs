using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Subject, IPlayerElement
{
    List<IPlayerElement> visitableComponents = new List<IPlayerElement>();

    // We reference our charactercontroller component
    public CharacterController controller;
    private ObserveHealth observeHealth;
    private PlayerShield playerShield;




    // Player's speed and jump height
    public float speed = 12f;
    private float shift = 10f;
    private bool isRunning = false;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;


    // Player's health and hud
    private float health = 100f;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthbar;


    [Header("Damage Overlay")]
    public Image overlay; // our DamageOverlay GameObject
    public float duration; // how long the image stays fully opaque
    public float fadeSpeed; // how quickly the image will fade

    [Header("Shield Overlay")]
    public Image shieldOverlay;
    public float shieldFadeSpeed;

    private float durationTimer; // timer to check against the duration



    // Checking when player is touching the ground
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        observeHealth = gameObject.GetComponent<ObserveHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);

        visitableComponents.Add(gameObject.GetOrAddComponent<PlayerHealth>());
        visitableComponents.Add(gameObject.GetOrAddComponent<PlayerBoost>());
        visitableComponents.Add(gameObject.GetOrAddComponent<PlayerShield>());
        visitableComponents.Add(gameObject.GetOrAddComponent<PlayerCoin>());
    }

    #region Observer

    private void OnEnable()
    {
        if (observeHealth)
        {
            Attach(observeHealth);
        }
    }

    private void OnDisable()
    {
        if (observeHealth)
        {
            Detach(observeHealth);
        }
    }

    #endregion

    #region Gets
    public float PlayerHealth { get { return health; }  }

    public float OverlayDuration { get { return duration; } }

    public float OverlayFadeSpeed { get { return fadeSpeed; } }

    public float ShieldOverlayFadeSpeed { get { return shieldFadeSpeed; } }

    public float OverlayDurationTimer { get { return durationTimer; } }

    public Image OverlayScreen { get { return overlay; } }

    #endregion

    // Update is called once per frame
    void Update()
    {
        #region Health
        health = Mathf.Clamp(health, 0f, 100f);
        if (Input.GetKeyDown("n"))
        {
            TakeDamage(25);
        }
        if (Input.GetKeyDown("m"))
        {
            RestoreHealth(25);
        }
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // fade the image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }

        #endregion

        #region Gravity
        //Check if Player is on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  //resets velocity.y
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        #endregion

        #region Running
        if (Input.GetKeyDown("left shift") && !isRunning)
        {
            speed += shift;
            isRunning = true;
            Debug.Log("Now Running");
            Debug.Log(speed);
        }
        if (Input.GetKeyUp("left shift") && isRunning)
        {
            speed -= shift;
            isRunning = false;
            Debug.Log("Not Running");
            
        }
        #endregion

    }

    #region HealthUI

    public void UpdateHealthUI()
    {
        frontHealthBar.color = Color.cyan;
        //Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthbar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthbar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthbar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthbar.color = Color.green;
            backHealthbar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthbar.fillAmount, percentComplete);
        }
    }

    #endregion

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        if (playerShield.ShieldState)
        NotifyObservers();
    }

    public void ShieldProtect(float damage)
    {
        if (playerShield.ShieldState)
        {
            playerShield.DestroyShield();
        }
        else
        {
            TakeDamage(damage);
        }
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    public void IncreaseSpeedBoost(float AddSpeed)
    {
        speed += AddSpeed;
    }

    public void DecreaseSpeedBoost(float RemoveSpeed)
    {
        speed -= RemoveSpeed;
    }

    public void Accept(IVisitor visitor)
    {
        foreach (IPlayerElement visitableComponent in visitableComponents)
        {
            visitableComponent.Accept(visitor);
        }
    }
}
