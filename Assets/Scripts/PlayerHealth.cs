using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 20;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip playerDamageSFX;
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] GameObject deathScreen;
    [SerializeField] AudioMixer audioMixer;

    private void Start()
    {
        healthText.text = "Health: " + playerHealth.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerHealth -= healthDecrease;
        if (playerHealth > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(playerDamageSFX);
        }
        else if(playerHealth == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(playerDeathSFX);
            DeathSequence();
        }
        healthText.text = "Health: " + playerHealth.ToString();
    }

    private void DeathSequence()
    {
        deathScreen.SetActive(true);
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "Volume", 1f, -80f));
    }
}
