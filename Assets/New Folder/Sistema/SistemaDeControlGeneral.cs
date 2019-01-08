using UnityEngine;
using Entidades.All;
using System.Collections.Generic;

namespace Sistema
{
    enum Entidad { Player1, Player2, Enemigo1 }
    class SistemaDeControlGeneral : MonoBehaviour
    {
        public Interfaz Interfaz { get; private set; }

        //Variables que cambian el juego desde fuera del script
        #region
        //Indica el tamaño del mapa X = Y (Es un cuadrado)
        public float TamañoMapa;

        //Escala de tanto el mapa, como los jugadores, enemigos, proyectiles y objetos 
        public float EscalaX = 1;
        public float EscalaY = 1;

        //Cambia la escala de las cosa relacionadas con la Gui (Interfaz)
        public float EscalaXGui = 1;
        public float EscalaYGui = 1;

        //Cambia la velocidad del juego
        public float VelocidadDelJuego = 1;

        //Velocidad general de los players
        public float VelocidadMovimientoPlayer = 1;

        //Velocidad general de los proyectiles
        public float VelocidadMovimientoProyectil = 1;
        public float VelocidadMovimientoProyectilEnemigo = 1;

        //Daño general de los proyectiles
        public float DañoProyectil = 40;

        //Enemigos
        public int MaxEnemigosTipo1 = 3;

        #endregion

        //Objetos Recogidos Externamente
        #region
        public GameObject Player;
        public GameObject Enemigo1;

        public GameObject Proyectil1;
        public GameObject Proyectil2;


        #endregion

        //Objetos Guardados desde scripting
        #region
        /// <summary>
        /// Objeto que indica el tamaño del mapa
        /// Es el objeto hijo Canvas del objeto sistema
        /// </summary>
        public GameObject Mapa { get; private set; }
        /// <summary>
        /// Camara que graba todo el mapa
        /// </summary>
        public GameObject MainCamera { get; private set; }
        #endregion

        //Entidades vivas
        #region
        //Variables
        #region
        //Players
        public GameObject Player1 { get; private set; }
        public GameObject Player2 { get; private set; }

        //Enemigos
        public List<GameObject> EnemigosTipo1 = new List<GameObject>();

        #endregion

        //Funciones
        #region
        /// <summary>
        /// Añade un jugador a la partida si este no esta inicializado
        /// Si esta instanciado sale directemante
        /// </summary>
        public void AñadirEntidad(Entidad entidad, Vector2 vector2)
        {
            switch (entidad)
            {
                case Entidad.Enemigo1:
                    if(EnemigosTipo1.Count < MaxEnemigosTipo1) EnemigosTipo1.Add(InicializarEntidad(Entidad.Enemigo1, Enemigo1, vector2));
                    break;
                case Entidad.Player1:
                    if (Player1 == null)
                        Player1 = InicializarEntidad(entidad, Player, vector2);
                    break;
                case Entidad.Player2:
                    if (Player2 != null)
                        Player2 = InicializarEntidad(entidad, Player, vector2);
                    break;
            }
        }

        /// <summary>
        /// Inicializa una entidad
        /// </summary>
        private GameObject InicializarEntidad(Entidad entidad, GameObject entidadObj, Vector2 posicion)
        {
            //Instanciamos la entidad
            GameObject nuevaEntidad = Instantiate(entidadObj);
            nuevaEntidad.transform.localScale = new Vector3(EscalaX, EscalaY, 0);
            nuevaEntidad.transform.position = new Vector3(posicion.x, posicion.y, 0);

            //Añade el script stats a la entidad
            Stats stats = nuevaEntidad.AddComponent<Stats>();
            stats.IniciarPlayer(entidad, this, Interfaz);

            return nuevaEntidad;
        }

        /// <summary>
        /// Elimina una entidad de la partida
        /// </summary>
        public void EliminarEntidad(Stats stats)
        {
            Interfaz.IniciarGUIPlayer(stats.Struct_Stats.Entidad, false);

            switch (stats.Struct_Stats.Entidad)
            {
                case Entidad.Enemigo1:
                    EnemigosTipo1.Remove(stats.gameObject);
                    break;
                case Entidad.Player1:
                    Player1 = null;
                    break;
                case Entidad.Player2:
                    Player2 = null;
                    break;
            }
            Destroy(stats.gameObject);
        }

        #endregion

        #endregion

        //Entidades objetos (inertes)
        #region
        #endregion
        
        //Inicializadores
        #region
        private void ActualizacionInicialJuego()
        {
            //Cambio de la escala de tiempo de todo el juego
            Time.timeScale = VelocidadDelJuego;

            //Reasignamos los valores del canvas del mapa
            Canvas CanvasMapa = Mapa.GetComponent<Canvas>();
            CanvasMapa.renderMode = (RenderMode)1;
            CanvasMapa.worldCamera = MainCamera.GetComponent<Camera>();

            //Tamaño mapa
            MainCamera.GetComponent<Camera>().orthographicSize = TamañoMapa;

            //Cambiar axis juego

        }

        private void BuscarReferencias()
        {
            //Buscar los hijos
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Canvas")
                {
                    Mapa = transform.GetChild(i).gameObject;
                }
                else if (transform.GetChild(i).tag == "MainCamera")
                {
                    MainCamera = transform.GetChild(i).gameObject;
                }
            }
        }

        private void Awake()
        {
            Interfaz = GetComponent<Interfaz>();

            BuscarReferencias();
            ActualizacionInicialJuego();
        }

        #endregion

        private void Update()
        {
            //Test
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AñadirEntidad(Entidad.Player1, new Vector2(0, -((TamañoMapa * 7) / 10)));
            }

            //Test 2
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AñadirEntidad(Entidad.Player2, new Vector2(0, -((TamañoMapa * 7) / 10)));
            }

            //Test 3
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                AñadirEntidad(Entidad.Enemigo1, new Vector2(0, ((TamañoMapa * 7) / 10)));
            }
        }
    }
}
