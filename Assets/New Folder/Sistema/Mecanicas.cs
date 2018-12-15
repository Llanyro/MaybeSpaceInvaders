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
        #region
        /// <summary>
        /// Mueve un objeto de manera vertical segun el valor que decidammos
        /// </summary>
        private void MovimientoVertical(Transform transformObjeto, float value)
        {
            if (value > 0) if ((transformObjeto.position.y + (Time.deltaTime * value)) >= SistemaDeControlGeneral.TamañoMapa) return;
            if (value < 0) if ((transformObjeto.position.y + (Time.deltaTime * value)) <= -SistemaDeControlGeneral.TamañoMapa) return;

            transformObjeto.Translate(0, Time.deltaTime * value, 0);
        }
        /// <summary>
        /// Mueve un objeto de manera horizontal segun el valor que decidammos
        /// </summary>
        private void MovimientoHorizontal(Transform transformObjeto, float value)
        {
            if (value > 0) if ((transformObjeto.position.x + (Time.deltaTime * value)) >= SistemaDeControlGeneral.TamañoMapa) return;
            if (value < 0) if ((transformObjeto.position.x + (Time.deltaTime * value)) <= -SistemaDeControlGeneral.TamañoMapa) return;

            transformObjeto.Translate(Time.deltaTime * value, 0, 0);
        }


        public void Mover(Transform transformObjeto, Direccion direccion, float value)
        {
            switch(direccion)
            {
                case (Direccion)1:
                    MovimientoVertical(transformObjeto, value);
                    break;
                case (Direccion)2:
                    MovimientoVertical(transformObjeto, -1 * value);
                    break;
                case (Direccion)3:
                    MovimientoHorizontal(transformObjeto, value);
                    break;
                case (Direccion)4:
                    MovimientoHorizontal(transformObjeto, -1 *  value);
                    break;
                case (Direccion)5:
                    MovimientoVertical(transformObjeto, value);
                    MovimientoHorizontal(transformObjeto, value);
                    break;
                case (Direccion)6:
                    MovimientoVertical(transformObjeto, -1 * value);
                    MovimientoHorizontal(transformObjeto, value);
                    break;
                case (Direccion)7:
                    MovimientoVertical(transformObjeto, value);
                    MovimientoHorizontal(transformObjeto, -1 * value);
                    break;
                case (Direccion)8:
                    MovimientoVertical(transformObjeto, -1 * value);
                    MovimientoHorizontal(transformObjeto, -1 * value);
                    break;
            }
        }

        #endregion

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
