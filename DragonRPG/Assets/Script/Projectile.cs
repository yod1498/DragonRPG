using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float projectileSpeed = 10f;
	float damageCaused = 10f;

    [SerializeField] GameObject shooter;
    public void SetShooter(GameObject shooter){
        this.shooter = shooter;
    }

	public float DamageCaused {
		get {return damageCaused;}
		set {damageCaused = value;}
	}

	public float ProjectileSpeed {
		get {return projectileSpeed;}
		set {projectileSpeed = value;}
	}

    void OnCollisionEnter(Collision collision){
        var layerCollideWith = collision.gameObject.layer;
        var layerOfShooter = shooter.layer;
        if (layerCollideWith != layerOfShooter)
        {
            Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamagable));
            if (damageableComponent)
            {
                (damageableComponent as IDamagable).TakeDamage(damageCaused);
            }
            Destroy(gameObject, 0.05f);
        }else{
            print("layerCollideWith = " + collision.gameObject.layer + "|  layerOfShooter = " + shooter.layer);
        }

	}
}
