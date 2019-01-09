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
            if (Stats.Struct_Stats.Entidad == Entidad.Player1)
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
            else if (Stats.Struct_Stats.Entidad == Entidad.Player2)
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

        //Usa el arma o el arma especial
        private void Disparar()
        {
            if (Stats.Struct_Stats.Entidad == Entidad.Player1)
            {
                if (Input.GetKey(KeyCode.Space)) Mecanicas.UsarArma(Stats, false);
                else Mecanicas.ReposarArma(Stats);

                if (Input.GetKeyDown(KeyCode.F)) Mecanicas.UsarArmaEspecial(Stats);
            }
            else if(Stats.Struct_Stats.Entidad == Entidad.Player2)
            {
                if (Input.GetKey(KeyCode.RightShift)) Mecanicas.UsarArma(Stats, false);
                else Mecanicas.ReposarArma(Stats);

                if (Input.GetKeyDown(KeyCode.RightControl)) Mecanicas.UsarArmaEspecial(Stats);
            }
            Stats.Interfaz.GUIArma(Stats);
        }

        private void Update()
        {
            Moverse();
            Disparar();

            if (Input.GetKeyDown(KeyCode.P)) Stats.RecibirDaño(10, null);
            if (Input.GetKeyDown(KeyCode.O)) Stats.RecibirExperiencia(10);
            if (Input.GetKeyDown(KeyCode.I)) Stats.RecibirCuracion(10);
            if (Input.GetKeyDown(KeyCode.U))
            {
                Stats.Struct_Stats.ArmaEspecial = new Objetos.ArmaEspecial()
                {
                    TipoDeArmaEspecial = TipoDeArmaEspecial.FullClear,
                    CargasRestantes = 1
                };
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Stats.Struct_Stats.ArmaEspecial = new Objetos.ArmaEspecial()
                {
                    TipoDeArmaEspecial = TipoDeArmaEspecial.CuraCompleta,
                    CargasRestantes = 1
                };
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemigo")
            {
                Stats statstarjet = collision.GetComponent<Stats>();
                switch(statstarjet.Struct_Stats.Entidad)
                {
                    case Entidad.Enemigo1:
                        Stats.RecibirDaño(100, statstarjet);
                        Stats.SistemaDeControlGeneral.EliminarEntidad(statstarjet);
                        break;
                }
            }
        }

    }
}
