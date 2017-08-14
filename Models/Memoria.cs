using System;
using System.Collections.Generic;
using System.Linq;

namespace MonitorOobj.Models
{
    public class Memoria
    {
        public Int64 MemoriaAtual { get; set; }
        public Int64 MemoriaMaxima => 2147483648;
        public Int64 MemoriaPico { get; set; }
        public string MemoriaAtualString { get; set; }
        public string MemoriaPicoString { get; set; }
        public string MemoriaMaximaString => "2 GB";
        public string MemoriaAtualPercent => ((int)((MemoriaAtual/(double) MemoriaMaxima)*100)) + "%";

    }
}