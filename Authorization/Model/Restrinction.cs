namespace PetsServer.Authorization.Model;

public enum Restrictions
{
    All,
    Organizations,
    Locality,
    None
}

public enum Possibilities
{
    Read,
    Insert,
    Update,
    Delete
}

public enum Entities
{
    Authorization, // Только для суперпользователя
    Organization,
    Contract, 
    Schedule,
    Act
}

