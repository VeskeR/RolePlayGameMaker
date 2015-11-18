using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayMaker
{
    [Serializable]
    public class RolePlayGame
    {
        public List<CharacterInfo> Characters { get; set; }
        public int Gold { get; set; }
        public int Ore { get; set; }

        public RolePlayGame(List<CharacterInfo> characters, int gold, int ore)
        {
            Characters = characters;
            Gold = gold;
            Ore = ore;
        }
    }
}
