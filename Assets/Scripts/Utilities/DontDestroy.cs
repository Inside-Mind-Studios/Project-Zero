using UnityEngine;
using System.Collections;

namespace EntroMinds.Utilities
{
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            //Causes object not to be destroyed when loading a new scene.
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
