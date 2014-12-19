using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public enum AbilityNames
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }

    public class Ability
    {
        public AbilityNames Name { get; set; }

        public int Score
        {
            get
            {
                return Score;
            }
            set
            {
                if (value < 2)
                {
                    Score = 2;
                }
                else if (value > 28)
                {
                    Score = 28;
                }
                else
                {
                    Score = value;
                }
            }
        }

        public int TempAdjustment { get; set; }

        public int Modifier
        {
            get
            {
                // Adjust the score based on the temp adjustment.
                // Usually it will be 0, except when character has some effect on them.
                int score = Score + TempAdjustment;

                if (score <= 3) return -4;
                else if (score > 3 && score <= 5) return -3;
                else if (score > 5 && score <= 7) return -2;
                else if (score > 7 && score <= 9) return -1;
                else if (score > 9 && score <= 11) return 0;
                else if (score > 11 && score <= 13) return 1;
                else if (score > 13 && score <= 15) return 2;
                else if (score > 15 && score <= 17) return 3;
                else if (score > 17 && score <= 19) return 4;
                else if (score > 19 && score <= 21) return 5;
                
                return 0;
            }
        }

        public Ability()
        {
            
        }

        public Ability(AbilityNames name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}