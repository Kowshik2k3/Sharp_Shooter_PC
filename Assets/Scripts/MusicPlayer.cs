using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Start()
    {
        int numofMusicPlayers = FindObjectsByType<MusicPlayer>(FindObjectsSortMode.None).Length; // Count all MusicPlayer instances in the scene
        if (numofMusicPlayers > 1)
        { 
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
