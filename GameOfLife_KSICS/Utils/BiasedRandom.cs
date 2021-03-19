namespace GameOfLife_KSICS.Utils
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  class BiasedRandom
  {
    public static void WriteBiasedRandomNumbers(int count, int min = 1, int max = 100)
    {
      var numbers = new List<int>();
      for (int i = 0; i < count; i++)
      {
        numbers.Add(GetBiased(min, max));
      }
      numbers.Sort();
      var total = 0;
      foreach (var number in numbers)
      {
        Console.WriteLine(number);
        total += number;
      }
      Console.WriteLine($"Average: {total / numbers.Count}");
    }

    /// <summary>
    /// Get a random number between min and max with bias 
    /// towards being on the lower end of the range. (averages towards 1/3)
    /// </summary>
    /// <inspiration>
    /// https://gamedev.stackexchange.com/questions/116832/random-number-in-a-range-biased-toward-the-low-end-of-the-range
    /// </inspiration>
    public static int GetBiased(int min = 1, int max = 100)
    {
      var random = new Random();
      return (int) Math.Floor(Math.Abs(random.NextDouble() - random.NextDouble()) * (1 + max - min) + min);
    }
  }
}
