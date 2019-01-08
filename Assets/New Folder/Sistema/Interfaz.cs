using Objetos;
using UnityEngine;
using UnityEngine.UI;
using Entidades.All;

namespace Sistema
{
    class Interfaz : MonoBehaviour
    {
        //Player1
        #region
        public GameObject Player1_PadreGui;

        public GameObject Player1_BG_Barra_Salud;
        public GameObject Player1_Barra_Salud;
        public GameObject Player1_TextoSalud;

        public GameObject Player1_BG_Barra_Experiencia;
        public GameObject Player1_Barra_Experiencia;
        public GameObject Player1_TextoExperiencia;

        public GameObject Player1_TextoNivel;
        #endregion

        //Player2
        #region
        public GameObject Player2_PadreGui;

        public GameObject Player2_BG_Barra_Salud;
        public GameObject Player2_Barra_Salud;
        public GameObject Player2_TextoSalud;

        public GameObject Player2_BG_Barra_Experiencia;
        public GameObject Player2_Barra_Experiencia;
        public GameObject Player2_TextoExperiencia;

        public GameObject Player2_TextoNivel;
        #endregion

        //Interfaz players
        #region
        public void IniciarGUIPlayer(int ID, bool iniciar)
        {
            if (ID == 1) Player1_PadreGui.SetActive(iniciar);
            else if (ID == 2) Player2_PadreGui.SetActive(iniciar);
        }

        public void GUISalud(Stats stats)
        {
            switch(stats.Struct_Stats.ID)
            {
                case 1:
                    Player1_Barra_Salud.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Salud / (float)stats.Struct_Stats.MaxSalud);
                    Player1_TextoSalud.GetComponent<Text>().text = stats.Struct_Stats.Salud + " / " + stats.Struct_Stats.MaxSalud;
                    break;
                case 2:
                    Player2_Barra_Salud.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Salud / (float)stats.Struct_Stats.MaxSalud);
                    Player2_TextoSalud.GetComponent<Text>().text = stats.Struct_Stats.Salud + " / " + stats.Struct_Stats.MaxSalud;
                    break;
            }
        }

        public void GUIExperiencia(Stats stats)
        {
            switch (stats.Struct_Stats.ID)
            {
                case 1:
                    Player1_Barra_Experiencia.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Exp / (float)stats.Struct_Stats.MaxExp);
                    Player1_TextoExperiencia.GetComponent<Text>().text = stats.Struct_Stats.Exp + " / " + stats.Struct_Stats.MaxExp;
                    break;
                case 2:
                    Player2_Barra_Experiencia.GetComponent<Image>().fillAmount = (stats.Struct_Stats.Exp / (float)stats.Struct_Stats.MaxExp);
                    Player2_TextoExperiencia.GetComponent<Text>().text = stats.Struct_Stats.Exp + " / " + stats.Struct_Stats.MaxExp;
                    break;
            }
        }

        public void GUINivel(Stats stats)
        {
            switch (stats.Struct_Stats.ID)
            {
                case 1:
                    Player1_TextoNivel.GetComponent<Text>().text = "Nivel: " + stats.Struct_Stats.Nivel;
                    break;
                case 2:
                    Player2_TextoNivel.GetComponent<Text>().text = "Nivel: " + stats.Struct_Stats.Nivel;
                    break;
            }
        }
        #endregion
    }
}
