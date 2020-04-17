using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour
{
    private const float TIME_TO_DESTROY = 10F;

    [SerializeField]
    private int maxHP = 1;

    private int currentHP;

    [SerializeField]
    private int scoreAdd = 10;

    private void Start()
    {
        currentHP = maxHP;
        Destroy(gameObject, TIME_TO_DESTROY);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int collidedObjectLayer = collision.gameObject.layer;
        if (collidedObjectLayer.Equals(Utils.BulletLayer))
        {
            //Destroy(collision.gameObject);

            TargetDamage();


        }
        else if (collidedObjectLayer.Equals(Utils.PlayerLayer) ||
            collidedObjectLayer.Equals(Utils.KillVolumeLayer))
        {

        }


        if (collidedObjectLayer.Equals(Utils.PlayerLayer) ||
            collidedObjectLayer.Equals(Utils.KillVolumeLayer))
        {
            Player.instance.DamagePlayer();

        }

    }

    public void TargetDamage()
    {
        currentHP -= 1;
        if (currentHP <= 0)
        {
            Player.instance.ScoreP(scoreAdd);

        }

    }
}