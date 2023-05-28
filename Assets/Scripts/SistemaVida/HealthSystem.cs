using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour 
{
    public float health;
    public float healthMax = 100;


   public HealthSystem(float healthMax) {
        this.health = healthMax;
        health=healthMax;
   }
   public float GetHealth(){
        return health;
   }
   public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }
   public void Damage (float damageAmount) {
        //Debug.Log("Entra");
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
        }
   }
   public void Heal(float healAmount){
        //Debug.Log("Entra2");
        health += healAmount;
        if (health > healthMax)
        {
            health = healthMax;
        }
   }
}
