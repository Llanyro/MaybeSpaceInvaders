using Player;
using UnityEngine;
using Objetos;

namespace Sistema
{
    class Mecanicas
    {
        public SistemaDeControlGeneral SistemaDeControlGeneral { get; set; }

        //Movimiento de los objetos, players y Enemigos
        //public void Mover(Transform transform, Direccion direccion, float VelocidadMovimiento);
        public void Mover(Transform transform, Vector2 direccion, float VelocidadMovimiento)
        {
            transform.Translate(direccion*VelocidadMovimiento);
        }

        //Disparar
        private void DispararProyectilBase(Movimiento causante, float dañoproyectil, Transform posicionDelCausante, Vector2[] direcciones )
        {
            foreach(Vector2 direccion in direcciones)
            {
                //tamaño mapa 55 = 3 distancia
                float varDistancia = ((3 * SistemaDeControlGeneral.TamañoMapa)  / 55);

                //Inicializa el proyectil
                GameObject proyectil;
                proyectil = Object.Instantiate(SistemaDeControlGeneral.Proyectil1);
                proyectil.transform.localScale = new Vector3(SistemaDeControlGeneral.EscalaX / 10, SistemaDeControlGeneral.EscalaY / 10, 0);
                proyectil.GetComponent<Proyectil>().Causante = causante;
                proyectil.GetComponent<Proyectil>().Mecanicas = this;
                proyectil.GetComponent<Proyectil>().Velocidad = SistemaDeControlGeneral.VelocidadMovimientoProyectil;
                proyectil.GetComponent<Proyectil>().Direccion = direccion;

                //Posiciona el proyectil segun la direccion del movimiento
                Vector3 postemp = posicionDelCausante.position;
                postemp.x += (varDistancia * direccion.x);
                postemp.y += (varDistancia * direccion.y);

                proyectil.transform.position = postemp;
                proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);

                //Activa el proyectil
                proyectil.SetActive(true);
            }
        }

        public void UsarArma(Arma arma, Movimiento causante)
        {
            Vector2[] direcciones;
            switch (arma.TipoDeArma)
            {
                case 0:
                    direcciones = new Vector2[1];
                    direcciones[0] = new Vector2(1,0);
                    DispararProyectilBase(causante, arma.Daño, causante.transform, direcciones);
                    break;
                case (TipoDeArma)1:
                    /*direcciones = new Vector2[3];
                    direcciones[0] = (Vector2)1;
                    direcciones[1] = (Vector2)5;
                    direcciones[2] = (Vector2)8;*/
                    //DispararProyectilBase(causante, arma.Daño, causante.transform, direcciones);
                    break;
                case (TipoDeArma)2:
                    /*direcciones = new Vector2[8];
                    direcciones[0] = (Vector2)1;
                    direcciones[1] = (Vector2)2;
                    direcciones[2] = (Vector2)3;
                    direcciones[3] = (Vector2)4;
                    direcciones[4] = (Vector2)5;
                    direcciones[5] = (Vector2)6;
                    direcciones[6] = (Vector2)7;
                    direcciones[7] = (Vector2)8;*/
                    //DispararProyectilBase(causante, arma.Daño, causante.transform, direcciones);
                    break;
            }
        }
    }


}
