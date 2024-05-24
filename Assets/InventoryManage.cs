using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManage : MonoBehaviour
{

    [SerializeField] GameObject UnlockRedSlash;


    // image for red slash instance 
    // [SerializeField] Image redSlash;

    private void Enable()
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log(playerController.unlockRedSlash);

        // if in playercontroller unlockRedSlash is true then enable redSlash image
        if (playerController.unlockRedSlash == true)
        {
            UnlockRedSlash.SetActive(true);
        }
        else
        {
            UnlockRedSlash.SetActive(false);
        }

    }
    private void Start()
    {
        Enable(); // Gọi Enable() khi bắt đầu game
    }

    private void Update()
    {
        Enable(); // Gọi Enable() mỗi frame để đảm bảo cập nhật trạng thái hiển thị
    }
}
