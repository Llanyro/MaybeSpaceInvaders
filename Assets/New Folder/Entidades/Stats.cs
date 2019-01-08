using System;
using Sistema;
using Objetos;
using UnityEngine;
using Entidades.Player;

namespace Entidades.All
{
    [Serializable]
    struct Struct_Stats
    {
        /// <summary>
        /// Si el ID es mayor a 0 quiere decir que es un player
        /// Si el id es menor a 0 es un enemigo
        /// </summary>
        public int ID { get; set; }

        public int Salud { get; set; }
        public int MaxSalud { get; set; }

        public int Nivel { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }

        public Arma Arma;
        public float VelocidadMovimiento { get; set; }
    }

    class Stats : MonoBehaviour
    {
        //Referencias
        #region
        public Struct_Stats Struct_Stats;
        public ControlPlayer ControlPlayer { get; private set; }
        public SistemaDeControlGeneral SistemaDeControlGeneral { get; private set; }
        public Interfaz Interfaz { get; private set; }
        #endregion

        //
        #region
        private void PoderRecibirDaño()
        {

        }

        private void PoderRecibirCuracion()
        {

        }
        #endregion

        //Recibir
        #region
        /// <summary>
        /// Recibe una cantidad de experiencia
        /// Si la cantidad hace subir de nivel y sobra, se añadira la exp sobrante despues de subir de nivel
        /// Ejecuta VariacionExperiencia
        /// Si la exp es menor o igual a 0 sale directamente
        /// </summary>
        public void RecibirExperiencia(int exp)
        {
            if (exp <= 0) return;

            if(exp + Struct_Stats.Exp > Struct_Stats.MaxExp)
            {
                int sobrante = (exp + Struct_Stats.Exp) - Struct_Stats.MaxExp;
                Struct_Stats.Exp += Struct_Stats.MaxExp;
                VariacionExperiencia();
                RecibirExperiencia(sobrante);
                VariacionExperiencia();
            }
            else
            {
                Struct_Stats.Exp += exp;
                VariacionExperiencia();
            }
        }

        /// <summary>
        /// Quita vida a una entidad
        /// Ejecuta VariacionSalud
        /// </summary>
        public void RecibirDaño(int cantidad, Stats causante)
        {
            if (cantidad <= 0) return;

            Struct_Stats.Salud -= cantidad;

            VariacionSalud(causante);
        }

        /// <summary>
        /// Añade vida a una entidad
        /// Ejecuta VariacionSalud
        /// </summary>
        public void RecibirCuracion(int cantidad)
        {
            Struct_Stats.Salud += cantidad;
            VariacionSalud(null);
        }
        #endregion

        //Variaciones
        #region
        /// <summary>
        /// Realiza varias acciones segun el stat en concreto
        /// </summary>
        private void VariacionSalud(Stats causante)
        {
            if(Struct_Stats.Salud <= 0)
            {
                Struct_Stats.Salud = Struct_Stats.MaxSalud;//Esto es temporal
                Morir(causante);
            }
            else if(Struct_Stats.Salud > Struct_Stats.MaxSalud)
            {
                Struct_Stats.Salud = Struct_Stats.MaxSalud;
            }
            Interfaz.GUISalud(this);
        }
        /// <summary>
        /// Realiza varias acciones segun el stat en concreto
        /// </summary>
        private void VariacionExperiencia()
        {
            if (Struct_Stats.Exp <= 0)
            {
                Struct_Stats.Exp = 0;
            }
            else if (Struct_Stats.Exp >= Struct_Stats.MaxExp)
            {
                Struct_Stats.Exp = 0;
                SubirNivel();
            }
            Interfaz.GUIExperiencia(this);
        }

        #endregion

        //Otros
        #region
        private void SubirNivel()
        {
            Struct_Stats.Nivel++;
            Struct_Stats.MaxSalud += 5;
            Struct_Stats.MaxExp += 10;
            Interfaz.GUISalud(this);
            Interfaz.GUINivel(this);
        }

        private void Morir(Stats causante)
        {
            if (Struct_Stats.ID < 0)    //Si la entidad es un enemigo
            {
                if (causante != null)   //Si un player ha matado la entidad
                {
                    causante.RecibirExperiencia(1);
                    /*
                     Acciones que deba de realizarse antes de eliminar el objeto
                     */
                    Destroy(gameObject);
                }
            }
            else                        //Si la entidad es un player
            {
                SistemaDeControlGeneral.AñadirJugador(Struct_Stats.ID, false);
            }
        }

        #endregion

        //Iniciar entidad
        #region
        /// <summary>
        /// Añade el arma inicial al player
        /// </summary>
        private void AñadirArmaInicial()
        {
            Struct_Stats.Arma = new Arma()
            {
                Daño = SistemaDeControlGeneral.DañoProyectil,
                EnEnfriamiento = false,
                MaxRecalentamiento = 100,
                Recalentamiento = 0,
                TipoDeArma = TipoDeArma.Base,
                UltimaVezReposada = 0,
                VelocidadDeAtaque = 0.3f,
                UltimoAtaque = 0,
            };
        }

        /// <summary>
        /// Hace que la entidad suba al nivel 1
        /// </summary>
        private void InicialNivel()
        {
            Struct_Stats.Nivel = 1;
            Struct_Stats.Exp = 0;
            Struct_Stats.MaxExp = 10;
            Struct_Stats.MaxSalud = 100;
            Struct_Stats.Salud = 100;
        }

        public void IniciarPlayer(int ID, SistemaDeControlGeneral sistemaDeControlGeneral, Interfaz interfaz)
        {
            SistemaDeControlGeneral = sistemaDeControlGeneral;
            Interfaz = interfaz;

            //Iniciamos los controles
            ControlPlayer =  gameObject.AddComponent<ControlPlayer>();
            ControlPlayer.Stats = this;
            ControlPlayer.Mecanicas = new Mecanicas()
            {
                SistemaDeControlGeneral = sistemaDeControlGeneral,
            };

            //Iniciamos las stats
            Struct_Stats.ID = ID;
            Struct_Stats.VelocidadMovimiento = sistemaDeControlGeneral.VelocidadMovimientoPlayer;
            InicialNivel();

            //Iniciamos el arma
            AñadirArmaInicial();

            Interfaz.GUISalud(this);
            Interfaz.GUIExperiencia(this);
            Interfaz.GUINivel(this);
        }
        #endregion

    }
}
