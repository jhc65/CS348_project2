using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

    public static class Words {
        public static string[] wordListOne = new string[] { "NEVER", "MOTHER", "BUSY", "EIGHT", "RICH", "LOCKER", "HUNTER", "CLUE", "SPRAY", "CATERPILLAR", "UNTIE" };
    }

    public static class Mole_Stats {
        public static float moleUpPosition_y = 9.2f;
        public static float moleHidePosition_y = 6.0f;
    }

    public static class Colors_RichText {
        public static string rt_green = "color=#00ff00ff";
        public static string rt_red = "color=#ff0000ff";
        public static string rt_endTag = "</color>";
    }

    public static class Functions {
        public static int RandomNumber(int start, int end) {
            return Random.Range(start, end);
        }

        public static string RandomLetter() {
            char kickBack = (char)Random.Range(65, 90);
            return kickBack.ToString();
        }

        public static string[] ShuffleStringArray(string[] arrayIn) {
            for (int i = 0; i < arrayIn.Length; i++) {
                string temp = arrayIn[i];
                int r = Random.Range(i, arrayIn.Length);
                arrayIn[i] = arrayIn[r];
                arrayIn[r] = temp;
            }

            return arrayIn;
        }

        public static string ColorCharacter_Next(string inString, string color) {
            string output = "";

            if (inString.StartsWith("<color")) {
                int endtagPosition = inString.IndexOf("</color>");
                output = inString.Substring(0, endtagPosition);
                output += inString.Substring((endtagPosition + 1), 1);
                output += "</color>";
                int temp = endtagPosition + 2;
                output += inString.Substring(temp, (inString.Length - temp));
            }
            else {
                output = "<" + color + ">";

                output += inString.Substring(0, 1);
                output += "</color>";
                output += inString.Substring(1, (inString.Length - 1));

                //if (charToChange == 0) {
                //    output += inString.Substring(charToChange, 1);
                //    output += "</color>";
                //    if (charToChange != (inString.Length - 1)) {
                //        int temp = charToChange + 1;
                //        output += inString.Substring(temp, (inString.Length - temp));
                //    }
                //}
            }

            return output;
        }

        public static string ColorCharacter_Current(string inString, string color) {
            string output = "";

            if (inString.StartsWith("<color")) {
                int endTagPos = inString.IndexOf("</color>");
                output = inString.Substring(0, (endTagPos - 1));
                output += "</color>";
                output += "<" + color + ">";
                output += inString.Substring((endTagPos - 1), (inString.Length - 1));
            }
            else {
                output = "<" + color + ">";

                output += inString.Substring(0, 1);
                output += "</color>";
                output += inString.Substring(1, (inString.Length - 1));
            }

            return output;
        }
    }
}
