using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gustavo.GameMechanics
{
    
    public class SceneController : MonoBehaviour
    {
        private GameObject _player;

        public bool playerDied;

        //Texto de energia
        public Text gameoverText;

        private GameObject _bossObject; //Para desativar ele depois de acabar

        // Start is called before the first frame update
        void Awake()
        {
            _bossObject = GameObject.Find("Boss");

            _player = GameObject.Find("Player");
            gameoverText.gameObject.SetActive(false);
        }

        public void WinCondition()//Checar a win condigiotn do jogo (torretas do boss morrerem)
        {
            _bossObject.SetActive(false);
            StartCoroutine(GameOver());//Por coincidencia a vitoria e derrota aparecem da mesma forma (texto de game over seguido por reset da cena) //Mas seria possivel fazer diferente
        }

        public void LoseCondition() //São publicas as duas pq sao referenciadas pelo resto do jogo 
        {
            StartCoroutine(GameOver());
        }

        IEnumerator GameOver()
        {
            _player.SetActive(false);
            gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(0);
        }
    }
}