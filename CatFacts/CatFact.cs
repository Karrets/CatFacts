namespace CatFacts;

public class CatFact(string fact, int length) {
    public string Fact { get; set; } = fact;
    public int Length { get; set; } = length;
}