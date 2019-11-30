using UnityEngine;
using UnityEngine.SceneManagement;
using enums;


namespace Controller
{
    public class SceneController : MonoBehaviour
    {
        #region Variables
        public GameScenes activeScene = GameScenes.Controller;
        private GameScenes loadScene = GameScenes.Controller;
        #endregion

        private void OnEnable()
        {
           // EventHandler.Instance.StartListening(GameEvent.SceneLoaded, ChangeActiveScene);
        }

        /// <summary>
        /// Load's a given scene after unloading the previous one
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(GameScenes scene)
        {
            loadScene = scene;
            if (activeScene != GameScenes.Controller)
            {
                SceneManager.UnloadSceneAsync((int)activeScene);
                Debug.Log("[SceneController] Unload scene: " + activeScene.ToString());
            }

            SceneManager.LoadScene((int)loadScene, LoadSceneMode.Additive);
            Debug.Log("[SceneController] Load scene: " + scene.ToString());

            //coz we dont use eventhandlers:
            ChangeActiveScene();
        }

        /// <summary>
        /// Set's the loaded scene as active scene 
        /// </summary>
        /// <param name="scene"></param>
        private void ChangeActiveScene()
        {
           
            activeScene = loadScene;
            //EventHandler.Instance.TriggerEvent(GameEvent.ActiveSceneChanged);
            Debug.Log("[SceneController] Active scene: " + activeScene.ToString());
      
        }
    }
}