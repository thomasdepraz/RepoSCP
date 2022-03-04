using UnityEngine;

namespace SCP.Building
{
    public class Grid
    {
        public Vector2 size { get; private set; }
        public bool[,] gridSlots;


        public Grid(Vector2 size)
        {
            gridSlots = new bool[(int)size.x, (int)size.y];
            gridSlots[0, 0] = true;
        }



        public void Build(Vector2 position, Vector2 size)
        {
            for (int x = (int)position.x; x < (int)position.x + size.x; x++)
            {
                for (int y = (int)position.y; y < (int)position.y + size.y; y++)
                {
                    gridSlots[x, y] = true;
                }
            }
        }

        public bool CanBuild(Vector2 position, Vector2 size)
        {
            return !Overlaps(position, size) && HasNeighbours(position, size);
        }

        bool Overlaps(Vector2 position, Vector2 size)
        {
            for (int x = (int)position.x; x < (int)position.x + size.x; x++)
            {
                for (int y = (int)position.y; y < (int)position.y + size.y; y++)
                {
                    if (gridSlots[x, y])
                        return true;

                }
            }
            return false;
        }
        bool HasNeighbours(Vector2 position, Vector2 size)
        {
            for (int x = (int)position.x; x < (int)position.x + size.x; x++)
            {
                for (int y = (int)position.y; y < (int)position.y + size.y; y++)
                {
                    if (CheckNeighbours(position))
                        return true;
                }
            }

            return false;
        }
        bool CheckNeighbours(Vector2 position)
        {
            if (position.x - 1 >= 0  && gridSlots[(int)position.x - 1, (int)position.y]) 
                return true;
            if (position.x + 1 < 13 && gridSlots[(int)position.x + 1, (int)position.y]) 
                return true;
            if (position.y + 1 < 6  && gridSlots[(int)position.x, (int)position.y + 1]) 
                return true;
            if (position.y - 1 >= 0  && gridSlots[(int)position.x, (int)position.y - 1]) 
                return true;
            return false;
        }


    }
}
