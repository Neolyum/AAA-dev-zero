/*using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;
using Manager;
using DesignPatterns;

namespace Controller
{
    public class SceneController : MonoBehaviour
    {
        #region Variables
        //public GameScene activeScene = GameScene.Manager;
        //private GameScene loadScene = GameScene.Manager;
        #endregion

        private void OnEnable()
        {
           // EventHandler.Instance.StartListening(GameEvent.SceneLoaded, ChangeActiveScene);
        }

        /// <summary>
        /// Load's a given scene after unloading the previous one
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(GameScene scene)
        {
            loadScene = scene;
            if (activeScene != GameScene.Manager)
            {
                SceneManager.UnloadSceneAsync((int)activeScene);
                Debug.Log("[SceneController] Unload scene: " + activeScene.ToString());
            }

            SceneManager.LoadScene((int)loadScene, LoadSceneMode.Additive);
            Debug.Log("[SceneController] Load scene: " + scene.ToString());
        }

        /// <summary>
        /// Set's the loaded scene as active scene 
        /// </summary>
        /// <param name="scene"></param>
        private void ChangeActiveScene()
        {
            if (activeScene != loadScene)
            {
                GameController.ResetCounter();
                GameController.SetTCounter(0);
            }
            activeScene = loadScene;
            EventHandler.Instance.TriggerEvent(GameEvent.ActiveSceneChanged);
            Debug.Log("[SceneController] Active scene: " + activeScene.ToString());
            if (activeScene != GameScene.Manager && activeScene != GameScene.Menu)
            {
            }
            else
            {
            }
        }
    }
}*/