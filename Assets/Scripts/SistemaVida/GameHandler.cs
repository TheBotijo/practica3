using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameHandler : MonoBehaviour
{
    
    HealthSystem healthSystem = new HealthSystem(100);
    public Transform pfHealthBar;
    public Animations Damage;
    public LifePotion Health;
    public Collision collision;
    private void Start()
    {
        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(0,10), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("START:"+ healthSystem.GetHealthPercent());
    }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Colision") 
                    {
                        healthSystem.Damage(10);
                        Debug.Log("Damaged:" +healthSystem.GetHealth());
                    }           
    }
        
        
        
    


}
