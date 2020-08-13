using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//simple attack
public class BRAttack : MonoBehaviour
{
    [SerializeField]
    private float attackTime = 3f;//Attack Time (in seconds)

    [SerializeField]
    private float attackDamage = 100f;

    //Raw gameobject that is excempt from damage
    private GameObject creatingObject;
    //Property for creator, makes sure we can only set a player
    public BRPlayer Creator {
        get { return creatingObject.GetComponent<BRPlayer>(); }
        set { creatingObject = (creatingObject == null ? value.gameObject : creatingObject); } //Only allow change if a creator is not set or was deleted
    }

    private void Start()
    {
        Destroy(this.gameObject, attackTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Don't damage our creator
        if(other.gameObject == creatingObject)
        {
            return;
        }


        //Only try to damage players
        BRPlayer player;
        if(other.TryGetComponent(out player))
        {
            player.Damage(attackDamage);
        }
    }
}
