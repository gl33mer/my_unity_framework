using System.Collections;

public static class CollectionExtesionMethods
{
    public static bool IsEmpty(this ICollection collection)
    {
        return collection.Count == 0;
    }
}
