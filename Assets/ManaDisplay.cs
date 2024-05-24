using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaDisplay : MonoBehaviour
{
    public GameObject mana;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GlobalController.Instance.player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        float ManaRemain = playerController.Mana;


        // make mana image fill amount to be the same as the player's mana, the mana image is setting image type filled and fill method in vertical 
        mana.GetComponent<UnityEngine.UI.Image>().fillAmount = ManaRemain;



    }
}
