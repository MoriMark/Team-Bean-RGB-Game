using RGB.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.game_logic
{
    public class Field 
    {
        private const Int32 border = 5;
        private GameObject[,] field;
        public Int32 TableSize { get; private set; }

        public Field(Int32 tableSize)
        {
            if (tableSize <= 0)
                throw new ArgumentException("table size can not be zero or negative!");

            TableSize = tableSize;

            field = new GameObject[TableSize + 2*border, TableSize + 2*border];
        }

        public GameObject[,] GetField()
        {
            return field;
        }

        public GameObject GetValue(Int32 i, Int32 j)
        {
            if (i < 0 || j < 0) throw new IndexOutOfRangeException($"(i:{i}|j:{j})<0");
            if (i >= TableSize || j >= TableSize) throw new IndexOutOfRangeException($"(i:{i}|j:{j})>=TableSize{TableSize}");
        
            return field[i + border, j + border];
        }

        public void SetValue(Int32 i, Int32 j, GameObject value)
        {
            if (i < 0 || j < 0) throw new IndexOutOfRangeException($"(i:{i}|j:{j})<0");
            if (i >= TableSize || j >= TableSize) throw new IndexOutOfRangeException($"(i:{i}|j:{j})>=TableSize{TableSize}");

            field[i + border, j + border] = value;
        }

        public Field CalculateVisionOfRobot(Robot robot)
        {
            Field calculatedField = (Field)field.Clone();

            //TODO calcualate

            return calculatedField;
        }
    }
}