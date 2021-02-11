using System;

namespace Nytte.Sample.ModuleB.Dtos.External
{
    public class UserDto
    {
        public string Name { get; set; }
        
        public Guid Id { get; set; }

        public UserDto(string name, Guid id)
        {
            Name = name;
            Id = id;    
        }
    }
}