using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //board positions, not wrt global
    int matrixX;
    int matrixY;

    //false is movement, true is attack
    public bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        if(attack)
        {
            //change to red for attacking
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        if(attack) 
        {
            controller = GameObject.FindGameObjectWithTag("GameController");

            if(attack)
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);//cp:chesspiece

                if(cp.name == "white_king")
                {
                    controller.GetComponent<Game>().Win("black");
                }
                if (cp.name == "black_king")
                {
                    controller.GetComponent<Game>().Win("white");
                }


                Destroy(cp);
            }

            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
                reference.GetComponent<Chessman>().GetYBoard());

            reference.GetComponent<Chessman>().SetXBoard(matrixX);
            reference.GetComponent<Chessman>().SetYBoard(matrixY);
            reference.GetComponent<Chessman>().SetCoords();

            controller.GetComponent<Game>().SetPosition(reference);

            controller.GetComponent <Game>().NextTurn();

            reference.GetComponent<Chessman>().DestroyMovePlates();
        }

        public void SetCoords(int x, int y) //public
        {
            matrixX = x;
            matrixY = y;
        }

        public void SetReference(GameObject obj) //public
        {
            reference = obj;
        }

        public GameObject GetReference() //public
        {
            return reference;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
