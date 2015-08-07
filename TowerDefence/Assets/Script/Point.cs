using System;

namespace common
{
    public class Point
    {
        public int x = 0;
        public int y = 0;

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public string ToString()
        {
            return "Point[x: " + x + ", y: " + y + "]";
        }

        public Point clone()
        {
            return new common.Point(x, y);
        }

        public bool isEqual(Point p)
        {
            return (p.x == x) && (p.y == y);
        }

        public static bool operator == (Point p1, Point p2)
        {
            if (object.ReferenceEquals(null, p1))
            {
                return object.ReferenceEquals(null, p2);
            }
            if (object.ReferenceEquals(null, p2))
            {
                return object.ReferenceEquals(null, p1);
            }
            return p1.isEqual(p2);
        }

        public static bool operator != (Point p1, Point p2)
        {
            if (object.ReferenceEquals(null, p1))
            {
                return !object.ReferenceEquals(null, p2);
            }
            if (object.ReferenceEquals(null, p2))
            {
                return !object.ReferenceEquals(null, p1);
            }
            return !p1.isEqual(p2);
        }

        public override bool Equals(object p1)
        {
            return this.Equals((Point) p1);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}