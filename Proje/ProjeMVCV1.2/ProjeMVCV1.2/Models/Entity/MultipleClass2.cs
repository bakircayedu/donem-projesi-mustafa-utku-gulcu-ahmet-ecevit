using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeMVCV1._2.Models.Entity
{
    public class MultipleClass2
    {
        public TblHesap hesapDetaylari { get; set; }
        public TblKesilenFatura kesilenFaturaDetaylari { get; set; }
        public List<TblKesilenFatura> KesilenFaturaDetaylariList { get; set; }

        public List<TblKesilenFaturaIcerik> KesilenFaturaIcerikDetaylari { get; set; }

        public TblBizeKesilenFatura bizeKesilenFaturaDetaylari { get; set; }
        public List<TblBizeKesilenFatura> bizeKesilenFaturaDetaylariList { get; set; }


        public List<TblBizeKesilenFaturaIcerik> bizeKesilenFaturaIcerikDetaylari { get; set; }

    }
}