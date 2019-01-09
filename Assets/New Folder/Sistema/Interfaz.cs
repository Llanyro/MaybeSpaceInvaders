using System;
using Objetos;
using UnityEngine;
using Entidades.All;
using UnityEngine.UI;

namespace Sistema
{
    class Interfaz : MonoBehaviour
    {
        //Player1
        #region
        public GameObject Player1_IniciarPLayer;
        public GameObject Player1_PadreGui;

        public GameObject Player1_BG_Barra_Salud;
        public GameObject Player1_Barra_Salud;
        public GameObject Player1_TextoSalud;

        public GameObject Player1_BG_Barra_Experiencia;
        public GameObject Player1_Barra_Experiencia;
        public GameObject Player1_TextoExperiencia;

        public GameObject Player1_TextoNivel;

        public GameObject Player1_BG_Barra_Recalentamiento;
        public GameObject Player1_Barra_Recalentamiento;
        public GameObject Player1_TextoRecalentamiento;
        public GameObject Player1_TextoArmaEspecial;

        #endregion

        //Player2
        #region
        public GameObject Player2_IniciarPLayer;
        public GameObject Player2_PadreGui;

        public GameObject Player2_BG_Barra_Salud;
        public GameObject Player2_Barra_Salud;
        public GameObject Player2_TextoSalud;

        public GameObject Player2_BG_Barra_Experiencia;
        public GameObject Player2_Barra_Experiencia;
        public GameObject Player2_TextoExperiencia;

        public GameObject Player2_TextoNivel;

        public GameObject Player2_BG_Barra_Recalentamiento;
        public GameObject Player2_Barra_Recalentamiento;
        public GameObject Player2_TextoRecalentamiento;
        public GameObject Player2_TextoArmaEspecial;

        #endregion

        //Interfaz players
        #region
        public void IniciarGUIPlayer(Entidad entidad, bool iniciar)
        {
            switch(entidad)
            {
                case Entidad.Player1:
                    Player1_PadreGui.SetActive(iniciar);
                    Player1_IniciarPLayer.SetActive(!iniciar);
                    break;
                case Entidad.Player2:
                    Player2_PadreGui.SetActive(iniciar);
                    Player2_IniciarPLayer.SetActive(!iniciar);
                    break;
            }
        }
        public void ActualizarGUI(Stats stats)
        {
            IniciarGUIPlayer(stats.Struct_Stats.Entidad, true);
            GUISalud(stats);
            GUIExperiencia(stats);
            GUINivel(stats);
            GUIArma(stats);
        }

        public void GUIArma(Stats stats)
        {
            switch (stats.Struct_Stats.Entidad)
            {
                case Entidad.Player1:
                    Player1_Barra_Recalentamiento.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Arma.Recalentamiento / (float)stats.Struct_Stats.Arma.MaxRecalentamiento);
                    Player1_TextoRecalentamiento.GetComponent<Text>().text = stats.Struct_Stats.Arma.Recalentamiento.ToString();
                    Player1_TextoArmaEspecial.GetComponent<Text>().text = 
                        Enum.GetName(typeof(TipoDeArmaEspecial), stats.Struct_Stats.ArmaEspecial.TipoDeArmaEspecial) +
                        ": " + stats.Struct_Stats.ArmaEspecial.CargasRestantes;
                    break;
                case Entidad.Player2:
                    Player2_Barra_Recalentamiento.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Arma.Recalentamiento / (float)stats.Struct_Stats.Arma.MaxRecalentamiento);
                    Player2_TextoRecalentamiento.GetComponent<Text>().text = stats.Struct_Stats.Arma.Recalentamiento.ToString();
                    Player2_TextoArmaEspecial.GetComponent<Text>().text = 
                        Enum.GetName(typeof(TipoDeArmaEspecial), stats.Struct_Stats.ArmaEspecial.TipoDeArmaEspecial) +
                        ": " + stats.Struct_Stats.ArmaEspecial.CargasRestantes;
                    break;
            }
        }
        public void GUISalud(Stats stats)
        {
            switch(stats.Struct_Stats.Entidad)
            {
                case Entidad.Player1:
                    Player1_Barra_Salud.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Salud / (float)stats.Struct_Stats.MaxSalud);
                    Player1_TextoSalud.GetComponent<Text>().text = stats.Struct_Stats.Salud + " / " + stats.Struct_Stats.MaxSalud;
                    break;
                case Entidad.Player2:
                    Player2_Barra_Salud.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Salud / (float)stats.Struct_Stats.MaxSalud);
                    Player2_TextoSalud.GetComponent<Text>().text = stats.Struct_Stats.Salud + " / " + stats.Struct_Stats.MaxSalud;
                    break;
            }
        }
        public void GUIExperiencia(Stats stats)
        {
            switch (stats.Struct_Stats.Entidad)
            {
                case Entidad.Player1:
                    Player1_Barra_Experiencia.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Exp / (float)stats.Struct_Stats.MaxExp);
                    Player1_TextoExperiencia.GetComponent<Text>().text = stats.Struct_Stats.Exp + " / " + stats.Struct_Stats.MaxExp;
                    break;
                case Entidad.Player2:
                    Player2_Barra_Experiencia.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Exp / (float)stats.Struct_Stats.MaxExp);
                    Player2_TextoExperiencia.GetComponent<Text>().text = stats.Struct_Stats.Exp + " / " + stats.Struct_Stats.MaxExp;
                    break;
            }
        }
        public void GUINivel(Stats stats)
        {
            switch (stats.Struct_Stats.Entidad)
            {
                case Entidad.Player1:
                    Player1_TextoNivel.GetComponent<Text>().text = "Nivel: " + stats.Struct_Stats.Nivel;
                    break;
                case Entidad.Player2:
                    Player2_TextoNivel.GetComponent<Text>().text = "Nivel: " + stats.Struct_Stats.Nivel;
                    break;
            }
        }
        #endregion
    }
}
