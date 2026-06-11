using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Common
{
    [DefaultExecutionOrder(-100)]
    public class SwishToBoot : MonoBehaviour
    {    
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            if (SceneManager.GetActiveScene().name != "Boot")
            {
                SceneManager.LoadScene("Boot");
            }
        }
    }
}