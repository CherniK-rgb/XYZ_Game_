using System.Collections;
using System.Collections.Generic;
using Assets.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            
            SceneManager.LoadScene(_sceneName);
        }
    }
}