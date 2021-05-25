using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    interface ICollidable //de som ärver av denna kan kollidera
    {
        void OnCollide(BaseClass sprite);
    }
}
