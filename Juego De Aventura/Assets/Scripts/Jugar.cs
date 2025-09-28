using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugar : MonoBehaviour
{
	public void CambiarASampleScene()
	{
		SceneManager.LoadScene("SampleScene");
	}
}
