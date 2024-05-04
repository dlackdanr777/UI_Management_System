using Muks.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UINavigationCoordinator))]
public class UINavTest : MonoBehaviour
{
    private UINavigationCoordinator _navCoordinator;

    // Start is called before the first frame update
    void Start()
    {
        _navCoordinator = GetComponent<UINavigationCoordinator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_navCoordinator.Pop())
                Debug.Log("전부 없음");
        }
    }
}
