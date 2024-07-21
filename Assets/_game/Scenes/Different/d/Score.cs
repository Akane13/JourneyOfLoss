using SQLite4Unity3d;

public class Score
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int Value { get; set; }

    public override string ToString()
    {
        return string.Format("[Score: Id={0}, Value={1}]", Id, Value);
    }
}
