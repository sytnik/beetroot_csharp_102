namespace Lesson22HomeTask;

// "name": "Persimmon",
// "id": 52,
// "family": "Ebenaceae",
// "order": "Rosales",
// "genus": "Diospyros",
// "nutritions": {
//     "calories": 81,
//     "fat": 0.0,
//     "sugar": 18.0,
//     "carbohydrates": 18.0,
//     "protein": 0.0
// }

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string Order { get; set; }
    public string Genus { get; set; }
    public Nutritions Nutritions { get; set; }
}

public sealed record Nutritions(int Calories, double Fat, double Sugar, double Carbohydrates, double Protein);
// public class Nutritions
// {
//     public int Calories { get; set; }
//     public double Fat { get; set; }
//     public double Sugar { get; set; }
//     public double Carbohydrates { get; set; }
//     public double Protein { get; set; }
// }