﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace mvc_rpg.Entities
{
    public class Enemy
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isAlive { get; set; }
        public int EnemyTypeID { get; set; }
        [IgnoreDataMember]
        public EnemyType EnemyType { get; set; }
    }
}