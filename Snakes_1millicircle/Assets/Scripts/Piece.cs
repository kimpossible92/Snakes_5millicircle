using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Piece : MonoBehaviour {

    ControllerNivel c;
    bool podemosMover = true;
    Rotations rotations;


    IEnumerator Start ()
    {
        c = FindObjectOfType<ControllerNivel>();
        rotations = new Rotations();
        rotations.getRotByType(name);

        bool salir = false;

        while (!salir)
        {
            transform.position += Vector3.down;

            if (nosPodemosMover())
            {
                actualTable();
            }
            else
            {


                transform.position -= Vector3.down;
                podemosMover = false;

                if (isGameOver())
                {
                    c.terminPart();
                    salir = true;
                }
                else
                {
                    c.generatorPiece.instanc();
                    c.comprFil();
                    //c.comprFil();
                }


                break;
            }

            yield return new WaitForSeconds(0.7f);
        }
            
    }
	
	// Update is called once per frame
	void Update () {

        if (!podemosMover)
            return;

        //Movimiento
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moverAux(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            moverAux(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            moverAux(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            moverAux(Vector3.back);

        if (Input.GetKeyDown(KeyCode.Space))
            movementSpecial();

        if (Input.GetKeyDown(KeyCode.W))
        {
            rotations.rotateLeft(transform);
            if (nosPodemosMover())
                actualTable();
            else
            {
                rotations.rotateRight(transform);

            }
        }

        /*
       if (Input.GetKeyDown(KeyCode.E))
        {
            if( (transform.eulerAngles.y >= 0) )
            {
                rotarAux(new Vector3(0, -90, 0));
            }
            else
            {
                rotarAux(new Vector3(0, 90, 0));
            }

        }
        */


    }

    private void movementSpecial()
    {
        transform.position += Vector3.down;
        if (nosPodemosMover())
        {
            actualTable();
            movementSpecial();
        }
        else
        {
            transform.position -= Vector3.down;
            return;
        }
    }


    private void moverAux(Vector3 v)
    {
        transform.position += v;
        if (nosPodemosMover())
            actualTable();
        else
            transform.position -= v;
    }

    private void rotarAux(Vector3 v)
    {
        transform.Rotate(v);
        if (nosPodemosMover())
            actualTable();
        else
            transform.Rotate(-v);
    }

    //Comprueba que todas las cubos de la pieza esten dentro del table del juego.
    bool todoDent()
    {
        for(int i = 0; i< transform.childCount; i++)
        {
            Vector3 t = transform.GetChild(i).position;
            Vector3Int p = new Vector3Int(t);

            if (!c.isDentTable(p))
                return false;
        }
        return true;
    }

    bool isGameOver()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 t = transform.GetChild(i).position;
            Vector3Int p = new Vector3Int(t);

            if (c.isFueraFromAbove(p))
            {
                //print("true");
                return true;
            }
        }
        return false;
    }

    bool nosPodemosMover()
    {
        if (!todoDent())
        {
            return false;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 t = transform.GetChild(i).position;
            Vector3Int v = new Vector3Int(t);
            if (c.table[v.x, v.y, v.z] != null &&
                // Para comprobar si ese bloque pertenece a la misma pieza que se va a mover
                c.table[v.x, v.y, v.z].parent != transform)
                return false;    
        }

        return true;
    }

    void actualTable()
    {
        for (int i = 0; i < c.tamanTable.x; i++)
        {
            for (int j = 0; j < c.tamanTable.y; j++)
            {
                for (int k = 0; k < c.tamanTable.z; k++)
                {
                    if (c.table[i, j, k] != null &&
                        c.table[i, j, k].parent == transform)

                        c.table[i, j, k] = null;
                }
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            Vector3Int v = new Vector3Int(t.position);
            c.table[v.x, v.y, v.z] = t;
        }
    }





}
