using System;

/// <summary>
/// Contains some util cost functions.
/// </summary>
public static class CostFunctions
{
    /// <summary>
    /// Always return a random value between 0.0 and 1.0.
    /// </summary>
    public static readonly Func<int, int, int, int, float> Rand =
        (i0, j0, i1, j1) => Random.Shared.NextSingle();
    
    /// <summary>
    /// Always return 0.
    /// </summary>
    public static readonly Func<int, int, int, int, float> Zero =
        (i0, j0, i1, j1) => 0;
    
    /// <summary>
    /// Return the euclidian distance between points.
    /// </summary>
    public static readonly Func<int, int, int, int, float> Dist =
        (i0, j0, i1, j1) => {
            var di = (i0 - i1);
            var dj = (j0 - j1);
            return MathF.Sqrt(di * di + dj * dj);
        };
}