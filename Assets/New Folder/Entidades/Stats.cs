using System;
using Sistema;
using Objetos;
using UnityEngine;
using Entidades.Player;
using Entidades.Enemigos;

namespace Entidades.All
{
    [Serializable]
    struct Struct_Stats
    {
        /// <summary>
        /// Si el ID es mayor a 0 quiere decir que es un player
        /// Si el id es menor a 0 es un enemigo
        /// </summary>
        public Entidad Entidad { get; set; }

        public int Salud { get; set; }
        public int MaxSalud { get; set; }

        public int Nivel { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }

        public Arma Arma;
        public ArmaEspecial ArmaEspecial;
        public float VelocidadMovimiento { get; set; }
    }

    class Stats : MonoBehaviour
    {
        //Referencias
        #region
        public Struct_Stats Struct_Stats;
        public ControlPlayer ControlPlayer { get; private set; }
        public ControlEnemigo ControlEnemigo { get; private set; }
        public SistemaDeControlGeneral SistemaDeControlGeneral { get; private set; }
        public Interfaz Interfaz { get; private set; }
        #endregion

        //
        #region
        public bool PoderRecibirDaño(Stats causante)
        {
            //Si no es una entidad viviente
            if (causante == null) return true;
            //Si es uno mismo
            if (causante == this) return false;

            switch(Struct_Stats.Entidad)
            {
                case Entidad.Player1:
                case Entidad.Player2:
                    //Si es un player
                    if (causante.Struct_Stats.Entidad == Entidad.Player1) return false;
                    else if (causante.Struct_Stats.Entidad == Entidad.Player2) return false;
                    //Si es un enemigo
                    else return true;

                case Entidad.Enemigo1:
                case Entidad.Enemigo2:
                    //Si es un player
                    if (causante.Struct_Stats.Entidad == Entidad.Player1) return true;
                    else if (causante.Struct_Stats.Entidad == Entidad.Player2) return true;
                    //Si es un enemigo
                    else return false;
            }

            return false;
        }

        private void PoderRecibirCuracion(Stats causante)
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
            Struct_Stats.Salud += 5;
            Struct_Stats.MaxExp += 10;
            Interfaz.GUISalud(this);
            Interfaz.GUINivel(this);

            if (Struct_Stats.Nivel % 10 == 0) Struct_Stats.ArmaEspecial.CargasRestantes++;
        }

        /// <summary>
        /// Elimina la entidad y le da experiencia a causante de la muerte en caso de ser un enemigo y el causante no ser null
        /// </summary>
        private void Morir(Stats causante)
        {
            if(causante != null)
            {
                switch (Struct_Stats.Entidad)
                {
                    case Entidad.Enemigo1:
                        causante.RecibirExperiencia(10);
                        DropObjetoAleatorio(2);
                        break;
                    case Entidad.Enemigo2:
                        causante.RecibirExperiencia(20);
                        DropObjetoAleatorio(2);
                        break;
                }
            }

            SistemaDeControlGeneral.EliminarEntidad(this);
        }

        private void DropObjetoAleatorio(int rango)
        {
            if (UnityEngine.Random.Range(0, rango) != 0) return;

            switch(UnityEngine.Random.Range(0, Enum.GetNames(typeof(TipoObjeto)).Length))
            {
                case (int)TipoObjeto.ArmaBase:
                    SistemaDeControlGeneral.InstanciarObjeto(TipoObjeto.ArmaBase, 0, transform.position, SistemaDeControlGeneral.SpritesArmas[0]);
                    break;
                case (int)TipoObjeto.ArmaTridireccional:
                    SistemaDeControlGeneral.InstanciarObjeto(TipoObjeto.ArmaTridireccional, 0, transform.position, SistemaDeControlGeneral.SpritesArmas[1]);
                    break;
                case (int)TipoObjeto.ArmaOctaDireccional:
                    SistemaDeControlGeneral.InstanciarObjeto(TipoObjeto.ArmaOctaDireccional, 0, transform.position, SistemaDeControlGeneral.SpritesArmas[2]);
                    break;
                case (int)TipoObjeto.Curacion:
                    switch(UnityEngine.Random.Range(0, 1))
                    {
                        case 0:
                            SistemaDeControlGeneral.InstanciarObjeto(TipoObjeto.Curacion, 10, transform.position, SistemaDeControlGeneral.SpritesCuras[0]);
                            break;
                        case 1:
                            SistemaDeControlGeneral.InstanciarObjeto(TipoObjeto.Curacion, 50, transform.position, SistemaDeControlGeneral.SpritesCuras[1]);
                            break;
                    }
                    break;
                case (int)TipoObjeto.Experiencia:
                    SistemaDeControlGeneral.InstanciarObjeto(TipoObjeto.Experiencia, 10, transform.position, SistemaDeControlGeneral.SpritesExperiencia[0]);
                    break;

            }
        }
        #endregion

        //Iniciar entidad
        #region

        //Armas
        #region
        /// <summary>
        /// Añade el arma inicial a la entidad
        /// </summary>
        private void AñadirArma1()
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
        /// Añade el arma inicial a la entidad
        /// </summary>
        private void AñadirArma2()
        {
            Struct_Stats.Arma = new Arma()
            {
                Daño = SistemaDeControlGeneral.DañoProyectil,
                EnEnfriamiento = false,
                MaxRecalentamiento = 100,
                Recalentamiento = 0,
                TipoDeArma = TipoDeArma.Tridireccional,
                UltimaVezReposada = 0,
                VelocidadDeAtaque = 0.3f,
                UltimoAtaque = 0,
            };
        }

        #endregion

        //Armas especiales
        #region
        private void ArmaEspecialCura()
        {
            Struct_Stats.ArmaEspecial = new ArmaEspecial()
            {
                CargasRestantes = 1,
                TipoDeArmaEspecial = TipoDeArmaEspecial.CuraCompleta
            };
        }
        private void ArmaEspecialFullClear()
        {
            Struct_Stats.ArmaEspecial = new ArmaEspecial()
            {
                CargasRestantes = 1,
                TipoDeArmaEspecial = TipoDeArmaEspecial.FullClear
            };
        }
        private void ArmaEspecialClear()
        {
            Struct_Stats.ArmaEspecial = new ArmaEspecial()
            {
                CargasRestantes = 1,
                TipoDeArmaEspecial = TipoDeArmaEspecial.Clear
            };
        }

        #endregion

        //Stats por nivel
        #region
        /// <summary>
        /// Hace que la entidad suba al nivel 1
        /// </summary>
        private void InicialNivel1()
        {
            Struct_Stats.Nivel = 1;
            Struct_Stats.Exp = 0;
            Struct_Stats.MaxExp = 10;
            Struct_Stats.MaxSalud = 100;
            Struct_Stats.Salud = 100;
        }

        /// <summary>
        /// Hace que la entidad suba al nivel 1
        /// </summary>
        private void InicialNivel10()
        {
            Struct_Stats.Nivel = 10;
            Struct_Stats.Exp = 0;
            Struct_Stats.MaxExp = 100;
            Struct_Stats.MaxSalud = 200;
            Struct_Stats.Salud = 200;
        }

        /// <summary>
        /// Hace que la entidad suba al nivel 100
        /// </summary>
        private void InicialNivelMax()
        {
            Struct_Stats.Nivel = 100;
            Struct_Stats.Exp = 0;
            Struct_Stats.MaxExp = 10;
            Struct_Stats.MaxSalud = 32767;
            Struct_Stats.Salud = 32767;
        }

        #endregion

        public void IniciarPlayer(Entidad Entidad, SistemaDeControlGeneral sistemaDeControlGeneral, Interfaz interfaz)
        {
            SistemaDeControlGeneral = sistemaDeControlGeneral;
            Interfaz = interfaz;

            //Iniciamos las stats
            Struct_Stats.Entidad = Entidad;
            Struct_Stats.VelocidadMovimiento = sistemaDeControlGeneral.VelocidadMovimientoPlayer;

            //Si es un player
            switch(Entidad)
            {
                case Entidad.Player1:
                case Entidad.Player2:
                    {
                        //Iniciamos los controles
                        ControlPlayer = gameObject.AddComponent<ControlPlayer>();
                        ControlPlayer.Stats = this;
                        ControlPlayer.Mecanicas = new Mecanicas()
                        {
                            SistemaDeControlGeneral = sistemaDeControlGeneral,
                        };

                        InicialNivel1();
                        AñadirArma1();
                        ArmaEspecialFullClear();
                    }
                    break;
                case Entidad.Enemigo1:
                    {
                        //Iniciamos los controles
                        ControlEnemigo = gameObject.AddComponent<ControlEnemigo>();
                        ControlEnemigo.Stats = this;
                        ControlEnemigo.Mecanicas = new Mecanicas()
                        {
                            SistemaDeControlGeneral = sistemaDeControlGeneral,
                        };

                        InicialNivel1();
                        AñadirArma1();
                        ArmaEspecialCura();
                    }
                    break;
                case Entidad.Enemigo2:
                    {
                        //Iniciamos los controles
                        ControlEnemigo = gameObject.AddComponent<ControlEnemigo>();
                        ControlEnemigo.Stats = this;
                        ControlEnemigo.Mecanicas = new Mecanicas()
                        {
                            SistemaDeControlGeneral = sistemaDeControlGeneral,
                        };

                        InicialNivel10();
                        AñadirArma2();
                        ArmaEspecialCura();
                    }
                    break;
            }
           //Actualiza la interfaz de la entidad
            Interfaz.ActualizarGUI(this);
        }
        #endregion



    }
}
