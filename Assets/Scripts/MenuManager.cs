using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Jugadores (para el Settings)")]
    [SerializeField] private Movement player1;
    [SerializeField] private Movement player2;

    [Header("Sliders y sus textos")]
    [SerializeField] private Slider speedSliderP1;
    [SerializeField] private Slider speedSliderP2;
    [SerializeField] private TextMeshProUGUI speedTextP1;
    [SerializeField] private TextMeshProUGUI speedTextP2;
    [SerializeField] private Button buttonP1Red;
    [SerializeField] private Button buttonP1Blue;
    [SerializeField] private Button buttonP1Green;
    [SerializeField] private Button buttonP2Red;
    [SerializeField] private Button buttonP2Blue;
    [SerializeField] private Button buttonP2Green;

    private bool isPaused = false;

    [Header("Tamaño de las paletas")]
    [SerializeField] private Slider heightSliderP1;
    [SerializeField] private Slider heightSliderP2;
     private SpriteRenderer spriteRendererP1;
     private SpriteRenderer spriteRendererP2;
    void Start()
    {
        // Al arrancar, los sliders reflejan la velocidad actual de cada jugador //
        speedSliderP1.value = player1.moveSpeed;
        speedSliderP2.value = player2.moveSpeed;
        speedTextP1.text = player1.moveSpeed.ToString("F1");
        speedTextP2.text = player2.moveSpeed.ToString("F1");
        heightSliderP1.value = player1.transform.localScale.y;
        heightSliderP2.value = player2.transform.localScale.y;
        spriteRendererP1 = player1.GetComponent<SpriteRenderer>();
        spriteRendererP2 = player2.GetComponent<SpriteRenderer>();
        buttonP1Red.onClick.AddListener(OnP1ColorRed);
        buttonP1Blue.onClick.AddListener(OnP1ColorBlue);
        buttonP1Green.onClick.AddListener(OnP1ColorGreen);
        buttonP2Red.onClick.AddListener(OnP2ColorRed);
        buttonP2Blue.onClick.AddListener(OnP2ColorBlue);
        buttonP2Green.onClick.AddListener(OnP2ColorGreen);

    }

    void Update()
    {
        // Escape activa o desactiva la pausa //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void Play()
    {
        mainMenuPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    // Back de Settings/Credits: vuelve al menú que corresponda //
    public void BackToMenu()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        if (isPaused)
        {
            pauseMenuPanel.SetActive(true);
        }
        else
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);

        if (!isPaused)
        {
            settingsPanel.SetActive(false);
            creditsPanel.SetActive(false);
        }

        // Pausar de verdad el juego: nada se mueve con timeScale en 0 //
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR // Llamada al Editor de Unity //
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Estos dos los llaman los sliders cuando el usuario los mueve //
    public void OnP1SpeedChanged(float value)
    {
        player1.moveSpeed = value;
        speedTextP1.text = value.ToString("F1");
    }

    public void OnP2SpeedChanged(float value)
    {
        player2.moveSpeed = value;
        speedTextP2.text = value.ToString("F1");
    }
    public void OnP1HeightChanged(float value)
    {
        Vector3 escala = player1.transform.localScale;
        escala.y = value;
        player1.transform.localScale = escala;
    }

    public void OnP2HeightChanged(float value)
    {
        Vector3 escala = player2.transform.localScale;
        escala.y = value;
        player2.transform.localScale = escala;
    }


    public void OnP1ColorRed()
    {
        spriteRendererP1.color = Color.red;
    }
    public void OnP1ColorBlue()
    {
        spriteRendererP1.color = Color.blue;
    }
    public void OnP1ColorGreen()
    {
        spriteRendererP1.color = Color.green;
    }
    public void OnP2ColorRed()
    {
        spriteRendererP2.color = Color.red;
    }
    public void OnP2ColorBlue()
    {
        spriteRendererP2.color = Color.blue;
    }
    public void OnP2ColorGreen()
    {
        spriteRendererP2.color = Color.green;
    }
    
}

