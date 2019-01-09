using Sistema;
using UnityEngine;
using Entidades.All;

namespace Entidades.Enemigos
{
    class ControlEnemigo : MonoBehaviour
    {
        //Referencias
        #region
        public Mecanicas Mecanicas { get; set; }
        public Stats Stats { get; set; }
        #endregion

        public float UltimoDisparo { get; private set; }
        public float UltimoMovimiento { get; private set; }

        private void Moverse()
        {
            Vector2 vector2 = new Vector2();
            float DeltaHeight = (Mathf.Sin(Time.fixedTime + Time.deltaTime) - Mathf.Sin(Time.fixedTime));
            vector2.x += DeltaHeight * 3.0f;

            if(Stats.Struct_Stats.Entidad == Entidad.Enemigo2)
            {
                if(UltimoMovimiento + 5 <= Time.fixedTime)
                {
                    UltimoMovimiento = Time.fixedTime;
                    vector2.y = -1;
                }
            }

            Mecanicas.Mover(transform, vector2, Stats.Struct_Stats.VelocidadMovimiento);
        }

        private void Disparar()
        {
            if (UltimoDisparo + 0.5f > Time.fixedTime) return;

            UltimoDisparo = Time.fixedTime;
            if (Random.Range(0, 4) != 0) return;

            Mecanicas.UsarArma(Stats, true);
        }

        private void Update()
        {
            Moverse();
            Disparar();
        }

    }
}
