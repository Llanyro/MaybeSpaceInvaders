using Sistema;
using Objetos;
using UnityEngine;

namespace Player
{
    class Movimiento : MonoBehaviour
    {
        //Referencias
        #region
        public SistemaDeControlGeneral SistemaDeControlGeneral { get; set; }
        public Mecanicas Mecanicas { get; set; }
        #endregion

        public byte ID { get; set; }
        public Arma Arma;

        public float VelocidadMovimiento { get; set; }

        //Movimiento del player 2D
        private void Moverse()
        {
            Vector2 direccion = new Vector2(0,0);
            if (ID == 0)
            {
                if (Input.GetKey(KeyCode.A)) direccion.x  = -1;
                if (Input.GetKey(KeyCode.D))
                {
                    if (direccion.x == 0) direccion.x = 1;
                    else direccion.x = 0;
                }

                if (Input.GetKey(KeyCode.W)) direccion.y = 1;
                if (Input.GetKey(KeyCode.S))
                {
                    if (direccion.y == 0) direccion.y = -1;
                    else direccion.y = 0;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow)) direccion.x = -1;
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (direccion.x == 0) direccion.x = 1;
                    else direccion.x = 0;
                }

                if (Input.GetKey(KeyCode.UpArrow)) direccion.y = 1;
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (direccion.y == 0) direccion.y = -1;
                    else direccion.y = 0;
                }
            }
            Mecanicas.Mover(transform, direccion, VelocidadMovimiento);
        }

        private void Disparar()
        {
            if (ID == 0)
            {
                if (Input.GetKey(KeyCode.Space)) Mecanicas.UsarArma(Arma, this);
                else Mecanicas.ReposarArma(Arma);
            }
            else if (Input.GetKey(KeyCode.RightShift)) Mecanicas.UsarArma(Arma, this);
        }

        private void Update()
        {
            Moverse();
            Disparar();
        }
    }
}
