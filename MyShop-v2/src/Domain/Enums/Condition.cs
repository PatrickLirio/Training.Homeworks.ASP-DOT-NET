namespace MyShop_v2.Domain.Enums
{
    public enum Condition
    {
        None = 0,
        EQ = 1,  //Equal
        NEQ = 2, //Not Equal
        LT = 4,  //Less Than
        GT = 8, //Greater Than
        LIKE = 16, 
        CONTAINS = 32,
        IN = 64,
        BETWEEN = 128,
    }
}