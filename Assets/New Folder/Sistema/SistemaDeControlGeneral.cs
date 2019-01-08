using Player;
using UnityEngine;

namespace Sistema
{
    class SistemaDeControlGeneral : MonoBehaviour
    {
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

        public GameObject Player1 { get; private set; }
        public GameObject Player2 { get; private set; }

        #endregion

        //Control Players
        #region
        /// <summary>
        /// Añade un jugador a la partida si este no esta inicializado
        /// </summary>
        private void AñadirJugador(byte IDJugador, bool añadir)
        {
            if (IDJugador > 1 && IDJugador < 0) Debug.LogError("Se intenta inicializar el jugador con ID: " + IDJugador);

            if(añadir)
            {
                if (IDJugador == 0 && Player1 != null) EliminarJugador(IDJugador);
                if (IDJugador == 1 && Player2 != null) EliminarJugador(IDJugador);

                InicializarJugador(IDJugador);
            }
            else
            {
                EliminarJugador(IDJugador);
            }
        }

        private void InicializarJugador(byte IDJugador)
        {
            GameObject nuevoJugador = Instantiate(Player);
            nuevoJugador.transform.localScale = new Vector3(EscalaX, EscalaY, 0);
            nuevoJugador.transform.position = new Vector3(0, -((TamañoMapa * 7) / 10), 0);

            Movimiento movimientoplayer = nuevoJugador.GetComponent<Movimiento>();
            movimientoplayer.ID = IDJugador;
            movimientoplayer.VelocidadMovimiento = VelocidadMovimientoPlayer;
            movimientoplayer.SistemaDeControlGeneral = this;
            movimientoplayer.Mecanicas = new Mecanicas()
            {
                SistemaDeControlGeneral = this,
            };
            movimientoplayer.Arma = new Objetos.Arma()
            {
                TipoDeArma = TipoDeArma.Base,
                Daño = DañoProyectil,
                Recalentamiento = 0,
                MaxRecalentamiento = 100,
                VelocidadDeAtaque = 0.3f
            };

            if (IDJugador == 0) Player1 = nuevoJugador;
            else Player2 = nuevoJugador;
        }

        public void EliminarJugador(byte IDJugador)
        {
            switch (IDJugador)
            {
                case 0:
                    Destroy(Player1);
                    Player1 = null;
                    break;
                case 2:
                    Destroy(Player1);
                    Player1 = null;
                    break;
            }
        }
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
            BuscarReferencias();
            ActualizacionInicialJuego();
        }

        #endregion

        private void Update()
        {
            //Test
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AñadirJugador(0, true);
            }

            //Test 2
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AñadirJugador(1, true);
            }

        }
    }
}
