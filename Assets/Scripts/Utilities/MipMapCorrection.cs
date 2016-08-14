using UnityEngine;

namespace EntroMinds.Utilities
{
    public class MipMapCorrection : MonoBehaviour
    {
        private Renderer render;
        void Awake()
        {
            render = GetComponent<Renderer>();
        }
    }
}
