using Player;
using Sistema;
using UnityEngine;

/// <summary>
/// Test desde visual studio
/// Si aparece esto en git es que se ha hehco bien
/// </summary>

namespace Objetos
{
    class Proyectil : MonoBehaviour
    {
        public Movimiento Causante { get; set; }
        public Mecanicas Mecanicas { get; set; }
        public float Velocidad { get; set; }
        public Direccion Direccion { get; set; }
        public float Daño { get; set; }

        private void Update()
        {
            Mecanicas.Mover(transform, Direccion, Velocidad);
        }
    }
}
