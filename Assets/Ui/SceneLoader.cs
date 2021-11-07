using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int load_scene_id;

    public void LoadScene() 
    {
        SceneManager.LoadSceneAsync(load_scene_id);
    }
}
