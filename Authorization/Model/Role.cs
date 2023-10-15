using System;
using System.Collections.Generic;

namespace PetsServer.Authorization.Model
{
    public class Role
    {
        public int Id { get; set; }   
        public Tuple<Restrictions, Possibilities[]> Plan { get; }
        public Tuple<Restrictions, Possibilities[]> Acts { get; }
        public Tuple<Restrictions, Possibilities[], int[]> Organizations { get; }
        public Tuple<Restrictions, Possibilities[]> Contracts { get; }
        public string Name { get; }

        public Role(int id, string name,
            Tuple<Restrictions, Possibilities[]> plans, 
            Tuple<Restrictions, Possibilities[]> acts,
            Tuple<Restrictions, Possibilities[], int[]> organizations,
            Tuple<Restrictions, Possibilities[]> contracts)
        {
            Id = id;
            Name = name;
            Plan = plans;
            Acts = acts;
            Organizations = organizations;
            Contracts = contracts;
        }
    }
}