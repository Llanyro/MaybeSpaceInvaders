using Entidades.All;
using UnityEngine;
using Objetos;

namespace Sistema
{
    class Mecanicas
    {
        public SistemaDeControlGeneral SistemaDeControlGeneral { get; set; }

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

        //Movimiento de los objetos, players y Enemigos
        //public void Mover(Transform transform, Direccion direccion, float VelocidadMovimiento);
        //Se adapta a los fps de la maquina
        public void Mover(Transform transform, Vector2 direccion, float VelocidadMovimiento)
        {
            //Si esta al borde del mapa, no se permite continuar
            #region
            if (direccion.x > 0)
            {
                if ((transform.position.x + (Time.deltaTime * 1 * VelocidadMovimiento)) >= SistemaDeControlGeneral.TamañoMapa) direccion.x = 0;
            }
            else if (direccion.x < 0)
            {
                if ((transform.position.x + (Time.deltaTime * -1 * VelocidadMovimiento)) <= -SistemaDeControlGeneral.TamañoMapa) direccion.x = 0;
            }

            if (direccion.y > 0)
            {
                if ((transform.position.y + (Time.deltaTime * 1 * VelocidadMovimiento)) >= SistemaDeControlGeneral.TamañoMapa) direccion.y = 0;
            }
            else if (direccion.y < 0)
            {
                if ((transform.position.y + (Time.deltaTime * -1 * VelocidadMovimiento)) <= -SistemaDeControlGeneral.TamañoMapa) direccion.y = 0;
            }
            #endregion

            transform.Translate(direccion * VelocidadMovimiento * Time.deltaTime);
        }

        //Disparar
        private void DispararProyectilBase(Stats causante, Transform posicionDelCausante, Vector2[] direcciones )
        {
            foreach(Vector2 direccion in direcciones)
            {
                //tamaño mapa 55 = 3 distancia
                float varDistancia = ((3 * SistemaDeControlGeneral.TamañoMapa)  / 55);

                //Inicializa el proyectil
                GameObject proyectilObj = Object.Instantiate(SistemaDeControlGeneral.Proyectil1);

                //Posicion, rotacion y escala
                #region
                Vector3 postemp = posicionDelCausante.position;
                postemp.x += (varDistancia * direccion.x);
                postemp.y += (varDistancia * direccion.y);
                proyectilObj.transform.position = postemp;
                proyectilObj.transform.rotation = Quaternion.Euler(0, 0, 0);
                proyectilObj.transform.localScale = new Vector3(SistemaDeControlGeneral.EscalaX / 10, SistemaDeControlGeneral.EscalaY / 10, 0);
                #endregion

                //Iniciar script
                #region
                Proyectil proyectil = proyectilObj.AddComponent<Proyectil>();
                proyectil.Causante = causante;
                proyectil.Mecanicas = this;
                proyectil.Velocidad = SistemaDeControlGeneral.VelocidadMovimientoProyectil;
                proyectil.Direccion = direccion;
                proyectil.Daño = causante.Struct_Stats.Arma.Daño;
                #endregion

                //Activa el proyectil
                proyectilObj.SetActive(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invertir"> Causa que las direcciones se inviertan </param>
        public void UsarArma(Stats causante, bool invertir)
        {
            if (causante.Struct_Stats.Arma.UltimoAtaque + causante.Struct_Stats.Arma.VelocidadDeAtaque > Time.fixedTime) return;
            if (causante.Struct_Stats.Arma.EnEnfriamiento)
            {
                ReposarArma(causante);
                return;
            }

            Arma arma = causante.Struct_Stats.Arma;

            Vector2[] direcciones;
            switch (arma.TipoDeArma)
            {
                case TipoDeArma.Base:
                    {
                        direcciones = new Vector2[1];
                        if(invertir) direcciones[0] = new Vector2(0, -1);
                        else direcciones[0] = new Vector2(0, 1);
                        DispararProyectilBase(causante, causante.transform, direcciones);
                        arma.Recalentamiento++;
                    }
                    break;
                case TipoDeArma.Tridireccional:
                    {
                        direcciones = new Vector2[3];
                        if(invertir)
                        {
                            direcciones[0] = new Vector2(0, -1);
                            direcciones[1] = new Vector2(-1, -1);
                            direcciones[2] = new Vector2(1, -1);
                        }
                        else
                        {
                            direcciones[0] = new Vector2(0, 1);
                            direcciones[1] = new Vector2(1, 1);
                            direcciones[2] = new Vector2(-1, 1);
                        }
                        DispararProyectilBase(causante, causante.transform, direcciones);
                        arma.Recalentamiento += 3;
                    }
                    break;
                    //Este arma no es necesario invertila, ya que dispara en todas las direcciones
                case TipoDeArma.OctaDireccional:
                    {
                        direcciones = new Vector2[8];
                        direcciones[0] = new Vector2(0, 1);
                        direcciones[1] = new Vector2(1, 1);
                        direcciones[2] = new Vector2(-1, 1);
                        direcciones[3] = new Vector2(1, 0);
                        direcciones[4] = new Vector2(-1, 0);
                        direcciones[5] = new Vector2(1, -1);
                        direcciones[6] = new Vector2(-1, -1);
                        direcciones[7] = new Vector2(0, -1);
                        DispararProyectilBase(causante, causante.transform, direcciones);
                        arma.Recalentamiento += 8;
                    }
                    break;
            }

            arma.UltimoAtaque = Time.fixedTime;
            if (arma.Recalentamiento >= arma.MaxRecalentamiento) arma.EnEnfriamiento = true;
        }

        public void ReposarArma(Stats causante)
        {
            if (causante.Struct_Stats.Arma.UltimaVezReposada + 0.1f > Time.fixedTime) return;
            if (causante.Struct_Stats.Arma.Recalentamiento <= 0) return;

            causante.Struct_Stats.Arma.UltimaVezReposada = Time.fixedTime;
            causante.Struct_Stats.Arma.Recalentamiento--;
        }
    }
}
