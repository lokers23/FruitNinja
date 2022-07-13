using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicX : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Vector2 leftbottom = Camera.main.ViewportToWorldPoint (new Vector2 (0,0)); // bottom-left corner
        //Vector2 righttop = Camera.main.ViewportToWorldPoint (new Vector2 (1,1)); // right-top
        Vector2 rightbot = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)); // right-bot
        //Vector2 lefttop = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)); // right-bot
        //random = Random.Range(righttop.x/2, leftbottom.x/2);
        float x = transform.position.x;
        float y = transform.position.y;
            
        if (leftbottom.y < y)
        {
            transform.position = new Vector2(x, leftbottom.y - 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
