using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

    public static class Words {
        public static string[] wordListOne = new string[] { "never", "mother", "busy", "eight", "rich", "locker", "hunter", "clue", "spray", "caterpillar", "untie" };
    }

    public static class Mole_Stats {
        public static float moleUpPosition_y = 9.2f;
        public static float moleHidePosition_y = 6.0f;
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
    }
}
