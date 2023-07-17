using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateShop : GameState
{

    public GameObject shopUI;
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI currentHatName;
    public HatLogic hatLogic;
    private bool isInit = false;


    // Shop Item
    public GameObject hatPrefab;
    public Transform hatContainer;

    public Hat[] hats;
    

    private void PopulateShop()
    {
        
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(hatPrefab, hatContainer);
            // Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));
            // Tumbnail
            go.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;
            // Name
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;
            // Price
            if (SaveManager.Instance.save.UnlockedHatFlag[i] ==0)
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();  
            else
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }

    }

    private void OnHatClick(int index)
    {
        if (SaveManager.Instance.save.UnlockedHatFlag[index] == 1)
        {
            SaveManager.Instance.save.CurrentHatIndex = index;
            currentHatName.text = hats[index].ItemName;
            hatLogic.SelectHat(index);
            SaveManager.Instance.SaveGame();
        }
        // if we dont have it canwe buy it 


        else if (hats[index].ItemPrice <= SaveManager.Instance.save.Fish)
        {
            SaveManager.Instance.save.Fish -= hats[index].ItemPrice;
            SaveManager.Instance.save.UnlockedHatFlag[index] = 1;
            currentHatName.text = hats[index].ItemName;
            hatLogic.SelectHat(index);
            SaveManager.Instance.save.CurrentHatIndex = index;
            totalFish.text = SaveManager.Instance.save.Fish.ToString("000");
            SaveManager.Instance.SaveGame();
            hatContainer.GetChild(index).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }
        // dont have it, cant buy it
        else
        {
            Debug.Log("Not enough fish");
        }
        

        
    }
        
        
        
        
        public override void Construct()
    {

        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Shop);
        hats = Resources.LoadAll<Hat>("Hat/");
        shopUI.SetActive(true);

        if (!isInit)   // prevent all this running every time
        {
            totalFish.text = SaveManager.Instance.save.Fish.ToString("000");
            currentHatName.text = hats[SaveManager.Instance.save.CurrentHatIndex].ItemName;
            PopulateShop();
            isInit = true;
        }
        
        
    }



    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    public void OnHomeClick()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
