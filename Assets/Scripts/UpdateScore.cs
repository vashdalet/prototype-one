using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    public int coinCount = 0;
    public TextMeshProUGUI coinText;

   void Update()
   {
        coinText.text = coinCount.ToString();
   }
}
