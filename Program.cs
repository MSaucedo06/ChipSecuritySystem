using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        private static readonly HashSet<ColorChip> Chips = new HashSet<ColorChip>();
        private static void Main(string[] args)
        {
            // Add Sample Chips
            Chips.Add(new ColorChip(Color.Blue, Color.Yellow));
            Chips.Add(new ColorChip(Color.Red, Color.Green));
            Chips.Add(new ColorChip(Color.Yellow, Color.Red));
            Chips.Add(new ColorChip(Color.Orange, Color.Purple));

            var usedChips = new HashSet<ColorChip>();

            var startChip = Chips.Where(chip => chip.StartColor == Color.Blue).ToList();

            // Find sequence of chips
            foreach (var chip in startChip)
            {
                usedChips.Add(chip);
                if (FindSequence(chip, usedChips))
                {
                    Console.WriteLine("Valid Chip Sequence:\n");

                    // Show sequence
                    foreach (var c in usedChips)
                    {
                        Console.WriteLine("[" + c + "]");
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine("Press Enter key to continue...\n");
                    Console.ReadLine();
                    return;
                }
                // Remove chip if no valid sequence
                usedChips.Remove(chip);
            }


            Console.WriteLine(Constants.ErrorMessage + "!\n");
            Console.ReadLine();
        }

        // Function to find a valid chip sequence. Returns true if a sequence is found, false otherwise
        private static bool FindSequence(ColorChip lastChip, HashSet<ColorChip> usedChips)
        {
            if (lastChip.EndColor == Color.Green)
            {
                return true;
            }
            // Next Chip
            var nextChips = Chips.Where(c => c.StartColor == lastChip.EndColor).ToList();

            foreach (var nextChip in nextChips)
            {
                // Check for duplicates
                if (!usedChips.Contains(nextChip))
                {
                    usedChips.Add(nextChip);
                    // Check if sequence is valid of nextChip
                    if (FindSequence(nextChip, usedChips))
                    {
                        return true;
                    }
                    // Remove chip if not valid
                    usedChips.Remove(nextChip);
                }
            }

            return false;
        }

    }
}
