﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.
    public int ammo = 1;

    float timer;                                    // A timer to determine when to fire.
    private Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
 //   AudioSource gunAudio;                           // Reference to the audio source.
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;
    bool fired = false;

    void Awake() {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
 //       gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position , transform.position + transform.forward * 100f);
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (timer >= timeBetweenBullets * effectsDisplayTime && fired) {
            // ... disable the effects.
            DisableEffects();
            fired = false;
        }
    }

    public void Fire () {
        if(ammo >= 1) {
            ammo--;
            //if ammo is 0, disable the turret
            if(ammo <= 0) {
                DisableTurret();
            }


            fired = true;
            if (timer >= timeBetweenBullets) {
                // Reset the timer.
                timer = 0f;

                // Play the gun shot audioclip.
                //       gunAudio.Play();

                // Enable the light.
                gunLight.enabled = true;

                // Stop the particles from playing if they were, then start the particles.
                gunParticles.Stop();
                gunParticles.Play();

                // Enable the line renderer and set it's first position to be the end of the gun.
                gunLine.enabled = true;
                gunLine.SetPosition(0 , transform.position);

                // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;

                // Perform the raycast against gameobjects on the shootable layer and if it hits something...
                if (Physics.Raycast(shootRay , out shootHit , range , shootableMask)) {
                    // Try and find an EnemyHealth script on the gameobject hit.
                    Explosive explosive = shootHit.collider.GetComponent<Explosive>();

                    // Set the second position of the line renderer to the point the raycast hit.
                    gunLine.SetPosition(1 , shootHit.point);
                    // If the Explosive component exist...
                    if (explosive != null) {

                        explosive.Explode(shootHit.point);
                    }

                }
                // If the raycast didn't hit anything on the shootable layer...
                else {
                    print("hit nothing");
                    // ... set the second position of the line renderer to the fullest extent of the gun's range.
                    gunLine.SetPosition(1 , shootRay.origin + shootRay.direction * range);
                }
            }
        }


    }

    void DisableTurret () {
        //TODO: play animation disabling turret.
        transform.parent.parent.gameObject.SetActive(false);
    }


    public void DisableEffects() {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
