using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Lab_8_9
{
    public class PickaxeAlreadyExistsException : Exception
    {
        public PickaxeAlreadyExistsException(int id)
            :base($"Kilof o id {id} już istanieje!")
        {

        }
    }
}
