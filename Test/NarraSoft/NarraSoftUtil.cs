using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NarraSoft {
    public class NarraSoftUtil {

        /*method that takes an unsigned integer as input,
             * and returns true if all the digits in the
             * base 10 representation of that number are unique.
             * 
             * param uint digits - unsigned int(>=0) if every base 10 digits are unique
             */
        public static bool AllDigitsUnique( uint digits ) {
            //place all digits to an array of characters
            char[] digitsArray = digits.ToString ().ToCharArray ();
            //create a container for unique digits
            char[] uniqueDigits = new char[digitsArray.Length];
            //loop through digitsArray and add the characters one by one on the unique container
            for(int i = 0; i < digitsArray.Length; i++) {
                char c = digitsArray[i];
                //if this char is already added to the unique container. then digits are not all unique
                if(uniqueDigits.Contains ( c )) {
                    return false;
                } else {
                    uniqueDigits[i] = c;
                }
            }
            //if everything is added without a copy then digits are unique
            return true;
        }

        /*modifies an input string, sorting the letters according to a sort order
         * defined by a second string. You may assume that every character in the input string also appears
         * somewhere in the sort order string. Make your method as fast as possible for long input strings. 98=b 99=c
         * 
         * param byte[] inputAndOutput - the byte array to be modified
         * param byte[] sortOrder - the byte array containing the order in which inputAndOutput to be sorted
         */
        public static void SortLetters( byte[] inputAndOutput, byte[] sortOrder ) {
            //Loop through inputAndOutput array and modify each byte by index
            for(int i = 0; i < inputAndOutput.Length; i++) {
                //Loop through inputAndOutput again to compare each byte
                for(int j = i + 1; j < inputAndOutput.Length; j++) {
                    //index of byte b1 in the sortOrder array
                    byte b1 = inputAndOutput[i];
                    int b1_sortOrder = Array.IndexOf ( sortOrder, b1 );
                    //index of byte b2 in the sortOrder array
                    byte b2 = inputAndOutput[j];
                    int b2_sortOrder = Array.IndexOf ( sortOrder, b2 );

                    //comparison of two bytes on inputAndOutput, based on the sortOrder array index
                    if(b2_sortOrder < b1_sortOrder) {
                        //if byte b2 which is always ahead of b1 is less in order, then switch their position
                        inputAndOutput[i] = b2;
                        inputAndOutput[j] = b1;
                    }
                }
            }
        }
    }
}
