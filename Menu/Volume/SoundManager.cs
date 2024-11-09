using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] private float changeAmount = 0.1f; // A quantidade de altera��o para cada tecla pressionada

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    // M�todo que ser� chamado quando o slider for alterado manualmente (pelo mouse ou teclado)
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    // Atualiza o slider baseado no valor salvo
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    // Salva o valor atual do slider
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    void Update()
    {
        // Verifica se as setas direita ou esquerda s�o pressionadas
        if (Input.GetKeyDown(KeyCode.P))
        {
            volumeSlider.value += changeAmount; // Aumenta o valor do slider
            ChangeVolume(); // Atualiza o volume e salva a configura��o
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            volumeSlider.value -= changeAmount; // Diminui o valor do slider
            ChangeVolume(); // Atualiza o volume e salva a configura��o
        }

        // Certifica-se de que o valor do slider n�o ultrapasse os limites
        volumeSlider.value = Mathf.Clamp(volumeSlider.value, 0f, 1f);
    }
}