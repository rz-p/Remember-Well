using UnityEngine;
using UnityEngine.SceneManagement;

namespace Statistics {
    public class StatisticsPage : MonoBehaviour
    {
        public void BackOptions(){
            SceneManager.LoadSceneAsync("Options");
        }
    }
}
