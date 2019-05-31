using System.Collections.Generic;
using System.Linq;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters;
using Aquiris.PacMan.PathFind;
using Aquiris.PacMan.Level.Item;
using Characters.Enemies;
using UnityEngine;

namespace Aquiris.PacMan.Gameplay
{
    internal static partial class Gameplay
    {
        private static int _lives;

        private static GameObject _player;

        public static GameObject Player = _player ?? (_player = GameObject.FindWithTag(GameConstants.PLAYER_TAG));

        private static GameObject _enemies;

        public static GameObject Enemies = _enemies ?? (_enemies = GameObject.Find("PlayGround").transform.Find("Enemies").gameObject);

        public static List<EnemyCharacter> EnemyList => Enemies.GetComponentsInChildren<EnemyCharacter>().ToList();

        public static List<ItemPath> ItemPathsList => _pathList ?? (_pathList = GameObject.FindObjectsOfType<ItemPath>().ToList());
        
        private static List<ItemPath> _pathList;

        public static List<DotItem> DotItemsList => _dotItemsList ?? (_dotItemsList = GameObject.FindObjectsOfType<DotItem>().ToList());
        
        private static List<DotItem> _dotItemsList;

        private static List<PowerUpItem> _powerUps;

        public static List<PowerUpItem> PowerUpList => _powerUps ?? (_powerUps = GameObject.FindObjectsOfType<PowerUpItem>().ToList());

        private static EnemySpeed _currentEnemySpeed;
    }
}