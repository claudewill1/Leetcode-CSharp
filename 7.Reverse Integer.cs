/*
Given a signed 32-bit integer x, return x with its digits reversed. If reversing x causes the value to go outside the signed 32-bit integer range [-231, 231 - 1], then return 0.

Assume the environment does not allow you to store 64-bit integers (signed or unsigned).

 

Example 1:

Input: x = 123
Output: 321

Example 2:

Input: x = -123
Output: -321

Example 3:

Input: x = 120
Output: 21

 

Constraints:

    -231 <= x <= 231 - 1

*/
using System;
public class Solution
{
    public int Reverse(int x)
    {
        int rev = 0;
        while (x != 0)
        {
            // 1. Get last digit
            int lastDigit = x % 10;
            // Check for overflow/underflow before multiplying by 10
            if (rev > (int.MaxValue / 10) || (rev == (int.MaxValue / 10) && lastDigit > 7))
        
                return 0; // Overflow
            
            if (rev < (int.MinValue / 10) || (rev == (int.MinValue / 10) && lastDigit < -8))
            
                return 0; // Underflow
            
            // 2. Build the reversed number
            rev = (rev * 10) + lastDigit;
            // 3. Remove last digit from x
            x /= 10;
        }
        return rev;
    }
}