using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselManager : MonoBehaviour 
{
    [SerializeField] private RectTransform contentRect;


    private void Start()
    {
        GameObject prefab = PrefabManager.instance.GetPrefabByName("QuestionCard");

        Instantiate(prefab, contentRect);
    }


}
