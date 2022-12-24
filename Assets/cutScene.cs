using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutScene : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    [SerializeField] GameObject fire;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip alertClip;
    [SerializeField] AudioClip boomClip;

    // Start is called before the first frame update
    void Start()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
        mesh.gameObject.SetActive(true);
        StartCoroutine(scene());
    }
    Vector2 startingPos;
    private float speed_x = 1;
    private float amount_x = 1;
    private float speed_y = 2;
    private float amount_y = 0.3f;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(startingPos.x + Mathf.Sin(Time.time * speed_x) * amount_x, startingPos.y + Mathf.Sin(Time.time * speed_y) * amount_y);
    }

    IEnumerator scene()
    {
        StartCoroutine(fade(1));
        yield return new WaitForSeconds(6);
        speed_x = 40;
        amount_x = 0.3f;
        speed_y = 50;
        amount_y = 0.5f;
        AS.clip = alertClip;
        AS.loop = true;
        AS.Play();
        yield return new WaitForSeconds(2);
        fire.SetActive(true);
        yield return new WaitForSeconds(2);
        fire.SetActive(false);
        AS.loop = false;
        AS.clip = boomClip;
        AS.Play();
        explosion.SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(fade(-1));
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("game");
    }


    IEnumerator fade(int isIn)
    {

        for(int i=0;i < 100; i++)
        {
            Color temp = mesh.material.color;
            temp.a -= 0.01f * isIn;
            mesh.material.color = temp;
            yield return new WaitForSeconds(0.01f);

        }
    }

}
