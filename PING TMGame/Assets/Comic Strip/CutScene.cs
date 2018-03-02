using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {
    public List<Sprite> list = new List<Sprite>();
	public string nextLevel;
    SpriteRenderer spriteRenderer;
    float startTime = 3f;
    float timer;
    int iterator = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = list[0];
        timer = startTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && iterator < list.Count)
        {
            spriteRenderer.sprite = list[iterator];
            iterator++;
            timer = startTime;
        }
		else if (iterator >= list.Count && timer < 0f)
        {
			SceneManager.LoadScene(nextLevel);
        }
    }

}
