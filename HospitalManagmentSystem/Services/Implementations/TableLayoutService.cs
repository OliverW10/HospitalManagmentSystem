using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagmentSystem.Services.Interfaces;

namespace HospitalManagmentSystem.Services.Implementations
{
    internal class TableLayoutService
    {
        public string[][] GetTableOfString<T>(IEnumerable<T> rows, TableColumns<T> columns)
        {
            var columnFuncs = columns.ValueGetters.Select(expr => expr.Compile());
            string[][] table = new string[rows.Count()][];

            int rowIndex = 0;
            foreach (var row in rows)
            {
                table[rowIndex] = new string[columnFuncs.Count()];
                int columnIndex = 0;
                foreach (var columnFunc in columnFuncs)
                {
                    var columnString = columnFunc(row);

                    table[rowIndex][columnIndex] = columnString;

                    columnIndex++;
                }
                rowIndex++;
            }

            return table;
        }

        public int[] GetColumnWidths(string[][] stringTable, IEnumerable<string> columnNames, int widthBudget)
        {
            int numColumns = columnNames.Count();
            int numRows = stringTable.Length;
            int[] columnSizes = new int[numColumns];

            void UpdateMaxes(IEnumerable<string> row)
            {
                int columnIndex = 0;
                foreach (var col in row)
                {
                    columnSizes[columnIndex] = Math.Max(columnSizes[columnIndex], col.Length + 1);
                    columnIndex++;
                }
            }

            // get max width for each column
            UpdateMaxes(columnNames);
            foreach (var row in stringTable)
            {
                UpdateMaxes(row);
            }

            EnlargeColumnsToFit(stringTable, numColumns, numRows, columnSizes, widthBudget);

            EnlargeColumnsToFit(numColumns, columnSizes, widthBudget);

            return columnSizes;
        }

        private static void EnlargeColumnsToFit(string[][] stringTable, int numColumns, int numRows, int[] columnSizes, int widthBudget)
        {
            var used = columnSizes.Sum();
            while (used > widthBudget)
            {
                int longestLengthIndex = 0;
                int longestLength = 0;
                // Find the longest column
                for (int i = 0; i < numColumns; i++)
                {
                    if (columnSizes[i] > longestLength)
                    {
                        longestLength = columnSizes[i];
                        longestLengthIndex = i;
                    }
                }

                // Remove one character from it
                columnSizes[longestLengthIndex]--;
                used--;
            }
        }

        private static void EnlargeColumnsToFit(int numColumns, int[] columnSizes, int widthBudget)
        {
            // if under, go left to right adding one to each until at the limit
            var used = columnSizes.Sum();
            int addSteps = 0;
            while (used < widthBudget)
            {
                columnSizes[addSteps % numColumns]++;
                used++;
                addSteps++;
            }
        }

        public string RightPadToWidth(string initial, int width)
        {
            if (initial.Length == width)
            {
                return initial;
            }
            else if (initial.Length > width)
            {
                return initial.Substring(0, width - 3) + "...";
            }
            else
            {
                return " " + initial + new string(' ', width - initial.Length - 1); // -1 for the left space
            }
        }
    }
}
