using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class juego : MonoBehaviour
{
    [SerializeField]
    public Sprite[] PosiblesImagenes;

    [SerializeField]
    GameObject MenuGanar;

    public GameObject PiezaSeleccionada;
    int capa = 1;
    public int PiezasEncajadas = 0;

    [SerializeField]
    int nivelInicio = 0;

    void Start()
    {
        //INICIALIZAR LAS PIEZAS
       // for (int i = 0; i < 36; i++)
        //{
         //   GameObject.Find("Pieza (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = PosiblesImagenes[nivelInicio];
        //}
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //AQUI OBTENEMOS LOS DATOS DE LA PIEZA QUE TOCAMOS
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //VERIFICAMOS QUE SEA DEL TIPO QUE NECESITAMOS SELECCIONAR
            if (hit.transform.CompareTag("Puzzle"))
            {
                if (!hit.transform.GetComponent<pieza>().Encajada)
                {
                    PiezaSeleccionada = hit.transform.gameObject;
                    PiezaSeleccionada.GetComponent<pieza>().Seleccionada = true;
                    PiezaSeleccionada.GetComponent<SortingGroup>().sortingOrder = capa;
                    capa++;
                }
            }
        }

        //DETECTAR CUANDO SE PRECIONA EL BOTON
        if (Input.GetMouseButtonUp(0))
        {
            if (PiezaSeleccionada != null)
            {
                PiezaSeleccionada.GetComponent<pieza>().Seleccionada = false;
                PiezaSeleccionada = null;
            }
        }

        //EN CASO DE QUE SE TENGA UNA PIEZA SELECCIONADA QUE ESTA SEA IGUAL A LA POSICION DE MI MOUSE
        if (PiezaSeleccionada != null)
        {
            Vector3 raton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PiezaSeleccionada.transform.position = new Vector3(raton.x, raton.y, 0);
        }

        //DETECTAR CUANDO SE GANE
        if (PiezasEncajadas == 36)
        {
           MenuGanar.SetActive(true);
        }
    }


   
}