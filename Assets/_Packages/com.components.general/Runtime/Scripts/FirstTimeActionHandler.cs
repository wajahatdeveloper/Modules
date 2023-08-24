public class FirstTimeActionHandler
{
    private bool hasExecutedOnce = false;

    public bool HasExecutedOnce()
    {
        if (!hasExecutedOnce)
        {
            hasExecutedOnce = true;
            return true;
        }
        return false;
    }
}