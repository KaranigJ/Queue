using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject sound;   

    [SerializeField]
    private Sprite[] sprites;
    private Image heroImage;
    private Image soundImage;
    public static bool soundState;
    private void Start()
    {
        heroImage = character.GetComponent<Image>();
        soundImage = sound.GetComponent<Image>();
        soundState = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Character(string sex)
    {
        if(sex == "m")
        {
            heroImage.sprite = sprites[2];
        }
        else if(sex == "f")
        {
            heroImage.sprite = sprites[3];
        }
    }
    public void About()
    {

    }
    public void Sound()
    {
        if (soundState == true)
        {
            soundImage.sprite = sprites[1];
            soundState = false;
        }
        else
        {
            soundImage.sprite = sprites[0];
            soundState = true;
        }
            
    }
}
