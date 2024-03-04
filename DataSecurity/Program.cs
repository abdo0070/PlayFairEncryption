using System;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Text;

namespace DataSecurity;

class PlayFair
{
    public static void Main(String[] args)
    {
        Console.WriteLine(Encrybt("communication", "playfairexample"));
    }

    public static string Encrybt(string planText, string key)
    {
        planText = planText.ToUpper();
        char[,] matrix = ConsturctTheMatrix(key);
        char [] plainTextArray = PlainTextToArray(planText);
        StringBuilder encryptedText = new StringBuilder();
        // Main Decrypt Algorithm
        for (int i = 0; i < plainTextArray.Length;)
        {
            if (SameColumn(matrix, plainTextArray[i], plainTextArray[i+1]))
            {
                int row = (getCurpos(matrix, plainTextArray[i], true) + 1) % 5;
                int col = getCurpos(matrix, plainTextArray[i], false);
                encryptedText.Append(matrix[row, col]);
                row = (getCurpos(matrix, plainTextArray[i+1], true) + 1) % 5;
                col = getCurpos(matrix, plainTextArray[i+1], false);
                encryptedText.Append(matrix[row, col]);
            }
            else if (SameRow(matrix, plainTextArray[i], plainTextArray[i + 1]))
            {
                int row = getCurpos(matrix, plainTextArray[i], true);
                int col = (getCurpos(matrix, plainTextArray[i], false) + 1) % 5;
                encryptedText.Append(matrix[row,col]);
                row = getCurpos(matrix, plainTextArray[i+1], true);
                col = (getCurpos(matrix, plainTextArray[i+1], false) + 1) % 5;
                encryptedText.Append(matrix[row,col]);
            }
            else
            {
                int row1 = getCurpos(matrix, plainTextArray[i], true);
                int col1 = getCurpos(matrix, plainTextArray[i], false);
                int row2 = getCurpos(matrix, plainTextArray[i + 1], true) % 5; 
                int col2 = getCurpos(matrix, plainTextArray[i + 1], false) % 5;
                encryptedText.Append(matrix[row1, col2]);
                encryptedText.Append(matrix[row2,col1]);
            }
            i = i + 2;
        }
       
        return encryptedText.ToString();
    }
    public static char[] PlainTextToArray(string planText)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in planText)
        {
            if(stringBuilder.Length == 0)
            {
                stringBuilder.Append(c);
                continue;
            }
            if (stringBuilder.Length % 2 == 0)
            {
                stringBuilder.Append(c);
            }
            else 
            {
                if (stringBuilder[stringBuilder.Length-1] == c)
                {
                    stringBuilder.Append('X');
                    stringBuilder.Append(c);
                    continue;
                }
                stringBuilder.Append(c);
            }
        }
        if (stringBuilder.Length % 2 == 1)
        {
            stringBuilder.Append('X');
        }
        char[] charArray = stringBuilder.ToString().ToCharArray();
        return charArray;
    }
    public static int getCurpos(char[,] matrix, char c, bool row)
    {
        int index = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (matrix[i, j] == c)
                {
                    if (row)
                    {
                        index = i;
                    }
                    else
                    {
                        index = j;
                    }
                }
            }
        }


        if ((c == 'J' || c == 'I') && index == 0)
        {
            if (c == 'J')
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ('I' == c)
                        {
                            if (row)
                            {
                                index = i;
                            }
                            else
                            {
                                index = j;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ('J' == c)
                        {
                            if (row)
                            {
                                index = i;
                            }
                            else
                            {
                                index = j;
                            }
                        }
                    }
                }
            }
        }

        return index;
    }
    public static bool SameRow(char [,] matrix ,char fChar , char sChar) // O(1)
    {
        int fItemRow = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (fChar == matrix[i, j])
                {
                    fItemRow = i; break;
                }
            }
        }
        if ((fChar == 'J' || fChar == 'I') && fItemRow == 0)
        {
            if (fChar == 'J')
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ('I' == matrix[i, j])
                        {
                            fItemRow = i; break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (fChar == 'J')
                        {
                            fItemRow = i; break;
                        }
                    }
                }
            }
        }

        int sItemRow = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (sChar == matrix[i, j])
                {
                    sItemRow = i; break;
                }
            }
        }


        if ((sChar == 'J' || sChar == 'I') && sItemRow == 0)
        {
            if (sChar == 'J')
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ('I' == matrix[i, j])
                        {
                            sItemRow = i; break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (sChar == 'J')
                        {
                            sItemRow = i; break;
                        }
                    }
                }
            }
        }

        return (sItemRow == fItemRow); 
    }
    public static bool SameColumn(char[,] matrix, char fChar, char sChar) // O(1)
    {
        int fItemColumn = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (fChar == matrix[i, j])
                {
                    fItemColumn = j; break;
                }
            }
        }
        if ((fChar == 'J' || fChar == 'I') && fItemColumn == 0)
        {
            if (fChar == 'J')
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ('I' == matrix[i, j])
                        {
                            fItemColumn = j; break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (fChar == 'J')
                        {
                            fItemColumn = j; break;
                        }
                    }
                }
            }
        }

        int sItemColumn = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (sChar == matrix[i, j])
                {
                    sItemColumn = j; break;
                }
            }
        }


        if ((sChar == 'J' || sChar == 'I') && sItemColumn == 0)
        {
            if (sChar == 'J')
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ('I' == matrix[i, j])
                        {
                            sItemColumn = j; break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (sChar == 'J')
                        {
                            sItemColumn = j; break;
                        }
                    }
                }
            }
        }
        return (fItemColumn == sItemColumn);
    }
    public static char [,] ConsturctTheMatrix(string keyword) // O(1)
    {

        keyword = keyword.ToUpper();
        char[,] matrix = new char[5,5];
        HashSet<char> alphaptic = new HashSet<char>();
        char[] alphabeticArray = {
        'A', 'B', 'C', 'D', 'E',
        'F', 'G', 'H', 'I', 'J',
        'K', 'L', 'M', 'N', 'O',
        'P', 'Q', 'R', 'S', 'T',
        'U', 'V', 'W', 'X', 'Y', 'Z'
        };
        // Construct the Keyword and the remaring alphaptic Into the AlphapticSet
        foreach (char c in keyword)
        {
            if (!alphaptic.Contains(c))
            {
                alphaptic.Add(c);
            }
        }
        
        foreach (char c in alphabeticArray) 
        {
            if (c == 'J' || c == 'J')
            {
                if (alphaptic.Contains('I') || alphaptic.Contains('J'))
                {
                    continue;
                }
            }
            if (!alphaptic.Contains(c))
            {
                alphaptic.Add(c);
            }
        }

        char [] FDM = new char[25];
        int counter = 0;
        foreach (char c in alphaptic)
        {
            FDM[counter++] = c;
        }
        counter = 0;
        for (int i = 0; i < 5; i++)
        {
            for( int j = 0; j < 5; j++)
            {
                matrix[i, j] = FDM[counter++];
            }
        }
        return matrix;
    }
}