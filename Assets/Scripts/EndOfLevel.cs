using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
        public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToProject(string project) {
        if(project == "RigAssembly") {
            Application.OpenURL("https://www.google.com/");
        } else {
            Application.OpenURL("hhtps://www.nothing.com");
        }
    }
}
