using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {
    public float force = 10.0f;
    public float radius = 5.0f;
    public float upwardsModifier = 3.0f;
    public GameObject explosionEffect;

    public float alarmWaitForSeconds = 3.0f;
    public BoxCollider alarmArea;

    public int explosionDamage = 70;
    public int pollution = 20;

    public AudioClip explodeClip;

    private Rigidbody rb;
    private bool exploded = false;
    private AudioSource aSource;
    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();
	}
    private void Start() {
        alarmArea.enabled = false;
    }

    // Update is called once per frame

	void Update () {
		
	}

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position , radius);
    }

    public void Explode(Vector3 shootDir ) {


        //Fire the explosion at the center of the explosion
      //  print("explosion called");
        
        if (exploded) {
          //  print("returning");
            return;
        }
        exploded = true;
        GameObject instancePS = Instantiate(explosionEffect , shootDir, Quaternion.identity);
        rb.drag = 2f;
        rb.angularDrag = 0.05f;

        //Increase pollution
        ScoreManager.instance.UpdatePollution(pollution);

        //Play explosion sound
        aSource.PlayOneShot(explodeClip);


        ParticleSystem explosion = instancePS.GetComponent<ParticleSystem>();
        explosion.Stop();
        explosion.Play();   

        //Simulate force from the center of the explosion

        Collider[] colliders = Physics.OverlapSphere(shootDir , radius);
        foreach (Collider hit in colliders) {
            Rigidbody rb1 = hit.GetComponent<Rigidbody>();

            if (rb1 != null)
                rb1.AddExplosionForce(force , shootDir , radius , upwardsModifier);

            EnemyHealth eh = hit.GetComponent<EnemyHealth>();


            if (eh) {
                eh.TakeDamage(explosionDamage);
            }
            else if (hit.CompareTag("Player1") || hit.CompareTag("Player2")) {
                
                PlayerHealth ph = hit.GetComponent<PlayerHealth>();
                if (ph) {
               //     print("damaging player");
                    ph.TakeDamage(explosionDamage);
                }
              /*  exploding other explosives through one explosive may get inefficient
               *  else if (hit.CompareTag("Explosive")) {
                    Explosive e = hit.GetComponent<Explosive>();
                    if (e) {
                        print("exploding others");
                        e.Explode(e.transform.position);
                    }
                }
                */


                Destroy(instancePS , 5f);

            }
 
            StartCoroutine(SetAlarm());
        }
    }
    IEnumerator SetAlarm() {

            alarmArea.enabled = true;

            yield return new WaitForSeconds(alarmWaitForSeconds);
            rb.drag = Mathf.Infinity;
            rb.angularDrag = Mathf.Infinity;
            alarmArea.enabled = false;
        }


    }
