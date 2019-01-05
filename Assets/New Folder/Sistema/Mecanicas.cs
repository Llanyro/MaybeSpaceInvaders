using Player;
using UnityEngine;
using Objetos;

enum Direccion { Ninguna, Arriba, Abajo, Derecha, Izquierda, ArribaDerecha, AbajoDerecha, ArribaIzquierda, AbajoIzquierda }

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
        private void DispararProyectilBase(Movimiento causante, float dañoproyectil, Transform posicionDelCausante, Direccion[] direcciones )
        {
            foreach(Direccion direccion in direcciones)
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
                switch (direccion)
                {
                    case 0:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x, posicionDelCausante.position.y, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (Direccion)1:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x, posicionDelCausante.position.y + varDistancia, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (Direccion)2:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x, posicionDelCausante.position.y - varDistancia, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (Direccion)3:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x + varDistancia, posicionDelCausante.position.y, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (Direccion)4:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x - varDistancia, posicionDelCausante.position.y, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (Direccion)5:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x + varDistancia, posicionDelCausante.position.y + varDistancia, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (Direccion)6:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x + varDistancia, posicionDelCausante.position.y - varDistancia, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (Direccion)7:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x - varDistancia, posicionDelCausante.position.y + varDistancia, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (Direccion)8:
                        proyectil.transform.position = new Vector3(posicionDelCausante.position.x - varDistancia, posicionDelCausante.position.y - varDistancia, 0);
                        proyectil.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                }

                //Activa el proyectil
                proyectil.SetActive(true);
            }
        }

        public void UsarArma(Arma arma, Movimiento causante)
        {
            Direccion[] direcciones;
            switch (arma.TipoDeArma)
            {
                case 0:
                    direcciones = new Direccion[1];
                    direcciones[0] = (Direccion)1;
                    DispararProyectilBase(causante, arma.Daño, causante.transform, direcciones);
                    break;
                case (TipoDeArma)1:
                    direcciones = new Direccion[3];
                    direcciones[0] = (Direccion)1;
                    direcciones[1] = (Direccion)5;
                    direcciones[2] = (Direccion)8;
                    DispararProyectilBase(causante, arma.Daño, causante.transform, direcciones);
                    break;
                case (TipoDeArma)2:
                    direcciones = new Direccion[8];
                    direcciones[0] = (Direccion)1;
                    direcciones[1] = (Direccion)2;
                    direcciones[2] = (Direccion)3;
                    direcciones[3] = (Direccion)4;
                    direcciones[4] = (Direccion)5;
                    direcciones[5] = (Direccion)6;
                    direcciones[6] = (Direccion)7;
                    direcciones[7] = (Direccion)8;
                    DispararProyectilBase(causante, arma.Daño, causante.transform, direcciones);
                    break;
            }
        }
    }


}
