public static class GameEvents
{

    // Delegate for the UpdatePoints event.
    public delegate void UpdatePoints(int newPoints);
    public static event UpdatePoints OnUpdatePoints;




    // Method to trigger the OnUpdatePoints event.
    public static void ReportPoints(int points)
    {
        OnUpdatePoints?.Invoke(points);

    }





}
