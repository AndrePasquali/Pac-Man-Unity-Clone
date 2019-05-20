namespace DroidDigital.PacMan.Path
{
    public class PathPosition
    {
        public int Column { get; set; }

        public int Row { get; set; }

        public int[,] PositionMap = new int[7,6]
        {
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0}
        };


    }
}