namespace ApiProject.Shared.Cases.Request;
public class UpdatePhoneCaseCommand : CreatePhoneCaseCommand
{
    public int Id { get; set; }
    public int Sold { get; set; }
}
