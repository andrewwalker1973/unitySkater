using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public float chunkLength;


    public void ShowChunk()
    {
        gameObject.SetActive(true);
      //  return this;
    }


    public void HideChunk()
    {

        gameObject.SetActive(false);
       // return this;
    }

}
