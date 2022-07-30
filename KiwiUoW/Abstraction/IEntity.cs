using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KiwiUoW.Abstraction
{
    public interface IEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
