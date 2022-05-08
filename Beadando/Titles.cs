using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Beadando
{
    class Titles
    {
        private ConcurrentBag<string> titleBag;

        public Titles()
        {
            this.titleBag = null;
        }
        public Titles(ConcurrentBag<string> titleBag)
        {
            this.titleBag = titleBag;
        }

        public ConcurrentBag<string> TitleBag { get => titleBag; set => titleBag = value; }


    }
}
