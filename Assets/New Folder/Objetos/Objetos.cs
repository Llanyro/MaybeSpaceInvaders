using System;
using Sistema;
using UnityEngine;
using Entidades.All;

/// <summary>
/// Este comentario tenia un poco de retraso
/// </summary>

enum TipoDeArma { Base, Tridireccional, OctaDireccional }
enum TipoDeArmaEspecial { CuraCompleta, Clear, FullClear }
enum TipoObjeto { ArmaBase, ArmaTridireccional, ArmaOctaDireccional, Curacion, Experiencia }
namespace Objetos
{
    class Proyectil : MonoBehaviour
    {
        public Stats Causante { get; set; }
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
            if(collision.tag == "Player")
            {
                Stats statstarjet = collision.GetComponent<Stats>();
                if (statstarjet.PoderRecibirDaño(Causante))
                {
                    statstarjet.RecibirDaño((int)Daño, Causante);
                    Destroy(gameObject);
                }
            }
            if (collision.tag == "Enemigo")
            {
                Stats statstarjet = collision.GetComponent<Stats>();
                if (statstarjet.PoderRecibirDaño(Causante))
                {
                    statstarjet.RecibirDaño((int)Daño, Causante);
                    Destroy(gameObject);
                }
            }
        }
    }

    [Serializable]
    class Objeto : MonoBehaviour
    {
        public TipoObjeto TipoObjeto { get; set; }
        public int Valor { get; set; }
        public Mecanicas Mecanicas { get; set; }
        public float Velocidad { get; set; }

        private void AplicarEfectoObjeto(Stats stats)
        {
            switch(TipoObjeto)
            {
                case TipoObjeto.ArmaBase:
                    if (stats.Struct_Stats.Arma.TipoDeArma == TipoDeArma.Base)
                    {
                        if (stats.Struct_Stats.Arma.Daño < 300)
                            stats.Struct_Stats.Arma.Daño += 10;
                    }
                    else
                    {
                        stats.Struct_Stats.Arma = new Arma()
                        {
                            Daño = stats.SistemaDeControlGeneral.DañoProyectil,
                            EnEnfriamiento = false,
                            MaxRecalentamiento = 100,
                            Recalentamiento = 0,
                            TipoDeArma = TipoDeArma.Base,
                            UltimaVezReposada = 0,
                            VelocidadDeAtaque = 0.3f,
                            UltimoAtaque = 0,
                        };
                    }
                    break;
                case TipoObjeto.ArmaTridireccional:
                    if (stats.Struct_Stats.Arma.TipoDeArma == TipoDeArma.Tridireccional)
                    {
                        if (stats.Struct_Stats.Arma.Daño < 300)
                            stats.Struct_Stats.Arma.Daño += 10;
                    }
                    else
                    {
                        stats.Struct_Stats.Arma = new Arma()
                        {
                            Daño = stats.SistemaDeControlGeneral.DañoProyectil,
                            EnEnfriamiento = false,
                            MaxRecalentamiento = 100,
                            Recalentamiento = 0,
                            TipoDeArma = TipoDeArma.Tridireccional,
                            UltimaVezReposada = 0,
                            VelocidadDeAtaque = 0.4f,
                            UltimoAtaque = 0,
                        };
                    }
                    break;
                case TipoObjeto.ArmaOctaDireccional:
                    if (stats.Struct_Stats.Arma.TipoDeArma == TipoDeArma.OctaDireccional)
                    {
                        if (stats.Struct_Stats.Arma.Daño < 300)
                            stats.Struct_Stats.Arma.Daño += 10;
                    }
                    else
                    {
                        stats.Struct_Stats.Arma = new Arma()
                        {
                            Daño = stats.SistemaDeControlGeneral.DañoProyectil,
                            EnEnfriamiento = false,
                            MaxRecalentamiento = 100,
                            Recalentamiento = 0,
                            TipoDeArma = TipoDeArma.OctaDireccional,
                            UltimaVezReposada = 0,
                            VelocidadDeAtaque = 0.5f,
                            UltimoAtaque = 0,
                        };
                    }
                    break;
                case TipoObjeto.Curacion:
                    stats.RecibirCuracion(Valor);
                    break;
                case TipoObjeto.Experiencia:
                    stats.RecibirExperiencia(Valor);
                    break;
            }

            Destroy(gameObject);
        }

        private void Update()
        {
            Mecanicas.Mover(transform, new Vector2(0,-1), Velocidad);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Muro") Destroy(gameObject);
            if (collision.tag == "Player") AplicarEfectoObjeto(collision.GetComponent<Stats>());
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

    [Serializable]
    class ArmaEspecial
    {
        public TipoDeArmaEspecial TipoDeArmaEspecial { get; set; }
        public int CargasRestantes { get; set; }
    }

}
