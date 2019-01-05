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
            //Direccion direccion = 0;
            Vector2 direccion = new Vector2(0,0);
            if (ID == 0)
            {
                if (Input.GetKey(KeyCode.A)) direccion.x -= 1;/*direccion = (Direccion)4;*/
                if (Input.GetKey(KeyCode.D))
                {
                    /*if (direccion == (Direccion)4) direccion = 0;
                    else direccion = (Direccion)3;*/
                    direccion.x += 1;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    /*if (direccion == (Direccion)4) direccion = (Direccion)7;
                    else if(direccion == (Direccion)3) direccion = (Direccion)5;
                    else direccion = (Direccion)1;*/
                    direccion.y += 1;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    /*if (direccion == (Direccion)4) direccion = (Direccion)8;
                    else if (direccion == (Direccion)3) direccion = (Direccion)6;
                    else if (direccion == (Direccion)1) direccion = 0;
                    else if (direccion == (Direccion)5) direccion = (Direccion)3;
                    else if (direccion == (Direccion)7) direccion = (Direccion)4;
                    else direccion = (Direccion)2;*/
                    direccion.y -= 1;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow)) direccion.x -= 1;/*direccion = (Direccion)4*/;
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    /*if (direccion == (Direccion)4) direccion = 0;
                    else direccion = (Direccion)3;*/
                    direccion.x += 1;
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    /*if (direccion == (Direccion)4) direccion = (Direccion)7;
                    else if (direccion == (Direccion)3) direccion = (Direccion)5;
                    else direccion = (Direccion)1;*/
                    direccion.y += 1;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    /*if (direccion == (Direccion)4) direccion = (Direccion)8;
                    else if (direccion == (Direccion)3) direccion = (Direccion)6;
                    else if (direccion == (Direccion)1) direccion = 0;
                    else if (direccion == (Direccion)5) direccion = (Direccion)3;
                    else if (direccion == (Direccion)7) direccion = (Direccion)4;
                    else direccion = (Direccion)2;*/
                    direccion.y -= 1;
                }
            }

            //Mecanicas.Mover(transform, direccion, VelocidadMovimiento);
            Mecanicas.Mover(transform, direccion, VelocidadMovimiento);
        }

        private void Disparar()
        {
            if (ID == 0) if (Input.GetKey(KeyCode.Space)) Mecanicas.UsarArma(Arma, this);
            else if (Input.GetKey(KeyCode.RightShift)) Mecanicas.UsarArma(Arma, this);
        }

        private void Update()
        {
            Moverse();
            Disparar();
        }
    }
}
