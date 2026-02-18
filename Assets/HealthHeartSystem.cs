using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartSystem : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerController player;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void Start()
    {
        // method used to draw hearts on canvas
        DrawHearts();
    }
    public void ClearHearts()
    {
        // clears all hearts by destroying each individually
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        // creating a new list for the hearts to be contained when redrawn
        hearts = new List<HealthHeart>();
    }

    public void CreateEmptyHeart()
    {
        // this will create a heart that is empty 
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void DrawHearts()
    {
        // this method starts by clearing the current hearts then calculating the players health and dividing it by 2 to find out the amount of hearts that are needed and how many full empty or half ones are needed
        ClearHearts();
        float maxHealthRemainder = player.maxhealth % 2;
        int HeartstoMake = (int) (player.maxhealth / 2 + maxHealthRemainder);
        for (int i = 0; i < HeartstoMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartstatusremainder = (int) Mathf.Clamp(player.health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartstatusremainder);
        }
    }
}
