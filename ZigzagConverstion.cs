/*The string "PAYPALISHIRING" is written in a zigzag pattern on a given number of rows like this: (you may want to display this pattern in a fixed font for better legibility)

P   A   H   N
A P L S I I G
Y   I   R

And then read line by line: "PAHNAPLSIIGYIR"

Write the code that will take a string and make this conversion given a number of rows:

string convert(string s, int numRows);

 

Example 1:

Input: s = "PAYPALISHIRING", numRows = 3
Output: "PAHNAPLSIIGYIR"

Example 2:

Input: s = "PAYPALISHIRING", numRows = 4
Output: "PINALSIGYAHRPI"
Explanation:
P     I    N
A   L S  I G
Y A   H R
P     I

Example 3:

Input: s = "A", numRows = 1
Output: "A"

 

Constraints:

    1 <= s.length <= 1000
    s consists of English letters (lower-case and upper-case), ',' and '.'.
    1 <= numRows <= 1000

*/
using System.Text;
using System;
public class Solution
{
    public string Convert(string s, int numRows)
    {
        if (numRows == 1 || numRows >= s.Length) return s;

        // Create a row buffer for each row
        var rows = new StringBuilder[numRows];
        for (int i = 0; i < numRows; i++) rows[i] = new StringBuilder();

        int currentRow = 0;
        int direction = 1; // +1 going down, -1 going up

        foreach (char c in s)
        {
            rows[currentRow].Append(c);

            // Flip direction at the top or bottom row
            if (currentRow == 0) direction = 1;
            else if (currentRow == numRows - 1) direction = -1;

            currentRow += direction;
        }

        // Join all rows
        var result = new StringBuilder(s.Length);
        foreach (var sb in rows) result.Append(sb);
        return result.ToString();
    }
}