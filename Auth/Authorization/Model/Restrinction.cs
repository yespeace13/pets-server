namespace PetsServer.Auth.Authorization.Model;

public enum Restrictions
{
    All,
    Organization,
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
    Act,
    Report,
    Log
}

