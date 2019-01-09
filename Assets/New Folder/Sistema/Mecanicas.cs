using Entidades.All;
using UnityEngine;
using Objetos;
using System.Collections.Generic;
using System;

namespace Sistema
{
    [Serializable]
    class Mecanicas
    {
        public SistemaDeControlGeneral SistemaDeControlGeneral { get; set; }

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

        //Armas
        #region
        //Disparar
        private void DispararProyectilBase(Stats causante, Transform posicionDelCausante, Vector2[] direcciones )
        {
            foreach(Vector2 direccion in direcciones)
            {
                //tamaño mapa 55 = 3 distancia
                float varDistancia = ((3 * SistemaDeControlGeneral.TamañoMapa)  / 55);

                //Inicializa el proyectil
                GameObject proyectilObj = UnityEngine.Object.Instantiate(SistemaDeControlGeneral.Proyectil1);

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
                proyectil.Mecanicas = new Mecanicas()
                {
                    SistemaDeControlGeneral = SistemaDeControlGeneral
                };
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
            if (causante.Struct_Stats.Arma.Recalentamiento < 0) return;
            if (causante.Struct_Stats.Arma.Recalentamiento == 0)
            {
                causante.Struct_Stats.Arma.EnEnfriamiento = false;
                return;
            }

            causante.Struct_Stats.Arma.UltimaVezReposada = Time.fixedTime;
            causante.Struct_Stats.Arma.Recalentamiento--;
        }
        #endregion


        public List<GameObject> enemigos = new List<GameObject>();

        //Armas especiales
        public void UsarArmaEspecial(Stats causante)
        {
            if (causante.Struct_Stats.ArmaEspecial.CargasRestantes <= 0) return;

            switch(causante.Struct_Stats.ArmaEspecial.TipoDeArmaEspecial)
            {
                case TipoDeArmaEspecial.CuraCompleta:
                    {
                        if (SistemaDeControlGeneral.Player1 != null)
                        {
                            Stats Stats = SistemaDeControlGeneral.Player1.GetComponent<Stats>();
                            Stats.Struct_Stats.Salud = Stats.Struct_Stats.MaxSalud;
                            Stats.RecibirCuracion(0);
                        }
                        if (SistemaDeControlGeneral.Player2 != null)
                        {
                            Stats Stats = SistemaDeControlGeneral.Player2.GetComponent<Stats>();
                            Stats.Struct_Stats.Salud = Stats.Struct_Stats.MaxSalud;
                            Stats.RecibirCuracion(0);
                        }
                    }
                    break;
                case TipoDeArmaEspecial.FullClear:
                    {
                        while (SistemaDeControlGeneral.EnemigosTipo1.Count != 0)
                        {
                            Stats Stats = SistemaDeControlGeneral.EnemigosTipo1[0].GetComponent<Stats>();
                            Stats.Struct_Stats.Salud = 0;
                            Stats.RecibirDaño(0, causante);
                        }
                        while (SistemaDeControlGeneral.EnemigosTipo2.Count != 0)
                        {
                            Stats Stats = SistemaDeControlGeneral.EnemigosTipo2[0].GetComponent<Stats>();
                            Stats.Struct_Stats.Salud = 0;
                            Stats.RecibirDaño(0, causante);
                        }
                    }
                    break;
                case TipoDeArmaEspecial.Clear:
                    {
                        /*Debug.DrawRay(causante.transform.position, causante.transform.forward);
                        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(causante.transform.position, causante.transform.forward);

                        //List<GameObject> enemigos = new List<GameObject>();

                        foreach (RaycastHit2D hit2D in raycastHits)
                            if (hit2D.transform.tag == "Enemigo") enemigos.Add(hit2D.transform.gameObject);



                        Debug.Log("Punto asdfv");
                        if (enemigos.Count > 0)
                        {
                            Debug.Log("Punto 1: " + enemigos.Count);
                            while (enemigos.Count != 0)
                            {
                                Stats Stats = enemigos[0].GetComponent<Stats>();
                                Stats.Struct_Stats.Salud = 0;
                                Stats.RecibirDaño(0, causante);
                                Debug.Log("Punto: " + enemigos.Count);
                                enemigos.Remove(enemigos[0]);
                                Debug.Log("Punto: " + enemigos.Count);
                            }
                        }*/
                    }
                    break;
            }

            causante.Struct_Stats.ArmaEspecial.CargasRestantes--;
        }

    }
}
