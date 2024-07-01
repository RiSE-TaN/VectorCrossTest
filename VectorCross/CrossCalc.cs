using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VectorCross
{
    internal class CrossCalc
    {
        public static float Cross(Vector2 a,  Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static Vector2 Intersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            float cross = Cross(b - a, d - c);

            Vector2 error = new Vector2(float.NaN, float.NaN);

            if (cross == 0.0)
            {
                // 線分が平行
                if ((b.X - a.X) == 0.0 || (d.X - c.X) == 0.0)
                {
                    return error;
                }
                // 切片qを求める
                float abAngle = (b.Y - a.Y) / (b.X - a.X);
                float abQ = abAngle * a.X + a.Y;
                float cdAngle = (d.Y - c.Y) / (d.X - c.X);
                float cdQ = cdAngle * c.X + d.Y;
                if (abQ == cdQ)
                {
                    // abとcdは一致している
                    return b;
                }
                return error;
            }

            float s = Cross(c - a, d - c) / cross;
            float t = Cross(b - a, a - c) / cross;

            if (s < 0.0 || 1.0 < s || t < 0.0 || 1.0 < t)
            {
                // 線分が交差していない
                return error;
            }

            return new Vector2(a.X + s * (b - a).X, a.Y + s * (b - a).Y);
        }
    }
}
