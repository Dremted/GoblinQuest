using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class AudioMixerManager : MonoBehaviour
{
    public static AudioMixerManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    private Slider musicSlider;
    private Slider fxSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndSetupSliders();
    }

    private void FindAndSetupSliders()
    {
        GameObject mSliderObj = GameObject.Find("MusicSlider");
        GameObject fSliderObj = GameObject.Find("FXSlider");

        if (mSliderObj != null)
        {
            musicSlider = mSliderObj.GetComponent<Slider>();

            musicSlider.onValueChanged.RemoveAllListeners();

            musicSlider.onValueChanged.AddListener(SetMusicVolume);

            musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);
        }

        if (fSliderObj != null)
        {
            fxSlider = fSliderObj.GetComponent<Slider>();
            fxSlider.onValueChanged.RemoveAllListeners();
            fxSlider.onValueChanged.AddListener(SetSoundFXVolume);
            fxSlider.value = PlayerPrefs.GetFloat("FXVol", 1f);
        }
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("VolumeMusic", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    public void SetSoundFXVolume(float volume)
    {
        audioMixer.SetFloat("VolumeEffects", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("FXVol", volume);
    }
}