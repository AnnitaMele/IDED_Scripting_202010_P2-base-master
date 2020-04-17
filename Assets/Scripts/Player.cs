using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour, I_Damageable
{
    public Action<int> OnPlayerHit;
    public Action OnPlayerDied;
    public Action<int> OnPlayerScoreC;

    private void Awake()
    {
        instance = this;
    }


    public const int PLAYER_LIVES = 3;

    private const float PLAYER_RADIUS = 0.4F;

    public static Player instance;
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 1F;

    private float hVal;

    #region Bullet

    [Header("Bullet")]
    [SerializeField]
    private Rigidbody bullet;

    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private float bulletSpeed = 3F;

    #endregion Bullet

    #region BoundsReferences

    private float referencePointComponent;
    private float leftCameraBound;
    private float rightCameraBound;

    #endregion BoundsReferences

    #region StatsProperties

    private int Score { get; set; }
    private int Lives { get; set; }

    #endregion StatsProperties

    #region MovementProperties

    public bool ShouldMove
    {
        get =>
            (hVal != 0F && InsideCamera) ||
            (hVal > 0F && ReachedLeftBound) ||
            (hVal < 0F && ReachedRightBound);
    }

    private bool InsideCamera
    {
        get => !ReachedRightBound && !ReachedLeftBound;
    }

    private bool ReachedRightBound { get => referencePointComponent >= rightCameraBound; }
    private bool ReachedLeftBound { get => referencePointComponent <= leftCameraBound; }

    private bool CanShoot { get => bulletSpawnPoint != null && bullet != null; }

    #endregion MovementProperties

    
    

    // Start is called before the first frame update
    private void Start()
    {
        leftCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            0F, 0F, 0F)).x + PLAYER_RADIUS;

        rightCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            1F, 0F, 0F)).x - PLAYER_RADIUS;

        Lives = PLAYER_LIVES;
        OnPlayerHit?.Invoke(Lives);
        OnPlayerScoreC?.Invoke(Score);
        

        

    }

    // Update is called once per frame
    private void Update()
    {
        if (Lives <= 0)
        {
        
            gameObject.SetActive(false);


            return;
        }
      
            FireB();
            MoveP();
                        
        

        
    }
    
    
    public void MoveP()
    {
        hVal = Input.GetAxis("Horizontal");

        if (ShouldMove)
        {
            transform.Translate(transform.right * hVal * moveSpeed * Time.deltaTime);
            referencePointComponent = transform.position.x;
        }
    }

    public void FireB()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                && CanShoot)
        {
            /*Instantiate<Rigidbody>
               (bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation)
               .AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
               */
            GameObject Thing = InterPooling.instance.RequestTT(bullet.gameObject);
            Thing.transform.position = bulletSpawnPoint.position;
            Thing.transform.rotation = bulletSpawnPoint.rotation;
            Thing.GetComponent<Rigidbody>().velocity = transform.up * bulletSpeed;
        }
    }

    public void DamagePlayer()
    {
        Lives -= 1;
        OnPlayerHit?.Invoke(Lives);
        if (Lives <= 0) OnPlayerDied?.Invoke();
    }
    public void ScoreP(int _score)
    {
        Score += _score;
        OnPlayerScoreC?.Invoke(Score);
    }
}

