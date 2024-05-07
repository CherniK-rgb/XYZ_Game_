using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Data;
using UnityEngine;

namespace Assets.Model 
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        public PlayerData _save;


        private void Awake()
        {
            if (IsSessionExit())
            {
                DestroyImmediate(gameObject); // если есть сессия то чтоб заменить на текущую нужно ее уничтожить
            }
            else
            {
                Save();
                DontDestroyOnLoad(this); // если сессии нет кроме текущей создает хранилище внутри сцен которое будет уничтожаться между загрузками
            }
        }
        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>(); // поиск в сцене объекта сессии

            foreach (var session in sessions)
            {
                if (session != this)
                    return true;
            }
            return false;
        }

        public void Save()
        {
            _save = _data.Clone();
        }

        public void LoadLastSave()
        {
            _save = _data.Clone();
        }
    }
}
