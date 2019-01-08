using Sistema;
using UnityEngine;
using Entidades.All;

namespace Entidades.Player
{
    class ControlPlayer : MonoBehaviour
    {
        //Referencias
        #region
        public Mecanicas Mecanicas { get; set; }
        public Stats Stats { get; set; }
        #endregion

        //Movimiento del player 2D
        private void Moverse()
        {
            Vector2 direccion = new Vector2(0,0);
            if (Stats.Struct_Stats.ID == 1)
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
            else if (Stats.Struct_Stats.ID == 2)
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
            Mecanicas.Mover(transform, direccion, Stats.Struct_Stats.VelocidadMovimiento);
        }

        private void Disparar()
        {
            if (Stats.Struct_Stats.ID == 1)
            {
                if (Input.GetKey(KeyCode.Space)) Mecanicas.UsarArma(Stats);
                else Mecanicas.ReposarArma(Stats);
            }
            else if(Stats.Struct_Stats.ID == 2)
            {
                if (Input.GetKey(KeyCode.RightShift)) Mecanicas.UsarArma(Stats);
                else Mecanicas.ReposarArma(Stats);
            }
        }

        private void Update()
        {
            Moverse();
            Disparar();

            if (Input.GetKeyDown(KeyCode.P)) Stats.RecibirDaño(10, null);
            if (Input.GetKeyDown(KeyCode.O)) Stats.RecibirExperiencia(9);
            if (Input.GetKeyDown(KeyCode.I)) Stats.RecibirCuracion(10);
        }
    }
}
