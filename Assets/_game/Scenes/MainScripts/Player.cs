using SQLite4Unity3d;

public class Player
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={1},  Gender={2}]", Id, Name, Gender);
    }
}
