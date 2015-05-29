using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCache
{
    public class Test
    {
        CancellationTokenSource cts;

        public void Start()
        {
            var result = "NOK";
            //cts = new CancellationTokenSource();
            //cts.CancelAfter(2000); //8s
            //var tache = Task.Run(()=>GetFunc(), cts.Token);
            //var res = tache.Result;


            //var tache2 = Task.Factory.StartNew(() => GetFunc(), cts.Token);
            //tache2.Start();
            //var res2 = tache2.Result;

            Task.WaitAny(
                Task.Factory.StartNew(() => result = GetFunc()), 
                Task.Delay(TimeSpan.FromSeconds(15)));
        }

        public string GetFunc()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            return "OK";
        }
    }
}
