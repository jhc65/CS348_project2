using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

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
    }
}
