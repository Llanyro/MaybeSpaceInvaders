using System;
using Player;
using Sistema;
using UnityEngine;

/// <summary>
/// Este comentario tenia un poco de retraso
/// </summary>

enum TipoDeArma { Base, Tridireccional, OctaDireccional }

namespace Objetos
{
    class Proyectil : MonoBehaviour
    {
        public Movimiento Causante { get; set; }
        public Mecanicas Mecanicas { get; set; }
        public float Velocidad { get; set; }
        public Vector2 Direccion { get; set; }
        public float Daño { get; set; }

        private void Update()
        {
            Mecanicas.Mover(transform, Direccion, Velocidad);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Muro") Destroy(gameObject);
        }

    }



    [Serializable]
    class Arma
    {
        public TipoDeArma TipoDeArma { get; set; }

        //Stats del arma
        public float Daño { get; set; }
        public float VelocidadDeAtaque { get; set; }
        /// <summary>
        /// Guarda la ultima vez que se ataco (en segundos)
        /// </summary>
        public float UltimoAtaque { get; set; }

        //Sobreuso del arma
        public float UltimaVezReposada { get; set; }
        public float Recalentamiento { get; set; }
        public float MaxRecalentamiento { get; set; }
        public bool EnEnfriamiento { get; set; }

    }
}
