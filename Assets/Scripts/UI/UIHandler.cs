using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    // Se definen cada uno de los menus del juego
    public GameObject mainMenu;
    public GameObject mainMenuBox;
    public GameObject optionsMenu;
    public GameObject inGameUI;
    public GameObject pauseMenu;

    public bool gameStarted = false;

    // ------------- MENÚ PRINCIPAL ---------------

    // Boton START
    public void StartGame() 
    {
        SceneManager.LoadScene("Map"); // Cargar el primer nivel desde el menú de inicio
        mainMenu.SetActive(false); // Ocultar menu principal
        inGameUI.SetActive(true); // Se muestra la UI del juego

        gameStarted = true;
    }
    
    // Boton OPTIONS
    public void OptionsMenu()
    {
        if(gameStarted == false)
        {
            mainMenuBox.SetActive(false); // Se oculta el menu box
            optionsMenu.SetActive(true); // Se muestra el menú de opciones
        } 
        else if(gameStarted == true) 
        {
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        
    }

     // Boton BACK del menú de opciones
    public void OptionsBackButton()
    {
        if(gameStarted == false) // Si ya inicio
        {
            optionsMenu.SetActive(false);
            mainMenuBox.SetActive(true);
        }
        else if(gameStarted == true) 
        {
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
        
    }

     // ------------- UI EN EL JUEGO ---------------

    public void PauseGame()
    {
        Time.timeScale = 0; // Se pausa el tiempo de ejecución en el juego
        pauseMenu.SetActive(true); // Se muestra el menú de pausa
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Se resume el tiempo de ejecución del juego
        pauseMenu.SetActive(false); // Se oculta el menú de pausa
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Nivel");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu"); // Cargar la escena de menú principal
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            PauseGame();
        }
    }
}
