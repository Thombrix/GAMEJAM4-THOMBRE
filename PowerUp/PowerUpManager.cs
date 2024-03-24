using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PowerUpManager : MonoBehaviour
{

    public enum PowerUp
    {
        GRAB = 0,
        MOVEMENT,
        NONE
    }

    private PowerUp activePowerUp;

    private void Start()
    {
        activePowerUp = PowerUp.NONE;
    }

    public PowerUp GetActivePowerUp()
    {
        return activePowerUp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (activePowerUp != PowerUp.NONE)
            return;

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("OI");
            Destroy(collision.gameObject);
            activePowerUp = RandomPowerUp();
            Debug.Log(activePowerUp);
            StartCoroutine("PowerUpTimer");
        }
    }

    private PowerUp RandomPowerUp()
    {
        Array values = Enum.GetValues(typeof(PowerUp));
        Random random = new Random();
        return (PowerUp)values.GetValue(random.Next(values.Length - 1));
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(3);
        activePowerUp = PowerUp.NONE;
    }
}
