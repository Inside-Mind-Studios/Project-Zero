using UnityEngine;
using UnityEngine.UI;

namespace EntroMinds.Managers
{
    public class GraphicsManager : MonoBehaviour
    {
        int[] ShadowDistanceValues = { 0, 25, 85, 135, 200};

        // Use this for initialization
        void Start()
        {
            //TODO: Load settings from a file
        }

        /*====================================================
         * Easy Methods
         * ==================================================*/
        
        /// <summary>
        /// One stop shop to the Quality Level, possible values are
        /// 0 - 4 or Low, Medium, High, Very High, Ultra
        /// </summary>
        public void increaseQualityLevel()
        {
            QualitySettings.IncreaseLevel();
        }

        /// <summary>
        /// One stop shop to decrement the Quality Level, possible values are
        /// 0 - 4 or Low, Medium, High, Very High, Ultra
        /// </summary>
        public void decreaseQualityLevel()
        {
            QualitySettings.DecreaseLevel();
        }

        /*====================================================
         * Manual Control Methods
         * ==================================================*/

        /// <summary>
        /// Toggles the Anisotropic Filtering
        /// </summary>
        /// <param name="toggle"></param>
        public void setAnisotropicFiltering(Toggle toggle)
        {
            if (toggle.isOn)
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
            else
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }

        /// <summary>
        /// Sets the Antialiasing level, useful with UnityEngine.UI 
        /// Slider objects
        /// </summary>
        /// <param name="level"></param>
        public void setAntiAliasing(int level)
        {
            QualitySettings.antiAliasing = level;
        }

        /// <summary>
        /// Sets the Antialiasing level, useful with UnityEngine.UI 
        /// Slider objects
        /// </summary>
        /// <param name="level"></param>
        public void setAntiAliasing(Slider slider)
        {
            QualitySettings.antiAliasing = (int)slider.value;
        }

        /// <summary>
        /// Sets the number of pixel lights that can effect a single
        /// object.
        /// </summary>
        /// <param name="slider"></param>
        public void setPixelLights(Slider slider)
        {
            QualitySettings.pixelLightCount = (int)slider.value;
        }

        /// <summary>
        /// Manually sets the Quality Level to a specific value, useful 
        /// for UnityEngine.UI Slider objects
        /// </summary>
        /// <param name="level"></param>
        public void setQualityLevel(float level)
        {
            QualitySettings.SetQualityLevel((int)level);
        }

        /// <summary>
        /// Manually sets the Quality Level to a specific value, useful 
        /// for UnityEngine.UI Slider objects
        /// </summary>
        /// <param name="level"></param>
        public void setQualityLevel(Slider slider)
        {
            QualitySettings.SetQualityLevel((int)(slider.value));
        }

        /// <summary>
        /// Sets the number of Shadow Cascades for all directional lights
        /// </summary>
        /// <param name="slider"></param>
        public void setShadowCascade(Slider slider)
        {
            QualitySettings.shadowCascades = (int)slider.value;
        }

        /// <summary>
        /// Sets the maximum distance shadows will still render
        /// 5 possible values (0 - 4) defined distances are set within 
        /// QualitySettings initially in the Editor
        /// </summary>
        /// <param name="slider"></param>
        public void setShadowDistance(Slider slider)
        {
            QualitySettings.shadowDistance = ShadowDistanceValues[(int)slider.value];
        }

        /// <summary>
        /// Sets the Screen Resolution of the game's window
        /// </summary>
        /// <param name="values"></param>
        public void setScreenResolution(int values)
        {
            //Screen.SetResolution();
        }

        /// <summary>
        /// Sets the Shadow Resolution Quality, 4 possible values 
        /// (0 - 3) or (Low, Medium, High, Very High)
        /// </summary>
        /// <param name="res"></param>
        public void setShadowResolution(ShadowResolution res)
        {
            QualitySettings.shadowResolution = res;
        }

        /// <summary>
        /// Sets the Shadow Resolution Quality, 4 possible values 
        /// (0 - 3) or (Low, Medium, High, Very High)
        /// </summary>
        /// <param name="res"></param>
        public void setShadowResolution(Slider slider)
        {
            QualitySettings.shadowResolution = (ShadowResolution)slider.value;
        }

        /// <summary>
        /// Toggles Triple Buffering
        /// </summary>
        /// <param name="b"></param>
        public void setTripleBuffering(bool b)
        {
            if (b)
                QualitySettings.maxQueuedFrames = 3;
            else
                QualitySettings.maxQueuedFrames = 0;
        }

        /// <summary>
        /// Toggles Triple Buffering
        /// </summary>
        /// <param name="b"></param>
        public void setTripleBuffering(Toggle toggle)
        {
            if (toggle.isOn)
                QualitySettings.maxQueuedFrames = 3;
            else
                QualitySettings.maxQueuedFrames = 0;
        }

        /// <summary>
        /// Toggles Vertical Sync
        /// </summary>
        /// <param name="b"></param>
        public void setVSync(bool b)
        {
            if (b)
                QualitySettings.vSyncCount = 1;
            else
                QualitySettings.vSyncCount = 0;
        }

        /// <summary>
        /// Toggles Vertical Sync
        /// </summary>
        /// <param name="b"></param>
        public void setVSync(Toggle toggle)
        {
            if (toggle.isOn)
                QualitySettings.vSyncCount = 1;
            else
                QualitySettings.vSyncCount = 0;
        }
    }
}