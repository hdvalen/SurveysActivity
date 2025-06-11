namespace ApiSurveys.Helpers;

public class UserAuthorization
{
    public enum Rols
    {
        Administrator,
        Student,
        School
    }

    public const Rols rol_predeterminado = Rols.Student;
}