using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterLifePlayer : MonoBehaviour {
    
    // config params
    [SerializeField] GameObject playerHealthIconPrefab;
    [SerializeField] int healthPerCounter = 100;
    [Range(0f, 1f)] [SerializeField] float iconOffset = 0.06f;
    [Range(0f,1f)][SerializeField] float xOffset = 0.08f;
    [Range(0f,1f)][SerializeField] float yOffset = 0.03f;

    private GameObject[] healthIcons; // store them left to right
    private PlayerLogic PL;
    float xSizeOfIcon; // size of each player icon
    int currentLifeIconIndex;

    // Use this for initialization
    void Start () {
        // initialize private fields
        PL = FindObjectOfType<PlayerLogic>();
        int numIcons = PL.GetHealth() / 100; // assumes HP things happen in multiples of 100. could make this a list for portions of the health icon
        healthIcons = new GameObject[numIcons]; // initialize icon array with number of icons total
        xSizeOfIcon = playerHealthIconPrefab.GetComponent<Renderer>().bounds.size.x;
        currentLifeIconIndex = numIcons - 1;

        DrawAndPopulateIconArray();
    }

    private void DrawAndPopulateIconArray()
    {
        for(int i=0; i<healthIcons.Length; i++)
        {
            Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(0+xOffset+i*iconOffset, 1-yOffset, 1));
            Debug.Log(position);
            healthIcons[i] = Instantiate(playerHealthIconPrefab,
                position,
                Quaternion.identity);
        }
    }
    // Destroy icons here
    public void ProcessDamage(int damage)
    {
        for(int i=0; i < damage/100; i++)
        {
            if (currentLifeIconIndex < 0)
            {
                // log error
            }
            Destroy(healthIcons[currentLifeIconIndex]);
            currentLifeIconIndex--;
        }
    }
}
