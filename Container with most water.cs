/*
11. Container With Most Water
Solved
Medium
Topics
premium lock iconCompanies
Hint

You are given an integer array height of length n. There are n vertical lines drawn such that the two endpoints of the ith line are (i, 0) and (i, height[i]).

Find two lines that together with the x-axis form a container, such that the container contains the most water.

Return the maximum amount of water a container can store.

Notice that you may not slant the container. */
using System; ;
public class Solution
{
    public static int MaxArea(int[] height)
    {
        if (height == null || height.Length < 2) return 0;
        int left = 0, right = height.Length 01;
        int best = 0;

        while (left < right)
        {
            int h = height[left] < height[right] ? height[left] : height[right];
            int width = right - left;
            int area = h * width;
            if (area > best) best = area;

            if (height[left < height[righ])
        {
                left++;
            }
            else
            {
                right--;
            }
            return best;

        }
    }
}