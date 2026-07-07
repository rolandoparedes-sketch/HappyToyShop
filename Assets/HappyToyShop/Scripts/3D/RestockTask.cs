[System.Serializable]
public class RestockTask
{
    public ToyData toyData;
    public int targetAmount;
    public int currentAmount;

    public bool Completed => currentAmount >= targetAmount;
}