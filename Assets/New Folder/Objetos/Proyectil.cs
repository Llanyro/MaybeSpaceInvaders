using Player;
using Sistema;
using UnityEngine;

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
