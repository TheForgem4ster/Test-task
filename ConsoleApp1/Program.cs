using System;

class Program
{
    static void Main()
    {
        try
        {
            var numbers = File.ReadAllLines("10m.txt").Select(int.Parse).ToList();

            Console.WriteLine("The task is to find the following six values:");
            Console.WriteLine("1) Maximum number: " + numbers.Max());
            Console.WriteLine("2) Minimum number: " + numbers.Min());
            Console.WriteLine("3) Arithmetic mean: " + numbers.Average());
            

            var increasingSequences = FindSequences(numbers, true);
            var decreasingSequences = FindSequences(numbers, false);

            Console.WriteLine("4) Largest ascending sequence:  " + string.Join(", ", increasingSequences.MaxBy(s => s.Count)));
            Console.WriteLine("5) Greatest descending sequence: " + string.Join(", ", decreasingSequences.MaxBy(s => s.Count)));
            Console.WriteLine("6) Median: " + FindMedian(numbers));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: The file was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static double FindMedian(List<int> numbers)
    {
        numbers.Sort();
        int count = numbers.Count;
        if (count % 2 == 0)
        {
            return (numbers[count / 2 + 1] + numbers[count / 2]) / 2.0;
        }
        else
        {
            return numbers[count / 2];
        }
    }

    static List<List<int>> FindSequences(List<int> numbers, bool increasing)
    {
        var sequences = new List<List<int>>();
        var currentSequence = new List<int> { numbers[0] };

        for (int i = 1; i < numbers.Count; i++)
        {
            if ((increasing && numbers[i] > currentSequence.Last()) || (!increasing && numbers[i] < currentSequence.Last()))
            {
                currentSequence.Add(numbers[i]);
            }
            else
            {
                sequences.Add(currentSequence);
                currentSequence = new List<int> { numbers[i] };
            }
        }

        sequences.Add(currentSequence);
        return sequences;
    }

}