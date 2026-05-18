using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }

    public void changeHealth(int ammount) 
    {
        currentHealth += ammount;
        healthTextAnim.Play("HPupdate");

        healthText.text = "HP: " + currentHealth + " / " + maxHealth;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
